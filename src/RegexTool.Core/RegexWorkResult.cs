using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public class RegexWorkResult<T> : IWorkResult<T> where T : class
    {
        public Regex RegexObj
        {
            get;
            protected set;
        }

        public T Data
        {
            get;
            protected set;
        }

        public bool IsSuccess
        {
            get;
            protected set;
        }

        public string ErrorMessage
        {
            get;
            protected set;
        }

        public RegexWorkResult(T data, Regex regex)
            : this(data, regex, true, string.Empty)
        {
        }

        public RegexWorkResult(T data, Regex regex, bool isSuccess, string errorMessage = "")
        {
            Data = data;
            RegexObj = regex;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}