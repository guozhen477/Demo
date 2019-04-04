namespace WinFrom
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bthCChu = new System.Windows.Forms.Button();
            this.bthChu = new System.Windows.Forms.Button();
            this.bthCVanning = new System.Windows.Forms.Button();
            this.bthSVanning = new System.Windows.Forms.Button();
            this.bthPlaceChange = new System.Windows.Forms.Button();
            this.bthBillChange = new System.Windows.Forms.Button();
            this.bthRK = new System.Windows.Forms.Button();
            this.bthReveice = new System.Windows.Forms.Button();
            this.bthChange = new System.Windows.Forms.Button();
            this.bthSplit = new System.Windows.Forms.Button();
            this.bthCombit = new System.Windows.Forms.Button();
            this.bthSubmit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(923, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.bthCChu);
            this.tabPage1.Controls.Add(this.bthChu);
            this.tabPage1.Controls.Add(this.bthCVanning);
            this.tabPage1.Controls.Add(this.bthSVanning);
            this.tabPage1.Controls.Add(this.bthPlaceChange);
            this.tabPage1.Controls.Add(this.bthBillChange);
            this.tabPage1.Controls.Add(this.bthRK);
            this.tabPage1.Controls.Add(this.bthReveice);
            this.tabPage1.Controls.Add(this.bthChange);
            this.tabPage1.Controls.Add(this.bthSplit);
            this.tabPage1.Controls.Add(this.bthCombit);
            this.tabPage1.Controls.Add(this.bthSubmit);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(915, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "出口拼箱功能测试";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(466, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "出场确认";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "装箱报告";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "申请单";
            // 
            // bthCChu
            // 
            this.bthCChu.Location = new System.Drawing.Point(531, 80);
            this.bthCChu.Name = "bthCChu";
            this.bthCChu.Size = new System.Drawing.Size(75, 23);
            this.bthCChu.TabIndex = 12;
            this.bthCChu.Text = "取消出场";
            this.bthCChu.UseVisualStyleBackColor = true;
            this.bthCChu.Click += new System.EventHandler(this.bthCChu_Click);
            // 
            // bthChu
            // 
            this.bthChu.Location = new System.Drawing.Point(531, 40);
            this.bthChu.Name = "bthChu";
            this.bthChu.Size = new System.Drawing.Size(75, 23);
            this.bthChu.TabIndex = 11;
            this.bthChu.Text = "出场确认";
            this.bthChu.UseVisualStyleBackColor = true;
            this.bthChu.Click += new System.EventHandler(this.bthChu_Click);
            // 
            // bthCVanning
            // 
            this.bthCVanning.Location = new System.Drawing.Point(280, 80);
            this.bthCVanning.Name = "bthCVanning";
            this.bthCVanning.Size = new System.Drawing.Size(75, 23);
            this.bthCVanning.TabIndex = 10;
            this.bthCVanning.Text = "取消装箱";
            this.bthCVanning.UseVisualStyleBackColor = true;
            this.bthCVanning.Click += new System.EventHandler(this.bthCVanning_Click);
            // 
            // bthSVanning
            // 
            this.bthSVanning.Location = new System.Drawing.Point(280, 40);
            this.bthSVanning.Name = "bthSVanning";
            this.bthSVanning.Size = new System.Drawing.Size(75, 23);
            this.bthSVanning.TabIndex = 9;
            this.bthSVanning.Text = "申报装箱";
            this.bthSVanning.UseVisualStyleBackColor = true;
            this.bthSVanning.Click += new System.EventHandler(this.bthSVanning_Click);
            // 
            // bthPlaceChange
            // 
            this.bthPlaceChange.Location = new System.Drawing.Point(45, 320);
            this.bthPlaceChange.Name = "bthPlaceChange";
            this.bthPlaceChange.Size = new System.Drawing.Size(75, 23);
            this.bthPlaceChange.TabIndex = 8;
            this.bthPlaceChange.Text = "库位变更";
            this.bthPlaceChange.UseVisualStyleBackColor = true;
            this.bthPlaceChange.Click += new System.EventHandler(this.bthPlaceChange_Click);
            // 
            // bthBillChange
            // 
            this.bthBillChange.Location = new System.Drawing.Point(45, 280);
            this.bthBillChange.Name = "bthBillChange";
            this.bthBillChange.Size = new System.Drawing.Size(75, 23);
            this.bthBillChange.TabIndex = 7;
            this.bthBillChange.Text = "提单变更";
            this.bthBillChange.UseVisualStyleBackColor = true;
            this.bthBillChange.Click += new System.EventHandler(this.bthBillChange_Click);
            // 
            // bthRK
            // 
            this.bthRK.Location = new System.Drawing.Point(45, 80);
            this.bthRK.Name = "bthRK";
            this.bthRK.Size = new System.Drawing.Size(75, 23);
            this.bthRK.TabIndex = 6;
            this.bthRK.Text = "入库确认";
            this.bthRK.UseVisualStyleBackColor = true;
            this.bthRK.Click += new System.EventHandler(this.button1_Click);
            // 
            // bthReveice
            // 
            this.bthReveice.Location = new System.Drawing.Point(45, 240);
            this.bthReveice.Name = "bthReveice";
            this.bthReveice.Size = new System.Drawing.Size(75, 23);
            this.bthReveice.TabIndex = 5;
            this.bthReveice.Text = "接收";
            this.bthReveice.UseVisualStyleBackColor = true;
            this.bthReveice.Click += new System.EventHandler(this.bthReveice_Click);
            // 
            // bthChange
            // 
            this.bthChange.Location = new System.Drawing.Point(45, 200);
            this.bthChange.Name = "bthChange";
            this.bthChange.Size = new System.Drawing.Size(75, 23);
            this.bthChange.TabIndex = 4;
            this.bthChange.Text = "换货";
            this.bthChange.UseVisualStyleBackColor = true;
            this.bthChange.Click += new System.EventHandler(this.bthChange_Click);
            // 
            // bthSplit
            // 
            this.bthSplit.Location = new System.Drawing.Point(45, 160);
            this.bthSplit.Name = "bthSplit";
            this.bthSplit.Size = new System.Drawing.Size(75, 23);
            this.bthSplit.TabIndex = 3;
            this.bthSplit.Text = "拆分";
            this.bthSplit.UseVisualStyleBackColor = true;
            this.bthSplit.Click += new System.EventHandler(this.bthSplit_Click);
            // 
            // bthCombit
            // 
            this.bthCombit.Location = new System.Drawing.Point(45, 120);
            this.bthCombit.Name = "bthCombit";
            this.bthCombit.Size = new System.Drawing.Size(75, 23);
            this.bthCombit.TabIndex = 2;
            this.bthCombit.Text = "合并";
            this.bthCombit.UseVisualStyleBackColor = true;
            this.bthCombit.Click += new System.EventHandler(this.bthCombit_Click);
            // 
            // bthSubmit
            // 
            this.bthSubmit.Location = new System.Drawing.Point(45, 40);
            this.bthSubmit.Name = "bthSubmit";
            this.bthSubmit.Size = new System.Drawing.Size(75, 23);
            this.bthSubmit.TabIndex = 1;
            this.bthSubmit.Text = "申报";
            this.bthSubmit.UseVisualStyleBackColor = true;
            this.bthSubmit.Click += new System.EventHandler(this.bthSubmit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(280, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 422);
            this.Controls.Add(this.tabControl1);
            this.MaximumSize = new System.Drawing.Size(939, 460);
            this.MinimumSize = new System.Drawing.Size(939, 460);
            this.Name = "Form1";
            this.Text = "出口拼箱系统接口Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button bthSubmit;
        private System.Windows.Forms.Button bthChange;
        private System.Windows.Forms.Button bthSplit;
        private System.Windows.Forms.Button bthCombit;
        private System.Windows.Forms.Button bthReveice;
        private System.Windows.Forms.Button bthRK;
        private System.Windows.Forms.Button bthPlaceChange;
        private System.Windows.Forms.Button bthBillChange;
        private System.Windows.Forms.Button bthCVanning;
        private System.Windows.Forms.Button bthSVanning;
        private System.Windows.Forms.Button bthCChu;
        private System.Windows.Forms.Button bthChu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

