using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using RegexTool.Core;
using System.Diagnostics;
using System.Reflection;

namespace RegexTool.Core
{
    public class KVCollection : KeyedCollection<string, KeyValue>
    {
        protected override string GetKeyForItem(KeyValue item)
        {
            return item.Key;
        }
    }

    public class KeyValueManager
    {
        public readonly string FileName = string.Empty;

        public readonly KVCollection Items = new KVCollection();

        public KeyValueManager(string fileName)
        {
            //TODO get the 
            //string codeBase = Assembly.GetExecutingAssembly().CodeBase;

            //if (codeBase.StartsWith("file:///"))
            //{
            //    codeBase = codeBase.Substring(8);
            //}

            //Debug.WriteLine("codeBase: " + codeBase);
            //string path = Path.Combine(Path.GetDirectoryName(codeBase), SysFileName);

            FileName = fileName;
        }

        public void Add(string key, string value)
        {
            Items.Add(new KeyValue(key, value));
        }

        public void Add(KeyValue kv)
        {
            if (Items.Contains(kv)) return;

            Items.Add(kv);
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<KeyValue>));

            try
            {
                using (var sw = File.Create("d:\\RE2.xml"))
                {
                    serializer.Serialize(sw, Items.ToList());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("KeyValueManager.Save:" + ex.Message);
#if DEBUG
                throw;
#endif
            }
        }
    }
}
