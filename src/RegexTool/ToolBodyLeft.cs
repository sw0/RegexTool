using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RegexTool.Core;
using RegexTool.Properties;

namespace RegexTool
{
    public partial class ToolBodyLeft : UserControl, ISetUserInterfaceTexts
    {
        public Action<string> OnFileLoaded = null;
        public Action<RowColumnIndex> OnRowColumnChangeNotification = null;
        public Action<String[]> OnRegexGroupNamesChanged = null;

        //private readonly RichTextBox txtInput = new RichTextBox();
        protected bool _regexExcuted = false;

        #region -- constructor --

        public ToolBodyLeft()
        {
            InitializeComponent();

            try
            {
                OnRowColumnChangeNotification = UIManager.Current.SetRowColumnIndex;
            }
            catch
            {
            }

            //txtInput.Focus();
            sourceBar1.OnFileLoaded += FileLoaded;

            if (tlpSource != null)
            {
                //txtInput.DoubleClick += this.txtInput_DoubleClick;
                txtInput.TextChanged += txtInput_TextChanged;
                //txtInput.Click += txtInput_Click;

                txtInput.HideSelection = false;

                txtInput.ContextMenuStrip = cmsInput;
            }

            InitializeContextMenuForSource();

#if INIT_TEST_DATA
            this.txtInput.Text = @"{""id"":1,""result"":{""genreID"":12475,""sort"":""standard"",""purchaseType"":""any"",""profile"":""none"",""pageNum"":14,""itemsPerPage"":20,""totalPages"":14,""totalItems"":271,""items"":[{""titleID"":421715,""boxartPrefix"":""cars_2_a3544430_"",""name"":""Cars 2""},{""titleID"":404395,""boxartPrefix"":""tangled_e1fe5cf3_"",""name"":""Tangled""},{""titleID"":463540,""boxartPrefix"":""beverly_hills_chihuahua_3_a7809127_"",""name"":""Beverly Hills Chihuahua 3: Viva La Fiesta!""},{""titleID"":339884,""boxartPrefix"":""the_santa_clause_51205caf_"",""name"":""The Santa Clause""},{""titleID"":469599,""boxartPrefix"":""disney_secret_of_the_wing_s_7167e3dd_"",""name"":""Disney Secret of the Wings""},{""titleID"":475615,""boxartPrefix"":""santa_paws_2_the_santa_pu__pups_18355b74_"",""name"":""Santa Paws 2: The Santa Pups""},{""titleID"":470728,""boxartPrefix"":""cinderella_diamond_editio_on_d4484a32_"",""name"":""Cinderella: Diamond Edition""},{""titleID"":463543,""boxartPrefix"":""marvels_the_avengers_a115529a_"",""name"":""Marvel's The Avengers""},{""titleID"":488481,""boxartPrefix"":""the_odd_life_of_timothy_g_reen_612f16ac_"",""name"":""The Odd Life of Timothy Green""},{""titleID"":475611,""boxartPrefix"":""brave_ff4e1c14_"",""name"":""Brave""},{""titleID"":333458,""boxartPrefix"":""finding_nemo_760de7fb_"",""name"":""Finding Nemo""}],""responseCode"":0,""responseMessage"":""Success""}}";

            //this.RegexPattern = @"""titleID"":(\d+)";
            //this.RegexPattern = @"{""titleID"":(\d+),""boxartPrefix"":""(.+?)"",""name"":""(.+?)""}";
            //SetLengthInfo();
            txtInput_SelectionChanged(txtInput, null);
#endif
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.regexBar1.OnRegexGroupNamesChanged = OnRegexGroupNamesChanged;
        }

        private void InitializeContextMenuForSource()
        {
            this.cmsInput.Items.Clear();

            this.cmsInput.InitTextContextMenuItems(txtInput,
                TextContextMenuType.SimpleText
                | TextContextMenuType.SaveContent
                | TextContextMenuType.WordWrap
                | TextContextMenuType.RegexSource);

            //HACK menu item "Put to Regex Box" needs access to another control, so hard code here.
            var tsmiPutToRegexBox = cmsInput.FindByName<ToolStripMenuItem>(FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX);
            if (tsmiPutToRegexBox != null) tsmiPutToRegexBox.Click += this.tsmiPutToRegexBox_Click;
        }
        #endregion

        #region -- properties --

