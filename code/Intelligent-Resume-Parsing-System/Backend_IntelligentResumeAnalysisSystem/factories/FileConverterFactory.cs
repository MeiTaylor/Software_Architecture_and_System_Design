using Backend_IntelligentResumeAnalysisSystem.converters;
using Backend_IntelligentResumeAnalysisSystem.interfaces;

namespace Backend_IntelligentResumeAnalysisSystem.factories
{
    public class FileConverterFactory: IFileConverterFactory
    {

        public IFileConverter CreateConverter(string fileType)
        {
            switch (fileType.ToLower())
            {
                case "pdf":
                    return new PdfConverter();
                case "docx":
                    return new DocxConverter();
                case "jpeg":
                case "jpg":
                case "png":
                    return new ImageConverter();
                default:
                    throw new ArgumentException("不支持的文件类型");
            }
        }

    }
}
