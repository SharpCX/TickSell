namespace TickSell
{
    partial class CellSpeedInitialer
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCan = new System.Windows.Forms.Button();
            this.txtRow = new System.Windows.Forms.TextBox();
            this.txtCol = new System.Windows.Forms.TextBox();
            this.Row = new System.Windows.Forms.Label();
            this.Column = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(53, 43);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCan
            // 
            this.btnCan.Location = new System.Drawing.Point(173, 43);
            this.btnCan.Name = "btnCan";
            this.btnCan.Size = new System.Drawing.Size(75, 23);
            this.btnCan.TabIndex = 1;
            this.btnCan.Text = "取消";
            this.btnCan.UseVisualStyleBackColor = true;
            this.btnCan.Click += new System.EventHandler(this.btnCan_Click);
            // 
            // txtRow
            // 
            this.txtRow.Location = new System.Drawing.Point(32, 12);
            this.txtRow.Name = "txtRow";
            this.txtRow.Size = new System.Drawing.Size(100, 21);
            this.txtRow.TabIndex = 2;
            // 
            // txtCol
            // 
            this.txtCol.Location = new System.Drawing.Point(183, 12);
            this.txtCol.Name = "txtCol";
            this.txtCol.Size = new System.Drawing.Size(100, 21);
            this.txtCol.TabIndex = 3;
            // 
            // Row
            // 
            this.Row.AutoSize = true;
            this.Row.Location = new System.Drawing.Point(9, 16);
            this.Row.Name = "Row";
            this.Row.Size = new System.Drawing.Size(17, 12);
            this.Row.TabIndex = 4;
            this.Row.Text = "行";
            // 
            // Column
            // 
            this.Column.AutoSize = true;
            this.Column.Location = new System.Drawing.Point(160, 16);
            this.Column.Name = "Column";
            this.Column.Size = new System.Drawing.Size(17, 12);
            this.Column.TabIndex = 5;
            this.Column.Text = "列";
            // 
            // CellSpeedInitialer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 78);
            this.Controls.Add(this.Column);
            this.Controls.Add(this.Row);
            this.Controls.Add(this.txtCol);
            this.Controls.Add(this.txtRow);
            this.Controls.Add(this.btnCan);
            this.Controls.Add(this.btnOK);
            this.Name = "CellSpeedInitialer";
            this.Text = "CellSpeedInitialer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCan;
        private System.Windows.Forms.TextBox txtRow;
        private System.Windows.Forms.TextBox txtCol;
        private System.Windows.Forms.Label Row;
        private System.Windows.Forms.Label Column;
    }
}