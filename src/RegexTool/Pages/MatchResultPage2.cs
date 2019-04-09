using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RegexTool.Core;
using RegexTool.Properties;
using System.Drawing;

namespace RegexTool.Pages
{
    public partial class MatchResultPage
    {
        protected bool _showCaptures = true;

        public Action<LocationInfo> OnNodeSelected = null;

        public void SetResult(Regex reg, MatchCollection mc)
        {
            if (tvResult.InvokeRequired)
            {
                Action<Regex, MatchCollection> act = SetResult;

                tvResult.Invoke(act, reg, mc);
            }
            else
            {
                if (mc == null)
                {
                    Debug.WriteLine("mc is null");
                    return;
                }

                tvResult.Nodes.Clear();

                try
                {
                    ToggleSearchFilterButtons(true);

                    int idx = 0;

                    var gNames = reg.GetGroupNames();

                    foreach (Match m in mc)
                    {
                        ++idx;

                        if (idx > 1 && idx % ToolBody.MatchMinCountNeedsDoEvents == 0) Application.DoEvents();

                        var nodeMatch = new TreeNode();
                        nodeMatch.Text = string.Format(ResxManager.GetResourceString(FormStringKeys.STR_TREEVIEW_NODE_MATCH), idx, m.Value);
                        tvResult.Nodes.Add(nodeMatch);
                        nodeMatch.Tag = new LocationInfo(m.Value, m.Index, m.Length);
                        nodeMatch.ContextMenuStrip = cmsResult;

                        for (int i = 1; i < m.Groups.Count; i++)
                        {
                            var gName = gNames[i];
                            var g = m.Groups[gName];

                            var nodeGroup = new TreeNode(string.Format(ResxManager.GetResourceString(FormStringKeys.STR_TREEVIEW_NODE_GROUP), gName, g.Value));
                            nodeMatch.Nodes.Add(nodeGroup);
                            nodeGroup.Tag = new LocationInfo(g.Value, g.Index, g.Length);
                            nodeGroup.ContextMenuStrip = cmsResult;
                            if (false == _showCaptures)
                                continue;

                            for (int j = 0; j < g.Captures.Count; j++)
                            {
                                var capture = g.Captures[j];
                                var nodeCapture =
                                    new TreeNode(string.Format(ResxManager.GetResourceString(FormStringKeys.STR_TREEVIEW_NODE_CAPTURE), j, capture.Value));
                                nodeGroup.Nodes.Add(nodeCapture);
                                nodeCapture.Tag = new LocationInfo(capture.Value, capture.Index, capture.Length);
                                nodeCapture.ContextMenuStrip = cmsResult;
                            }
                        }
                    }

                    lblMatchInfo.Text = string.Format(ResxManager.GetResourceString(FormStringKeys.STR_MATCH_INFO), mc.Count, string.Empty);
                }
                catch (Exception)
                {
#if DEBUG
                    throw;
#endif
                }
                finally
                {
                    ToggleSearchFilterButtons(true);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var matchWhole = cbWhole.Checked;
            var regexMode = cbRegexMode.Checked;
            var filter = txtFilter.Text;

            if (txtFilter.Text == string.Empty)
            {
                tvResult.CollapseAll();
                foreach (TreeNode node in tvResult.Nodes)
                {
                    //Reset the backcolor for node if it was highlighted.
                    HighlightNode(node, false);
                }
                return;
            }

            Regex reg = null;
            try
            {
                if (regexMode)
                    reg = new Regex(filter, RegexOptions.Compiled);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Regular Expression invlid");
                return;
            }

            if (tvResult.Nodes.Count == 0) return;

            tvResult.CollapseAll();

            TreeNode firstFound = null;
            int foundItemsNumber = 0;

            foreach (TreeNode node in tvResult.Nodes)
            {
                node.BackColor = Color.White;

                IEnumerable<TreeNode> items = null;

                if (regexMode)
                {
                    items = SearchNodes(node, reg);
                }
                else
                {
                    items = SearchNodes(node, filter, matchWhole);
                }

                if (firstFound == null && items.Any()) firstFound = items.First();

                foundItemsNumber += items.Count();

                foreach (var treeNode in items)
                {
                    treeNode.BackColor = Color.LightBlue;
                    treeNode.Expand();
                }
            }

            lblMatchInfo.Text = string.Format(STR_MATCH_INFO_WITH_FILTER, tvResult.Nodes.Count, foundItemsNumber);
            //if (firstFound != null) tvResult.Select();
        }

        private IEnumerable<TreeNode> SearchNodes(TreeNode node, Regex reg)
        {
            if (node == null) yield break;

            if (node.Nodes != null && node.Nodes.Count > 0)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    var items = SearchNodes(childNode, reg);

                    foreach (var treeNode in items)
                    {
                        yield return treeNode;
                    }
                }
            }

            if (node.Tag != null)
            {
                var txt = ((LocationInfo)node.Tag).Text;
                if (reg.IsMatch(txt))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<TreeNode> SearchNodes(TreeNode node, string data, bool matchWhole)
        {
            if (node == null) yield break;

            if (node.Nodes != null && node.Nodes.Count > 0)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    var items = SearchNodes(childNode, data, matchWhole);

                    foreach (var treeNode in items)
                    {
                        yield return treeNode;
                    }
                }
            }

            if (node.Tag != null)
            {
                var txt = ((LocationInfo)node.Tag).Text;

                if (matchWhole)
                {
                    if (txt.Equals(data, StringComparison.OrdinalIgnoreCase))
                        yield return node;
                }
                else
                {
                    if (txt.IndexOf(data, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        yield return node;
                    }
                }
            }
        }

        public void tvResult_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (OnNodeSelected == null) return;

            //var tv = sender as TreeView;
            //if (tv == null)
            //    return;

            //var node = tv.SelectedNode as TreeNode;

            //if(node ==null)
            //{
            //    throw new Exception("sender is not TreeNode");
            //}

            var li = e.Node.Tag as LocationInfo;

            if (li == null)
                return;

            OnNodeSelected(li);
        }

        private void ToggleSearchFilterButtons(bool isActive)
        {
            btnSearch.Enabled = btnClear.Enabled =
                btnExpend.Enabled = btnCollapse.Enabled = isActive;
        }

        public void BindModel(PageFile pf)
        {

        }

    }
}
