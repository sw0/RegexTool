namespace RegexTool.Core
{
    public interface IRegexTemplateInfo : IRegexBasicInfo
    {
        string Template { get; }

        TemplateParameters TplParameters { get; }
    }
}