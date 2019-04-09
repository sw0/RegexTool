using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using RegexTool.Core;
using System.Diagnostics;
using System.Threading;

namespace RegexTool
{
    public partial class RegexBar : UserControl, ISetUserInterfaceTexts
    {
        private IRegexAnalyst _regAnalyst = new RegexAnalyst();

        public Action<string[]> OnRegexGroupNamesChanged = null;

        public RegexBar()
        {
            InitializeComponent();

            //var cms = new CommonTextContextMenuStrip(this.components, "xdsds", txtRegPattern);
            //txtRegPattern.ContextMenuStrip = cms;
#if INIT_TEST_DATA
            this.txtRegPattern.Text = @"{""titleID"":(\d+),""boxartPrefix"":""(.+?)"",""name"":""(.+?)""}";
#endif
            //txtRegPattern.AutoWordSelection = true;
            //txtRegPattern.AutoWordSelection = false;

            InitializeContextMenuItemsForRegexBox();

            ControlHelper.SetDragDropAbility(txtRegPattern, true);
            {
                cbIgnoreCase.Checked = true;
                cbIgnoreCase.ToolTipText = "指定不区分大小写的匹配。";

                cbIgnoreCase.Click += RegexOptionBox_Click;
                cbExplicitCapture.Click += RegexOptionBox_Click;
                cbECMAScript.Click += RegexOptionBox_Click;
                cbCultureInvariant.Click += RegexOptionBox_Click;
                cbIgnorePatternWhitespace.Click += RegexOptionBox_Click;
                cbMultiline.Click += RegexOptionBox_Click;
                cbRightToLeft.Click += RegexOptionBox_Click;
                cbSingleline.Click += RegexOptionBox_Click;

                cbSingleline.DisplayStyle = cbExplicitCapture.DisplayStyle
                                            = cbECMAScript.DisplayStyle
                                              = cbCultureInvariant.DisplayStyle
                                                = cbIgnorePatternWhitespace.DisplayStyle
                                                  = cbMultiline.DisplayStyle
                                                    = cbRightToLeft.DisplayStyle
                                                      = cbIgnoreCase.DisplayStyle
                                                        = ToolStripItemDisplayStyle.Text;

                tsmiRegOptionsHelp.Click += (s, o) =>
                    Process.Start(string.Format("http://msdn.microsoft.com/{0}/library/yd1hzczs.aspx", ResxManager.GetCultureInfo().Name));
            }

            InitializeRegexButtons();

            var tsmiSelectionOnly = cmsRegex.FindByName<ToolStripMenuItem>(FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY);
            if (tsmiSelectionOnly != null)
                txtRegPattern.HideSelection = !tsmiSelectionOnly.Checked;
        }

        private void InitializeContextMenuItemsForRegexBox()
        {
            cmsRegex.Items.Clear();
            cmsRegex.InitTextContextMenuItems(txtRegPattern,
                TextContextMenuType.SimpleText
                | TextContextMenuType.SaveContent
                | TextContextMenuType.RegexTextBox);
        }


        void RegexOptionBox_Click(object sender, EventArgs e)
        {
            var btn = sender as ToolStripButton;
            if (btn != null)
            {
                btn.Checked = !btn.Checked;
                if (btn == cbSingleline && cbMultiline.Checked)
                {
                    cbMultiline.Checked = !cbSingleline.Checked;
                }
                if (btn == cbMultiline && cbSingleline.Checked)
                {
                    cbSingleline.Checked = !cbMultiline.Checked;
                }
            }
            else
            {
                var btn2 = sender as ToolStripMenuItem;
                if (btn2 != null)
                    btn2.Checked = !btn2.Checked;
            }
        }

        public string RegexPattern
        {
            get
            {
                var selectionOnly = false;
                var tsmiSelectionOnly = cmsRegex.FindByName<ToolStripMenuItem>(FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY);
                if (tsmiSelectionOnly != null)
                    selectionOnly = tsmiSelectionOnly.Checked;

                if (selectionOnly && txtRegPattern.SelectedText.Length > 0)
                    return txtRegPattern.SelectedText;

                return this.txtRegPattern.Text;
            }
#if DEBUG
            //set { this.txtRegPattern.Text = value; }
#endif
        }

