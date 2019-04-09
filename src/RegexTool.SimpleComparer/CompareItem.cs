using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using RegexTool.Core;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RegexTool.SimpleComparer
{
    public partial class CompareItem : UserControl
    {
        public CompareItem()
        {
            InitializeComponent();

            txtInput.Dock = DockStyle.Fill;

            cms1.InitTextContextMenuItems(txtInput,
                 TextContextMenuType.SimpleText | TextContextMenuType.SaveContent);
        }

        public bool HasContent()
        {
            return this.txtInput.Text.Length > 0;
        }

        public List<string> GetItems()
        {
            string txt = txtInput.Text;

            var tmp = txtItemSeparator.Text
                .Replace("\\n", "\n")
                .Replace("\\r", "\r")
                .Replace("\\t", "\t")
                .Replace("\\\\", "\\");

            Regex reg = new Regex(":spliter=(.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            string[] separator = null;
            var m = reg.Match(tmp);
            if (m.Success)
            {
                tmp = tmp.Replace(m.Groups[0].Value, "");
                var separator2 = new[] { m.Groups[1].Value };
                separator = tmp.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                separator = tmp.Split('#');
            }

            if (separator.Length == 0) separator = new[] { Environment.NewLine };

            var result = txt.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            return result;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            //var btnLoadStr = btnLoad.Text;
            btnLoad.Enabled = false;

            string text = string.Empty;
            string fileOrURL = string.Empty;

            if (string.IsNullOrWhiteSpace(txtSource.Text))
            {
                txtSource.Text = string.Empty;

                var ofd = new OpenFileDialog();
                ofd.FileName = "*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileOrURL = txtSource.Text = ofd.FileName;
                }
                else
                {
                    btnLoad.Enabled = true;
                    return;
                }

                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    text = File.ReadAllText(fileOrURL);
                    sw.Stop();
                    Debug.WriteLine("Read file took " + sw.Elapsed.TotalSeconds + " seconds.");
                    sw.Reset();
                    sw.Start();
                    txtInput.Text = text;
                    sw.Stop();
                    //TODO bad performance in loading big data, like 8M text file, it took 40 senconds.
                    Debug.WriteLine("Refresh textbox took " + sw.Elapsed.TotalSeconds + " seconds.");
                }
                catch (Exception ex)
                {
                    txtInput.Text = ex.Message;
#if DEBUG
                    throw;
#endif
                }
                finally
                {
                    btnLoad.Enabled = true;
                }
            }
            else
            {
                fileOrURL = txtSource.Text;

                try
                {
                    if (Uri.IsWellFormedUriString(fileOrURL, UriKind.Absolute))
                    {
                        try
                        {
                            Stopwatch sw = new Stopwatch();
                            sw.Start();
                            var wr = (HttpWebRequest)WebRequest.Create(fileOrURL);

                            var response = (HttpWebResponse)wr.GetResponse();

                            using (StreamReader sr = new StreamReader(response.GetResponseStream(), true))
                            {
                                text = sr.ReadToEnd();
                            }

                            //text = File.ReadAllText(fileOrURL);
                            sw.Stop();
                            Debug.WriteLine("Open page took " + sw.Elapsed.TotalSeconds + " seconds.");
                            txtInput.Text = text;
                            response.Close();
                        }
                        catch (Exception ex)
                        {
                            txtInput.Text = ex.Message;
                        }
                        finally
                        {
                            btnLoad.Enabled = true;
                        }
                    }
                    else if (File.Exists(fileOrURL))
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        text = File.ReadAllText(fileOrURL);
                        sw.Stop();

                        Debug.WriteLine("Read file took " + sw.Elapsed.TotalSeconds + " seconds.");
                        sw.Reset();
                        sw.Start();
                        //TODO bad performance in loading big data, like 8M text file, it took 40 senconds.
                        txtInput.Text = text;
                        sw.Stop();
                        Debug.WriteLine("Refresh textbox took " + sw.Elapsed.TotalSeconds + " seconds.");
                    }
                }
                catch (Exception ex)
                {
                    txtInput.Text = ex.Message;
                }
                finally
                {
                    btnLoad.Enabled = true;
                }
            }
        }

        private void txtSource_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLoad_Click(null, null);
        }
    }
}
