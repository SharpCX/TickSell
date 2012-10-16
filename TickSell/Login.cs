using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TickSell.Model;

namespace TickSell
{
    public partial class Login : Form
    {
        public delegate void on_Ok_Login(User u);
        public event on_Ok_Login On_Ok_Login;

        public delegate void on_Exit_Login();
        public event on_Exit_Login On_Exit_Login;

        TicksModelContainer context;

        public Login()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            User u= context.Users.FirstOrDefault(c => c.UserName == txtUserName.Text && c.UserPassword == txtUserPass.Text);
            if (u == null)
            {
                MessageBox.Show("账号或密码错误");
            }
            else
            {
                if (On_Ok_Login != null)
                {
                    On_Ok_Login(u);
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            context = new TicksModelContainer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (On_Exit_Login != null)
            {
                On_Exit_Login();
                this.Close();
                this.Dispose();
            }
        }

        private void Login_Deactivate(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (On_Exit_Login != null)
            {
                On_Exit_Login();
            }
        }
    }
}
