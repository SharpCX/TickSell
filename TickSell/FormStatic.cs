using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TickSell.Model;

namespace TickSell
{
    public partial class FormStatic : Form
    {
        public FormStatic()
        {
            InitializeComponent();
            context = new TicksModelContainer();
        }
        TicksModelContainer context;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<staticSum> Lss = new List<staticSum>();
                foreach (User s in context.Users)
                {
                    Lss.Add(new staticSum
                    {
                        用户名 = s.UserName,
                        售票数量 = s.TimeCellSeats
                        .Where(c => c.TimeCell.TimeBe.Date >= dtpBe.Value.Date && c.TimeCell.TimeBe.Date <= dtpEn.Value.Date)
                        .ToList().Count.ToString(),

                        售票总价 = s.TimeCellSeats
                        .Where(c => c.TimeCell.TimeBe.Date >= dtpBe.Value.Date && c.TimeCell.TimeBe.Date <= dtpEn.Value.Date)
                        .Sum(c => c.TicketPrice).ToString()
                    });
                }
                dgvStatic.DataSource = Lss;
                txtAllPrice.Text = Lss.Sum(c => int.Parse(c.售票总价)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormStatic_Load(object sender, EventArgs e)
        {

        }

        private void dgvStatic_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string sName=dgvStatic.Rows[e.RowIndex].Cells[0].Value.ToString();
            User u = context.Users.FirstOrDefault(c => c.UserName == sName);
            if (u == null) return;
            List<staticDetail> Lsd = new List<staticDetail>();
            List<TimeCellSeat> timeSeats = u.TimeCellSeats.OrderBy(c => c.CreatDate)
                .Where(c => c.TimeCell.TimeBe.Date >= dtpBe.Value.Date && c.TimeCell.TimeBe.Date <= dtpEn.Value.Date)
                .ToList();

            foreach (TimeCellSeat tcs in timeSeats)
            {
                Lsd.Add(new staticDetail
                {
                     电影名=tcs.TimeCell.MovieName,
                     播放时间 = tcs.TimeCell.TimeBe.ToString() + tcs.TimeCell.TimeBe.ToString(),
                     影厅=tcs.TimeCell.Cell.CellName,
                     座位信息=tcs.RowIndex + "行" + tcs.ColIndex + "号",
                     票价=tcs.TicketPrice.ToString(),
                     票类=tcs.TicketType,
                     座类=tcs.SeatType
                });
            }
            dgvDetail.DataSource = Lsd;
        }

        private void dgvStatic_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)MenueOutput.SourceControl;
            
            string mode = "XLS";
            List<MyExcelExport.ColumnConditions> conds =
                new List<MyExcelExport.ColumnConditions>();
            MyExcelExport.ColumnConditions curr = null;

            curr = new MyExcelExport.ColumnConditions();
            curr.Column = 2;
            curr.Cond = MyExcelExport.Conditon.CurrencyEuro4;
            conds.Add(curr);

            curr = new MyExcelExport.ColumnConditions();
            curr.Column = 3;
            curr.Cond = MyExcelExport.Conditon.Percentage;
            curr.Alignment = MyExcelExport.Align.Left;
            conds.Add(curr);

            List<MyExcelExport.ColumnRowConditon> rowConds = new List<MyExcelExport.ColumnRowConditon>();
            MyExcelExport.ColumnRowConditon cond = new MyExcelExport.ColumnRowConditon();
            cond.Column = 1;
            cond.ConditionValue = "Total";
            rowConds.Add(cond);
            try
            {
                MyExcelExport.GenericFormattedExcel2003Export gExp =
                    new MyExcelExport.GenericFormattedExcel2003Export(mode, dgv,
                        MyExcelExport.Theme.BlueSky, conds, rowConds, null);

            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenueOutput_Opening(object sender, CancelEventArgs e)
        {

        }

    }
     
    public class staticSum
    {
        public string 用户名 { get; set; }
        public string 售票数量 { get; set; }
        public string 售票总价 { get; set; }
    }
    public class staticDetail
    {
        public string 电影名 { get; set; }
        public string 影厅 { get; set; }
        public string 播放时间 { get; set; }
        public string 座位信息 { get; set; }
        public string 票价 { get; set; }
        public string 座类{get;set;}
        public string 票类{get;set;}
    }
}
