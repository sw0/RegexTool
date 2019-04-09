namespace RegexTool.Core
{
    public interface IRegexReplaceInfo : IRegexBasicInfo
    {
        string Replacement { get; }
        bool AllEmpty { get; }
    }
}