using Common;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models;
using TrendAnalysis.Models.MarkSix;
using TrendAnalysis.Service.MarkSix;
using Winform.Common;

namespace Winform.Marksix
{
    public partial class frmMarkSixSpecifiedLocationPurchaseRecord : Form
    {
        /// <summary>
        /// 引用主窗口
        /// </summary>
        private frmMain frmMdi = null;


        /// <summary>
        /// 事件是否可用，可用方可执行查询
        /// </summary>
        private bool enableEvent = false;

        public frmMarkSixSpecifiedLocationPurchaseRecord()
        {
            InitializeComponent();
        }
        private void frmMarkSixSpecifiedLocationPurchaseRecord_Load(object sender, EventArgs e)
        {
            monthCalendar.LostFocus += MonthCalendar_LostFocus;
            //取得MDI窗体的引用
            frmMdi = this.MdiParent as frmMain;
            //设置默认页数
            if (tlscombo.Items.Count > 1)
            {
                tlscombo.SelectedIndex = 1;
            }
            bdnPositionItem.Enabled = true;
            bdnPositionItem.Text = "1";
            enableEvent = true;
            cboLocation.Items.Add(new ComboBoxItem<int> { Text = "全部", Value = 0 });
            for (var i = 1; i <= 7; i++)
            {
                cboLocation.Items.Add(new ComboBoxItem<int> { Text = i.ToString(), Value = i });
            }
            cboLocation.SelectedIndex = 0;
            tsbSearch_Click(null, e);

        }

        private DataTable Search()
        {
            //每页数量
            var pageSize = 0;
            int.TryParse(tlscombo.Text, out pageSize);
            //页码开始数
            var pageIndex = 1;
            int.TryParse(bdnPositionItem.Text, out pageIndex);
            //开始位置
            var startIndex = pageSize * (pageIndex - 1);
            if (startIndex < 0) startIndex = 0;

            var searchDto = new MarkSixSpecifiedLocationPurchaseSearchDto { StartIndex = startIndex, PageSize = pageSize };
            if (tbStartDateTime.Text.Trim().Length > 0)
            {
                var strDate = tbStartDateTime.Text.Trim();
                DateTime dt;
                if (DateTime.TryParse(strDate, out dt))
                {
                    searchDto.StartDateTime = dt;
                }
            }
            if (tbEndDateTime.Text.Trim().Length > 0)
            {
                var strDate = tbEndDateTime.Text.Trim();
                DateTime dt;
                if (DateTime.TryParse(strDate, out dt))
                {
                    searchDto.EndDateTime = dt;
                }
            }
            var selectedLocation = cboLocation.SelectedItem as ComboBoxItem<int>;
            if (selectedLocation != null)
            {
                searchDto.Location = selectedLocation.Value;
            }

            searchDto.Times = tbTimes.Text;
            var service = new MarkSixPurchaseService();
            try
            {
                var rows = service.SearchSpecifiedLocation(searchDto);
                if (rows.Count == 0)
                {
                    MessageBox.Show(
                        "没有找到符合条件的数据！",
                        "失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    frmMdi.tsslInfo.Text = "查询内容为空！";
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                    //重新绑定数据
                    var sourceTable = dgvMarksixPurchaseRecordList.DataSource as DataTable;
                    if (sourceTable != null)
                    {
                        sourceTable.Rows.Clear();
                        dgvMarksixPurchaseRecordList.DataSource = sourceTable;
                    }
                    return null;
                }
                frmMdi.tsslInfo.Text = "查询完成！";
                frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
                var table = rows.ConvertDataTable(properties =>
                {
                    var rowVersionProperty = properties.FirstOrDefault(p => p.Name == nameof(MarkSixSpecifiedLocationPurchase.RowVersion));
                    if (rowVersionProperty != null)
                    {
                        properties.Remove(rowVersionProperty);
                    }
                    var purchasesProperty = properties.FirstOrDefault(p => p.Name == nameof(MarkSixSpecifiedLocationPurchase.Purchases));
                    if (purchasesProperty != null)
                    {
                        properties.Remove(purchasesProperty);
                    }
                    var idProperty = properties.FirstOrDefault(p => p.Name == nameof(MarkSixSpecifiedLocationPurchase.Id));
                    if (idProperty != null)
                    {
                        properties.Remove(idProperty);
                    }
                    //Id列移动到首位
                    properties.Insert(0, idProperty);
                });
                dgvMarksixPurchaseRecordList.DataSource = table;

                var pageCount = searchDto.PageCount;
                bdnCountItem.Text = pageCount.ToString();

                if (pageIndex <= 1)
                {
                    bdnMoveFirstItem.Enabled = false;
                    bdnMovePreviousItem.Enabled = false;
                }
                else
                {
                    bdnMoveFirstItem.Enabled = true;
                    bdnMovePreviousItem.Enabled = true;
                }

                if (pageIndex == pageCount)
                {
                    bdnMoveNextItem.Enabled = false;
                    bdnMoveLastItem.Enabled = false;
                }
                else
                {
                    bdnMoveNextItem.Enabled = true;
                    bdnMoveLastItem.Enabled = true;
                }
                return table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "查询发生错误，" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                frmMdi.tsslInfo.Text = "查询失败！";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
                return null;
            }

        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void dgvMarksixPurchaseRecordList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //设置ID列只读
            dgvMarksixPurchaseRecordList.Columns[nameof(MarkSixRecord.Id)].ReadOnly = true;
            //设置日期列的显示格式
            dgvMarksixPurchaseRecordList.Columns["录入时间"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvMarksixPurchaseRecordList.Columns["修改时间"].DefaultCellStyle.Format = "yyyy-MM-dd";

            var name = "actionButtonColumn";
            var column = new DataGridViewActionButtonColumn { Name = name, Width = 130 };
            if (!dgvMarksixPurchaseRecordList.Columns.Contains(name))
            {
                dgvMarksixPurchaseRecordList.Columns.Add(column);
            }
        }

        private void dgvMarksixPurchaseRecordList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //用户单击DataGridView“操作”列中的“修改”按钮。
            if (DataGridViewActionButtonCell.IsModifyButtonClick(sender, e))
            {
                string id = dgvMarksixPurchaseRecordList[nameof(MarkSixSpecifiedLocationPurchase.Id), e.RowIndex].Value.ToString(); // 获取所要修改关联对象的主键。
                PurchaseUpdate(id);
            }

            //用户单击DataGridView“操作”列中的“删除”按钮。
            if (DataGridViewActionButtonCell.IsDeleteButtonClick(sender, e))
            {
                string id = dgvMarksixPurchaseRecordList[nameof(MarkSixSpecifiedLocationPurchase.Id), e.RowIndex].Value.ToString(); // 获取所要删除关联对象的主键。
                PurchaseDelete(id);
            }
        }

        private void PurchaseUpdate(string id)
        {
            var service = new MarkSixPurchaseService();
            try
            {
                var recordId = long.Parse(id);

                var frm = new frmMarkSixSpecifiedLocationPurchase();
                var purchase = service.GetSpecifiedLocationPurchaseById(recordId);
                if (purchase == null)
                    throw new Exception(string.Format("错误，购买记录不存在！(Id:{0})", id));
                frm.MarkSixSpecifiedLocationPurchase = purchase;
                frm.Text = string.Format("编辑 第{0}期第{0}位 购买记录", purchase.Times, purchase.Location);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Search();
                    frmMdi.tsslInfo.Text = "修改成功！";
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                }
                else
                {
                    frmMdi.tsslInfo.Text = "取消修改";
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("修改Id:{0}的购买记录失败！{1}", id, ex.Message), "修改失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmMdi.tsslInfo.Text = "修改失败！";
                frmMdi.tsslInfo.BackColor = Color.Red;
            }
        }

        private void PurchaseDelete(string id)
        {
            if (MessageBox.Show($"你确定删除第{id}条记录吗？", "确认删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }
            var service = new MarkSixPurchaseService();
            try
            {
                var recordId = long.Parse(id);
                service.RemoveSpecifiedLocation(recordId);
                Search();
                frmMdi.tsslInfo.Text = "删除成功！";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("删除Id:{0}的购买记录失败！{1}", id, ex.Message), "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmMdi.tsslInfo.Text = "删除失败！";
                frmMdi.tsslInfo.BackColor = Color.Red;
            }
        }
        private void bdnMoveFirstItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);
            if (pageIndex > 1)
            {
                pageIndex = 1;
                bdnPositionItem.Text = pageIndex.ToString();
                Search();
            }
        }



        private void bdnMovePreviousItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);
            if (pageIndex > 1)
            {
                pageIndex--;
                bdnPositionItem.Text = pageIndex.ToString();
            }
        }

