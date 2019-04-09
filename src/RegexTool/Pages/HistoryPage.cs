using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegexTool.Pages
{
    public partial class HistoryPage : UserControl
    {
        public HistoryPage()
        {
            InitializeComponent();

            var a = new List<string>
                        {
                            "abc sdf",
                            "Continuous integration capabilities in Visual Studio 2012 facilitate the incremental development process, ensure integration from the start of a project, and discover bugs earlier in the process. Implement review processes to ensure quality. Enhanced code review workflows facilitate the collaboration among developers, providing a rich environment to review the code and propose changes. ",
                            "Embrace an integrated test approach and engage the entire team by connecting Test Manager with Team Foundation Server. Manage test requirements, automate the testing process, perform Manual and Exploratory testing, and provide all the information that developers need to reproduce and solve errors.",
                            "Full traceability and visibility into the progress and quality of each requirement, every step of the way, means you can always tie the team’s work items and test cases to business goals.",
                        };

            a.ForEach(
                s =>
                    //lboxHistory.Items.Add(s)
                    lboxHistory.Items.Add(new Label() { Text = s })
                );
        }

        private void lboxHistory_DragDrop(object sender, DragEventArgs e)
        {
            var x = e.Data.GetData(DataFormats.Text) as string;

            if (!string.IsNullOrEmpty(x))
            {
                Label lbl = new Label();
                lbl.Text = x;

                lboxHistory.Items.Add(lbl);
            }
        }

        private void lboxHistory_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.Text) ?
                DragDropEffects.Copy : DragDropEffects.None;
        }
    }
}
