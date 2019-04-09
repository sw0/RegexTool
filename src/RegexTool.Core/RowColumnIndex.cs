using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexTool.Core
{
    public struct RowColumnIndex
    {
        public int Row;
        public int Column;
        public int Length;
        public int SelectionLength;

        public override string ToString()
        {
            return string.Format("Line: {0}, Column: {1}, Select len: {2}, len: {3}", Row, Column, SelectionLength, Length);
        }
    }
}
