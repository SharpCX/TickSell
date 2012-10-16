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
    public partial class InputDialog : Form
    {
        private Control btn;
        public InputDialog()
        {
            InitializeComponent();
        }

        public InputDialog(Action<string> Complete):this()
        {
            complete = Complete;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (complete != null)
                complete(txtInput.Text);

            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {
            
        }
        Action<string> complete;

        public void ShowDia()
        {
            ShowDia(this.complete);
        }

        public void ShowDia(Action<string> Complete=null)
        {
            this.Show();
            txtInput.Focus();

            if(Complete!=null)
                complete = Complete;
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar==13)btnOK_Click(btnOK,null);
        }

    }
}
