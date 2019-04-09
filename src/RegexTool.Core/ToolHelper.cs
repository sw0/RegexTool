using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RegexTool.Core;

namespace RegexTool.Core
{
    public class LocationInfo
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public int Length { get; set; }

        public LocationInfo(string text, int index, int length)
        {
            Text = text;
            Index = index;
            Length = length;
        }
    }

    public class ToolHelper
    {
        #region -- Constants --

        public const string STR_FILE_FILTER = "RegexProejct files(*.xml)|*.xml|All files(*.*)|*.*";

        public const string STR_MENUITEM_CLOSE_WITHOUT_SAVE = "Close Project without save";

        public const string STR_MENUITEM_RECENT_FILES = "RecentFiles";
        public const string STR_MENUITEM_RECENT_PROJECTS = "RecentProjects";

        //public const string STR_MENUITEM_UNDO = "Undo";
        //public const string STR_MENUITEM_REDO = "Redo";

        public const string STR_MENUITEM_SELECTION_ONLY = "Selection Only";

        public const string STR_MENUITEM_GROUP_IT = "Group It";
        public const string STR_MENUITEM_GROUP_IT_WITH_NAME = "Group It With Name";


        #endregion

        #region MyRegion

        public const string STR_PLACE_HOLDER_GROUP = "GroupName";
        public const string STR_PLACE_HOLDER_GROUP_PART = "(?<GroupName>";
        public const string STR_PLACE_HOLDER_GROUP_FMT = "(?<GroupName>{0})";
        #endregion


        public static ToolSetting Settings
        {
            get;
            set;
        }

        public static string LastOpenedFile { get; set; }

        private static readonly Regex _regHighlightCheck = new Regex("\\\\highlight[012]\\s?", RegexOptions.Compiled);
        private static readonly Regex _regHighlight = new Regex("\\\\highlight[012]\\s?", RegexOptions.Compiled);

        public static void LocateText(RichTextBox txtInput, LocationInfo li)
        {
            //txtInput.SelectionBackColor = Color.White;
            txtInput.DeselectAll();
            //RemoveHighlights(txtInput, false);

            txtInput.Select(li.Index, li.Length);
            //txtInput.SelectionBackColor = Color.LightBlue;
            //txtInput.ScrollToCaret();
        }


        public static void RemoveHighlights(RichTextBox rich, bool keepPos)
        {
            if (rich == null) throw new ArgumentNullException("rich");

            if (false == _regHighlightCheck.Match(rich.Rtf).Success)
            {
                return;
            }
            var pos = rich.SelectionStart;
#if DEBUG
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string s = _regHighlight.Replace(rich.Rtf, string.Empty);
            if (s != rich.Rtf)
                rich.Rtf = s;
            stopwatch.Stop();
            Debug.WriteLine(" _regHighlight.Replace(rich.Rtf, string.Empty) took: " + stopwatch.Elapsed.TotalSeconds +
                            " seconds");
#else
           string s = _regHighlight.Replace(rich.Rtf, string.Empty);
           if (s != rich.Rtf)
               rich.Rtf = s;
#endif
            if (keepPos) rich.SelectionStart = pos;
        }

        public static void RichFillAll(RichTextBox txtInput, string s)
        {
            int currentSelectedStart = txtInput.SelectionStart;
            string selectedText = txtInput.SelectedText;

            //ToolHelper.RemoveHighlights(txtInput, false);

            //txtInput.DeselectAll();
            string input = txtInput.Text;
#if DEBUG
            Debug.WriteLine("Find (len:{0}): {1}", s.Length, s);
#endif
            if (s == string.Empty) return;

            //Regex reg = new Regex(s, RegexOptions.IgnoreCase);

            //txtInput.Rtf = reg.Replace(txtInput.Rtf, "\\highlight1 $0 \\highlight0 ");

            int lastIdx = -2;
            int idx = -1;

            do
            {
                idx = input.IndexOf(s, idx >= 0 ? idx + s.Length : 0, StringComparison.OrdinalIgnoreCase);

                if (idx != -1)
                {
                    txtInput.SelectionStart = idx;
                    txtInput.SelectionLength = s.Length;
                    //txtInput.SelectionBackColor = Color.LightBlue;
                }

                if (idx != lastIdx) lastIdx = idx;
                else
                {
                    throw new Exception("endless circle occurred.");
                }

                //Debug.WriteLine("idx: " + idx);
                //if (i > 100) break;
            } while (idx >= 0);

            if (selectedText == s && currentSelectedStart >= 0)
            {
                //txtInput.SelectionStart = currentSelectedStart;
                //txtInput.SelectionLength = s.Length;
            }
        }

        public static void Paste(TextBoxBase txt, string textToPaste = null)
        {
            if (textToPaste == null)
            {
                if (Clipboard.ContainsText())
                {
                    textToPaste = Clipboard.GetText();
                }
                else
                {
                    return;
                }
            }
            var oldStart = txt.SelectionStart;

            txt.Text = txt.Text.Insert(txt.SelectionStart, textToPaste);
            txt.SelectionStart = oldStart + textToPaste.Length;
        }

        public static void ProcessTextContextMenu(TextBoxBase textbox, object sender)
        {
            var tsmi = sender as ToolStripMenuItem;

            if (tsmi == null) return;

            var cmd = tsmi.Tag as string;

            var text = string.Empty;
            try
            {
                if (cmd == FormStringKeys.STR_MENU_ITEM_COPY_ALL.ToString())
                {
                    if (textbox.Text != "")
                        Clipboard.SetText(textbox.Text);
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_COPY.ToString())
                {
                    if (textbox.SelectedText != "")
                        Clipboard.SetText(textbox.SelectedText);
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_CUT.ToString())
                {
                    if (textbox.SelectedText != "")
                    {
                        textbox.Cut();
                    }
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_PASTE.ToString())
                {
                    if (Clipboard.ContainsText())
                    {
                        //TODO no RTF
                        textbox.Paste();
                    }
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_SELECT_ALL.ToString())
                {
                    textbox.Focus();
                    textbox.SelectAll();
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_CLEAR.ToString())
                {
                    textbox.Clear();
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_DELETE.ToString())
                {
                    if (textbox.SelectionLength > 0)
                    {
                        var oldStart = textbox.SelectionStart;
                        textbox.Text = textbox.Text.Remove(textbox.SelectionStart, textbox.SelectionLength);
                        textbox.SelectionStart = oldStart;
                    }
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_SAVE.ToString())
                {
                    var sfd = new SaveFileDialog();
                    sfd.Filter = ResxManager.GetResourceString(FormStringKeys.STR_TEXT_SAVE_FILTER);
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, textbox.Text);
                    }
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_WORDWRAP.ToString())
                {
                    textbox.WordWrap = !tsmi.Checked;
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_SEND_TO_SOURCE.ToString())
                {
                    //TODO STR_MENUITEM_SEND_TO_SOURCE
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_UNDO.ToString())
                {
                    if (textbox.CanUndo) textbox.Undo();
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_COPY_AS_REGEX.ToString())
                {
                    text = textbox.SelectedText;
                    if (text != string.Empty)
                    {
                        IRegexAnalyst ra = new RegexAnalyst();
                        text = ra.ToSimpleRegexString(text);
                        Clipboard.SetText(text);
                    }
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_TO_REGEX_FORMAT.ToString())
                {
                    text = textbox.SelectedText;

                    if (text == string.Empty) text = textbox.Text;

                    if (text != string.Empty)
                    {
                        IRegexAnalyst ra = new RegexAnalyst();
                        text = ra.ToSimpleRegexString(text);

                        if (textbox.SelectedText.Length > 0)
                        {
                            if (text != textbox.SelectedText)
                            {
                                //TODO it can be undo,but affect the clipboard
                                Clipboard.SetText(text);
                                textbox.Paste();
                                Clipboard.Clear();
                            }
                        }
                        else
                        {
                            if (text != textbox.Text)
                            {
                                //TODO it can be undo,but affect the clipboard
                                Clipboard.SetText(text);
                                textbox.SelectAll();
                                textbox.Paste();
                                Clipboard.Clear();
                            }
                        }
                    }
                }
                else if (cmd == FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY.ToString())
                {
                    tsmi.Checked = !tsmi.Checked;
                    //textbox.HideSelection = !tsmi.Checked;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public static void TextBoxSelectionChanged(TextBoxBase textbox, Action<RowColumnIndex> notificationAct)
        {
            if (null == textbox) return;

            var line = textbox.GetLineFromCharIndex(textbox.SelectionStart);
            var column = textbox.SelectionStart - textbox.GetFirstCharIndexOfCurrentLine();

            //Debug.WriteLine("Line: {0}, Column: {1}", line, column);

            RowColumnIndex rci = new RowColumnIndex()
            {
                Row = line + 1,
                Column = column + 1,
                SelectionLength = textbox.SelectionLength,
                Length = textbox.TextLength,
            };

            if (notificationAct != null)
                notificationAct(rci);
        }
    }
}
