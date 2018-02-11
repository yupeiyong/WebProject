namespace Winform.Marksix
{
    partial class frmMarkSixSpecifiedLocationAnalysis
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboNumberLocation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTimes = new System.Windows.Forms.ComboBox();
            this.btnAnalysis = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBeforeTimesCount = new System.Windows.Forms.TextBox();
            this.btnAnalysisBefore = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvBeforeAnalysis = new Winform.Common.myDataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBeforeAnalysis)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "位置：";
            // 
            // cboNumberLocation
            // 
            this.cboNumberLocation.FormattingEnabled = true;
            this.cboNumberLocation.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cboNumberLocation.Location = new System.Drawing.Point(79, 39);
            this.cboNumberLocation.Name = "cboNumberLocation";
            this.cboNumberLocation.Size = new System.Drawing.Size(121, 26);
            this.cboNumberLocation.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "期次:";
            // 
            // cboTimes
            // 
            this.cboTimes.FormattingEnabled = true;
            this.cboTimes.Location = new System.Drawing.Point(310, 39);
            this.cboTimes.Name = "cboTimes";
            this.cboTimes.Size = new System.Drawing.Size(121, 26);
            this.cboTimes.TabIndex = 1;
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Location = new System.Drawing.Point(478, 33);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(80, 38);
            this.btnAnalysis.TabIndex = 2;
            this.btnAnalysis.Text = "分析";
            this.btnAnalysis.UseVisualStyleBackColor = true;
            this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(20, 84);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(547, 360);
            this.tbResult.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "当前期次以上";
            // 
            // tbBeforeTimesCount
            // 
            this.tbBeforeTimesCount.Location = new System.Drawing.Point(133, 37);
            this.tbBeforeTimesCount.Name = "tbBeforeTimesCount";
            this.tbBeforeTimesCount.Size = new System.Drawing.Size(100, 28);
            this.tbBeforeTimesCount.TabIndex = 5;
            this.tbBeforeTimesCount.Text = "50";
            // 
            // btnAnalysisBefore
            // 
            this.btnAnalysisBefore.Location = new System.Drawing.Point(310, 36);
            this.btnAnalysisBefore.Name = "btnAnalysisBefore";
            this.btnAnalysisBefore.Size = new System.Drawing.Size(75, 31);
            this.btnAnalysisBefore.TabIndex = 6;
            this.btnAnalysisBefore.Text = "分析";
            this.btnAnalysisBefore.UseVisualStyleBackColor = true;
            this.btnAnalysisBefore.Click += new System.EventHandler(this.btnAnalysisBefore_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cboNumberLocation);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbResult);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboTimes);
            this.groupBox1.Controls.Add(this.btnPurchase);
            this.groupBox1.Controls.Add(this.btnAnalysis);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(591, 646);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分析指定位置号码";
            // 
            // btnPurchase
            // 
            this.btnPurchase.Location = new System.Drawing.Point(20, 458);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(80, 38);
            this.btnPurchase.TabIndex = 2;
            this.btnPurchase.Text = "购买";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbBeforeTimesCount);
            this.groupBox2.Controls.Add(this.dgvBeforeAnalysis);
            this.groupBox2.Controls.Add(this.btnAnalysisBefore);
            this.groupBox2.Location = new System.Drawing.Point(621, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(777, 646);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "指定位置号码以前期次分析";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "期";
            // 
            // dgvBeforeAnalysis
            // 
            this.dgvBeforeAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBeforeAnalysis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBeforeAnalysis.Location = new System.Drawing.Point(9, 89);
            this.dgvBeforeAnalysis.Name = "dgvBeforeAnalysis";
            this.dgvBeforeAnalysis.RowTemplate.Height = 30;
            this.dgvBeforeAnalysis.Size = new System.Drawing.Size(730, 547);
            this.dgvBeforeAnalysis.TabIndex = 7;
            // 
            // frmMarkSixSpecifiedLocationAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1440, 670);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMarkSixSpecifiedLocationAnalysis";
            this.Text = "MarkSix指定位置号码分析";
            this.Load += new System.EventHandler(this.frmMarkSixSpecifiedLocationAnalysis_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBeforeAnalysis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboNumberLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTimes;
        private System.Windows.Forms.Button btnAnalysis;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBeforeTimesCount;
        private System.Windows.Forms.Button btnAnalysisBefore;
        private Common.myDataGridView dgvBeforeAnalysis;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Label label4;
    }
}