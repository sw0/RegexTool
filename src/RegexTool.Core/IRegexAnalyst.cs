using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public class RegexStyle
    {
        public string Name { get; set; }
        public string Pattern { get; set; }
    }

    public interface IRegexAnalyst
    {
        int MaxAcceptTextLength {get;}

        string ToSimpleRegexString(string input);

        List<RegexStyle> GetRegexStyleOptions(string input, RegexOptions options = RegexOptions.None);
    }
}
