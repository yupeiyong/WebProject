using System;
using System.Windows.Forms;
using TrendAnalysis.DataTransferObject;
using Common;
using System.Data;
using System.Linq;
using TrendAnalysis.Models;
using System.Drawing;
using System.ComponentModel;
using TrendAnalysis.Models.MarkSix;
using Winform.Common;
using TrendAnalysis.Service.MarkSix;

namespace Winform.Marksix
{
    public partial class frmMarksixRecord : Form
    {
        /// <summary>
        /// 导入的记录条数
        /// </summary>
        private int importCount = 0;


        /// <summary>
        /// 记录总条数
        /// </summary>
        private int recordCount = 0;


        /// <summary>
        /// 已导入条数
        /// </summary>
        private int importedCount = 0;
        /// <summary>
        /// 是否停止导入记录
        /// </summary>
        private bool isStopImport = false;
        /// <summary>
        /// 引用主窗口
        /// </summary>
        private frmMain frmMdi = null;
        /// <summary>
        /// 导入趋势分析后台对象
        /// </summary>
        private BackgroundWorker bgwImport = null;

        private bool enableEvent = false;

        public frmMarksixRecord()
        {
            InitializeComponent();
            monthCalendar.LostFocus += MonthCalendar_LostFocus;
        }

        private void frmMarksixRecord_Load(object sender, EventArgs e)
        {
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
            tsbSearch_Click(null, e);
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            Search();
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
            var startIndex = pageSize*(pageIndex-1);
            if (startIndex < 0) startIndex = 0;

            var searchDto = new MarkSixRecordSearchDto { StartIndex = startIndex, PageSize = pageSize };
            if (tbStartDateTime.Text.Trim().Length > 0)
            {
                var strDate = tbStartDateTime.Text.Trim();
                DateTime dt;
                if(DateTime.TryParse(strDate,out dt))
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

            searchDto.Times = tbTimes.Text;
            var service = new MarkSixRecordService();
            try
            {
                var rows = service.Search(searchDto);
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
                    return null;
                }
                frmMdi.tsslInfo.Text = "查询完成！";
                frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
                var table = rows.ConvertDataTable(properties =>
                {
                    var rowVersionProperty = properties.FirstOrDefault(p => p.Name == nameof(MarkSixRecord.RowVersion));
                    if (rowVersionProperty != null)
                    {
                        properties.Remove(rowVersionProperty);
                    }
                    var idProperty = properties.FirstOrDefault(p => p.Name == nameof(MarkSixRecord.Id));
                    if (idProperty != null)
                    {
                        properties.Remove(idProperty);
                    }
                    properties.Insert(0, idProperty);
                });
                dgvMarksixRecordList.DataSource = table;

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
            catch(Exception ex)
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
        private void tsbImport_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确认导入记录数据吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //文件选择窗口
            OpenFileDialog ofd = new OpenFileDialog();
            //筛选条件
            ofd.Filter = "Excel2007(*.xlsx)|*.xlsx";
            //文件名
            string fileName = string.Empty;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                fileName = ofd.FileName;
            }
            if (fileName.Length > 0)
            {
                if (bgwImport == null)
                {
                    bgwImport = new BackgroundWorker();
                    //记录完成进度
                    bgwImport.WorkerReportsProgress = true;
                    //支持取消任务
                    bgwImport.WorkerSupportsCancellation = true;
                    //绑定事件
                    bgwImport.DoWork += new DoWorkEventHandler(bgwImport_DoWork);
                    bgwImport.ProgressChanged += new ProgressChangedEventHandler(bgwImport_ProgressChanged);
                    bgwImport.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwImport_RunWorkerCompleted);
                }
                //设置工具条和菜单状态
                foreach (ToolStripItem item in this.msMaster.Items)
                {
                    item.Enabled = false;
                }
                //tsmStopImport.Visible = true;
                //tsmStopImport.Enabled = true;
                foreach (ToolStripItem item in tslMaster.Items)
                {
                    item.Enabled = false;
                }
                tsbStopImport.Visible = true;
                tsbStopImport.Enabled = true;
                frmMdi.tspbCompletePrecent.Visible = true;
                frmMdi.tspbCompletePrecent.Value = 0;
                frmMdi.tsslInfo.Text = "正在导入记录";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
                //执行异步获取记录列表
                bgwImport.RunWorkerAsync(fileName);
            }
        }

