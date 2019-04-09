using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RegexTool.Core;

namespace RegexTool.Pages
{
    public partial class ReplacePage
    {
        public string Replacement
        {
            get
            {
                return this.txtReplacement.Text;
            }
        }
        public string ReplaceResult
        {
            get
            {
                return this.txtReplaceResult.Text;
            }
        }

        public bool AllowEmptyReplacement
        {
            get { return cbAllowEmpty.Checked; }
        }
        
        public void BindModel(PageFile pf)
        {
            txtReplacement.Text = pf.Replacement;
            txtReplaceResult.Text = pf.ReplacementResult;
            cbAllowEmpty.Checked = pf.AllowEmptyReplacement;
        }
        
        public string GetText()
        {
            return txtReplaceResult.Text;
        }
    }
}
