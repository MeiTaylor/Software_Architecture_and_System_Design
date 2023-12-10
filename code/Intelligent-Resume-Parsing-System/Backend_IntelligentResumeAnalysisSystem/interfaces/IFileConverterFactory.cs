namespace Backend_IntelligentResumeAnalysisSystem.interfaces
{
    public interface IFileConverterFactory
    {

        IFileConverter CreateConverter(string fileType);

    }
}
