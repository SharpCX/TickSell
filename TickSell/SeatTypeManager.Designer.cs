namespace TickSell
{
    partial class SeatTypeManager
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
            this.SeatTypeGridView = new System.Windows.Forms.DataGridView();
            this.txtSeatName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SeatTypeGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SeatTypeGridView
            // 
            this.SeatTypeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SeatTypeGridView.Location = new System.Drawing.Point(12, 12);
            this.SeatTypeGridView.Name = "SeatTypeGridView";
            this.SeatTypeGridView.RowTemplate.Height = 23;
            this.SeatTypeGridView.Size = new System.Drawing.Size(343, 151);
            this.SeatTypeGridView.TabIndex = 0;
            this.SeatTypeGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SeatTypeGridView_CellClick);
            this.SeatTypeGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SeatTypeGridView_CellContentClick);
            // 
            // txtSeatName
            // 
            this.txtSeatName.Location = new System.Drawing.Point(12, 186);
            this.txtSeatName.Name = "txtSeatName";
            this.txtSeatName.Size = new System.Drawing.Size(343, 21);
            this.txtSeatName.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(16, 213);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(145, 213);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "修改";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(280, 213);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 4;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "类型名称";
            // 
            // SeatTypeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 241);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtSeatName);
            this.Controls.Add(this.SeatTypeGridView);
            this.Name = "SeatTypeManager";
            this.Text = "座位类型管理";
            this.Load += new System.EventHandler(this.SeatTypeManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SeatTypeGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView SeatTypeGridView;
        private System.Windows.Forms.TextBox txtSeatName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label label1;
    }
}