using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TickSell
{
    public partial class TimeCellDia : Form
    {
        public delegate void CompleteHandler(DateTime timebe,DateTime timeen,string movieName,string showTimes);
        public event CompleteHandler On_Complete;

        public TimeCellDia()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(DTPTimeBe.Value>=DTPTimeEn.Value)
            {
                MessageBox.Show("请从新选择时间，起始时间不能大于等于终止时间。");
                return;
            }

            if (string.IsNullOrEmpty(txtMovieName.Text))
            {
                MessageBox.Show("请输入影片名称");
                return;
            }

            if (On_Complete != null)
            {
                On_Complete(DTPTimeBe.Value, DTPTimeEn.Value, txtMovieName.Text,txtShowTimes.Text);
            }
            else
            {
                MessageBox.Show("您没有指定正确的事件处理程序");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void 影片播放信息_Load(object sender, EventArgs e)
        {

        }
    }
}