        private void bdnMoveNextItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);

            var pageCount = 0;
            int.TryParse(bdnCountItem.Text, out pageCount);
            if (pageIndex < pageCount)
            {
                pageIndex++;
                bdnPositionItem.Text = pageIndex.ToString();
            }
        }
        private void bdnMoveLastItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);

            var pageCount = 0;
            int.TryParse(bdnCountItem.Text, out pageCount);
            if (pageIndex < pageCount)
            {
                pageIndex = pageCount;
                bdnPositionItem.Text = pageIndex.ToString();
            }
        }
        private void bdnPositionItem_TextChanged(object sender, EventArgs e)
        {
            if (!enableEvent) return;
            Search();
        }
        private void tlscombo_TextChanged(object sender, EventArgs e)
        {
            if (!enableEvent) return;
            enableEvent = false;
            bdnPositionItem.Text = "1";
            Search();
            enableEvent = true;
        }

        /// <summary>
        /// 只允许输入数字的控件的输入键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnlyNumberControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void btnStartDateTime_Click(object sender, EventArgs e)
        {
            //日期选择
            ControlHelper.SetMonthCalendarPosition(monthCalendar, tbStartDateTime, true);
        }

        private void btnEndDateTime_Click(object sender, EventArgs e)
        {
            //日期选择
            ControlHelper.SetMonthCalendarPosition(monthCalendar, tbEndDateTime, true);
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            var mc = sender as MonthCalendar;
            if (mc == null)
                return;
            var tb = mc.Tag as Control;
            if (tb != null)
            {
                tb.Text = mc.SelectionStart.ToString("yyyy-MM-dd");
                tb.Focus();
            }
        }
        private void MonthCalendar_LostFocus(object sender, EventArgs e)
        {
            //失去焦点就隐藏日期选择控件
            monthCalendar.Visible = false;
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Close();
            this.Dispose();
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Execl 2007files(*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true; //保存对话框是否记忆上次打开的目录 
                                                        //saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                saveFileDialog.FileName = "MarksixPurchaseRecord.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = saveFileDialog.FileName;
                    var table = Search();
                    if (table != null)
                    {
                        var service = new MarkSixPurchaseService();
                        service.Export(table, fileName);
                        frmMdi.tsslInfo.Text = "导出成功！";
                        frmMdi.tsslInfo.BackColor = Color.Yellow;
                    }
                }
            }
        }
    }
}