        public RegexOptions Options
        {
            get
            {
                var result = RegexOptions.None;

                if (cbCultureInvariant.Checked)
                    result = result | RegexOptions.CultureInvariant;
                if (cbECMAScript.Checked)
                    result = result | RegexOptions.ECMAScript;
                if (cbExplicitCapture.Checked)
                    result = result | RegexOptions.ExplicitCapture;
                if (cbIgnoreCase.Checked)
                    result = result | RegexOptions.IgnoreCase;
                if (cbIgnorePatternWhitespace.Checked)
                    result = result | RegexOptions.IgnorePatternWhitespace;
                if (cbMultiline.Checked)
                    result = result | RegexOptions.Multiline;
                if (cbRightToLeft.Checked)
                    result = result | RegexOptions.RightToLeft;
                if (cbSingleline.Checked)
                    result = result | RegexOptions.Singleline;

                return result;
            }
        }

        //private List<ToolStripMenuItem> _orignalMenuItems = null;

        //private void cmsRegex_Opening(object sender, CancelEventArgs e)
        //{
        //    tsmiCare.Visible = txtRegPattern.SelectedText.Length > 0 || txtRegPattern.Text.Length < _regAnalyst.MaxAcceptTextLength;
        //    tsmiNotCare.Visible = txtRegPattern.SelectedText.Length > 0;
        //}

        //private void tsmiCare_DropDownOpening(object sender, EventArgs e)
        //{
        //    var tsmi = sender as ToolStripMenuItem;

        //    if (tsmi == null) return;

        //    string tag = tsmi.Tag as string;

        //    var text = txtRegPattern.SelectedText;
        //    if (text == string.Empty) text = txtRegPattern.Text;

        //    var regStyles = _regAnalyst.GetRegexStyleOptions(text);

        //    tsmi.DropDownItems.Clear();
        //    if (regStyles.Count > 0)
        //    {
        //        var groupItMenuItem = new ToolStripMenuItem()
        //                                  {
        //                                      Text = ToolHelper.STR_MENUITEM_GROUP_IT,
        //                                      Name = "tsmiGroupIt",
        //                                      Tag = ToolHelper.STR_MENUITEM_GROUP_IT,
        //                                  };
        //        groupItMenuItem.Click += GroupIt_Click;
        //        tsmi.DropDownItems.Add(groupItMenuItem);

        //        if (tsmi == tsmiCare)
        //        {
        //            var groupItMenuItem2 = new ToolStripMenuItem()
        //            {
        //                Text = ToolHelper.STR_MENUITEM_GROUP_IT_WITH_NAME,
        //                Name = "tsmiGroupIt2",
        //                Tag = ToolHelper.STR_MENUITEM_GROUP_IT_WITH_NAME,
        //            };
        //            groupItMenuItem2.Click += GroupIt_Click;
        //            tsmi.DropDownItems.Add(groupItMenuItem2);
        //        }

        //        var tsmiArr = ParseToTMI(regStyles);
        //        foreach (ToolStripMenuItem i in tsmiArr)
        //        {
        //            i.Click += RegexMenuItem_Click;
        //            tsmi.DropDownItems.Add(i);
        //        }
        //    }
        //    else
        //    {
        //        //tsmi.DropDownItems.Add(new ToolStripMenuItem("Empty") { Enabled = false });
        //        tsmi.DropDownItems.Add(new ToolStripMenuItem("Empty") { Enabled = false });
        //    }
        //}

        //private void GroupIt_Click(object sender, EventArgs e)
        //{
        //    var tsmi = sender as ToolStripMenuItem;
        //    if (tsmi == null) return;

        //    string tag = tsmi.Tag as string;

        //    int selectionStart = txtRegPattern.SelectionStart;

        //    var text = txtRegPattern.SelectedText;
        //    if (text.Length == 0) return;

        //    if (tag == ToolHelper.STR_MENUITEM_GROUP_IT_WITH_NAME)
        //    {
        //        //text = "(?<" + STR_PLACE_HOLDER_GROUP + ">" + text + ")";
        //        text = string.Format(ToolHelper.STR_PLACE_HOLDER_GROUP_FMT, text);
        //    }
        //    else
        //    {
        //        var parent = tsmi.OwnerItem;

        //        if (parent.Text == tsmiNotCare.Text)
        //        {
        //            text = "(?:" + text + ")";
        //        }
        //        else
        //        {
        //            text = "(" + text + ")";
        //        }
        //    }
        //    //TODO UNDO support
        //    Clipboard.SetText(text);
        //    txtRegPattern.Paste();
        //    Clipboard.Clear();

        //    if (tag == ToolHelper.STR_MENUITEM_GROUP_IT_WITH_NAME)
        //    {
        //        txtRegPattern.SelectionStart = selectionStart + 3;
        //        txtRegPattern.SelectionLength = ToolHelper.STR_PLACE_HOLDER_GROUP.Length;
        //    }
        //}

