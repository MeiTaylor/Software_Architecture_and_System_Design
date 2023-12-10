using Backend_IntelligentResumeAnalysisSystem.factories;
using Backend_IntelligentResumeAnalysisSystem.interfaces;
using System;
using System.IO;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public class resume_converter_analysis
    {
        public bool ResumeConverterService(string filePath)
        {
            Console.WriteLine("开始文件转换测试");
            try
            {
                string extension = Path.GetExtension(filePath).TrimStart('.').ToLower();

                // 工厂模式接口对接
                IFileConverterFactory factory = new FileConverterFactory();

                //工厂模式创建产品
                IFileConverter converter = factory.CreateConverter(extension);

                //策略模式实现
                converter.ConvertToText(filePath);

                // 如果没有异常发生，返回 true 表示成功
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"无法创建文件转换器：{ex.Message}");
                return false; // 发生异常时，返回 false 表示失败
            }
            catch (Exception ex) // 捕获其他潜在的异常
            {
                Console.WriteLine($"文件转换时发生错误：{ex.Message}");
                return false; // 发生异常时，返回 false 表示失败
            }
        }
    




        public bool ResumeAnalysisService(string originalFilePath, string parserType)
        {
            // 初始化filePath为空字符串
            string filePath = string.Empty;

            switch (parserType.ToLower())
            {
                case "educationandworkexperience":
                    filePath = originalFilePath;
                    break;
                case "basicinfo":
                case "talentprofile":
                case "jobmatch":
                    var directory = new DirectoryInfo(Path.GetDirectoryName(originalFilePath));
                    var parentDirectory = directory.Parent;
                    if (parentDirectory != null)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(originalFilePath) + ".txt";
                        filePath = Path.Combine(parentDirectory.FullName, "Text_Conversions", fileName);
                    }
                    else
                    {
                        // 无法获取父目录
                        Console.WriteLine("无法获取父目录");
                        return false;
                    }
                    break;
                default:
                    Console.WriteLine("不支持的简历分析格式");
                    return false;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                // 文件路径未正确设置
                Console.WriteLine("文件路径未正确设置");
                return false;
            }

            try
            {
                IResumeParserFactory factory = new ResumeParserFactory();
                IResumeParser parser = factory.CreateParser(parserType);
                parser.ParseAndSaveAsJson(filePath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"解析简历时发生错误：{ex.Message}");
                return false;
            }
        }
    }




}