        #region "处理导入记录后台事件"
        void bgwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isStopImport)
            {
                MessageBox.Show(
                    "您取消了导入记录！",
                    "取消",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                    );
                frmMdi.tsslInfo.Text = "取消了导入记录";
                frmMdi.tsslInfo.BackColor = Color.Yellow;
            }
            else
            {
                if (importCount > 0)
                {
                    MessageBox.Show(
                        string.Format("成功导入{0}条记录", importCount),
                        "成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.None
                        );
                    frmMdi.tsslInfo.Text = "导入成功";
                    frmMdi.tsslInfo.BackColor = frmMdi.BackColor;
                }
                else
                {
                    MessageBox.Show(
                        string.Format("导入了{0}条记录", importCount),
                        "失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    frmMdi.tsslInfo.Text = string.Format("导入了{0}条记录", importCount);
                    frmMdi.tsslInfo.BackColor = Color.Yellow;
                }
            }
            //设置工具条和菜单状态
            foreach (ToolStripItem item in this.msMaster.Items)
            {
                item.Enabled = true;
            }
            //tsmStopImport.Visible = false;
            foreach (ToolStripItem item in tslMaster.Items)
            {
                item.Enabled = true;
            }
            tsbStopImport.Visible = false;
            try
            {
                frmMdi.tspbCompletePrecent.Visible = false;
            }
            catch { }
        }

        void bgwImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            frmMdi.tspbCompletePrecent.Value = e.ProgressPercentage;
            frmMdi.tsslInfo.Text =string.Format( "导入 {0}/{1}", importedCount, recordCount);
        }

        void bgwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = e.Argument.ToString();
            var service = new MarkSixRecordService();
            service.BeforeImportEvent += Service_BeforeImportEvent;
            service.ImportingEvent += Service_ImportingEvent;
            try
            {
               importCount= service.Import(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入记录时错误，" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bgwImport.CancelAsync();
            }
        }

        private void Service_BeforeImportEvent(object sender, EventArgs e)
        {
            var recordService = sender as MarkSixRecordService;
            if (recordService != null)
            {
                if (bgwImport.CancellationPending == true)
                {
                    recordService.IsStopImporting = true;
                    return;
                }
            }
            //frmMdi.tspbCompletePrecent.Maximum = recordService.RecordCount;
            this.recordCount = recordService.RecordCount;
        }

        private void Service_ImportingEvent(object sender, EventArgs e)
        {
            var  recordService = sender as MarkSixRecordService;
            if (recordService != null)
            {
                if (bgwImport.CancellationPending == true)
                {
                    recordService.IsStopImporting = true;
                    return;
                }
            }
            int value = recordService.ImportedCount;
            importedCount = value;
            int max = frmMdi.tspbCompletePrecent.Maximum;
            if (value > max)
            {
                value = value % max;
            }
            bgwImport.ReportProgress(value);
        }

        #endregion

        private void tsbExport_Click(object sender, EventArgs e)
        {
            using(var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Execl 2007files(*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true; //保存对话框是否记忆上次打开的目录 
                                                        //saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                saveFileDialog.FileName = "MarksixRecord.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = saveFileDialog.FileName;
                    var table = Search();
                    if (table != null)
                    {
                        var service = new MarkSixRecordService();
                        service.Export(table,fileName);
                        frmMdi.tsslInfo.Text = "导出成功！";
                        frmMdi.tsslInfo.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Close();
            this.Dispose();
        }

        private void tsbStopImport_Click(object sender, EventArgs e)
        {
            bgwImport.CancelAsync();
            isStopImport = true;
        }

        private void dgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ////自动调整列宽
            //dgvList.AutoResizeColumns();
            //设置ID列只读
            dgvMarksixRecordList.Columns[nameof(MarkSixRecord.Id)].ReadOnly = true;
            //设置日期列的显示格式
            dgvMarksixRecordList.Columns["开奖日期"].DefaultCellStyle.Format = "yyyy-MM-dd";

        }

        private void dgvList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0 || rowIndex > dgvMarksixRecordList.Rows.Count) { return; }
            dgvMarksixRecordList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;

        }

        private void dgvList_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0 || rowIndex > dgvMarksixRecordList.Rows.Count) { return; }
            dgvMarksixRecordList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
        }

        private void bdnMoveFirstItem_Click(object sender, EventArgs e)
        {
            //页码开始数
            var pageIndex = 0;
            int.TryParse(bdnPositionItem.Text, out pageIndex);
            if (pageIndex > 1)
            {
                pageIndex=1;
                bdnPositionItem.Text = pageIndex.ToString();
                Search();
            }
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
        private void OnlyNumberControl_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
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
            if (pageIndex <pageCount)
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
                pageIndex=pageCount;
                bdnPositionItem.Text = pageIndex.ToString();
            }
        }
        private void bdnPositionItem_TextChanged(object sender, EventArgs e)
        {
            if (!enableEvent) return;
            Search();
        }


        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            var mc = sender as MonthCalendar;
            if (mc == null)
                return;
            var tb = mc.Tag as Control;
            if (tb != null)
            {
                tb.Text=mc.SelectionStart.ToString("yyyy-MM-dd");
                tb.Focus();
            }
        }
        private void MonthCalendar_LostFocus(object sender, EventArgs e)
        {
            //失去焦点就隐藏日期选择控件
            monthCalendar.Visible = false;
        }

        private void TextBox_Enter(object sender,EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                //var len = tb.Text.Length;
                //tb.SelectionLength = len;
                tb.Focus();
                tb.SelectAll();
            }
        }

        private void btnEndDateTime_Click(object sender, EventArgs e)
        {
            //日期选择
            ControlHelper.SetMonthCalendarPosition(monthCalendar, tbEndDateTime, true);
        }

        private void btnStartDateTime_Click(object sender, EventArgs e)
        {
            //日期选择
            ControlHelper.SetMonthCalendarPosition(monthCalendar, tbStartDateTime, true);
        }
    }
}
