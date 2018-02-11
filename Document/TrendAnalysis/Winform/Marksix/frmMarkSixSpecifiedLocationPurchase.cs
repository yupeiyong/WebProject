using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrendAnalysis.Models;
using TrendAnalysis.Models.MarkSix;
using TrendAnalysis.Service.MarkSix;

namespace Winform.Marksix
{
    public partial class frmMarkSixSpecifiedLocationPurchase : Form
    {
        public MarkSixSpecifiedLocationPurchase MarkSixSpecifiedLocationPurchase { get; set; }

        private Dictionary<byte, TextBox> _textBoxs = new Dictionary<byte, TextBox>();
        public frmMarkSixSpecifiedLocationPurchase()
        {
            InitializeComponent();
        }

        private void frmMarkSixSpecifiedLocationPurchase_Load(object sender, EventArgs e)
        {
            if (MarkSixSpecifiedLocationPurchase == null)
            {
                return;
            }
            var purchaseNumbers = MarkSixSpecifiedLocationPurchase.Purchases;
            //OnShowDialogEvent(null);
            if (purchaseNumbers != null && purchaseNumbers.Count > 0)
            {
                //按行列设置文本框和标签
                //顶距
                var marginTop = 20;

                //左边距
                var marginLeft = 15;

                //行高
                var rowHeight = 30;

                //列宽
                var columnWidth = 110;

                //每行显示5个
                var columnCountEveryRow = 5;
                var numbers = purchaseNumbers.Keys.ToList();
                for (int i = 0, len = numbers.Count; i < len; i++)
                {
                    var number = numbers[i];
                    var rowIndex = i / columnCountEveryRow;
                    var columnIndex = i % columnCountEveryRow;
                    var yPoint = marginTop + rowIndex * rowHeight;
                    var xPoint = marginLeft + columnIndex * columnWidth;
                    var labelLocation = new Point(xPoint, yPoint + 3);
                    var textboxLocation = new Point(xPoint + 23, yPoint);
                    var label = new Label { Text = number.ToString("00"), Location = labelLocation, Width = 17, Height = 12 };

                    var amount = purchaseNumbers[number];
                    var tb = new TextBox { Location = textboxLocation, Width = 50, Height = 20 ,Text=amount};
                    _textBoxs.Add(number, tb);
                    gbNumberList.Controls.Add(label);
                    gbNumberList.Controls.Add(tb);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (MarkSixSpecifiedLocationPurchase == null)
            {
                return;
            }
            var purchaseNumbers = MarkSixSpecifiedLocationPurchase.Purchases;
            //OnShowDialogEvent(null);
            if (purchaseNumbers != null && purchaseNumbers.Count > 0)
            {
                var totalAmount = 0;
                var numberAmountListString = string.Empty;
                foreach (var number in purchaseNumbers.Keys)
                {
                    //购买金额文本框
                    var tb = _textBoxs[number];
                    var str = tb.Text;
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        continue;
                    }
                    var amount = 0;
                    if (!int.TryParse(tb.Text, out amount))
                    {
                        MessageBox.Show("请选择1-7的号码位置！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tb.Focus();
                        return;
                    }
                    numberAmountListString += string.Format("{0}:{1};", number, amount);
                    totalAmount += amount;
                }
                if (MarkSixSpecifiedLocationPurchase == null)
                {
                    throw new Exception("对象：MarkSixSpecifiedLocationPurchase为空！");
                }
                if (string.IsNullOrWhiteSpace(numberAmountListString))
                {
                    MessageBox.Show("购买清单为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var oddsString = tbOdds.Text;

                var odds = 0;
                if (!int.TryParse(oddsString, out odds) || odds < 1)
                {
                    MessageBox.Show("赔率必须为大于0的整数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MarkSixSpecifiedLocationPurchase.PurchaseAmount = totalAmount;
                MarkSixSpecifiedLocationPurchase.PurchaseList = numberAmountListString;
                MarkSixSpecifiedLocationPurchase.Odds = odds;

                var purchaseService = new MarkSixPurchaseService();
                purchaseService.SaveSpecifiedLocation(MarkSixSpecifiedLocationPurchase);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //public event EventHandler<EventArgs> ShowDialogEvent;
        //protected void OnShowDialogEvent(EventArgs e)
        //{
        //    if (ShowDialogEvent != null)
        //    {
        //        ShowDialogEvent(this, e);
        //    }
        //}
    }
}
