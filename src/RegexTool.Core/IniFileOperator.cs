using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RegexTool.Core
{
    //http://www.th7.cn/Program/net/201209/91193.shtml
    public class IniFileOperator
    {
        public static readonly string IniFileName = string.Empty;

        static IniFileOperator()
        {
            IniFileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().CodeBase).Substring(6) + "\\settings.ini";
        }


        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string GetFileName
        {
            get;
            set;
        }
        public IniFileOperator(string filePath)
        {
            GetFileName = filePath;
        }
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void WriteValue(string Section, string key, string value)
        {
            WritePrivateProfileString(Section, key, value, GetFileName);
        }
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="key">待获取数据的键</param>
        /// <param name="defValue">表示当通过键获取数据时，如果键不存在或无法找到数据，则返回该值</param>
        /// <returns></returns>
        public string ReadValue(string Section, string key, string defValue)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, key, defValue, temp, 255, this.GetFileName);
            return temp.ToString();
        }

    }
}
