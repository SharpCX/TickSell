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
    public partial class CellSpeedInitialer : Form
    {

        public delegate void diaCallback(int row, int col);
        public event diaCallback DiaCallBack;
        
        public CellSpeedInitialer()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int row=int.Parse(txtRow.Text);
            int col=int.Parse(txtCol.Text);
            DiaCallBack(row, col);
            this.Dispose();
        }

        private void btnCan_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