        public string SourceText
        {
            get
            {
                var selectionOnly = false;

                var miSelectionOnly = cmsInput.FindByName<ToolStripMenuItem>(FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY);
                if (miSelectionOnly != null) selectionOnly = miSelectionOnly.Checked;
                if (selectionOnly && txtInput.SelectedText.Length > 0)
                {
                    return txtInput.SelectedText;
                }
                return this.txtInput.Text;
            }
        }


        public string PathOrUrl
        {
            get
            {
                return sourceBar1.PathOrUrl;
            }
        }

        public string EncodingName
        {
            get
            {
                return sourceBar1.EncodingName;
            }
        }
        public string RegexPattern
        {
            get
            {
                return this.regexBar1.RegexPattern;
            }
            //#if DEBUG
            //            private set
            //            {
            //                this.regexBar1.RegexPattern = value;
            //            }
            //#endif
        }

        public RegexOptions Options
        {
            get
            {
                return this.regexBar1.Options;
            }
        }
        #endregion

        public void LocateSelected(LocationInfo li)
        {
            if (_regexExcuted == false) return;

            ToolHelper.LocateText(txtInput, li);
        }

        private void FileLoaded(LoadResult lr)
        {
            if (txtInput.InvokeRequired)
            {
                Action<LoadResult> act = FileLoaded;
                txtInput.BeginInvoke(act, lr);
            }
            else
            {
                if (lr.IsSuccess)
                {
                    txtInput.Text = lr.Text;
                    _regexExcuted = false;

                    txtInput_SelectionChanged(txtInput, null);
                    //SetLengthInfo();
                }
                else
                {
                    SetErrorInfo(lr.Error);
                }
                txtInput.Select(0, 0);

                if (OnFileLoaded != null) OnFileLoaded(lr.FileOrUrl);
            }
        }

        private void txtInput_DoubleClick(object sender, EventArgs e)
        {
            txtInput.TextChanged -= txtInput_TextChanged;
            txtInput.TextChanged -= txtInput_TextChanged;
            string selectedText = txtInput.SelectedText;

            ToolHelper.RichFillAll(txtInput, selectedText);

            txtInput.TextChanged += txtInput_TextChanged;
        }


        private void txtInput_Click(object sender, EventArgs e)
        {
            txtInput.TextChanged -= txtInput_TextChanged;
            txtInput.TextChanged -= txtInput_TextChanged;
            txtInput.SetRefreshable(false);
            int x = txtInput.GetScrollPos();
            ToolHelper.RemoveHighlights(txtInput, true);
            txtInput.SetRefreshable(true);
            txtInput.Refresh();
            txtInput.SetScrollPos(x);
            txtInput.TextChanged += txtInput_TextChanged;
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            _regexExcuted = false;
            //TODO set colors also will fire this issue.
#if  DEBUG
            Debug.WriteLine("txtInput's value has been changed. " + DateTime.Now + ", txtInput.Text.Length=" + txtInput.Text.Length + ",Rtf.Length=" + txtInput.Rtf.Length);
#endif
            //SetLengthInfo();
        }


        internal void RegexExcuted()
        {
            _regexExcuted = true;
        }

        public void BindModel(PageFile pf)
        {
            txtInput.Text = pf.SourceText;
            sourceBar1.BindModel(pf);
        }

        internal void OpenFileOrUrl(string file)
        {
            sourceBar1.OpenFileOrUrl(file);
        }

        public void SetInput(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                txtInput.Text = input;
                //SetLengthInfo();
                txtInput_SelectionChanged(txtInput, null);
            }
        }

        private void tsmiPutToRegexBox_Click(object sender, EventArgs e)
        {
            string text = txtInput.SelectedText;
            if (text != string.Empty)
            {
                IRegexAnalyst ra = new RegexAnalyst();
                text = ra.ToSimpleRegexString(text);
                regexBar1.SetRegexPattern(text);
            }
        }

        private void SetErrorInfo(string msg)
        {
            //lblFileError.ForeColor = Color.Red;
            //lblFileError.Text = msg;
            UIManager.Current.SetStatusInfo(msg, true);
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                txtInput.Paste(DataFormats.GetFormat(DataFormats.Text));

                e.Handled = true;
            }
        }

        private void txtInput_SelectionChanged(object sender, EventArgs e)
        {
            var textbox = sender as TextBoxBase;
            if (null == textbox) return;

            ToolHelper.TextBoxSelectionChanged(textbox, this.OnRowColumnChangeNotification);
        }

        public void SetUserInterfaceTexts()
        {
            sourceBar1.SetUserInterfaceTexts();
            InitializeContextMenuForSource();
            regexBar1.SetUserInterfaceTexts();
        }
    }
}
