using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RegexTool.Core
{
    public class ReplaceItemCollection : List<ReplaceItem>
    {
        public string Replace(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            foreach (var ri in this)
            {
                bool success = true;
                input = ri.Replace(input, out success);
#if DEBUG
                Debug.WriteLine("ReplaceItemCollection.Replace(): has invalid replace item.");
#endif
            }

            return input;
        }
    }

    public class ReplaceItem
    {
        #region -- properties --

        private string _regexPattern;

        public string RegexPattern
        {
            get { return _regexPattern; }
            private set { _regexPattern = value; }
        }

        private RegexOptions _options;

        public RegexOptions Options
        {
            get { return _options; }
            private set { _options = value; }
        }


        private string _replacement;

        public string Replacement
        {
            get { return _replacement; }
            private set { _replacement = value; }
        }

        private Regex _regexObj;

        public Regex RegexObj
        {
            get
            {
                if (_regexObj != null) return _regexObj;
                if (string.IsNullOrEmpty(_regexPattern)) return null;
                else
                {

                    try
                    {
                        _regexObj = new Regex(_regexPattern, _options);
                        return _regexObj;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        #endregion

        public ReplaceItem(string regexPattern, RegexOptions options, string replacement)
        {
            _regexPattern = regexPattern;
            _replacement = replacement;
            _options = options;
        }

        public string Replace(string input, out bool success)
        {
            if (RegexObj != null)
            {
                success = true;
                return RegexObj.Replace(input, Replacement);
            }
            else
            {
                success = false;
                return input;
            }
        }
    }
}
