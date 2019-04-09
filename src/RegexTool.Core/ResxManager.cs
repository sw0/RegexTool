using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;


namespace RegexTool.Core
{
    public class ResxManager
    {
        private static ResourceManager _resourceManager = 
            new ResourceManager("RegexTool.Core.Properties.FormStrings", Assembly.Load("RegexTool.Core"));

        public static string GetResourceString(string name, CultureInfo ci = null)
        {
            if (ci == null)
            {
                ci = GetCultureInfo() ?? Thread.CurrentThread.CurrentUICulture;
            }
            //ci = CultureInfo.CreateSpecificCulture("zh-cn");
            return _resourceManager.GetString(name, ci);
        }
        public static string GetResourceString(FormStringKeys name, CultureInfo ci = null)
        {
            if (ci == null)
            {
                ci = GetCultureInfo() ?? Thread.CurrentThread.CurrentUICulture;
            }
            //ci = CultureInfo.CreateSpecificCulture("zh-cn");
            return _resourceManager.GetString(name.ToString(), ci);
        }

        public static CultureInfo GetCultureInfo()
        {
            try
            {
                if (_cultureName != null) return CultureInfo.CreateSpecificCulture(_cultureName);

                var iniPath = IniFileOperator.IniFileName;
                var ini = new IniFileOperator(iniPath);
                _cultureName = ini.ReadValue("Languages", "Name", Thread.CurrentThread.CurrentUICulture.Name);

                return CultureInfo.CreateSpecificCulture(_cultureName);
            }
            catch
            {
#if DEBUG
                Debug.WriteLine("failed to get UI-CULTURE");
#endif
            }

            return null;
        }

        private static string _cultureName = null;

        public static CultureInfo SetCultureInfo(CultureInfo culture)
        {
            try
            {
                _cultureName = culture.Name;
                var iniPath = IniFileOperator.IniFileName;
                var ini = new IniFileOperator(iniPath);
                ini.WriteValue("Languages", "Name", _cultureName);
            }
            catch
            {
#if DEBUG
                Debug.WriteLine("failed to get UI-CULTURE");
#endif
            }

            return null;
        }
    }
}

