using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swf2Apk
{
    public class ApkTool
    {
        private static readonly string[] DefaultJavaProperties =
        [
            "-Duser.language=en",
            "-Dfile.encoding=UTF8",
            "-Djdk.util.zip.disableZip64ExtraFieldValidation=true",
            "-Djdk.nio.zipfs.allowDotZipEntry=true",
        ];

        private readonly string _javaExecutable;
        private readonly string _jarPath;

        public ApkTool(string javaExecutable, string jarPath)
        {
            _javaExecutable = javaExecutable;
            _jarPath = jarPath;
        }

        private async Task ExecuteApkTool(string[] arguments, string workingDirectory, Action<string>? onOutput, Action<string>? onError)
        {
            JavaUtils.JarStartInfo startInfo = new JavaUtils.JarStartInfo
            {
                JarPath = _jarPath,
                Arguments = arguments,
                JavaExecutable = _javaExecutable,
                JavaProperties = DefaultJavaProperties,
                WorkingDirectory = workingDirectory,
                OnStdOutput = onOutput,
                OnStdError = onError,
            };
            int status = await JavaUtils.ExecuteJar(startInfo);
            if (status != 0)
                throw new Exception($"ApkTool exited with status {status}.");
        }

        /// <summary>
        /// 解包 APK 文件
        /// </summary>
        /// <param name="apkPath">APK路径</param>
        /// <param name="outputPath">输出目录</param>
        /// <param name="onOutput">标准输出</param>
        /// <param name="onError">标准错误输出</param>
        /// <returns>异步任务对象</returns>
        public async Task DecompileApk(string apkPath, string outputPath, Action<string>? onOutput, Action<string>? onError)
        {
            await ExecuteApkTool(new[] { "d", apkPath, "-f", "-o", outputPath }, "", onOutput, onError);
        }

        /// <summary>
        /// 打包 APK
        /// </summary>
        /// <param name="inputPath">输入文件夹</param>
        /// <param name="apkPath">输出 APK</param>
        /// <param name="onOutput">标准输出</param>
        /// <param name="onError">标准错误输出</param>
        /// <returns>异步任务对象</returns>
        public async Task CompileApk(string inputPath, string apkPath, Action<string>? onOutput,
            Action<string>? onError)
        {
            await ExecuteApkTool(new[] { "b", inputPath, "-f", "-o", apkPath, "--use-aapt1" }, "", onOutput, onError);
        }
    }
}