        //private void RegexMenuItem_Click(object sender, EventArgs e)
        //{
        //    //ToolHelper.ProcessTextContextMenu(txtRegPattern, sender);
        //    var tmsi = sender as ToolStripMenuItem;
        //    if (tmsi == null) return;

        //    string pattern = tmsi.Tag as string;

        //    if (string.IsNullOrEmpty(pattern)) return;

        //    int selectionStart = txtRegPattern.SelectionStart;

        //    Clipboard.SetText(pattern);
        //    txtRegPattern.Paste();

        //    if (pattern.StartsWith(ToolHelper.STR_PLACE_HOLDER_GROUP_PART))
        //    {
        //        txtRegPattern.SelectionStart = selectionStart + 3;
        //        txtRegPattern.SelectionLength = ToolHelper.STR_PLACE_HOLDER_GROUP.Length;
        //    }
        //    else
        //    {
        //        txtRegPattern.SelectionStart = selectionStart;
        //        txtRegPattern.SelectionLength = pattern.Length;
        //    }
        //    Clipboard.Clear();
        //}

        //private IEnumerable<ToolStripMenuItem> ParseToTMI(IEnumerable<RegexStyle> items)
        //{
        //    foreach (RegexStyle regexStyle in items)
        //    {
        //        yield return new ToolStripMenuItem()
        //                         {
        //                             Text = regexStyle.Name,
        //                             Tag = regexStyle.Pattern,
        //                         };
        //    }
        //}

        #region -- Publich Methods --
        public void SetRegexPattern(string pattern)
        {
            txtRegPattern.Text = pattern;
        }

        #endregion

