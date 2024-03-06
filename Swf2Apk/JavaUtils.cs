using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Swf2Apk
{
    public class JavaUtils
    {
        /// <summary>
        /// 查找 JAVA_HOME
        /// 
        /// 优先搜索环境变量，如果没有，查找注册表。
        /// </summary>
        /// <returns>JAVA安装目录，如果没有返回 null</returns>
        public static string? GetJavaHome()
        {
            string? envJavaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (envJavaHome != null)
                return envJavaHome;

            if (OperatingSystem.IsWindows())
            {
                string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment";
                using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(javaKey);
                string? currentVersion = baseKey?.GetValue("CurrentVersion")?.ToString();
                if (currentVersion != null)
                {
                    using var homeKey = baseKey?.OpenSubKey(currentVersion);
                    return homeKey?.GetValue("JavaHome")?.ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// 查找 JAVA 可执行文件
        /// </summary>
        /// <returns>JAVA可执行文件，如果没有返回 null</returns>
        public static string? GetJavaExecutable()
        {
            string? javaHome = GetJavaHome();
            if (javaHome == null)
                return null;

            if (OperatingSystem.IsWindows())
                return Path.Combine(javaHome, "bin", "java.exe");
            else
                return Path.Combine(javaHome, "bin", "java");
        }

        public struct JarStartInfo
        {
            public string JarPath;
            public string[]? Arguments;
            public string JavaExecutable;
            public string[]? JavaProperties;
            public string? WorkingDirectory;
            public Action<string>? OnStdOutput;
            public Action<string>? OnStdError;
        }

        /// <summary>
        /// 执行 JAR 文件
        /// </summary>
        /// <param name="startInfo">JAR 启动参数</param>
        /// <returns>进程执行结果</returns>
        /// <exception cref="InvalidOperationException">若启动失败，抛出异常</exception>
        public static async Task<int> ExecuteJar(JarStartInfo startInfo)
        {
            List<string> args = ["-jar", "-Xmx1024M"];
            if (startInfo.JavaProperties != null)
                args.AddRange(startInfo.JavaProperties);
            args.Add(startInfo.JarPath);
            if (startInfo.Arguments != null)
                args.AddRange(startInfo.Arguments);

            var processStartInfo = new ProcessStartInfo(startInfo.JavaExecutable, args)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = startInfo.WorkingDirectory,
            };

            using var process = Process.Start(processStartInfo);
            if (process == null)
                throw new InvalidOperationException("Failed to start java process.");
            
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                    startInfo.OnStdOutput?.Invoke(e.Data);
            };
            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                    startInfo.OnStdError?.Invoke(e.Data);
            };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();
            return process.ExitCode;
        }
    }
}
