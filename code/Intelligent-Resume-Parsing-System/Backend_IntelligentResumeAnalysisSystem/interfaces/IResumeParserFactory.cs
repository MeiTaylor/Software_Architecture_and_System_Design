namespace Backend_IntelligentResumeAnalysisSystem.interfaces
{
    public interface IResumeParserFactory
    {
        IResumeParser CreateParser(string parserType);

    }
}
