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
    public partial class TickTypeManager : Form
    {
        public TicksModelContainer Context { get; set; }
        private Guid currentID;
        public TickTypeManager()
        {
            InitializeComponent();
        }

        private void SeatTypeManager_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            TickTypeGridView.DataSource = Context.TickTypes.OrderBy(c=>c.CreatDate).Select(c => new {ID=c.ID,票类型名=c.Name,票价=c.Price}).ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtTickName.Text==string.Empty)
            {
                MessageBox.Show("请输入类型名称");
                return;
            }


            if (Context.TickTypes.FirstOrDefault(c => c.Name == txtTickName.Text) != null)
            {
                MessageBox.Show("该类型已经存在");
                return;
            }

            double price;
            if (double.TryParse(txtPrice.Text, out price) == false)
            {
                MessageBox.Show("请输入正确的价格！");
                return;
            }


            if(Context.TickTypes.FirstOrDefault(c=>c.Name==txtTickName.Text)!=null)
            {
                MessageBox.Show("该名称已经存在！");
                return;
            }
            
            Context.TickTypes.Add(new TickType
            {
                ID = Guid.NewGuid(),
                Name = txtTickName.Text,
                Price=price,
                CreatDate=DateTime.Now
            });

            Context.SaveChanges();
            BindGrid();
            
        }

        private void TickTypeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TickTypeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                currentID = new Guid(TickTypeGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                TickType tk = Context.TickTypes.First(c => c.ID == currentID);
                txtTickName.Text = tk.Name;
                txtPrice.Text = tk.Price.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentID == null)
            {
                MessageBox.Show("请选择一条记录");
                return;
            }

            if (txtTickName.Text == string.Empty)
            {
                MessageBox.Show("请输入类型名称");
                return;
            }

            if(Context.TickTypes.FirstOrDefault(c=>c.ID==currentID).Name!=txtTickName.Text)
            {
                if(Context.TickTypes.FirstOrDefault(c=>c.Name==txtTickName.Text)!=null)
                {
                    MessageBox.Show("该名称已经存在！");
                    return;
                }
            }

            double price;
            if (double.TryParse(txtPrice.Text, out price) == false)
            {
                MessageBox.Show("请输入正确的价格！");
                return;
            }

            TickType tk = Context.TickTypes.First(c => c.ID == currentID);
            tk.Name = txtTickName.Text;
            tk.Price = price;
            Context.SaveChanges();
            BindGrid();
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (currentID == null)
            {
                MessageBox.Show("请选择一条记录");
                return;
            }

            try
            {
                Context.TickTypes.Remove(Context.TickTypes.First(c => c.ID == currentID));
                Context.SaveChanges();
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SeatTypeManager_Load_1(object sender, EventArgs e)
        {

        }
    }
}
