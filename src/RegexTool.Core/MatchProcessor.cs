using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public class MatchProcessor
    {
        public bool CheckRegexPattern(string input, out string message)
        {
            try
            {
                Regex r = new Regex(input);
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public MatchCollection MatchAll(Regex regObj, string input)
        {
            if (regObj == null)
                throw new ArgumentNullException("regObj");

            if (input == null)
                throw new ArgumentNullException("input");

            var result = regObj.Matches(input);
            //Match m = null;

            return result;
        }
    }
}
