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
    public partial class EditSeatMessage : Form
    {
        #region 没用的

        public EditSeatMessage()
        {
            InitializeComponent();

        }

        public new void ShowDialog()
        {
            if (CurrentCell == null)
            {
                MessageBox.Show("需要指定房间");
                this.Close();
            }
            if (CurrentSeat == null && CurrentTimeCellSeat==null)
            {
                MessageBox.Show("需要指定座位号");
                this.Close();
            }
            if (On_Complete == null)
            {
                MessageBox.Show("请指定事件处理程序");
                this.Close();
            }


            base.ShowDialog();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        } 
        #endregion

        public Cell CurrentCell{get;set;}
        public Seat CurrentSeat { get; set; }
        public TimeCellSeat CurrentTimeCellSeat { get; set; }

        public delegate void Complete(int row,int col,double price,string seattype,string ticktype);
        public event Complete On_Complete;

        private TicksModelContainer tkcontainer;
        private void EditSeatMessage_Load(object sender, EventArgs e)
        {
            initialControls();
        }

        private void initialControls()
        {
            try
            {
                try
                {
                    if (comRow.Items.Count != 0)
                        comRow.Items.Clear();
                    if (comCol.Items.Count != 0)
                        comRow.Items.Clear();
                }
                catch
                {
                    MessageBox.Show("1");
                }
                
                try
                {
                    //初始化行
                    for (int i = 1; i <= CurrentCell.RowNum; i++)
                    {
                        comRow.Items.Add(i);
                    }
                    //初始化行
                    for (int i = 1; i <= CurrentCell.ColNum; i++)
                    {
                        comCol.Items.Add(i);
                    }
                    //初始化座位
                    foreach (string s in GlobalVars.SeatTypeList)
                    {
                        comSeatType.Items.Add(s);
                    }
                    //初始化票类
                    foreach (string s in GlobalVars.TicketTypeList)
                    {
                        comTickType.Items.Add(s);
                    }
                }
                catch
                {
                    MessageBox.Show("2");
                }

                if (CurrentSeat == null && CurrentTimeCellSeat == null)
                {
                    MessageBox.Show("抱歉，内部错误！！不能同时不指定两个类型的座位哦！！");
                }

                try{
                    if (CurrentSeat != null)
                    {
                        comRow.Text = CurrentSeat.RowIndex.ToString();
                        comCol.Text = CurrentSeat.ColIndex.ToString();
                        txtPrice.Text = CurrentSeat.TicketPrice.ToString();
                        comSeatType.Text = CurrentSeat.SeatType.ToString();
                        comTickType.Text = CurrentSeat.TicketType.ToString();
                    }
                }
                catch
                {
                    MessageBox.Show("3");
                }

                try{
                    if (CurrentTimeCellSeat != null)
                    {
                        try
                        {
                            comRow.Text = CurrentTimeCellSeat.RowIndex.ToString();
                            comCol.Text = CurrentTimeCellSeat.ColIndex.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("41");
                        }

                        try
                        {
                            txtPrice.Text = CurrentTimeCellSeat.TicketPrice.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("42");
                        }
                        try
                        {
                            comSeatType.Text = CurrentTimeCellSeat.SeatType.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("43");
                        }

                        try
                        {
                            comTickType.Text = CurrentTimeCellSeat.TicketType ?? "";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("44");
                        }

                    }
                }
                catch
                {
                    MessageBox.Show("4");
                }

                if (CurrentSeat != null && CurrentTimeCellSeat != null)
                {
                    MessageBox.Show("抱歉，内部错误！！不能同时指定两个类型的座位哦！！");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                On_Complete(int.Parse(comRow.Text)
                    , int.Parse(comCol.Text)
                    , double.Parse(txtPrice.Text)
                    , comSeatType.Text
                    , comTickType.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("请重新填写信息");
                return;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comSeatType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comTickType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tkcontainer = new TicksModelContainer();

            txtPrice.Text = tkcontainer.TickTypes.FirstOrDefault(c => c.Name == comTickType.Text).Price.ToString();
        }
    }
}
