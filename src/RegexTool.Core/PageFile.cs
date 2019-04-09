using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexTool.Core
{
    [Serializable]
    public class PageFile
    {
        public string FileOrUrl { get; set; }
        public string EncodingName { get; set; }
        public string SourceText { get; set; }
        public string RegexPattern { get; set; }
        public string Replacement { get; set; }
        public bool AllowEmptyReplacement { get; set; }
        public string Template { get; set; }
        public string ReplacementResult { get; set; }
        public string TemplateResult { get; set; }
    }
}
