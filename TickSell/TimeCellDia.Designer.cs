namespace TickSell
{
    partial class TimeCellDia
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
            this.label2 = new System.Windows.Forms.Label();
            this.DTPTimeEn = new System.Windows.Forms.DateTimePicker();
            this.txtMovieName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.DTPTimeBe = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtShowTimes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "播放时间起";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "播放时间止";
            // 
            // DTPTimeEn
            // 
            this.DTPTimeEn.CustomFormat = "yyyy/MM/dd  HH:mm";
            this.DTPTimeEn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTimeEn.Location = new System.Drawing.Point(369, 12);
            this.DTPTimeEn.Name = "DTPTimeEn";
            this.DTPTimeEn.Size = new System.Drawing.Size(200, 21);
            this.DTPTimeEn.TabIndex = 2;
            this.DTPTimeEn.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // txtMovieName
            // 
            this.txtMovieName.Location = new System.Drawing.Point(84, 39);
            this.txtMovieName.Name = "txtMovieName";
            this.txtMovieName.Size = new System.Drawing.Size(200, 21);
            this.txtMovieName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "影片名称";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(152, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(345, 66);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DTPTimeBe
            // 
            this.DTPTimeBe.CustomFormat = "yyyy/MM/dd  HH:mm";
            this.DTPTimeBe.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTimeBe.Location = new System.Drawing.Point(89, 6);
            this.DTPTimeBe.Name = "DTPTimeBe";
            this.DTPTimeBe.Size = new System.Drawing.Size(200, 21);
            this.DTPTimeBe.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "场次";
            // 
            // txtShowTimes
            // 
            this.txtShowTimes.Location = new System.Drawing.Point(369, 39);
            this.txtShowTimes.Name = "txtShowTimes";
            this.txtShowTimes.Size = new System.Drawing.Size(200, 21);
            this.txtShowTimes.TabIndex = 9;
            // 
            // TimeCellDia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 91);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtShowTimes);
            this.Controls.Add(this.DTPTimeBe);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMovieName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DTPTimeEn);
            this.Controls.Add(this.label1);
            this.Name = "TimeCellDia";
            this.Text = "影片播放信息";
            this.Load += new System.EventHandler(this.影片播放信息_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DTPTimeEn;
        private System.Windows.Forms.TextBox txtMovieName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker DTPTimeBe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtShowTimes;
    }
}