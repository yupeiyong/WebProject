namespace Winform.Marksix
{
    partial class frmMarksixRecord
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMarksixRecord));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tslMaster = new System.Windows.Forms.ToolStrip();
            this.tsbSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbStopImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.msMaster = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStopImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEndDateTime = new System.Windows.Forms.Button();
            this.btnStartDateTime = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbFirstNum = new System.Windows.Forms.TextBox();
            this.tbSecondNum = new System.Windows.Forms.TextBox();
            this.tbFourthNum = new System.Windows.Forms.TextBox();
            this.tbFifthNum = new System.Windows.Forms.TextBox();
            this.tbSixthNum = new System.Windows.Forms.TextBox();
            this.tbSeventhNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbStartDateTime = new System.Windows.Forms.TextBox();
            this.tbTimes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbEndDateTime = new System.Windows.Forms.TextBox();
            this.tbThirdNum = new System.Windows.Forms.TextBox();
            this.bdnMarksixRecord = new System.Windows.Forms.BindingNavigator(this.components);
            this.bdnCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bdnMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bdnMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bdnPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bdnMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bdnMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tlscombo = new System.Windows.Forms.ToolStripComboBox();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.dgvMarksixRecordList = new Winform.Common.myDataGridView();
            this.tslMaster.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.msMaster.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdnMarksixRecord)).BeginInit();
            this.bdnMarksixRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarksixRecordList)).BeginInit();
            this.SuspendLayout();
            // 
            // tslMaster
            // 
            this.tslMaster.BackColor = System.Drawing.Color.Transparent;
            this.tslMaster.Dock = System.Windows.Forms.DockStyle.None;
            this.tslMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tslMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSearch,
            this.tsbImport,
            this.tsbStopImport,
            this.tsbExport,
            this.tsbExit});
            this.tslMaster.Location = new System.Drawing.Point(9, 105);
            this.tslMaster.Name = "tslMaster";
            this.tslMaster.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tslMaster.Size = new System.Drawing.Size(341, 28);
            this.tslMaster.TabIndex = 2;
            this.tslMaster.Text = "toolStrip1";
            // 
            // tsbSearch
            // 
            this.tsbSearch.Image = global::Winform.Properties.Resources.search;
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsbSearch.Size = new System.Drawing.Size(82, 28);
            this.tsbSearch.Text = "查询";
            this.tsbSearch.ToolTipText = "按F7可以执行查询\r\n数字支持>、<、=、>=、<=、<>";
            this.tsbSearch.Click += new System.EventHandler(this.tsbSearch_Click);
            // 
            // tsbImport
            // 
            this.tsbImport.Image = global::Winform.Properties.Resources.导入;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsbImport.Size = new System.Drawing.Size(82, 28);
            this.tsbImport.Text = "导入";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbStopImport
            // 
            this.tsbStopImport.Image = global::Winform.Properties.Resources.停止small;
            this.tsbStopImport.Name = "tsbStopImport";
            this.tsbStopImport.Size = new System.Drawing.Size(154, 28);
            this.tsbStopImport.Text = "停止导入记录";
            this.tsbStopImport.Visible = false;
            this.tsbStopImport.Click += new System.EventHandler(this.tsbStopImport_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::Winform.Properties.Resources.excel;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsbExport.Size = new System.Drawing.Size(82, 28);
            this.tsbExport.Text = "导出";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = global::Winform.Properties.Resources.离开;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsbExit.Size = new System.Drawing.Size(82, 28);
            this.tsbExit.Text = "退出";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.msMaster);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1382, 101);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // msMaster
            // 
            this.msMaster.BackColor = System.Drawing.Color.Transparent;
            this.msMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.msMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.msMaster.Location = new System.Drawing.Point(4, 25);
            this.msMaster.Name = "msMaster";
            this.msMaster.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.msMaster.Size = new System.Drawing.Size(1374, 34);
            this.msMaster.TabIndex = 9;
            this.msMaster.Text = "menuStrip1";
            this.msMaster.Visible = false;
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAdd,
            this.tsmSave,
            this.tsmImport,
            this.tsmStopImport,
            this.tsmExport,
            this.tsmSearch,
            this.tsmExit});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(58, 28);
            this.文件ToolStripMenuItem.Text = "操作";
            // 
            // tsmAdd
            // 
            this.tsmAdd.Name = "tsmAdd";
            this.tsmAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmAdd.Size = new System.Drawing.Size(225, 30);
            this.tsmAdd.Text = "新增(&N)";
            // 
            // tsmSave
            // 
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(225, 30);
            this.tsmSave.Text = "保存(&S)";
            // 
            // tsmImport
            // 
            this.tsmImport.Name = "tsmImport";
            this.tsmImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsmImport.Size = new System.Drawing.Size(225, 30);
            this.tsmImport.Text = "导入(&I)";
            // 
            // tsmStopImport
            // 
            this.tsmStopImport.Name = "tsmStopImport";
            this.tsmStopImport.Size = new System.Drawing.Size(225, 30);
            this.tsmStopImport.Text = "停止导入记录";
            this.tsmStopImport.Visible = false;
            // 
            // tsmExport
            // 
            this.tsmExport.Name = "tsmExport";
            this.tsmExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmExport.Size = new System.Drawing.Size(225, 30);
            this.tsmExport.Text = "导出(&E)";
            // 
            // tsmSearch
            // 
            this.tsmSearch.Name = "tsmSearch";
            this.tsmSearch.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmSearch.Size = new System.Drawing.Size(225, 30);
            this.tsmSearch.Text = "查询(&C)";
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsmExit.Size = new System.Drawing.Size(225, 30);
            this.tsmExit.Text = "退出(&T)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 14;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnEndDateTime, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStartDateTime, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 10, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 12, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbFirstNum, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbSecondNum, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbFourthNum, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbFifthNum, 9, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbSixthNum, 11, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbSeventhNum, 13, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbStartDateTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbTimes, 11, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbEndDateTime, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbThirdNum, 5, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 13);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1169, 71);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnEndDateTime
            // 
            this.btnEndDateTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEndDateTime.Image = ((System.Drawing.Image)(resources.GetObject("btnEndDateTime.Image")));
            this.btnEndDateTime.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEndDateTime.Location = new System.Drawing.Point(587, 2);
            this.btnEndDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.btnEndDateTime.Name = "btnEndDateTime";
            this.btnEndDateTime.Size = new System.Drawing.Size(41, 31);
            this.btnEndDateTime.TabIndex = 16;
            this.btnEndDateTime.UseVisualStyleBackColor = true;
            this.btnEndDateTime.Click += new System.EventHandler(this.btnEndDateTime_Click);
            // 
            // btnStartDateTime
            // 
            this.btnStartDateTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStartDateTime.Image = ((System.Drawing.Image)(resources.GetObject("btnStartDateTime.Image")));
            this.btnStartDateTime.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStartDateTime.Location = new System.Drawing.Point(257, 2);
            this.btnStartDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartDateTime.Name = "btnStartDateTime";
            this.btnStartDateTime.Size = new System.Drawing.Size(41, 31);
            this.btnStartDateTime.TabIndex = 15;
            this.btnStartDateTime.UseVisualStyleBackColor = true;
            this.btnStartDateTime.Click += new System.EventHandler(this.btnStartDateTime_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "开奖日期";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "第一数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "第二数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(354, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "第三数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(684, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "第五数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(849, 44);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "第六数";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1014, 44);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "第七数";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbFirstNum
            // 
            this.tbFirstNum.Location = new System.Drawing.Point(94, 39);
            this.tbFirstNum.Margin = new System.Windows.Forms.Padding(4);
            this.tbFirstNum.Name = "tbFirstNum";
            this.tbFirstNum.Size = new System.Drawing.Size(79, 28);
            this.tbFirstNum.TabIndex = 1;
            this.tbFirstNum.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // tbSecondNum
            // 
            this.tbSecondNum.Location = new System.Drawing.Point(259, 39);
            this.tbSecondNum.Margin = new System.Windows.Forms.Padding(4);
            this.tbSecondNum.Name = "tbSecondNum";
            this.tbSecondNum.Size = new System.Drawing.Size(79, 28);
            this.tbSecondNum.TabIndex = 1;
            this.tbSecondNum.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // tbFourthNum
            // 
            this.tbFourthNum.Location = new System.Drawing.Point(589, 39);
            this.tbFourthNum.Margin = new System.Windows.Forms.Padding(4);
            this.tbFourthNum.Name = "tbFourthNum";
            this.tbFourthNum.Size = new System.Drawing.Size(79, 28);
            this.tbFourthNum.TabIndex = 1;
            this.tbFourthNum.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // tbFifthNum
            // 
            this.tbFifthNum.Location = new System.Drawing.Point(754, 39);
            this.tbFifthNum.Margin = new System.Windows.Forms.Padding(4);
            this.tbFifthNum.Name = "tbFifthNum";
            this.tbFifthNum.Size = new System.Drawing.Size(79, 28);
            this.tbFifthNum.TabIndex = 1;
            this.tbFifthNum.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // tbSixthNum
            // 
            this.tbSixthNum.Location = new System.Drawing.Point(919, 39);
            this.tbSixthNum.Margin = new System.Windows.Forms.Padding(4);
            this.tbSixthNum.Name = "tbSixthNum";
            this.tbSixthNum.Size = new System.Drawing.Size(79, 28);
            this.tbSixthNum.TabIndex = 1;
            this.tbSixthNum.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // tbSeventhNum
            // 
            this.tbSeventhNum.Location = new System.Drawing.Point(1084, 39);
            this.tbSeventhNum.Margin = new System.Windows.Forms.Padding(4);
            this.tbSeventhNum.Name = "tbSeventhNum";
            this.tbSeventhNum.Size = new System.Drawing.Size(79, 28);
            this.tbSeventhNum.TabIndex = 1;
            this.tbSeventhNum.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(519, 44);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "第四数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbStartDateTime
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbStartDateTime, 2);
            this.tbStartDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbStartDateTime.Location = new System.Drawing.Point(93, 3);
            this.tbStartDateTime.Name = "tbStartDateTime";
            this.tbStartDateTime.Size = new System.Drawing.Size(159, 28);
            this.tbStartDateTime.TabIndex = 8;
            this.tbStartDateTime.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // tbTimes
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbTimes, 2);
            this.tbTimes.Location = new System.Drawing.Point(919, 4);
            this.tbTimes.Margin = new System.Windows.Forms.Padding(4);
            this.tbTimes.Name = "tbTimes";
            this.tbTimes.Size = new System.Drawing.Size(157, 28);
            this.tbTimes.TabIndex = 1;
            this.tbTimes.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(867, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "期数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(390, 8);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 18);
            this.label10.TabIndex = 7;
            this.label10.Text = "至";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbEndDateTime
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbEndDateTime, 2);
            this.tbEndDateTime.Location = new System.Drawing.Point(423, 3);
            this.tbEndDateTime.Name = "tbEndDateTime";
            this.tbEndDateTime.Size = new System.Drawing.Size(159, 28);
            this.tbEndDateTime.TabIndex = 9;
            this.tbEndDateTime.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // tbThirdNum
            // 
            this.tbThirdNum.Location = new System.Drawing.Point(424, 39);
            this.tbThirdNum.Margin = new System.Windows.Forms.Padding(4);
            this.tbThirdNum.Name = "tbThirdNum";
            this.tbThirdNum.Size = new System.Drawing.Size(79, 28);
            this.tbThirdNum.TabIndex = 1;
            this.tbThirdNum.Enter += new System.EventHandler(this.TextBox_Enter);
            // 
            // bdnMarksixRecord
            // 
            this.bdnMarksixRecord.AddNewItem = null;
            this.bdnMarksixRecord.BackColor = System.Drawing.Color.Transparent;
            this.bdnMarksixRecord.CountItem = this.bdnCountItem;
            this.bdnMarksixRecord.CountItemFormat = "/ 1";
            this.bdnMarksixRecord.DeleteItem = null;
            this.bdnMarksixRecord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bdnMarksixRecord.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.bdnMarksixRecord.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bdnMoveFirstItem,
            this.bdnMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bdnPositionItem,
            this.bdnCountItem,
            this.bindingNavigatorSeparator1,
            this.bdnMoveNextItem,
            this.bdnMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripLabel1,
            this.tlscombo});
            this.bdnMarksixRecord.Location = new System.Drawing.Point(0, 593);
            this.bdnMarksixRecord.MoveFirstItem = this.bdnMoveFirstItem;
            this.bdnMarksixRecord.MoveLastItem = this.bdnMoveLastItem;
            this.bdnMarksixRecord.MoveNextItem = this.bdnMoveNextItem;
            this.bdnMarksixRecord.MovePreviousItem = this.bdnMovePreviousItem;
            this.bdnMarksixRecord.Name = "bdnMarksixRecord";
            this.bdnMarksixRecord.PositionItem = this.bdnPositionItem;
            this.bdnMarksixRecord.Size = new System.Drawing.Size(1382, 32);
            this.bdnMarksixRecord.TabIndex = 7;
            this.bdnMarksixRecord.Text = "bindingNavigator1";
            // 
            // bdnCountItem
            // 
            this.bdnCountItem.Name = "bdnCountItem";
            this.bdnCountItem.Size = new System.Drawing.Size(34, 29);
            this.bdnCountItem.Text = "/ 1";
            this.bdnCountItem.ToolTipText = "总项数";
            // 
            // bdnMoveFirstItem
            // 
            this.bdnMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bdnMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bdnMoveFirstItem.Image")));
            this.bdnMoveFirstItem.Name = "bdnMoveFirstItem";
            this.bdnMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bdnMoveFirstItem.Size = new System.Drawing.Size(28, 29);
            this.bdnMoveFirstItem.Text = "移到第一条记录";
            this.bdnMoveFirstItem.Click += new System.EventHandler(this.bdnMoveFirstItem_Click);
            // 
            // bdnMovePreviousItem
            // 
            this.bdnMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bdnMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bdnMovePreviousItem.Image")));
            this.bdnMovePreviousItem.Name = "bdnMovePreviousItem";
            this.bdnMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bdnMovePreviousItem.Size = new System.Drawing.Size(28, 29);
            this.bdnMovePreviousItem.Text = "移到上一条记录";
            this.bdnMovePreviousItem.Click += new System.EventHandler(this.bdnMovePreviousItem_Click);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 32);
            // 
            // bdnPositionItem
            // 
            this.bdnPositionItem.AccessibleName = "位置";
            this.bdnPositionItem.AutoSize = false;
            this.bdnPositionItem.Name = "bdnPositionItem";
            this.bdnPositionItem.Size = new System.Drawing.Size(50, 30);
            this.bdnPositionItem.Text = "0";
            this.bdnPositionItem.ToolTipText = "当前位置";
            this.bdnPositionItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberControl_KeyPress);
            this.bdnPositionItem.TextChanged += new System.EventHandler(this.bdnPositionItem_TextChanged);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // bdnMoveNextItem
            // 
            this.bdnMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bdnMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bdnMoveNextItem.Image")));
            this.bdnMoveNextItem.Name = "bdnMoveNextItem";
            this.bdnMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bdnMoveNextItem.Size = new System.Drawing.Size(28, 29);
            this.bdnMoveNextItem.Text = "移到下一条记录";
            this.bdnMoveNextItem.Click += new System.EventHandler(this.bdnMoveNextItem_Click);
            // 
            // bdnMoveLastItem
            // 
            this.bdnMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bdnMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bdnMoveLastItem.Image")));
            this.bdnMoveLastItem.Name = "bdnMoveLastItem";
            this.bdnMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bdnMoveLastItem.Size = new System.Drawing.Size(28, 29);
            this.bdnMoveLastItem.Text = "移到最后一条记录";
            this.bdnMoveLastItem.Click += new System.EventHandler(this.bdnMoveLastItem_Click);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(82, 29);
            this.toolStripLabel1.Text = "每页条数";
            // 
            // tlscombo
            // 
            this.tlscombo.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100",
            "200"});
            this.tlscombo.Name = "tlscombo";
            this.tlscombo.Size = new System.Drawing.Size(121, 32);
            this.tlscombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberControl_KeyPress);
            this.tlscombo.TextChanged += new System.EventHandler(this.tlscombo_TextChanged);
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(73, 194);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 8;
            this.monthCalendar.Visible = false;
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
            // 
            // dgvMarksixRecordList
            // 
            this.dgvMarksixRecordList.AllowUserToAddRows = false;
            this.dgvMarksixRecordList.AllowUserToDeleteRows = false;
            this.dgvMarksixRecordList.AllowUserToResizeColumns = false;
            this.dgvMarksixRecordList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvMarksixRecordList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMarksixRecordList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMarksixRecordList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMarksixRecordList.ColumnHeadersHeight = 25;
            this.dgvMarksixRecordList.Location = new System.Drawing.Point(0, 147);
            this.dgvMarksixRecordList.Name = "dgvMarksixRecordList";
            this.dgvMarksixRecordList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMarksixRecordList.RowTemplate.Height = 30;
            this.dgvMarksixRecordList.Size = new System.Drawing.Size(1382, 444);
            this.dgvMarksixRecordList.TabIndex = 6;
            this.dgvMarksixRecordList.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellMouseEnter);
            this.dgvMarksixRecordList.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellMouseLeave);
            this.dgvMarksixRecordList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvList_DataBindingComplete);
            // 
            // frmMarksixRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1382, 625);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.bdnMarksixRecord);
            this.Controls.Add(this.dgvMarksixRecordList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tslMaster);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMarksixRecord";
            this.Text = "Marksix历史记录";
            this.Load += new System.EventHandler(this.frmMarksixRecord_Load);
            this.tslMaster.ResumeLayout(false);
            this.tslMaster.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.msMaster.ResumeLayout(false);
            this.msMaster.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdnMarksixRecord)).EndInit();
            this.bdnMarksixRecord.ResumeLayout(false);
            this.bdnMarksixRecord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarksixRecordList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tslMaster;
        private System.Windows.Forms.ToolStripMenuItem tsbImport;
        private System.Windows.Forms.ToolStripMenuItem tsbStopImport;
        private System.Windows.Forms.ToolStripMenuItem tsbSearch;
        private System.Windows.Forms.ToolStripMenuItem tsbExport;
        private System.Windows.Forms.ToolStripMenuItem tsbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbTimes;
        private System.Windows.Forms.TextBox tbFirstNum;
        private System.Windows.Forms.TextBox tbSecondNum;
        private System.Windows.Forms.TextBox tbThirdNum;
        private System.Windows.Forms.TextBox tbFourthNum;
        private System.Windows.Forms.TextBox tbFifthNum;
        private System.Windows.Forms.TextBox tbSixthNum;
        private System.Windows.Forms.TextBox tbSeventhNum;
        private System.Windows.Forms.Label label6;
        private Common.myDataGridView dgvMarksixRecordList;
        private System.Windows.Forms.BindingNavigator bdnMarksixRecord;
        private System.Windows.Forms.ToolStripLabel bdnCountItem;
        private System.Windows.Forms.ToolStripButton bdnMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bdnMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bdnPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bdnMoveNextItem;
        private System.Windows.Forms.ToolStripButton bdnMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tlscombo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbEndDateTime;
        private System.Windows.Forms.TextBox tbStartDateTime;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        internal System.Windows.Forms.Button btnEndDateTime;
        internal System.Windows.Forms.Button btnStartDateTime;
        private System.Windows.Forms.MenuStrip msMaster;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmImport;
        private System.Windows.Forms.ToolStripMenuItem tsmStopImport;
        private System.Windows.Forms.ToolStripMenuItem tsmExport;
        private System.Windows.Forms.ToolStripMenuItem tsmSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
    }
}