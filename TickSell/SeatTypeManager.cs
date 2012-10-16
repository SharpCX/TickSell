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
    public partial class SeatTypeManager : Form
    {
        public TicksModelContainer Context { get; set; }
        private Guid currentID;
        public SeatTypeManager()
        {
            InitializeComponent();
        }

        private void SeatTypeManager_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            SeatTypeGridView.DataSource = Context.SeatTypes.OrderBy(c=>c.CreatDate).Select(c => new {ID=c.ID, 座位类型 = c.Name, 价格 = c.Price }).ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtSeatName.Text==string.Empty)
            {
                MessageBox.Show("请输入类型名称");
                return;
            }

            if (Context.SeatTypes.FirstOrDefault(c => c.Name == txtSeatName.Text) != null)
            {
                MessageBox.Show("该类型已经存在");
                return;
            }

            Context.SeatTypes.Add(new SeatType
            {
                ID = Guid.NewGuid(),
                Name = txtSeatName.Text,
                CreatDate=DateTime.Now
            });
            Context.SaveChanges();
            BindGrid();
        }

        private void SeatTypeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SeatTypeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                currentID = new Guid(SeatTypeGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtSeatName.Text = Context.SeatTypes.First(c=>c.ID==currentID).Name;
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

            if (txtSeatName.Text == string.Empty)
            {
                MessageBox.Show("请输入类型名称");
                return;
            }

            double price = 0.0;

            if (Context.SeatTypes.FirstOrDefault(c => c.ID == currentID).Name != txtSeatName.Text)
            {
                if (Context.SeatTypes.FirstOrDefault(c => c.Name == txtSeatName.Text) != null)
                {
                    MessageBox.Show("该类型已经存在");
                    return;
                }
            }

            SeatType st = Context.SeatTypes.First(c => c.ID == currentID);
            st.Name = txtSeatName.Text;
            st.Price = price;
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
                Context.SeatTypes.Remove(Context.SeatTypes.First(c => c.ID == currentID));
                Context.SaveChanges();
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
