using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public class RegexValidator
    {
        public static bool IsValid(string regexPatter)
        {
            try
            {
                var g = new Regex(regexPatter);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
