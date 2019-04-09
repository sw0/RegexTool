using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace RegexTool.Core
{
    public class AppHelper
    {
        internal const string STR_PUBLIC_KEY = "<RSAKeyValue><Modulus>2xWwj4jDzZB9dYIFbNeWoB+xFS2ttZsY6Cv5QbAh2KlFZ5BlnAEvNtf3IvBD7T0X/VhrrE20LhS5m2zyX10VZpUxzfvZEb1ucz8aQKuzyuhV7R62N8GmhxJTeQGZ/5cm3cJ7Ty7WXPpKfdbJhMqKYUXsMDhTwArg6TrUAw3awPM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";//TODO
#if DEBUG
        internal const string STR_PRIVATE_KEY = "<RSAKeyValue><Modulus>2xWwj4jDzZB9dYIFbNeWoB+xFS2ttZsY6Cv5QbAh2KlFZ5BlnAEvNtf3IvBD7T0X/VhrrE20LhS5m2zyX10VZpUxzfvZEb1ucz8aQKuzyuhV7R62N8GmhxJTeQGZ/5cm3cJ7Ty7WXPpKfdbJhMqKYUXsMDhTwArg6TrUAw3awPM=</Modulus><Exponent>AQAB</Exponent><P>/HYx8zBUw5bcrelY9Z3ac+55S47HRfX227gOIrrESvP6dU0B6rjCW40hdPP7ssNke/BwtvfUpYoaR9zut6zGaw==</P><Q>3ie93NNGA+6REIa91EexArZRSxdKU6Te6sqzOnueXyOWA9QXZiu7I4Klqqt4JYz6MJHnEfhUbK3FHN6co2VBmQ==</Q><DP>2fMoGCyHYPtitHkZJaILL76W1JYEju9TGEiDW5QlVhffB1ld6Eds8yCZ25+ukZuBqkXe6PZ3jBn3qkafh84O1Q==</DP><DQ>mfFkKe/lprfcol0ckkuTp9N7BdP/13J6Xq1UAYTEPB1GySRhipVnOrY/4sKroSnN/XY9b0BsEFtiKh1A4iE6kQ==</DQ><InverseQ>EMoHmLVJEOMWkh0PiFeOUPCDyuu3qzbyq6JHI5MNTlSJ0AMI7yQ6SBQh9i0FR0SAoncd9lmvBKva6J4grbVjwA==</InverseQ><D>IohGrjkmc85kmBMIe+F3coHMTcRHbyqlQGM+BKaYjygPMoXGHlLpeEGOSMmKS2in0V0Qi7Yrwl+t6dvAUFvBtYDbMdfC699S/XUMXVGkSsfG3eSIdxs0zJtgqLGWqfOLx+jZ+Uekik9IL+irDsS/dA1ua5kTH4bOTiia7rMVhGE=</D></RSAKeyValue>";//TODO
#endif

        internal readonly static string STR_USEA_AGENT_DEFAULT = string.Empty;

        static AppHelper()
        {
            try
            {
                ISysInfoHelper sih = new SysInfoHelper();
                STR_USEA_AGENT_DEFAULT = sih.GetUserAgentForIE();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
#if DEBUG
                throw;
#endif
            }
        }

        public static AppInfo GetAppInfo()
        {
            ISysInfoHelper sih = new SysInfoHelper();

            var result = new AppInfo();

            try
            {
                result.AppVersion = sih.GetAppVersion();
                result.ComputerName = sih.GetComputerName();
                result.MAC = sih.GetMAC();
                result.RuntimeVersion = sih.GetRuntimeVersion();

                var ini = new IniFileOperator(IniFileOperator.IniFileName);
                result.AppId = ini.ReadValue("Application", "AppId", "");
                //Serial Number
                result.SN = ini.ReadValue("Application", "SN", "");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }
        public static void StartUp(AppStartRequest asr)
        {
            StartUp(0, asr);
        }

        private static void StartUp(int count, AppStartRequest asReq)
        {
            const int tryCount = 2;
            if (count > tryCount) return;
            bool failed = false;

            var info = GetAppInfo();
            var ini = new IniFileOperator(IniFileOperator.IniFileName);

            if (info != null && string.IsNullOrWhiteSpace(info.AppId))
            {
                var guid = Guid.NewGuid();
                info.AppId = guid.ToString();
                ini.WriteValue("Application", "AppId", info.AppId);
            }

            var data = new StartUpInfo(info);
            //asReq.StartUpData = data;

            try
            {
#if DEBUG
                string abc = data.ToQueryString();

                //var abc2 = System.Web.HttpUtility.UrlEncode(abc);
                //Debug.WriteLine(abc);
                //Debug.WriteLine(abc2);
#endif
                byte[] bytes = Encoding.UTF8.GetBytes("data=" + System.Web.HttpUtility.UrlEncode(data.ToQueryString()));
                //byte[] bytes = Encoding.UTF8.GetBytes("data=testdata");

                //TODO remove .aspx 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RegexToolHelper.ULR_APP_STARTUP);
                request.Method = "POST";
                request.UserAgent = STR_USEA_AGENT_DEFAULT;
                AppendLanguageInfoToHeader(request);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                System.IO.Stream newStream = request.GetRequestStream();
                // Send the data.
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream strmResp = response.GetResponseStream();
                    if (response.Headers["AuthResult"] == "Success")
                    {
                        if (response.ContentLength > 0 && false == data.SNExists())
                        {
                            string sn = response.Headers["SN"];
                            if (false == string.IsNullOrEmpty(sn))
                            {
                                ini.WriteValue("Application", "SN", sn);
                            }
                        }
                    }

                    if (response.ContentLength > 0) {
                        string content = string.Empty;
                        using (StreamReader reader = new StreamReader(strmResp))
                        {
                            content = reader.ReadToEnd();
                        }

                        if (!string.IsNullOrEmpty(content))
                        {
                            var asr = JsonConvert.DeserializeObject<AppStartResponse>(content);
                            if (asr != null)
                            {
                                if (asr.UpdateInfo != null)
                                {
                                    asReq.UpdateInfo = asr.UpdateInfo;
                                    // ProcessUpdateInfo(asr.UpdateInfo);
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                failed = true;
                Debug.WriteLine(wex.Message);
            }
            catch (Exception ex)
            {
                //TODO ....Important
                failed = true;
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if ((!failed) || count == tryCount)
                {
                    //var x = data.SNVerifyLocally();
                }
            }

            if (failed)
            {
                Thread.Sleep(1000 * (count + 1));
                StartUp(count + 1, asReq);
            }
        }

        private static void AppendLanguageInfoToHeader(HttpWebRequest request)
        {
            request.Headers.Add("Lang", ResxManager.GetCultureInfo().LCID.ToString());
        }

        public static string GetLangHeaderString()
        {
            return string.Format("Lang: {0}\r\n", ResxManager.GetCultureInfo().LCID);
        }

        //private static bool CheckLocal(StartUpInfo info)
        //{
        //    var ini = new IniFileOperator(IniFileOperator.IniFileName);
        //    string sn = ini.ReadValue("Application", "SN", string.Empty);

        //    if (sn == string.Empty)
        //    {
        //        //TODO show Ads
        //    }
        //    else
        //    {
        //        //CHECK SN
        //        var sns = sn.Split(new char[] { '|' }, 2);
        //        if (sns.Length == 2) {
        //            var localMD5 = sns[1];
        //        }
        //    }

        //    return true;
        //}
    }
}
