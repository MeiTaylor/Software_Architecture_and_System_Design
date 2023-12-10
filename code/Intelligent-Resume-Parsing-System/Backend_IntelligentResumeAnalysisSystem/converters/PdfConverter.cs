using Backend_IntelligentResumeAnalysisSystem.interfaces;
using System;
using System.Diagnostics;
using System.IO;

namespace Backend_IntelligentResumeAnalysisSystem.converters
{
    public class PdfConverter : IFileConverter
    {
        public void ConvertToText(string filePath)
        {
            try
            {
                // 获取 bin 目录路径
                string binDirectory = AppDomain.CurrentDomain.BaseDirectory;
                // 构建到 python_scripts 目录的路径
                string pythonScriptPath = Path.Combine(binDirectory, @"..\..\..\python_scripts\pdf_to_txt.py");

                string arguments = $"\"{pythonScriptPath}\" \"{filePath}\" ";

                // 启动 Python 脚本进程
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = Environment.GetEnvironmentVariable("PYTHONPATH") ?? "python";
                start.Arguments = arguments;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;

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
                            Console.WriteLine($"PDF文件转txt文件出错: {stderr}");
                        }
                        else
                        {
                            Console.WriteLine("PDF文件转txt文件成功");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"在转换PDF文件时发生错误: {ex.Message}");
            }
        }
    }
}
