using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrendAnalysis.Models;
using TrendAnalysis.Models.MarkSix;
using TrendAnalysis.Service;
using Winform.Common;

namespace Winform.Marksix
{
    public partial class frmMarkSixSpecifiedLocationAnalysis : Form
    {
        /// <summary>
        /// 引用主窗口
        /// </summary>
        private frmMain frmMdi = null;

        public frmMarkSixSpecifiedLocationAnalysis()
        {
            InitializeComponent();
        }
        private void frmMarkSixSpecifiedLocationAnalysis_Load(object sender, EventArgs e)
        {
            frmMdi = ParentForm as frmMain;
        }
        private void btnAnalysis_Click(object sender, EventArgs e)
        {

        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            var frm = new frmMarkSixSpecifiedLocationPurchase();

            byte location = 1;
            if (!byte.TryParse(cboNumberLocation.Text, out location) || location < 1 || location > 7)
            {
                MessageBox.Show("请选择1-7的号码位置！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboNumberLocation.Focus();
                return;
            }
            var times = cboTimes.Text;
            if (string.IsNullOrWhiteSpace(times))
            {
                MessageBox.Show("期次不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboTimes.Focus();
                return;
            }

            var numbers = new byte[] { 3, 6, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 88, 99 }.ToList();
            var purchasesString = string.Join(";", numbers.Select(a => a.ToString() + ":"));

            var purchase = new MarkSixSpecifiedLocationPurchase { Times = times, Location = location ,PurchaseList=purchasesString};

            frm.MarkSixSpecifiedLocationPurchase = purchase;
            frm.Text = string.Format("第{0}期第{0}位 号码清单", times, location);
            try
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    frmMdi.tsslInfo.Text = "保存购买成功！";
                    frmMdi.tsslInfo.BackColor = Color.Green;
                    var frmPurchaseRecord = FormHelper.OpenForm<frmMarkSixSpecifiedLocationPurchaseRecord>(frmMdi);
                }
                else
                {
                    frmMdi.tsslInfo.Text = "取消购买";
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("生成购买时，发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmMdi.tsslInfo.Text = "购买失败";
                frmMdi.tsslInfo.BackColor = Color.Red;
                return;
            }
        }


        private void btnAnalysisBefore_Click(object sender, EventArgs e)
        {

        }

    }
}
