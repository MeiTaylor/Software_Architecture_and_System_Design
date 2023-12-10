using Backend_IntelligentResumeAnalysisSystem.interfaces;
using System.Diagnostics;
using System.Reflection;

namespace Backend_IntelligentResumeAnalysisSystem.converters
{
    public class DocxConverter : IFileConverter
    {
        public void ConvertToText(string filePath)
        {

            try
            {


                // 获取 bin 目录路径
                string binDirectory = AppDomain.CurrentDomain.BaseDirectory;
                // 构建到 python_scripts 目录的路径
                string pythonScriptPath = Path.Combine(binDirectory, @"..\..\..\python_scripts\docx_to_txt.py");

                // 标准化路径
                pythonScriptPath = Path.GetFullPath(pythonScriptPath);



                string arguments = $"\"{pythonScriptPath}\" \"{filePath}\" ";

                // 启动 Python 脚本进程
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = Environment.GetEnvironmentVariable("PYTHONPATH") ?? "python"; // 确保 python 命令在环境变量中配置正确
                start.Arguments = arguments;
                start.UseShellExecute = false; // 不使用操作系统外壳程序启动
                start.RedirectStandardOutput = true; // 重定向输出，以便可以读取输出
                start.RedirectStandardError = true; // 重定向标准错误输出

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
                        string stderr = process.StandardError.ReadToEnd(); // 读取错误输出
                        if (process.ExitCode != 0)
                        {
                            Console.WriteLine($"Docx文件转txt文件出错: {stderr}");
                        }
                        else
                        {
                            Console.WriteLine("Docx文件转txt文件成功");
                        }

                    }

                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"在转换Docx文件时发生错误: {ex.Message}");
            }

        }
    }
}
