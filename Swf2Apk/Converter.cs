using System.Runtime.InteropServices;
using System.Text;
using Scriban;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Swf2Apk
{
    public class Converter
    {
        public struct ConverterSettings
        {
            public IDictionary<string, string> Variables;
            public IDictionary<string, string> Assets;
            public IDictionary<string, string> Icons;
            public string WorkingDirectory;
            public string OutputFilename;
            public Action<string>? OnStdOutput;
            public Action<string>? OnStdError;
        }

        private struct Config
        {
            public IDictionary<string, string> Variables;
            public IList<Func<ConverterSettings, IDictionary<string, string>, Task>> Jobs;
        }

        private readonly string _templateDir;
        private readonly string _configJsonFile;

        private static async Task CopyFileAsync(string sourceFile, string destinationFile, CancellationToken token)
        {
            await using FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
                FileOptions.Asynchronous | FileOptions.SequentialScan);
            await using FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None,
                4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
            await sourceStream.CopyToAsync(destinationStream, token);
        }

        private static async Task CopyDirectoryRecursive(string sourceDir, string destinationDir, CancellationToken token,
            Action<string>? statusCallback)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();

            token.ThrowIfCancellationRequested();
            if (statusCallback != null)
                statusCallback($"Creating directory {sourceDir}");
            Directory.CreateDirectory(destinationDir);
            foreach (FileInfo file in dir.GetFiles())
            {
                token.ThrowIfCancellationRequested();
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                if (statusCallback != null)
                    statusCallback($"Copying file {file.FullName}");
                await CopyFileAsync(file.FullName, targetFilePath, token);
            }

            foreach (DirectoryInfo subDir in dirs)
            {
                token.ThrowIfCancellationRequested();
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                await CopyDirectoryRecursive(subDir.FullName, newDestinationDir, token, statusCallback);
            }
        }

        private static async Task<string> EvaluateTemplate(string template, IDictionary<string, string> variables)
        {
            var t = Template.Parse(template);
            return await t.RenderAsync(variables);
        }

        private static async Task TaskApplyTemplate(ConverterSettings settings, IDictionary<string, string> mergeVariables, string file)
        {
            var templateText = await File.ReadAllTextAsync(Path.Combine(settings.WorkingDirectory, file), Encoding.UTF8);
            var result = await EvaluateTemplate(templateText, mergeVariables);
            await using var sw = new StreamWriter(Path.Combine(settings.WorkingDirectory, file), false, new UTF8Encoding(false));
            await sw.WriteAsync(result);
            sw.Close();
        }

        private static async Task TaskRename(ConverterSettings settings, IDictionary<string, string> mergeVariables, string src,
            string dest)
        {
            var templatedSrc = await EvaluateTemplate(src, mergeVariables);
            var templatedDest = await EvaluateTemplate(dest, mergeVariables);
            var moveSrc = Path.Combine(settings.WorkingDirectory, templatedSrc);
            var moveDest = Path.Combine(settings.WorkingDirectory, templatedDest);

            if (moveSrc != moveDest)
            {
                if (Directory.Exists(moveSrc))
                {
                    if (Directory.Exists(moveDest))
                        Directory.Delete(moveDest, true);
                    Directory.Move(moveSrc, moveDest);
                }
                else
                {
                    if (File.Exists(moveDest))
                        File.Delete(moveDest);
                    File.Move(moveSrc, moveDest);
                }
            }
        }

        private static async Task TaskSaveIcon(ConverterSettings settings, IDictionary<string, string> mergeVariables, string type,
            string target)
        {
            var templatedTarget = await EvaluateTemplate(target, mergeVariables);
            if (settings.Icons.TryGetValue(type, out var iconPath))
                await CopyFileAsync(iconPath, Path.Combine(settings.WorkingDirectory, templatedTarget), CancellationToken.None);
        }

        private static async Task TaskCopyAssets(ConverterSettings settings, IDictionary<string, string> mergeVariables, string target)
        {
            var templatedTarget = await EvaluateTemplate(target, mergeVariables);
            var fullTarget = Path.Combine(settings.WorkingDirectory, templatedTarget);
            if (!Directory.Exists(fullTarget))
                Directory.CreateDirectory(fullTarget);
            foreach (var asset in settings.Assets)
            {
                var assetSrc = asset.Value;
                var assetDest = Path.Combine(fullTarget, await EvaluateTemplate(asset.Key, mergeVariables));
                if (settings.OnStdOutput != null)
                    settings.OnStdOutput($"Copying asset {asset.Key}");
                
                // Create directory if not exists
                var assetDestDir = Path.GetDirectoryName(assetDest);
                if (assetDestDir != null && !Directory.Exists(assetDestDir))
                    Directory.CreateDirectory(assetDestDir);

                await CopyFileAsync(assetSrc, assetDest, CancellationToken.None);
            }
        }

        public Converter(string templateDir, string configJsonFile)
        {
            _templateDir = templateDir;
            _configJsonFile = configJsonFile;
        }

        private async Task<Config> LoadConfig()
        {
            Config ret = new Config
            {
                Jobs = new List<Func<ConverterSettings, IDictionary<string, string>, Task>>(),
                Variables = new Dictionary<string, string>()
            };

            await using FileStream fs = File.OpenRead(_configJsonFile);
            var node = await JsonNode.ParseAsync(fs);
            if (node == null || node.GetValueKind() != JsonValueKind.Object)
                throw new Exception("invalid config file, config root should be an object");

            // reading jobs
            var jobsNode = node["jobs"];
            if (jobsNode == null || jobsNode.GetValueKind() != JsonValueKind.Array)
                throw new Exception("invalid config file, \"jobs\" should be an array");
            var jobsNodeArray = jobsNode.AsArray();
            foreach (var jobNode in jobsNodeArray)
            {
                if (jobNode == null || jobNode.GetValueKind() != JsonValueKind.Object)
                    throw new Exception("invalid config file, job config should be an object");
                var jobNameNode = jobNode["task"];
                if (jobNameNode == null || jobNameNode.GetValueKind() != JsonValueKind.String)
                    throw new Exception("invalid config file, \"task\" should be a string");
                var jobName = jobNameNode.GetValue<string>();

                if (jobName == "apply_template")
                {
                    var fileNode = jobNode["file"];
                    if (fileNode == null || fileNode.GetValueKind() != JsonValueKind.String)
                        throw new Exception("invalid config file, \"file\" should be a string");
                    var file = fileNode.GetValue<string>();

                    ret.Jobs.Add((settings, variables) => TaskApplyTemplate(settings, variables, file));
                }
                else if (jobName == "rename")
                {
                    var srcNode = jobNode["src"];
                    if (srcNode == null || srcNode.GetValueKind() != JsonValueKind.String)
                        throw new Exception("invalid config file, \"src\" should be a string");
                    var src = srcNode.GetValue<string>();

                    var destNode = jobNode["dest"];
                    if (destNode == null || destNode.GetValueKind() != JsonValueKind.String)
                        throw new Exception("invalid config file, \"dest\" should be a string");
                    var dest = destNode.GetValue<string>();

                    ret.Jobs.Add((settings, variables) => TaskRename(settings, variables, src, dest));
                }
                else if (jobName == "save_icon")
                {
                    var typeNode = jobNode["type"];
                    if (typeNode == null || typeNode.GetValueKind() != JsonValueKind.String)
                        throw new Exception("invalid config file, \"type\" should be a string");
                    var type = typeNode.GetValue<string>();

                    var targetNode = jobNode["target"];
                    if (targetNode == null || targetNode.GetValueKind() != JsonValueKind.String)
                        throw new Exception("invalid config file, \"target\" should be a string");
                    var target = targetNode.GetValue<string>();

                    ret.Jobs.Add((settings, variables) => TaskSaveIcon(settings, variables, type, target));
                }
                else if (jobName == "copy_assets")
                {
                    var targetNode = jobNode["target"];
                    if (targetNode == null || targetNode.GetValueKind() != JsonValueKind.String)
                        throw new Exception("invalid config file, \"target\" should be a string");
                    var target = targetNode.GetValue<string>();

                    ret.Jobs.Add((settings, variables) => TaskCopyAssets(settings, variables, target));
                }
                else
                {
                    throw new Exception($"invalid config file, unknown task \"{jobName}\"");
                }
            }

            // reading variables
            var variablesNode = node["variables"];
            if (variablesNode == null || variablesNode.GetValueKind() != JsonValueKind.Object)
                throw new Exception("invalid config file, \"variables\" should be an object");
            var variablesNodeObject = variablesNode.AsObject();
            foreach (var variable in variablesNodeObject)
            {
                if (variable.Value == null || variable.Value.GetValueKind() != JsonValueKind.String)
                    throw new Exception("invalid config file, variable value should be a string");
                ret.Variables.Add(variable.Key, variable.Value.GetValue<string>());
            }

            return ret;
        }

        /// <summary>
        /// 执行转换 APK 任务
        /// </summary>
        /// <param name="apkTool">APK 工具</param>
        /// <param name="settings">转换配置</param>
        /// <param name="token">取消令牌</param>
        /// <returns>任务对象</returns>
        public async Task Convert(ApkTool apkTool, ConverterSettings settings, CancellationToken token)
        {
            // Step1: Copy template directory to working directory
            if (Directory.Exists(settings.WorkingDirectory))
                Directory.Delete(settings.WorkingDirectory, true);
            await CopyDirectoryRecursive(_templateDir, settings.WorkingDirectory, token, settings.OnStdOutput);

            // Step2: Load config
            var config = await LoadConfig();

            // Step3: Apply variables
            Dictionary<string, string> variables = new Dictionary<string, string>(config.Variables);
            foreach (var variable in settings.Variables)
                variables[variable.Key] = variable.Value;
            
            // Step4: Apply jobs
            foreach (var task in config.Jobs)
            {
                token.ThrowIfCancellationRequested();
                await task.Invoke(settings, variables);
                await Task.Delay(100, token);
            }

            // Step5: Compile apk
            token.ThrowIfCancellationRequested();
            await apkTool.CompileApk(settings.WorkingDirectory, settings.OutputFilename, settings.OnStdOutput,
                settings.OnStdError);
        }
    }
}
