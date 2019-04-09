using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegexTool.ReplaceTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            LoadModules();

            this.Width = 1280;
            this.Height = 800;
        }

        private void LoadModules()
        {
            //ReplacePage rp = new ReplacePage();
            //rp.Dock = DockStyle.Fill;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
