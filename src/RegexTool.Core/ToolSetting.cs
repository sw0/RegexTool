using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RegexTool.Core
{
    public class ToolSetting
    {
        /// <summary>
        /// if ignore source selection, it will match all the text in the source;
        /// </summary>
        public bool IgnoreSourceSelection { get; set; }
    }

    public class ToolSettingManager
    {
        public ToolSettingManager()
        {

        }

        public ToolSetting Get()
        {
            ToolSetting setting = new ToolSetting();

            //TODO
            return setting;
        }
    }

    //public class IniFileManager
    //{
    //    public readonly string IniPath = string.Empty;

    //    public IniFileManager(string iniPath)
    //    {
    //        IniPath = iniPath;

    //        try
    //        {
    //            if (!File.Exists(iniPath))
    //            {
    //                using (var sw = File.CreateText(iniPath))
    //                {
    //                    sw.Close();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //TODO log
    //        }
    //    }

    //    #region -- read and write --

    //    public string ReadIniData(string Section, string Key, string NoText)
    //    {
    //        if (File.Exists(IniPath))
    //        {
    //            StringBuilder temp = new StringBuilder(1024);
    //            GetPrivateProfileString(Section, Key, NoText, temp, 1024, IniPath);
    //            return temp.ToString();
    //        }
    //        else
    //        {
    //            return String.Empty;
    //        }
    //    }


    //    public bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
    //    {
    //        if (File.Exists(iniFilePath))
    //        {
    //            long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
    //            if (OpStation == 0)
    //            {
    //                return false;
    //            }
    //            else
    //            {
    //                return true;
    //            }
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    #endregion

    //    #region -- INIT Kernel32 --

    //    [DllImport("kernel32")]
    //    private static extern long WritePrivateProfileString(string section,
    //                                                         string key,
    //                                                         string val,
    //                                                         string filePath);

    //    [DllImport("kernel32")]
    //    private static extern int GetPrivateProfileString(string section,
    //                                                      string key,
    //                                                      string def,
    //                                                      StringBuilder retVal,
    //                                                      int size,
    //                                                      string filePath);

    //    #endregion
    //}
}
