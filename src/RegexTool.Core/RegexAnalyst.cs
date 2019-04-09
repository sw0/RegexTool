using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public class RegexAnalyst : IRegexAnalyst
    {
        public int MaxAcceptTextLength { get; protected set; }

        public static Regex RegMultipleSpace = new Regex("\\s{4,}", RegexOptions.Compiled);
        public static Regex RegAllDigit = new Regex("^\\d+$", RegexOptions.Compiled);
        public static Regex RegAllSpace = new Regex("^\\s+$", RegexOptions.Compiled);
        public static Regex RegAllLetter = new Regex("^[a-zA-Z]+$", RegexOptions.Compiled);
        public static Regex RegWord = new Regex("^\\w+$", RegexOptions.Compiled);

        public RegexAnalyst(int maxAcceptTextLength = 50)
        {
            MaxAcceptTextLength = maxAcceptTextLength < 50 ? 50 : maxAcceptTextLength;
            if (MaxAcceptTextLength > 200) MaxAcceptTextLength = 200;
        }

        public string ToSimpleRegexString(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            input = input.Replace("\\", "\\\\")
                .Replace("+", "\\+")
                .Replace("-", "\\-")
                .Replace("*", "\\*")
                .Replace("?", "\\?")
                .Replace("{", "\\{")
                .Replace("}", "\\}")
                .Replace("(", "\\(")
                .Replace(")", "\\)")
                .Replace("[", "\\[")
                .Replace("]", "\\]")
                .Replace("^", "\\^")
                .Replace("$", "\\$")
                .Replace("\t", "\\t")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace(".", "\\.");

            //replace 4+ space to \d+
            input = RegMultipleSpace.Replace(input, "\\d+");

            return input;
        }

        public List<RegexStyle> GetRegexStyleOptions(string input, RegexOptions options = RegexOptions.None)
        {
            var result = new List<RegexStyle>();

            if (RegAllDigit.Match(input).Success)
            {
                result.Add(new RegexStyle()
                               {
                                   Name = "\\d+",
                                   Pattern = "\\d+",
                               });
                result.Add(new RegexStyle()
                {
                    Name = "\\d*",
                    Pattern = "\\d*",
                });

                result.Add(new RegexStyle()
                {
                    Name = "\\d{0," + input.Length + "}",
                    Pattern = "\\d{0," + input.Length + "}",
                });

                result.Add(new RegexStyle()
                {
                    Name = "\\d{" + input.Length + "}",
                    Pattern = "\\d{" + input.Length + "}",
                });

                result.Add(new RegexStyle()
                {
                    Name = "\\w+",
                    Pattern = "\\w+",
                });
                result.Add(new RegexStyle()
                {
                    Name = "\\w{" + input.Length + "}",
                    Pattern = "\\w{" + input.Length + "}",
                });
            }
            else if (RegAllLetter.Match(input).Success)
            {
                //if (input.Length <= MaxAcceptTextLength)
                //{
                //    result.Add(new RegexStyle()
                //    {
                //        Name = input,
                //        Pattern = input,
                //    });
                //}

                result.Add(new RegexStyle()
                {
                    Name = "[a-zA-Z]+",
                    Pattern = "[a-zA-Z]+",
                });
                result.Add(new RegexStyle()
                {
                    Name = "[a-zA-Z]*",
                    Pattern = "[a-zA-Z]*",
                });

                result.Add(new RegexStyle()
                {
                    Name = "[a-zA-Z]{0," + input.Length + "}",
                    Pattern = "[a-zA-Z]0," + input.Length + "}",
                });

                result.Add(new RegexStyle()
                {
                    Name = "[a-zA-Z]{" + input.Length + "}",
                    Pattern = "[a-zA-Z]{" + input.Length + "}",
                });
                result.Add(new RegexStyle()
                {
                    Name = "\\w+",
                    Pattern = "\\w+",
                });
            }
            else if (RegAllSpace.Match(input).Success)
            {
                result.Add(new RegexStyle()
                {
                    Name = "\\s{0," + input.Length + "}",
                    Pattern = "\\s0," + input.Length + "}",
                });

                result.Add(new RegexStyle()
                {
                    Name = "\\s{" + input.Length + "}",
                    Pattern = "\\s{" + input.Length + "}",
                });
                result.Add(new RegexStyle()
                {
                    Name = "\\s+",
                    Pattern = "\\s+",
                });
                result.Add(new RegexStyle()
                {
                    Name = "\\s*",
                    Pattern = "\\s*",
                });
            }
            else
            {
                result.Add(new RegexStyle()
                {
                    Name = string.Format("A: .+ B: .* C: .+? D: .{{{0}}}", input.Length),
                    Pattern = ".+?",
                });

                result.Add(new RegexStyle()
                {
                    Name = string.Format("Group it: A: .+ B: .* C: .+? D: .{{{0}}}", input.Length),
                    Pattern = "(.+?)",
                });

                result.Add(new RegexStyle()
                {
                    Name = string.Format("Group it with name: A: .+ B: .* C: .+? D: .{{{0}}}", input.Length),
                    Pattern = "(?<GroupName>.+?)",
                });
            }

            //TODO enhancement
            return result;
        }

        #region -- helper methods --

        private bool AllDigital(string input)
        {
            bool result = true;
            foreach (char c in input)
            {
                if (!Char.IsDigit(c))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool AllLetter(string input)
        {
            bool result = true;
            foreach (char c in input)
            {
                if (!Char.IsLetter(c))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }


        #endregion
    }
}