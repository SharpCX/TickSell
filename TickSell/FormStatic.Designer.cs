namespace TickSell
{
    partial class FormStatic
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
            this.dgvStatic = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.dtpBe = new System.Windows.Forms.DateTimePicker();
            this.dtpEn = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAllPrice = new System.Windows.Forms.TextBox();
            this.MenueOutput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatic)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.MenueOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStatic
            // 
            this.dgvStatic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatic.ContextMenuStrip = this.MenueOutput;
            this.dgvStatic.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvStatic.Location = new System.Drawing.Point(0, 0);
            this.dgvStatic.Name = "dgvStatic";
            this.dgvStatic.RowTemplate.Height = 23;
            this.dgvStatic.Size = new System.Drawing.Size(833, 186);
            this.dgvStatic.TabIndex = 0;
            this.dgvStatic.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatic_CellContentClick);
            this.dgvStatic.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatic_CellDoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(569, 416);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "统计";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpBe
            // 
            this.dtpBe.Location = new System.Drawing.Point(58, 415);
            this.dtpBe.Name = "dtpBe";
            this.dtpBe.Size = new System.Drawing.Size(148, 21);
            this.dtpBe.TabIndex = 2;
            // 
            // dtpEn
            // 
            this.dtpEn.Location = new System.Drawing.Point(229, 415);
            this.dtpEn.Name = "dtpEn";
            this.dtpEn.Size = new System.Drawing.Size(128, 21);
            this.dtpEn.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "时间段";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 418);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "-";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 186);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(833, 23);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "详细信息";
            // 
            // dgvDetail
            // 
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.ContextMenuStrip = this.MenueOutput;
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvDetail.Location = new System.Drawing.Point(0, 209);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(833, 186);
            this.dgvDetail.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(379, 421);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "总营业额";
            // 
            // txtAllPrice
            // 
            this.txtAllPrice.Enabled = false;
            this.txtAllPrice.Location = new System.Drawing.Point(439, 414);
            this.txtAllPrice.Name = "txtAllPrice";
            this.txtAllPrice.Size = new System.Drawing.Size(100, 21);
            this.txtAllPrice.TabIndex = 9;
            // 
            // MenueOutput
            // 
            this.MenueOutput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出ToolStripMenuItem});
            this.MenueOutput.Name = "MenueOutput";
            this.MenueOutput.Size = new System.Drawing.Size(153, 48);
            this.MenueOutput.Opening += new System.ComponentModel.CancelEventHandler(this.MenueOutput_Opening);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.导出ToolStripMenuItem.Text = "导出";
            this.导出ToolStripMenuItem.Click += new System.EventHandler(this.导出ToolStripMenuItem_Click);
            // 
            // FormStatic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 445);
            this.Controls.Add(this.txtAllPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvDetail);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEn);
            this.Controls.Add(this.dtpBe);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvStatic);
            this.Name = "FormStatic";
            this.Text = "售票统计";
            this.Load += new System.EventHandler(this.FormStatic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatic)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.MenueOutput.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStatic;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtpBe;
        private System.Windows.Forms.DateTimePicker dtpEn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAllPrice;
        private System.Windows.Forms.ContextMenuStrip MenueOutput;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
    }
}