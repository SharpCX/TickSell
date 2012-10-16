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
    public partial class UserManager : Form
    {
        Guid CurrentId;
        Dictionary<string, string> UserLevel;
        public TicksModelContainer context;
        BindingSource bs;


        public UserManager()
        {
            InitializeComponent();
        }

        private void UserManager_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“ticksDataSet2.Users”中。您可以根据需要移动或删除它。
            //this.usersTableAdapter.Fill(this.ticksDataSet2.Users);

            try
            {
                //context = new TicksModelContainer();
                bs = new BindingSource();
                bs.DataSource = context.Users.ToList();
                dataGridView1.DataSource = bs;

                UserLevel = new Dictionary<string, string>();
                UserLevel.Add("1", "经理");
                UserLevel.Add("2", "员工");
                comLevel.Items.Add(UserLevel["1"]);
                comLevel.Items.Add(UserLevel["2"]);

                txtUserName.Enabled = false;
                txtUserPas.Enabled = false;
                comLevel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Button btn= (Button)sender;
            if (btn.Text == "添加")
            {
                btn.Text = "确定添加";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;

                txtUserName.Enabled = true;
                txtUserPas.Enabled = true;
                comLevel.Enabled = true;

                txtUserName.Text = "";
                txtUserPas.Text = "";
                comLevel.Text = "";
            }
            else 
            {
                btn.Text = "添加";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                txtUserName.Enabled = false;
                txtUserPas.Enabled = false;
                comLevel.Enabled = false;

                if (context.Users.FirstOrDefault(c => c.UserName == txtUserName.Text) != null)
                {
                    MessageBox.Show("用户名重复");
                    return;
                }

                User u = new User();
                u.ID = Guid.NewGuid();
                u.UserName = txtUserName.Text;
                u.UserPassword = txtUserPas.Text;
                u.UserLevel = comLevel.Text;
                u.CreatDate = DateTime.Now.ToString() ;

                context.Users.Add(u);
                context.SaveChanges();
                dataGridView1.DataSource = context.Users.ToList() ;
                MessageBox.Show("添加用户成功。");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //User u = new User();
            //u.ID = Guid.NewGuid();
            //u.UserName = txtUserName.Text;
            //u.Password = txtUserPas;
            //u.UserLevel=
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CurrentId =new Guid(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells["UserName"].Value.ToString());
            txtUserName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtUserPas.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comLevel.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn= (Button)sender;
            if (btn.Text == "修改")
            {
                btn.Text = "确认修改";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;

                txtUserName.Enabled = true;
                txtUserPas.Enabled = true;
                comLevel.Enabled = true;
            }
            else
            {
                btn.Text = "确认修改";
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;

                txtUserName.Enabled = false;
                txtUserPas.Enabled = false ;
                comLevel.Enabled = false;

                User u = context.Users.FirstOrDefault(c=>c.ID==CurrentId);
                if (u == null)
                {
                    return;   
                }

                u.UserName = txtUserName.Text;
                u.UserPassword = txtUserPas.Text;
                u.UserLevel = comLevel.Text;

                context.SaveChanges();
                dataGridView1.DataSource = context.Users.ToList();
                MessageBox.Show("修改用户成功。");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentId == null) return;
            User u = context.Users.FirstOrDefault(c=>c.ID==CurrentId);
            if (u == null) return;
            DialogResult dr= MessageBox.Show("确定要删除" + u.UserName + "??");
            if (dr != DialogResult.OK) return;

            context.Users.Remove(u);
            context.SaveChanges();
            dataGridView1.DataSource = context.Users.ToList();
            MessageBox.Show("删除用户成功。");

        }
    }
}
