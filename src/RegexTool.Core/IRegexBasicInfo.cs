using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public interface IRegexBasicInfo
    {
        string RegexPattern { get; }
        string Input { get; }
        RegexOptions RegexOptions { get; }
        Regex RegexObj { get; }

        RegexValidationResult Validate();
    }

    public interface IRegexFullInfo : IRegexReplaceInfo, IRegexTemplateInfo
    {

    }
}