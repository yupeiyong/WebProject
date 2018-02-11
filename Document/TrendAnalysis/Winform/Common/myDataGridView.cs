using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Winform.Common
{
    public partial class myDataGridView : DataGridView
    {
        public myDataGridView()
        {
            //设置双缓冲减少控件闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint|ControlStyles.OptimizedDoubleBuffer, true);
        }
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);
            //当前行的矩形位置
            Rectangle rec1 = e.RowBounds;
            //获取写入行号的矩形位置
            Rectangle rec = new Rectangle(rec1.Location.X, rec1.Location.Y, this.RowHeadersWidth - 4, rec1.Height);
            //画布
            Graphics g = e.Graphics;
            //行号
            string rowNum = (e.RowIndex + 1).ToString();
            //字体
            Font f = this.RowHeadersDefaultCellStyle.Font;
            //字体颜色
            Color c = this.RowHeadersDefaultCellStyle.ForeColor;
            //对齐方式为垂直居中+靠右
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
            //画上行号
            TextRenderer.DrawText(g, rowNum, f, rec, c, flags);
        }
    }
}
