using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexTool.Core
{
    public enum ToolRequirements
    {
        /// <summary>
        /// MATCH
        /// </summary>
        R_01_MATCH,

        /// <summary>
        /// SOURCE
        /// </summary>
        R_01_01_SOURCE,

        R_01_01_01_MENU_CUT,
        R_01_01_02_MENU_COPY,
        R_01_01_03_MENU_COPY_ALL,

        R_01_01_04_MENU_SELECT_ALL,

        R_01_01_05_MENU_PASTE,


        R_01_01_06_MENU_CLEAR,

        R_01_01_07_MENU_WORDWRAP,

        R_01_01_08_MENU_COPY_AS_REGEX,

        R_01_01_09_MENU_PUT_IN_REGEXBOX,

        R_01_01_10_MENU_UNDO,

        /// <summary>
        /// REGEX BAR
        /// </summary>
        R_01_02_REGEX_BAR,

        R_01_02_MENU_CUT,
        R_01_02_MENU_COPY,
        R_01_03_MENU_COPY_ALL,

        R_01_04_MENU_SELECT_ALL,

        R_01_05_MENU_PASTE,
        R_01_06_MENU_CLEAR,

        /// <summary>
        /// options
        /// </summary>
        R_1_02_01_OPTIONS,

        /// <summary>
        /// REPLACEMENT
        /// </summary>
        R_1_03_REPLACEMENT,


    }
}