        private void txtRegPattern_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ToolHelper.TextBoxSelectionChanged(txtRegPattern, UIManager.Current.SetRowColumnIndex);
            }
        }

        #region -- initialize tool strip buttons for regular expression --
        private void InitializeRegexButtons()
        {
            tsRegexButtons.Items.Clear();

            var tsis = GetRegexToolStripItems();

            Regex reg = new Regex(@"[^(]+\((.+)\)");
            foreach (var tsb in tsis)
            {
                tsRegexButtons.Items.Add(tsb);
                if (!string.IsNullOrEmpty(tsb.Text))
                {
                    var m = reg.Match(tsb.Text);

                    if (m.Success)
                        tsb.Tag = m.Groups[1].Value;
                    else
                        tsb.Tag = tsb.Text;

                    if (tsb.Text.IndexOf("document", 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        //Open online Regular Expression document
                        tsb.Click += (sender, e) => Process.Start(
                            string.Format("http://msdn.microsoft.com/{0}/library/az24scfc.aspx",
                            (ResxManager.GetCultureInfo() ?? Thread.CurrentThread.CurrentUICulture).Name));
                    }
                    else
                    {
                        tsb.MouseUp += RegexButton_MouseUp;
                    }
                }
            }
        }

        private IEnumerable<ToolStripItem> GetRegexToolStripItems()
        {
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegStartOrEnd",
                    Text = "^",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_START),
                    //@"The match must start at the beginning of the string or line. Right click for $, The match must occur at the end of the string or before \n at the end of the line or string.",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegDigit",
                    Text = "\\d",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_DIGIT),
                    //@"Matches any decimal digit. Press Ctrl for \d+, Shift for \d*, Alt for \d?",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegWord",
                    Text = "\\w",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_WORD),
                    //@"Matches any word character. Press Ctrl for \w+, Shift for \w*, Alt for \w?",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegSpace",
                    Text = "\\s",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_SPACE),
                    //@"Matches any white-space character. Press Ctrl for \s+, Shift for \s*, Alt for \s?",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegTab",
                    Text = "\\t",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_TAB),
                    //"Match a tab. You can click it with Shift, Ctrl or Alt key.",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegWildcard",
                    Text = ".",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_WILDCARD),
                    //@"Wildcard: Matches any single character except \n. To match a literal period character (. or \u002E), you must precede it with the escape character (\.).",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegReturn",
                    Text = "NewLine(\\r\\n)",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_RETURN),
                    //"Match a new line. You can click it with Shift, Ctrl or Alt key.",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegBorder",
                    Text = "Border(\\b)",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_BORDER),
                    //"You can click it with Shift, Ctrl or Alt key.",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripSeparator();
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegMany",
                    Text = "+",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_MANY),
                    //"Many (1 or more). Press ALT for +?",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegAny",
                    Text = "*",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_ANY),
                    //"Any (0 or more). Press ALT for *?",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegOneOrNone",
                    Text = "?",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_NONE_OR_ONE),
                    //"0 or 1",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegMTimes",
                    Text = "{m}",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_M_TIMES),
                    //"Matches the previous element exactly m times: {m}, or not more than n {m,n}.",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegOr",
                    Text = "|",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_OR),
                    //"Matches any one element separated by the vertical bar (|) character. For example: th(e|is|at) ",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripSeparator();
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegCharGroup",
                    Text = "[]",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_CHAR_GROUP),
                    //"character group, right click to define any single character that is not in character_group. By default, characters in character_group are case-sensitive.",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegGroup",
                    Text = "()",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_GROUP),
                    //"group the selected text. Right click to define an uncapturing group.",
                };
                yield return tsb;
            }
            {
                var tsb = new ToolStripSeparator();
                yield return tsb;
            }
            {
                var tsb = new ToolStripButton()
                {
                    Name = "tblRegDoc",
                    Text = "Online Document",
                    ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_REGEX_ONELINE_DOC),
                    //"Online Document about Regular Expressions",
                };
                yield return tsb;
            }
        }

        private void RegexButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                return;

            var textbox = txtRegPattern;

            var tag = ((ToolStripItem)sender).Tag.ToString();

            var oldSS = textbox.SelectionStart;
            var oldSL = textbox.SelectionLength;

            //MessageBox.Show(((ToolStripItem)sender).Text + ", tag: " + ((ToolStripItem)sender).Tag.ToString());
            if (!string.IsNullOrWhiteSpace(tag))
            {
                if (tag == "()" || tag == "[]")
                {
                    const string groupWithName = "(?<Name>)";
                    if (e.Button == MouseButtons.Right)
                    {
                        tag = tag == "()" ? "(?:)" : "[^]";
                    }
                    else if (tag == "()" && (Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        tag = groupWithName;
                    }

                    if (textbox.SelectionLength > 0)
                    {
                        textbox.Insert(tag.Substring(0, tag.Length - 1), false);
                        textbox.SelectionStart = oldSS + oldSL + tag.Length - 1;
                        textbox.SelectionLength = 0;
                        textbox.Insert(tag.Last().ToString(), false);

                        if (groupWithName != tag)
                        {
                            textbox.SelectionStart = oldSS + tag.Length - 1;
                            textbox.SelectionLength = oldSL;
                        }
                        else
                        {
                            textbox.SelectionStart = oldSS + tag.Length - 6;
                            textbox.SelectionLength = 4;
                        }
                    }
                    else
                    {
                        if (tag == "()" && e.Button == MouseButtons.Right)
                        {
                            textbox.Insert("(?:)", false);
                        }
                        else if (tag == "[]" && e.Button == MouseButtons.Right)
                        {
                            textbox.Insert("[^", false);
                        }
                        else
                        {
                            //gotCtrlPressed = false;
                            textbox.Insert(tag, false);
                        }

                        if (tag == groupWithName)
                        {
                            textbox.SelectionStart = oldSS + tag.Length - 6;//(?<Name>)
                            textbox.SelectionLength = 4; //length for "Name"
                        }
                        else
                        {
                            //textbox.SelectionStart = gotCtrlPressed ? oldSS + 3 : oldSS + 1;
                            textbox.SelectionStart = oldSS + tag.Length - 1;
                            textbox.SelectionLength = 0;
                        }
                    }
                }
                else if (tag == "^")// when right click, then produce a "$"
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        textbox.Insert(tag, true, false);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        textbox.Insert("$", true, false);
                    }
                }
                else
                {
                    const string quantifierM = "{m}";

                    //bool replaceSelectedText = false == IsRegexStringByRegexButtons(textbox.SelectedText);
                    Func<string, bool> IsQuantifier =
                        x => "+*?|".Contains(tag);

                    if (IsQuantifier(tag) == false
                        && (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        if (tag == @"\r\n") tag = "(?:\\r\\n)";
                        textbox.Insert(tag + "*", true, false);
                    }
                    else if (IsQuantifier(tag) == false
                        && (((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        || e.Button == MouseButtons.Right))
                    {
                        if (tag == @"\r\n") tag = "(?:\\r\\n)";
                        textbox.Insert(tag + "+", true, false);
                    }
                    else if (tag != "?" && tag != quantifierM
                        && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        if (tag == @"\r\n") tag = "(?:\\r\\n)";
                        textbox.Insert(tag + "?", true, false);
                    }
                    else
                        textbox.Insert(tag, true, false);

                    if (tag == quantifierM)
                    {
                        //Select "m" that user can modify the value to a number that user want.
                        textbox.SelectionStart = oldSS + 1;
                        textbox.SelectionLength = 1;
                    }
                }
            }
        }
        #endregion
        //private void txtRegPattern_DragDrop(object sender, DragEventArgs e)
        //{
        //    var txt = sender as TextBoxBase;
        //    if (txt == null) throw new Exception("txt is null");

        //    var textDrag = e.Data.GetData(DataFormats.Text) as string;

        //    var txtPoint = txt.PointToScreen(new Point(0, 0));
        //    var pointInsideTextbox = new Point(e.X - txtPoint.X, e.Y - txtPoint.Y);

        //    var idx = txt.GetCharIndexFromPosition(pointInsideTextbox);

        //    if (!string.IsNullOrEmpty(textDrag))
        //    {
        //        txt.Text = txt.Text.Insert(idx, textDrag);
        //        txt.SelectionStart = idx;
        //        txt.SelectionLength = textDrag.Length;
        //    }
        //}

        //private void txtRegPattern_DragEnter(object sender, DragEventArgs e)
        //{
        //    var txt = sender as TextBoxBase;
        //    if (txt == null) throw new Exception("txt is null");

        //    var txtPoint = txt.PointToScreen(new Point(0, 0));
        //    var pointInsideTextbox = new Point(e.X - txtPoint.X, e.Y - txtPoint.Y);
        //    var idx = txt.GetCharIndexFromPosition(pointInsideTextbox);
        //    txt.SelectionStart = idx;
        //    txt.SelectionLength = 0;

        //    txt.Focus();
        //    e.Effect = e.Data.GetDataPresent(DataFormats.Text) ?
        //        DragDropEffects.Copy : DragDropEffects.None;
        //}

        //private void txtRegPattern_DragOver(object sender, DragEventArgs e)
        //{
        //    var txt = sender as TextBoxBase;
        //    if (txt == null) throw new Exception("txt is null");

        //    var txtPoint = txt.PointToScreen(new Point(0, 0));
        //    var pointInsideTextbox = new Point(e.X - txtPoint.X, e.Y - txtPoint.Y);
        //    var idx = txt.GetCharIndexFromPosition(pointInsideTextbox);
        //    txt.SelectionStart = idx;
        //    txt.SelectionLength = 0;
        //}

        public void SetUserInterfaceTexts()
        {
            InitializeContextMenuItemsForRegexBox();
            InitializeRegexButtons();
        }

        private void txtRegPattern_TextChanged(object sender, EventArgs e)
        {
            if (OnRegexGroupNamesChanged == null) return;

            var regexStr = txtRegPattern.Text;
            string[] gNames = new string[] { };

            if (regexStr != string.Empty)
            {
                try
                {
                    Regex reg = new Regex(regexStr);

                    gNames = reg.GetGroupNames();
                }
                catch
                {
                    //Do nothing, it's not a valid Regular Expression
                }
            }

            OnRegexGroupNamesChanged(gNames);
        }
    }

    public static class TextBoxExtensions
    {
        public static void Insert(this TextBoxBase txtbox, string text, bool removeSelectedText = true, bool selectNewlyInserted = true)
        {
            if (string.IsNullOrEmpty(text)) return;

            var oldSelectStart = txtbox.SelectionStart;
            var oldSelectLength = txtbox.SelectionLength;
            if (txtbox.SelectionLength > 0)
            {
                if (removeSelectedText)
                {
                    txtbox.Text = txtbox.Text.Remove(oldSelectStart, oldSelectLength).Insert(oldSelectStart, text);
                    txtbox.Focus();
                    txtbox.SelectionStart = selectNewlyInserted ? oldSelectStart : oldSelectStart + text.Length;
                    txtbox.SelectionLength = selectNewlyInserted ? text.Length : 0;
                }
                else
                {
                    txtbox.Text = txtbox.Text.Insert(oldSelectStart, text);
                    txtbox.Focus();
                    txtbox.SelectionStart = selectNewlyInserted ? oldSelectStart : oldSelectStart + text.Length;
                    txtbox.SelectionLength = selectNewlyInserted ? text.Length : 0;
                }
            }
            else
            {
                txtbox.Text = txtbox.Text.Insert(oldSelectStart, text);
                txtbox.Focus();
                txtbox.SelectionStart = selectNewlyInserted ? oldSelectStart : oldSelectStart + text.Length;
                txtbox.SelectionLength = selectNewlyInserted ? text.Length : 0;
            }
        }
    }
}
