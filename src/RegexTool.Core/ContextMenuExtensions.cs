using RegexTool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegexTool.Core
{
    [Flags]
    public enum TextContextMenuType
    {
        SimpleText = 1,
        WordWrap = 2,
        SaveContent = 4,
        RegexSource = 8,
        RegexTextBox = 16,
        SendToSource = 32,
    }

    public static class ContextMenuExtensions
    {
        public static ToolStripMenuItem CreateEmptyMenuItem(this ContextMenuStrip menu)
        {
            return new ToolStripMenuItem() { Text = "Empty", Enabled = false };
        }
        /// <summary>
        /// find the item by name: menu.Name + "_" + nameByKey.ToString()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menu"></param>
        /// <param name="nameByKey"></param>
        /// <returns></returns>
        public static T FindByName<T>(this ContextMenuStrip menu, FormStringKeys nameByKey) where T : ToolStripItem
        {
            var idx = menu.Items.IndexOfKey(menu.Name + "_" + nameByKey.ToString());
            if (idx >= 0) return menu.Items[idx] as T;
            return null;
        }

        public static void InitTextContextMenuItems(this ContextMenuStrip menu, TextBoxBase textbox, TextContextMenuType cat)
        {
            menu.Tag = textbox;

            menu.Items.AddRange(
                new ToolStripItem[]{
                   new ToolStripMenuItem() {
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_SELECT_ALL),
                       Tag = FormStringKeys.STR_MENU_ITEM_SELECT_ALL.ToString(),
                       Name =  menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_SELECT_ALL.ToString() ,
                   },
                   new ToolStripMenuItem() {
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_CUT),
                       Tag = FormStringKeys.STR_MENU_ITEM_CUT.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_CUT.ToString() ,
                   },
                   new ToolStripMenuItem { 
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_COPY) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_COPY.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_COPY.ToString() ,
                   },
                   new ToolStripMenuItem { 
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_COPY_ALL) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_COPY_ALL.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_COPY_ALL.ToString() ,
                   },
                   new ToolStripMenuItem { 
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_PASTE) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_PASTE.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_PASTE.ToString() ,
                   },
                   new ToolStripMenuItem { 
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_DELETE) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_DELETE.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_DELETE.ToString() ,
                   },
                   new ToolStripMenuItem { 
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_CLEAR) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_CLEAR.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_CLEAR.ToString() ,
                   },
                   new ToolStripMenuItem { 
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_UNDO) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_UNDO.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_UNDO.ToString() ,
                   },
                });

            if ((int)(cat & TextContextMenuType.SaveContent) > 0)
            {
                menu.Items.AddRange(new ToolStripItem[]{
                   new ToolStripSeparator(),
                   new ToolStripMenuItem { 
                       Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_SAVE) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_SAVE.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_SAVE.ToString() ,}});

            }

            if ((int)(cat & TextContextMenuType.WordWrap) > 0)
            {
                menu.Items.AddRange(new ToolStripItem[]{
                   new ToolStripSeparator(),
                   new ToolStripMenuItem { Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_WORDWRAP) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_WORDWRAP.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_WORDWRAP.ToString() , }});
            }

            if ((int)(cat & TextContextMenuType.RegexSource) > 0)
            {
                menu.Items.AddRange(new ToolStripItem[]{
                   new ToolStripSeparator(),
                   new ToolStripMenuItem { Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_COPY_AS_REGEX) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_COPY_AS_REGEX.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_COPY_AS_REGEX.ToString()
                   },
                   new ToolStripMenuItem { Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX.ToString() }
                });
            }

            if ((int)(cat & TextContextMenuType.RegexTextBox) > 0)
            {
                menu.Items.AddRange(new ToolStripItem[]{
                   new ToolStripSeparator(),
                   new ToolStripMenuItem { Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_TO_REGEX_FORMAT) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_TO_REGEX_FORMAT.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_TO_REGEX_FORMAT.ToString()
                   },
                   //new ToolStripMenuItem { Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX) ,
                   //    Tag = FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX.ToString(),
                   //    Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX.ToString() },
                   //new ToolStripSeparator(),
                   //new ToolStripMenuItem { Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY) ,
                   //    Tag = FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY.ToString(),
                   //    Checked = true,
                   //    Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY.ToString() }
                });
            }

            if ((int)(cat & TextContextMenuType.RegexTextBox) > 0
                || (int)(cat & TextContextMenuType.RegexSource) > 0)
            {
                menu.Items.AddRange(new ToolStripItem[]{
                   new ToolStripSeparator(),
                   new ToolStripMenuItem { Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY.ToString(),
                       Checked = true,
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_SELECTION_ONLY.ToString() }
                });
            }

            if ((int)(cat & TextContextMenuType.SendToSource) > 0)
            {
                menu.Items.AddRange(new ToolStripItem[]{
                   new ToolStripSeparator(),
                   new ToolStripMenuItem { Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_SEND_TO_SOURCE) ,
                       Tag = FormStringKeys.STR_MENU_ITEM_SEND_TO_SOURCE.ToString(),
                       Name = menu.Name + "_" +FormStringKeys.STR_MENU_ITEM_SEND_TO_SOURCE.ToString() }
                });
            }
            foreach (ToolStripItem item in menu.Items)
            {
                if (item is ToolStripSeparator == false)
                {
                    if (item.Name.IndexOf(FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX.ToString()) >= 0
                        || item.Name.IndexOf(FormStringKeys.STR_MENU_ITEM_SEND_TO_SOURCE.ToString()) >= 0)
                    {
                        //do nothing, the click event will be attached by a specific method
                    }
                    else
                    {
                        item.Click += (sender, e) => ToolHelper.ProcessTextContextMenu(menu.Tag as TextBoxBase, sender);
                    }
                }
            }

            menu.Opening += TextboxMenu_Opening;
        }

        static void TextboxMenu_Opening(object sender, CancelEventArgs e)
        {
            var menu = sender as ContextMenuStrip;

            if (menu != null)
            {
                var txt = menu.Tag as TextBoxBase;

                if (txt == null) return;

                int idx = -1;
                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_SELECT_ALL.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.TextLength > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_CUT.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.SelectedText.Length > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_COPY.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.SelectedText.Length > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_PASTE.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = Clipboard.ContainsText();

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_SAVE.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.Text.Length > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_WORDWRAP.ToString());
                if (idx >= 0) ((ToolStripMenuItem)menu.Items[idx]).Checked = txt.WordWrap;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_DELETE.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.SelectedText.Length > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_CLEAR.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.Text.Length > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_PUT_INTO_REGEX_BOX.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.SelectedText.Length > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_COPY_AS_REGEX.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.SelectedText.Length > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_SEND_TO_SOURCE.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.Text.Length > 0;

                idx = menu.Items.IndexOfKey(menu.Name + "_" + FormStringKeys.STR_MENU_ITEM_UNDO.ToString());
                if (idx >= 0) menu.Items[idx].Enabled = txt.CanUndo;
            }
        }
    }
}
