using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;

namespace RegexTool.Core
{
    /// <summary>
    /// reference: http://blog.csdn.net/llwinnner/article/details/4011936
    /// </summary>
    public interface ISysInfoHelper
    {
        string GetMAC();
        string GetComputerName();
        string GetAppVersion();
        string GetRuntimeVersion();
        /// <summary>
        /// get the disc serial number
        /// </summary>
        /// <param name="driveLetter">optional, default c:. Please pass in c:, d: or something like that.</param>
        /// <returns></returns>
        string GetDiskVolumeSerialNumber(string driveLetter);
        /// <summary>
        /// get CPU
        /// </summary>
        /// <returns></returns>
        string GetCpu();
        /// <summary>
        /// get disk id, example: Hitachi HTS723232A7A364
        /// </summary>
        /// <returns></returns>
        string GetDiskId();

        string GetOS();

        string GetUserAgentForIE();
    }

    public class SysInfoHelper : ISysInfoHelper
    {
        public string GetMAC()
        {
            try
            {
                var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                var moc = mc.GetInstances();
                string str = "";
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                        str = mo["MacAddress"].ToString();
                }
                return str;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return string.Empty;
        }

        public string GetComputerName()
        {
            try
            {
                string domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                string hostName = Dns.GetHostName();
                string fqdn = "";
                if (!hostName.Contains(domainName))
                    fqdn = hostName + "." + domainName;
                else
                    fqdn = hostName;

                return fqdn;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return string.Empty;
        }

        public string GetAppVersion()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        public string GetRuntimeVersion()
        {
            return System.Reflection.Assembly.GetEntryAssembly().ImageRuntimeVersion;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="driveLetter">like c:, d:. Can be null or empty. Then c: will be used by default</param>
        /// <returns></returns>
        public string GetDiskVolumeSerialNumber(string driveLetter)
        {
            if (string.IsNullOrWhiteSpace(driveLetter))
                driveLetter = "c:";

            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + driveLetter + "\"");
                disk.Get();

                return disk.GetPropertyValue("VolumeSerialNumber").ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
#if DEBUG
                throw;
#endif
            }
#if DEBUG
#else
            return string.Empty;
#endif
        }

        public string GetDiskId()
        {
            string hdInfo = "";

            try
            {
                ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc1 = cimobject1.GetInstances();

                //TODO what if the computer/laptop got two or more hard disks?
                foreach (ManagementObject mo in moc1)
                {
                    hdInfo = (string)mo.Properties["Model"].Value;
                    break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
#if DEBUG
                throw;
#endif
            }

            return hdInfo;
        }


        public string GetCpu()
        {
            string cpu = string.Empty;

            try
            {
                ManagementClass mc = new ManagementClass("win32_Processor");
                ManagementObjectCollection mcc = mc.GetInstances();

                foreach (ManagementObject obj in mcc)
                {
                    cpu = obj.Properties["Processorid"].Value.ToString();
                    break;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
#if DEBUG
                throw;
#endif
            }

            return cpu;
        }

        public string GetMainHardDiskId()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            String strHardDiskID = null;
            foreach (ManagementObject mo in searcher.Get())
            {
                strHardDiskID = mo["SerialNumber"].ToString().Trim();
                break;
            }
            return strHardDiskID;
        }

        public string GetOS()
        {
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
            return rk.GetValue("ProductName").ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="http://social.msdn.microsoft.com/Forums/zh-CN/76919fe9-1b2e-4b62-a12f-769e55b90cc9/get-ie-useragent-value-in-c"/>
        /// <returns></returns>
        public string GetUserAgentForIE()
        {
            string js = @"<script type='text/javascript'>function getUserAgent(){document.write(navigator.userAgent)}</script>";

            System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser();
            wb.Url = new Uri("about:blank");
            wb.Document.Write(js);
            wb.Document.InvokeScript("getUserAgent");

            string userAgent = wb.DocumentText.Substring(js.Length);

            //System.Console.WriteLine(userAgent);
            return userAgent;
        }
    }
}
