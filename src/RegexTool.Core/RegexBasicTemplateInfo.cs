using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public enum OrderOption
    {
        None,
        Asc,
        Desc,
    }

    public class OrderInfo
    {
        public string GroupForSort { get; set; }
        public OrderOption OrderType { get; set; }
        private bool _autoTurnDigitsToNumber;

        /// <summary>
        /// indicates whether convert digit string to number
        /// </summary>
        public bool AutoTurnDigitsToNumber
        {
            get { return _autoTurnDigitsToNumber; }
            set { _autoTurnDigitsToNumber = value; }
        }
    }

    public class BatchInfo
    {
        public int ItemsPerBatch { get; set; }
        //TODO BatchSeparator is hard-coded in TemplatePage.cs currently
        public string BatchSeparator { get; set; }
    }

    public class TemplateParameters
    {
        public const string STR_ROWNUM_TEXT = "RowNum";

        public OrderInfo OrderInfo { get; set; }
        public bool IgnoreDuplicated { get; set; }
        public bool ShowDuplicatedOnly { get; set; }

        public BatchInfo BatchInfo { get; set; }

        private string[] _rowNumberSpaceHolder = new string[] { "$", "$" };

        public string[] RowNumberDelimiter
        {
            get { return _rowNumberSpaceHolder; }
            set
            {
                if (value.Length == 1 && !string.IsNullOrEmpty(value[0]))
                {
                    _rowNumberSpaceHolder = new string[] { value[0], value[0] };
                    return;
                }
                else if (value.Length == 2)
                {
                    if (!string.IsNullOrEmpty(value[0]) && !string.IsNullOrEmpty(value[1]))
                    {
                        _rowNumberSpaceHolder = value;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public TemplateParameters()
        {
            OrderInfo = new OrderInfo();
        }
    }

    public class TemplateResultItem
    {
        public string Line { get; set; }
        public string DataForSort { get; set; }

        public TemplateResultItem(string line, string dataForSort)
        {
            Line = line;
            DataForSort = dataForSort;
        }
    }

    public class TemplateResultItemEqualityComparer : IEqualityComparer<TemplateResultItem>
    {
        public bool Equals(TemplateResultItem x, TemplateResultItem y)
        {
            return x.Line == y.Line;
        }

        public int GetHashCode(TemplateResultItem obj)
        {
            if (string.IsNullOrEmpty(obj.Line)) return 0;
            return obj.Line.GetHashCode();
        }
    }

    public class RegexTemplateInfo : RegexBasicInfo, IRegexTemplateInfo
    {
        public string Template
        {
            get;
            protected set;
        }

        public TemplateParameters TplParameters { get; set; }

        public RegexTemplateInfo(string regexPattern,
            RegexOptions regexOptions,
            string input,
            string template,
            TemplateParameters tplParams)
            : base(regexPattern, regexOptions, input)
        {
            this.Template = template ?? string.Empty;
            this.TplParameters = tplParams ?? new TemplateParameters();
        }
    }
}