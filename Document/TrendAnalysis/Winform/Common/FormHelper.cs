using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform.Common
{
    public class FormHelper
    {
        /// <summary>
        /// 打开指定子窗口
        /// </summary>
        /// <typeparam name="T">窗体类型</typeparam>
        /// <param name="mdiFrm">当前MDI父窗口</param>
        /// <returns></returns>
        public static Form OpenForm<T>(Form mdiFrm) where T : Form, new()
        {
            //mdi窗口的子窗体
            T sonFrm = null;
            foreach (Form frm in mdiFrm.MdiChildren)
            {
                if (frm is T)
                {
                    sonFrm = (T)frm;
                    break;
                }
            }
            if (sonFrm == null)
            {
                sonFrm = new T();
            }
            //指定MDI父窗体
            sonFrm.MdiParent = mdiFrm;
            sonFrm.Show();
            //窗口最大化
            sonFrm.WindowState = FormWindowState.Maximized;
            return sonFrm;
        }
    }
}
