using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform.Common
{
    public class ControlHelper
    {
        /// <summary>
        /// 设置将要显示的日期选择框控件位置到指定控件下方
        /// </summary>
        /// <param name="mc">待显示的MonthCalendar控件</param>
        /// <param name="ctrl">显示日期的目标控件</param>
        /// <param name="isSetSelected">是否设置当前选中的日期为控件显示的日期</param>
        public static void SetMonthCalendarPosition(MonthCalendar mc, Control ctrl, bool isSetSelected)
        {
            //当前窗体
            Form frm = ctrl.FindForm();
            //不存在窗体就退出
            if (frm == null) { return; }
            if (isSetSelected)
            {
                //设置焦点
                ctrl.Focus();
                //当前选择的日期为文本框显示的日期
                DateTime dt;
                if (DateTime.TryParse(ctrl.Text, out dt))
                {
                    mc.SelectionStart = dt;
                }
            }
            //将MonthCalendar的父控件设置为窗体本身
            mc.Parent = frm;
            //左边距
            int left = ctrl.Left;
            //顶边距
            int top = ctrl.Top + ctrl.Height;
            //当前父控件
            Control par = ctrl.Parent;
            //遍历各父控件并累加左边距与顶边距
            while (par != frm)
            {
                //累加边距
                left += par.Left;
                top += par.Top;
                //转到上一级父控件
                par = par.Parent;
            }
            //设置边距
            mc.Left = left;
            mc.Top = top;
            //该日期控件置顶
            mc.BringToFront();
            //设置焦点到日期选择控件
            mc.Visible = true;
            mc.Focus();
            //保存当前指定的控件
            mc.Tag = ctrl;
        }
    }
}
