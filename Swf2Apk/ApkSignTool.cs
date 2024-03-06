using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swf2Apk
{
    public class ApkSignTool
    {
        private readonly string _javaExecutable;
        private readonly string _jarPath;

        public ApkSignTool(string javaExecutable, string jarPath)
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
                JavaProperties = null,
                WorkingDirectory = workingDirectory,
                OnStdOutput = onOutput,
                OnStdError = onError,
            };
            int status = await JavaUtils.ExecuteJar(startInfo);
            if (status != 0)
                throw new Exception($"ApkSignTool exited with status {status}.");
        }

        /// <summary>
        /// 签名 APK
        /// </summary>
        /// <param name="apkPath">APK 路径</param>
        /// <param name="outputDir">输出文件夹</param>
        /// <param name="onOutput">标准输出</param>
        /// <param name="onError">标准错误输出</param>
        /// <returns>任务对象</returns>
        public async Task DebugSignApk(string apkPath, string outputDir, Action<string>? onOutput, Action<string>? onError)
        {
            await ExecuteApkTool(new[] { "-a", apkPath, "-o", outputDir }, "", onOutput, onError);
        }
    }
}
