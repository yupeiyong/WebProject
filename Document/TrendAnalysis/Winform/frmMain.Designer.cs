namespace Winform
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.msMaster = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tslMaster = new System.Windows.Forms.ToolStrip();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ssMaster = new System.Windows.Forms.StatusStrip();
            this.tsslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbCompletePrecent = new System.Windows.Forms.ToolStripProgressBar();
            this.tsbMarksix = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMarksixRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMarksixSpecifiedLocationAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMarksix = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMarksixRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMarksixSpecifiedLocationAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMarksixSpecifiedLocationPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMarksixSpecifiedLocationPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.msMaster.SuspendLayout();
            this.tslMaster.SuspendLayout();
            this.ssMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMaster
            // 
            this.msMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.msMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.toolsMenu,
            this.windowsMenu,
            this.helpMenu});
            this.msMaster.Location = new System.Drawing.Point(0, 0);
            this.msMaster.MdiWindowListItem = this.windowsMenu;
            this.msMaster.Name = "msMaster";
            this.msMaster.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.msMaster.Size = new System.Drawing.Size(948, 34);
            this.msMaster.TabIndex = 0;
            this.msMaster.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMarksix,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(80, 28);
            this.fileMenu.Text = "文件(&F)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(253, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(256, 30);
            this.exitToolStripMenuItem.Text = "退出(&X)";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator6,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator7,
            this.selectAllToolStripMenuItem});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(80, 28);
            this.editMenu.Text = "编辑(&E)";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(218, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(218, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(221, 30);
            this.selectAllToolStripMenuItem.Text = "全选(&A)";
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(82, 28);
            this.viewMenu.Text = "视图(&V)";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(169, 30);
            this.toolBarToolStripMenuItem.Text = "工具栏(&T)";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(169, 30);
            this.statusBarToolStripMenuItem.Text = "状态栏(&S)";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.statusBarToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(80, 28);
            this.toolsMenu.Text = "工具(&T)";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(156, 30);
            this.optionsToolStripMenuItem.Text = "选项(&O)";
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.arrangeIconsToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(88, 28);
            this.windowsMenu.Text = "窗口(&W)";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(191, 30);
            this.cascadeToolStripMenuItem.Text = "层叠(&C)";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(191, 30);
            this.tileVerticalToolStripMenuItem.Text = "垂直平铺(&V)";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(191, 30);
            this.tileHorizontalToolStripMenuItem.Text = "水平平铺(&H)";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(191, 30);
            this.closeAllToolStripMenuItem.Text = "全部关闭(&L)";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // arrangeIconsToolStripMenuItem
            // 
            this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
            this.arrangeIconsToolStripMenuItem.Size = new System.Drawing.Size(191, 30);
            this.arrangeIconsToolStripMenuItem.Text = "排列图标(&A)";
            this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(84, 28);
            this.helpMenu.Text = "帮助(&H)";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(228, 30);
            this.contentsToolStripMenuItem.Text = "目录(&C)";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(225, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(228, 30);
            this.aboutToolStripMenuItem.Text = "关于(&A) ... ...";
            // 
            // tslMaster
            // 
            this.tslMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tslMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMarksix,
            this.tsbExit});
            this.tslMaster.Location = new System.Drawing.Point(0, 34);
            this.tslMaster.Name = "tslMaster";
            this.tslMaster.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tslMaster.Size = new System.Drawing.Size(948, 28);
            this.tslMaster.TabIndex = 1;
            this.tslMaster.Text = "ToolStrip";
            // 
            // ssMaster
            // 
            this.ssMaster.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ssMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslInfo,
            this.tspbCompletePrecent});
            this.ssMaster.Location = new System.Drawing.Point(0, 597);
            this.ssMaster.Name = "ssMaster";
            this.ssMaster.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.ssMaster.Size = new System.Drawing.Size(948, 30);
            this.ssMaster.TabIndex = 8;
            this.ssMaster.Text = "statusStrip1";
            // 
            // tsslInfo
            // 
            this.tsslInfo.Name = "tsslInfo";
            this.tsslInfo.Size = new System.Drawing.Size(46, 24);
            this.tsslInfo.Text = "就绪";
            // 
            // tspbCompletePrecent
            // 
            this.tspbCompletePrecent.Maximum = 1000;
            this.tspbCompletePrecent.Name = "tspbCompletePrecent";
            this.tspbCompletePrecent.Size = new System.Drawing.Size(300, 24);
            this.tspbCompletePrecent.Step = 1;
            this.tspbCompletePrecent.Visible = false;
            // 
            // tsbMarksix
            // 
            this.tsbMarksix.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMarksixRecord,
            this.tsbMarksixSpecifiedLocationAnalysis,
            this.tsbMarksixSpecifiedLocationPurchase});
            this.tsbMarksix.Image = ((System.Drawing.Image)(resources.GetObject("tsbMarksix.Image")));
            this.tsbMarksix.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbMarksix.Name = "tsbMarksix";
            this.tsbMarksix.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsbMarksix.Size = new System.Drawing.Size(140, 28);
            this.tsbMarksix.Text = "Marksix(&N)";
            // 
            // tsbMarksixRecord
            // 
            this.tsbMarksixRecord.Image = global::Winform.Properties.Resources.记录;
            this.tsbMarksixRecord.Name = "tsbMarksixRecord";
            this.tsbMarksixRecord.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsbMarksixRecord.Size = new System.Drawing.Size(302, 30);
            this.tsbMarksixRecord.Text = "记录";
            this.tsbMarksixRecord.Click += new System.EventHandler(this.tsbMarksixRecord_Click);
            // 
            // tsbMarksixSpecifiedLocationAnalysis
            // 
            this.tsbMarksixSpecifiedLocationAnalysis.Image = global::Winform.Properties.Resources.分析;
            this.tsbMarksixSpecifiedLocationAnalysis.Name = "tsbMarksixSpecifiedLocationAnalysis";
            this.tsbMarksixSpecifiedLocationAnalysis.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsbMarksixSpecifiedLocationAnalysis.Size = new System.Drawing.Size(302, 30);
            this.tsbMarksixSpecifiedLocationAnalysis.Text = "分析指定位置";
            this.tsbMarksixSpecifiedLocationAnalysis.Click += new System.EventHandler(this.tsbMarksixSpecifiedLocationAnalysis_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = global::Winform.Properties.Resources.离开;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(106, 28);
            this.tsbExit.Text = "退出(&X)";
            this.tsbExit.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // tsmiMarksix
            // 
            this.tsmiMarksix.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMarksixRecord,
            this.tsmiMarksixSpecifiedLocationAnalysis,
            this.tsmiMarksixSpecifiedLocationPurchase});
            this.tsmiMarksix.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMarksix.Image")));
            this.tsmiMarksix.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsmiMarksix.Name = "tsmiMarksix";
            this.tsmiMarksix.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiMarksix.Size = new System.Drawing.Size(256, 30);
            this.tsmiMarksix.Text = "Marksix(&N)";
            // 
            // tsmiMarksixRecord
            // 
            this.tsmiMarksixRecord.Image = global::Winform.Properties.Resources.记录;
            this.tsmiMarksixRecord.Name = "tsmiMarksixRecord";
            this.tsmiMarksixRecord.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsmiMarksixRecord.Size = new System.Drawing.Size(302, 30);
            this.tsmiMarksixRecord.Text = "记录";
            this.tsmiMarksixRecord.Click += new System.EventHandler(this.tsbMarksixRecord_Click);
            // 
            // tsmiMarksixSpecifiedLocationAnalysis
            // 
            this.tsmiMarksixSpecifiedLocationAnalysis.Image = global::Winform.Properties.Resources.分析;
            this.tsmiMarksixSpecifiedLocationAnalysis.Name = "tsmiMarksixSpecifiedLocationAnalysis";
            this.tsmiMarksixSpecifiedLocationAnalysis.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmiMarksixSpecifiedLocationAnalysis.Size = new System.Drawing.Size(302, 30);
            this.tsmiMarksixSpecifiedLocationAnalysis.Text = "分析指定位置";
            this.tsmiMarksixSpecifiedLocationAnalysis.Click += new System.EventHandler(this.tsbMarksixSpecifiedLocationAnalysis_Click);
            // 
            // tsmiMarksixSpecifiedLocationPurchase
            // 
            this.tsmiMarksixSpecifiedLocationPurchase.Image = global::Winform.Properties.Resources.partition;
            this.tsmiMarksixSpecifiedLocationPurchase.Name = "tsmiMarksixSpecifiedLocationPurchase";
            this.tsmiMarksixSpecifiedLocationPurchase.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsmiMarksixSpecifiedLocationPurchase.Size = new System.Drawing.Size(302, 30);
            this.tsmiMarksixSpecifiedLocationPurchase.Text = "指定位置购买记录";
            this.tsmiMarksixSpecifiedLocationPurchase.Click += new System.EventHandler(this.tsmiMarksixSpecifiedLocationPurchase_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(221, 30);
            this.undoToolStripMenuItem.Text = "撤消(&U)";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(221, 30);
            this.redoToolStripMenuItem.Text = "重复(&R)";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(221, 30);
            this.cutToolStripMenuItem.Text = "剪切(&T)";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(221, 30);
            this.copyToolStripMenuItem.Text = "复制(&C)";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(221, 30);
            this.pasteToolStripMenuItem.Text = "粘贴(&P)";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexToolStripMenuItem.Image")));
            this.indexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(228, 30);
            this.indexToolStripMenuItem.Text = "索引(&I)";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripMenuItem.Image")));
            this.searchToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(228, 30);
            this.searchToolStripMenuItem.Text = "搜索(&S)";
            // 
            // tsbMarksixSpecifiedLocationPurchase
            // 
            this.tsbMarksixSpecifiedLocationPurchase.Image = global::Winform.Properties.Resources.partition;
            this.tsbMarksixSpecifiedLocationPurchase.Name = "tsbMarksixSpecifiedLocationPurchase";
            this.tsbMarksixSpecifiedLocationPurchase.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsbMarksixSpecifiedLocationPurchase.Size = new System.Drawing.Size(302, 30);
            this.tsbMarksixSpecifiedLocationPurchase.Text = "指定位置购买记录";
            this.tsbMarksixSpecifiedLocationPurchase.Click += new System.EventHandler(this.tsmiMarksixSpecifiedLocationPurchase_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 627);
            this.Controls.Add(this.ssMaster);
            this.Controls.Add(this.tslMaster);
            this.Controls.Add(this.msMaster);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMaster;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "趋势分析";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.msMaster.ResumeLayout(false);
            this.msMaster.PerformLayout();
            this.tslMaster.ResumeLayout(false);
            this.tslMaster.PerformLayout();
            this.ssMaster.ResumeLayout(false);
            this.ssMaster.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip msMaster;
        private System.Windows.Forms.ToolStrip tslMaster;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarksix;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        internal System.Windows.Forms.ToolStripMenuItem tsmiMarksixRecord;
        internal System.Windows.Forms.ToolStripMenuItem tsmiMarksixSpecifiedLocationAnalysis;
        private System.Windows.Forms.ToolStripMenuItem tsbMarksix;
        internal System.Windows.Forms.ToolStripMenuItem tsbMarksixRecord;
        internal System.Windows.Forms.ToolStripMenuItem tsbMarksixSpecifiedLocationAnalysis;
        private System.Windows.Forms.ToolStripMenuItem tsbExit;
        private System.Windows.Forms.StatusStrip ssMaster;
        internal System.Windows.Forms.ToolStripStatusLabel tsslInfo;
        internal System.Windows.Forms.ToolStripProgressBar tspbCompletePrecent;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarksixSpecifiedLocationPurchase;
        private System.Windows.Forms.ToolStripMenuItem tsbMarksixSpecifiedLocationPurchase;
    }
}



