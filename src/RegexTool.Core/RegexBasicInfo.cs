using System;
using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public class RegexBasicInfo : IRegexBasicInfo
    {
        public string RegexPattern { get; protected set; }
        public string Input { get; protected set; }
        public RegexOptions RegexOptions { get; protected set; }

        public Regex _regexObj = null;

        public Regex RegexObj
        {
            get
            {
                if (_regexObj == null)
                {
                    try
                    {
                        _regexObj = new Regex(RegexPattern, this.RegexOptions);

                        _validationResult = new RegexValidationResult
                        {
                            InvalidReason = string.Empty,
                            IsValid = true,
                        };
                    }
                    catch (Exception ex)
                    {
                        //ErrorMessage = ex.Message;
                        _validationResult = new RegexValidationResult
                                                {
                                                    InvalidReason = ex.Message,
                                                    IsValid = false,
                                                };
                    }
                }

                return _regexObj;
            }
        }

        //public string ErrorMessage { get; set; }

        public RegexBasicInfo(string regexPattern, RegexOptions regexOptions, string input)
        {
            this.RegexPattern = regexPattern;
            this.RegexOptions = regexOptions;
            this.Input = input;
        }

        private RegexValidationResult _validationResult = null;

        public virtual RegexValidationResult Validate()
        {
            if (_validationResult == null)
            {
                var o = this.RegexObj;
            }

            return _validationResult;
        }
    }

    public class RegexFullInfo : RegexReplaceInfo, IRegexFullInfo
    {
        public string Template { get; set; }
        //public bool IgnoreDuplicated { get; set; }
        //public bool ShowDuplicatedOnly { get; set; }
        //public OrderInfo OrderParams { get; set; }

        public TemplateParameters TplParameters { get; set; }

        public RegexFullInfo(string regexPattern, RegexOptions regexOptions,
            string input,
            string replacement, bool allEmptyReplacement,
            string template, TemplateParameters tplParams)
            : base(regexPattern, regexOptions, input, replacement, allEmptyReplacement)
        {
            this.Template = template ?? string.Empty;
            this.TplParameters = tplParams ?? new TemplateParameters();
        }
    }
}