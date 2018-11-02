using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace Mo.Money.EndToEnd.Common
{
    public static class Bash
    {
        public static Process Execute(string command, string workingDirectory = null)
        {
            var process = Process.Start(CreateBashStartInfo(command, workingDirectory));
            process.EnableRaisingEvents = true;
            return process;
        }
        
        private static ProcessStartInfo CreateBashStartInfo(string command, string workingDirectory)
        {
            var info = new ProcessStartInfo
            {
                FileName = "/usr/local/bin/zsh",
                Arguments = $"-c \"{command}",
                WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            foreach (DictionaryEntry env in Environment.GetEnvironmentVariables())
                info.Environment.Add((string) env.Key, (string) env.Value);
            return info;
        }
    }
}