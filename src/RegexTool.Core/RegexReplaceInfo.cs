using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public class RegexReplaceInfo : RegexBasicInfo, IRegexReplaceInfo
    {
        public string Replacement
        {
            get;
            protected set;
        }


        public bool AllEmpty
        {
            get;
            protected set;
        }
        

        public RegexReplaceInfo(string regexPattern, RegexOptions regexOptions, string input, string replacement,bool allowEmpty)
            : base(regexPattern, regexOptions, input)
        {
            this.Replacement = replacement ?? string.Empty;
            AllEmpty = allowEmpty;
        }
    }
}