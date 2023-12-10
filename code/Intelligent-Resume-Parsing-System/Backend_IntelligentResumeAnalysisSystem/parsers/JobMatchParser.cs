using Backend_IntelligentResumeAnalysisSystem.interfaces;
using System;
using System.Diagnostics;
using System.IO;

namespace Backend_IntelligentResumeAnalysisSystem.parsers
{
    public class JobMatchParser : IResumeParser
    {
        public void ParseAndSaveAsJson(string filePath)
        {
            try
            {
                // 获取 bin 目录路径
                string binDirectory = AppDomain.CurrentDomain.BaseDirectory;
                // 构建到 python_scripts 目录的路径
                string pythonScriptPath = Path.Combine(binDirectory, @"..\..\..\python_scripts\gpt_analyze_resume.py");

                string promptFilename = "Job_Matching_Guide"; // 提示文件名
                string arguments = $"\"{pythonScriptPath}\" \"{filePath}\" \"{promptFilename}\"";

                // 启动 Python 脚本进程
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = Environment.GetEnvironmentVariable("PYTHONPATH") ?? "python";
                start.Arguments = arguments;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;

                Console.WriteLine("开始分析人岗匹配");

                using (Process process = Process.Start(start))
                {

                    // 实时读取标准输出
                    using (StreamReader reader = process.StandardOutput)
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            Console.WriteLine(line); // 实时打印每一行输出
                        }
                    }

                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd();
                        if (process.ExitCode != 0)
                        {
                            Console.WriteLine($"人岗匹配分析出错: {stderr}");
                        }
                        else
                        {
                            Console.WriteLine("人岗匹配分析成功");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"在进行人岗匹配分析时发生错误: {ex.Message}");
            }


        }


    }
}
