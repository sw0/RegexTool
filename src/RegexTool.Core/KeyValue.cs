using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexTool.Core
{
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public KeyValue()
        {
        }

        public KeyValue(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("key");

            Key = key;
            Value = value;
        }
    }
}
