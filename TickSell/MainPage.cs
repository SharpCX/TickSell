using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TickSell.Model;
using System.Threading;
using System.Transactions;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Configuration;

namespace TickSell
{
    public partial class TS : Form
    {
        public User CurrentUser;
        private Cell CurrentCell;
        private TimeCell CurrentTimeCell;
        //private TimeCell CurrentTimeCell;

        private TicksModelContainer context;

        //

        //定义两种座位菜单，一种是编辑模式，一种是售票模式
        //在不影响本来定义的基础上修改菜单
        //这个只作用在
        ContextMenuStrip seatContextMenuStrip;


        public TS()
        {
            InitializeComponent();
        }

        #region 座位操作
        //保存房间及座位
        private void 保存修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TicksModelContainer context = new TicksModelContainer();
            User user = CurrentUser;
            if (user == null)
            {
                context.SaveChanges();
                user = context.Users.FirstOrDefault();
            }

            //得到当前Cell
            //TreeNode tn=tvCells.Nodes[tvCells.SelectedNode.Name];

            //Guid tnId = new Guid(tvCells.SelectedNode.Name);
            if (CurrentCell == null)
            {
                MessageBox.Show("请先选择房间");
                return;
            }

            Cell cell = CurrentCell;

            cell.RowNum = getRowNum();
            cell.ColNum = getColNum();
            //context.SaveChanges();

            int currentIndex = 1;
            int currentRow = 0;
            int currentCol = 0;
            bool isUsed = false;
            Seat seat;
            bool seatisnull = false;

            foreach (SeatButton control in flSeats.Controls)
            {
                //currentRow = (currentIndex / getColNum()) + 1;
                //currentCol = currentIndex % getRowNum();
                //currentCol = (currentCol == 0) ? getColNum() : currentCol;

                isUsed = control.Enabled;
                seat = cell.Seats.SingleOrDefault(c => c.ID == control.SeatId);

                if (seat == null)
                {
                    seatisnull = true;
                    seat = new Seat { ID = control.SeatId };
                }

                seat.SeatName = control.Text;
                seat.Cell = cell;

                //seat.ColIndex = currentCol;
                //seat.RowIndex = currentRow;

                seat.ColIndex = control.ColIndex;
                seat.RowIndex = control.RowIndex;

                seat.SeatIndex = control.SeatIndex;
                seat.IsUsing = control.IsUsing;
                seat.CreatDate = DateTime.Now.ToString();
                seat.User = CurrentUser;
                seat.SeatType = control.SeatType;
                seat.TicketType = control.TicketType;
                seat.TicketPrice = control.TicketPrice;

                if (seatisnull == true)
                {
                    cell.Seats.Add(seat);
                    seatisnull = false;
                }

                currentIndex++;
            }
            context.SaveChanges();
        }

        private string getCellName()
        {
            return CurrentCell.CellName;
        }

        private int getColNum()
        {
            if (CurrentCell == null) return 1;
            return CurrentCell.ColNum;
        }

        private int getRowNum()
        {
            if (CurrentCell == null) return 1;
            return CurrentCell.RowNum;
        }

        //获得座位大小
        private void setSeatsSize(Control control, Size lsSize, int Row, int Col)
        {
            double lsWidth = (double)lsSize.Width;
            double lsHeight = (double)lsSize.Height;

            double ctWidth;
            double ctHeight;

            int marginwidth;
            //int marginheight;

            ctWidth = Math.Floor(lsWidth / Col);
            ctHeight = Math.Floor(lsHeight / Row);

            marginwidth = (int)Math.Ceiling(ctWidth / 20.0);

            control.Width = ((int)ctWidth) - 2 * marginwidth;
            control.Height = (int)ctHeight - 2 * marginwidth;


            //这里应该加一个验证座位是不是太小的部分

            control.Margin = new Padding(marginwidth);
        }
        //快速建立新的房间
        private void releaseCellSeat(FlowLayoutPanel fl, int row, int col)
        {
            flSeats.Controls.Clear();

            int all = row * col;
            CurrentCell.RowNum = row;
            CurrentCell.ColNum = col;

            SeatButton btn = new SeatButton();
            int currentrow = 1;
            int currentcol=1;
            for (int i = 1; i <= all; i++)
            {
                btn = new SeatButton();
                btn.SeatId = Guid.NewGuid();
                btn.SeatIndex = i + 1;
                btn.ContextMenuStrip = MouseMenu;
                btn.SeatType = GlobalVars.SeatTypeList.FirstOrDefault() ;
                btn.TicketPrice = GlobalVars.DefaultPrice;
                btn.TicketType = GlobalVars.TicketTypeList.FirstOrDefault();

                //计算行列
                currentcol = i % col;
                if(i%col==0)
                {
                    currentcol = col;
                }
                btn.ColIndex = currentcol;
                btn.RowIndex = currentrow;

                if (i % col == 0)
                {
                    currentrow++;
                }

                setSeatsSize(btn, flSeats.Size, row, col);
                flSeats.Controls.Add(btn);
            }
        }
        /// <summary>
        /// 删除座位
        /// </summary>
        private void 删除座位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeatButton xbtn = (SeatButton)MouseMenu.SourceControl;
            if (xbtn != null)
            {
                xbtn.BackColor = xbtn.SelectedColor;
            }

            foreach (SeatButton btn in flSeats.Controls)
            {
                if (btn.BackColor != btn.SelectedColor) continue;

                //处理大座位
                Seat s = context.Seats.FirstOrDefault(c => c.ID == btn.SeatId);

                try
                {
                    if (s != null)
                    {
                        s.IsUsing = false;

                        context.SaveChanges();
                        btn.IsUsing = false;
                        btn.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("房间结构已经个改变，请从新调整房间结构。");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改信息出错，请重试。错误信息" + ex.Message);
                    return;
                }
                finally
                {
                    if (s != null)
                    {
                        btn.SetButtonContent(s);
                    }
                }
            }
            int CurrentRolIndex=0;
            int CurrentColIndex = 0;
            foreach (SeatButton btn in flSeats.Controls)
            {
                if (btn.RowIndex != CurrentRolIndex)
                {
                    CurrentRolIndex = btn.RowIndex;
                    CurrentColIndex = 1;
                }

                if (btn.RowIndex == CurrentRolIndex)
                {
                    if (btn.IsUsing == true)
                    {
                        btn.ColIndex = CurrentColIndex;
                        context.Seats.FirstOrDefault(c => c.ID == btn.SeatId).ColIndex = CurrentColIndex;
                        context.SaveChanges();
                        btn.SetButtonContent(context.Seats.FirstOrDefault(c => c.ID == btn.SeatId));
                        CurrentColIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// 修改座位信息
        /// </summary>
        private void 指定座位号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeatButton xbtn = (SeatButton)MouseMenu.SourceControl;
            if (xbtn != null)
            {
                xbtn.BackColor = xbtn.SelectedColor;
            }

            EditSeatMessage esmDia = new EditSeatMessage();
            esmDia.CurrentSeat = context.Seats.FirstOrDefault(c=>c.ID==xbtn.SeatId);
            esmDia.CurrentTimeCellSeat = null;
            esmDia.CurrentCell = CurrentCell;
            esmDia.On_Complete +=(int row,int col,double price,string seattype,string ticketype)=>{

                int count = 1;
                foreach (SeatButton btn in flSeats.Controls)
                {
                    if (btn.BackColor != btn.SelectedColor) continue;
                    count++;
                }

                int i = 1;
                foreach (SeatButton btn in flSeats.Controls)
                {
                    if (btn.BackColor != btn.SelectedColor) continue;

                    Seat sx = context.Seats.FirstOrDefault(c => c.ID == btn.SeatId);
                    if (sx != null)
                    {
                        try
                        {
                            if (i == count)
                            {
                                sx.RowIndex = row;
                                sx.ColIndex = col;
                                i++;
                            }

                            sx.TicketPrice = price;
                            sx.SeatType = seattype;
                            sx.TicketType = ticketype;
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("程序出错了错误信息为：" + ex.Message);
                        }
                        finally
                        {
                            Seat sxx = context.Seats.FirstOrDefault(c => c.ID == btn.SeatId);
                            if (sxx != null)
                                btn.SetButtonContent(sxx);
                        }
                    }
                }
            };

            esmDia.ShowDialog();
        }

        //保存对新座位的修改
        private void Seat_Edit()
        {
            if (CurrentCell == null) MessageBox.Show("请先选择房间");
            foreach(SeatButton c in flSeats.Controls)
            {
                Cell cell = context.Cells.FirstOrDefault(lqcell => lqcell.ID == CurrentCell.ID);
                Seat seat= cell.Seats.FirstOrDefault(lqSeat => lqSeat.ID == c.SeatId);
                seat.SeatName = c.Text;
                seat.IsUsing = c.Enabled;
            }
        }
        //快速生成房间座位
        private void SpeedInitialer_Click(object sender, EventArgs e)
        {
            CellSpeedInitialer dia = new CellSpeedInitialer();
            CurrentCell.Seats.Clear();

            dia.DiaCallBack += (int row, int col) =>
            {
                releaseCellSeat(flSeats, row, col);
            };
            dia.Show();
        }
        //恢复座位
        private void RecoverSeat_Click(object sender, EventArgs e)
        {

            string message = string.Empty;
            string seatNum = string.Empty;

            SeatButton xbtn = (SeatButton)SellModeSeatMenu.SourceControl;
            if (xbtn != null)
            {
                xbtn.BackColor = xbtn.SelectedColor;
            }

            foreach (SeatButton btn in flSeats.Controls)
            {
                if (btn.BackColor != btn.SelectedColor) continue;

                //处理大座位
                Seat s = context.Seats.FirstOrDefault(c => c.ID == btn.SeatId);

                try
                {
                    if (s != null)
                    {
                        s.IsUsing = false;
                        s.SeatName = "";
                        context.SaveChanges();

                    }
                    else
                    {
                        MessageBox.Show("房间结构已经个改变，请从新调整房间结构。");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改信息出错，请重试。错误信息" + ex.Message);
                    return;
                }
                finally
                {
                    Seat sxx = context.Seats.FirstOrDefault(c => c.ID == btn.SeatId);
                    if (sxx != null)
                        btn.SetButtonContent(sxx);
                }
            }
        }

        #endregion


        #region 房间操作
        //初始化房间列表
        private void initialCell(IEnumerable<Cell> cells, TreeNode parent = null)
        {
            try
            {
                foreach (Cell c in cells)
                {
                    if (c.CellParent == null)
                    {
                        tvCells.Nodes.Add(c.ID.ToString(), c.CellName);
                    }
                }

                foreach (Cell c in cells)
                {
                    if (c.CellParent != null && tvCells.Nodes.ContainsKey(c.CellParent.ID.ToString()))
                    {
                        tvCells.Nodes[c.CellParent.ID.ToString()].Nodes.Add(c.ID.ToString(), c.CellName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
 
        //删除房间
        private void DeleteCell_Click(object sender, EventArgs e)
        {
            if (tvCells.SelectedNode == null)
            {
                return;
            }
            else
            {
                Guid cellid = new Guid(tvCells.SelectedNode.Name);
                Cell cell = context.Cells.FirstOrDefault(c => c.ID == cellid);
                context.Cells.Remove(cell);
            }
            context.SaveChanges();
            tvCells.Nodes.Clear();
            initialCell(context.Cells);
        }
        //修改房间名称
        private void EditCellName_Click(object sender, EventArgs e)
        {
            InputDialog id = new InputDialog((s) =>
            {
                if (tvCells.SelectedNode == null)
                {
                    MessageBox.Show("请选择房间");
                }
                else
                {
                    Guid cellid = new Guid(tvCells.SelectedNode.Name);
                    Cell cell = context.Cells.FirstOrDefault(c => c.ID == cellid);
                    tvCells.SelectedNode.Text = s;
                    cell.CellName = s;
                }
                context.SaveChanges();
            });

            id.ShowDia();


        }

        //显示相应房间的座位
        private void initialCellSeat(Cell currentCell)
        {
            IEnumerable<Seat> cellList = currentCell.Seats.OrderBy(c => c.SeatIndex);

            flSeats.Controls.Clear();

            int row = currentCell.RowNum;
            int col = currentCell.ColNum;
            int all = row * col;

            foreach (Seat s in cellList)
            {
                SeatButton btn = new SeatButton();

                btn = new SeatButton();

                btn.SetButtonContent(s);

                btn.ContextMenuStrip = seatContextMenuStrip;
                setSeatsSize(btn, flSeats.Size, row, col);

                flSeats.Controls.Add(btn);
            }

        }
        //显示相应房间的座位的时间安排
        private void tvCells_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
           try
           {
                Guid guid = new Guid();
                guid = new Guid(tvCells.SelectedNode.Name);
                if (guid == null) return;
                ShowCellSeats(guid);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ShowCellSeats(Guid CurrentCellId)
        {
            try
            {
                Guid guid = new Guid();
                guid = CurrentCellId;

                if (guid == null) return;
                Cell currentCell;

                currentCell = context.Cells.FirstOrDefault(c => c.ID == guid);
                if (currentCell == null)
                {
                    return;
                }
                //当前编辑房间的ID号
                CurrentCell = currentCell;

                initialCellSeat(currentCell);
                flSeats.ContextMenuStrip = CellMenu;
                tvTimeCell.CurrentUser = CurrentUser;
                tvTimeCell.CurrentCell = currentCell;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        #endregion
        /// <summary>
        /// 登陆窗体
        /// </summary>
        Login l;
        /// <summary>
        /// 打印机名称
        /// </summary>
        string printerName;
        private void TS_Load(object sender, EventArgs e)
        {
            try
            {
                context = new TicksModelContainer();

                //添加初始用户
                if (context.Users.Count() == 0)
                {
                    context.Users.Add(new User
                    {
                        UserName = "admin",
                        UserPassword = "admin",
                        CreatDate = DateTime.Now.ToString(),
                        UserLevel = "经理"
                    });
                    context.SaveChanges();
                }

                //添加座位类型
                if (context.SeatTypes.Count() == 0)
                {
                    context.SeatTypes.Add(new SeatType
                    {
                        ID=Guid.NewGuid(),
                        Name="一等座",
                        Price=20.0,
                        CreatDate=DateTime.Now
                    });
                    context.SaveChanges();
                }
                //添加票类型
                if (context.SeatTypes.Count() == 0)
                {
                    context.SeatTypes.Add(new SeatType
                    {
                        ID = Guid.NewGuid(),
                        Name = "普通票",
                        Price = 20.0,
                        CreatDate = DateTime.Now
                    });
                    context.SaveChanges();
                }
                
                //初始化房间
                if (context.Cells.Count() == 0)
                {
                    context.Cells.Add(
                        new Cell
                        {
                             CellName="01",
                              CellParent=null,
                               CellText="01",
                                ColNum=10,
                                 RowNum=10,
                                  CreatDate=DateTime.Now.ToString(),
                                   User=CurrentUser,
                                    Father=null
                        }
                        );
                    context.SaveChanges();
                }

                //测试打印机
                printerName = ConfigurationSettings.AppSettings["printerName"].ToString();
                //MessageBox.Show(PrinterSettings.InstalledPrinters.ToString());
                //MessageBox.Show(printerName);
                //设置初始的CellMenu
                seatContextMenuStrip = MouseMenu;

                //初始化列表
                initialCell(context.Cells);

                //初始化一些TimeCell参数
                tvTimeCell.EditModeBtnMenuStrip = SellModeSeatMenu;
                tvTimeCell.SellModeBtnMenuStrip = null;
                //tvTimeCell.ContextMenuStrip = TimeCellMenu;
                tvTimeCell.Context = context;
                tvTimeCell.FlSeats = flSeats;
                CurrentTimeCell = tvTimeCell.CurrentTimeCell;

                tvCells.ContextMenuStrip = TreeViewMenu;

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

                timer.Tick += timer_Tick;
                timer.Start();

                this.Visible = false;

                l = new Login();
                l.On_Exit_Login += () =>
                {
                    if (CurrentUser == null)
                    {
                        this.Close();
                        this.Dispose();
                    }
                };

                l.On_Ok_Login += (u) =>
                {
                    CurrentUser =context.Users.FirstOrDefault(c=>c.ID==u.ID);
                    this.Enabled = true;
                    if (CurrentUser.UserLevel != "经理")
                    {
                        MouseMenu.Items.Clear();
                        TreeViewMenu.Items.Clear();
                        CellMenu.Items.Clear();
                        TimeCellMenu.Items.Clear();
                        btnUserManage.Visible = false;
                        btnStatic.Visible = false;
                        SeatTypeManagement.Visible = false;
                        btnTickManagement.Visible = false;
                    }
                };
                this.Enabled = false;

                //定时刷新座位信息
                //System.Windows.Forms.Timer ticketSellTimer = new System.Windows.Forms.Timer();
                timeFreshSeat.Tick += (d, c) =>
                {
                    try
                    {
                        if (flSeats.Controls.Count == 0) return;
                        foreach (SeatButton sb in flSeats.Controls)
                        {
                            Seat s = context.Seats.FirstOrDefault(cid => cid.ID == sb.SeatId);
                            if (s != null)
                            {
                                sb.SetButtonContent(s);
                            }
                            TimeCellSeat tcs = context.TimeCellSeats.FirstOrDefault(cid => cid.ID == sb.SeatId);
                            if (tcs != null)
                            {
                                sb.SetButtonContent(tcs);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("出错了 " + ex.Message);
                    }
                };
                timeFreshSeat.Interval = 180 * 1000;
                timeFreshSeat.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (CurrentCell == null) CurrentCell = new Cell();
            lblState.Text = "当前处理房间:" + CurrentCell.CellName;
            //
        }

        private void tvCells_Click(object sender, EventArgs e)
        {

        }

        private void 清理房间爱你ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flSeats.Controls.Clear();
            Cell cell=context.Cells.FirstOrDefault(c => c.ID == CurrentCell.ID);

            List<Guid> seatsGuid = new List<Guid>();

            foreach (Guid g in cell.Seats.Select(s => s.ID))
            {
                seatsGuid.Add(new Guid(g.ToString()));
            }
            

            foreach (Guid sid in seatsGuid)
            {
                cell.Seats.Remove(cell.Seats.FirstOrDefault(c=>c.ID==sid));
            }
            context.SaveChanges();
        }

        ///////////////////////////////////////售票模式///////////////////////////////////////
        private void 出售此座位_Click(object sender, EventArgs e)
        {
            try
            {
                SeatButton xbtn = (SeatButton)SellModeSeatMenu.SourceControl;
                if (xbtn != null)
                {
                    xbtn.BackColor = xbtn.SelectedColor;
                }

                foreach (SeatButton sbtn in flSeats.Controls)
                {
                    if (sbtn.BackColor == sbtn.SelectedColor)
                    {
                        xbtn = sbtn;
                        break;
                    }
                }


                EditSeatMessage esmDia = new EditSeatMessage();
                esmDia.CurrentTimeCellSeat = context.TimeCellSeats.FirstOrDefault(c => c.ID == xbtn.SeatId);
                esmDia.CurrentSeat = null;
                esmDia.CurrentCell = CurrentCell;
                esmDia.comRow.Enabled = false;
                esmDia.comCol.Enabled = false;
                esmDia.txtPrice.ReadOnly = true;

                esmDia.On_Complete += (int row, int col, double price, string seattype, string ticketype) =>
                {

                    int count = 1;
                    foreach (SeatButton btn in flSeats.Controls)
                    {
                        if (btn.BackColor != btn.SelectedColor) continue;
                        count++;
                    }

                    List<PrintMessage> pml = new List<PrintMessage>();

                    int i = 1;
                    foreach (SeatButton btn in flSeats.Controls)
                    {
                        if (btn.BackColor != btn.SelectedColor) continue;

                        TimeCellSeat sx = context.TimeCellSeats.FirstOrDefault(c => c.ID == btn.SeatId);
                        if (sx != null)
                        {
                            try
                            {
                                if (i == count)
                                {
                                    sx.RowIndex = row;
                                    sx.ColIndex = col;
                                    i++;
                                }

                                sx.TicketPrice = price;
                                sx.SeatType = seattype;
                                sx.TicketType = ticketype;

                                if (sx.IsUsing == false) continue;
                                if (sx.IsSold)
                                {
                                    MessageBox.Show("座位" + sx.SeatName + "已经售出");
                                    continue;
                                }
                                sx.SoldUser = CurrentUser;
                                sx.IsSold = true;

                                pml.Add(new PrintMessage
                                {
                                    Seat = sx.SeatName,
                                    Hall = sx.TimeCell.Cell.CellName,
                                    Movie = tvTimeCell.CurrentTimeCell.MovieName,
                                    No = sx.ID.ToString().Substring(0,sx.ID.ToString().IndexOf("-")),
                                    SeatType = sx.SeatType,
                                    Showtimes = sx.TimeCell.ShowTimes,
                                    Time = tvTimeCell.CurrentTimeCell.TimeBe.ToShortTimeString(),
                                    TicketPrice = sx.TicketPrice.ToString(),
                                    TicketType = sx.TicketType,
                                    Date = tvTimeCell.CurrentTimeCell.TimeBe.ToShortDateString(),
                                    UserName = CurrentUser.UserName,
                                });
                                
                                
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("程序出错了错误信息为：" + ex.Message);
                            }
                            finally
                            {
                                sx = context.TimeCellSeats.FirstOrDefault(c => c.ID == btn.SeatId);
                                btn.SetButtonContent(sx);
                            }
                        }
                    }
                    try
                    {
                        TicketPrinter.printMessages = pml;
                        TicketPrinter.Print("xxx", printerName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                };

                esmDia.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        ///////////////////////////////////控制切换部分//////////////////////////////////////////
        
        private void btnSwitchMode_Click(object sender, EventArgs e)
        {
            foreach (Control c in flSeats.Controls)
            {
                c.ContextMenuStrip = SellModeSeatMenu;
            }

            flSeats.ContextMenuStrip = null;
            tvCells.ContextMenuStrip = null;
        }

        private void tvCells_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region 修改房间各个时间段的安排
        //处理影片播放信息
        /// <summary>
        /// 增加房间时间段安排信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 增加影片播放信息_Click(object sender, EventArgs e)
        {
            TimeCellDia tcd = new TimeCellDia();
            tcd.Text = "增加影片播放房间信息";
            tcd.On_Complete += (tb, te, mn,st) =>
            {
                Cell curCell=context.Cells.FirstOrDefault(c => c.ID == CurrentCell.ID);
                if (curCell.Seats.Count == 0)
                {
                    MessageBox.Show("您的房间是空房间，或者您生成的房间还未保存。");
                }
                TimeCell tctest= curCell.TimeCells.FirstOrDefault(c => 
                    (c.TimeBe < tb && c.TimeEn > tb) 
                    ||
                    (c.TimeBe < te && c.TimeEn > te)
                    ||
                    (c.TimeBe>tb && c.TimeEn<te)
                    );
                if(tctest!=null)
                {
                    MessageBox.Show("抱歉此电影时间段已经被占用，请修改。");
                    return;
                }
                if (tb >= te)
                {
                    MessageBox.Show("开始时间不能大于等于结束时间");
                    return;
                }
                if (st.Trim() == string.Empty)
                { 
                    MessageBox.Show("请输入场次");
                    return;
                }

                TimeCell tc = new TimeCell
                {
                    ID=Guid.NewGuid()
                    ,
                    TimeBe = tb
                    ,
                    TimeEn = te
                    ,
                    MovieName = mn
                    ,
                    ColNum=CurrentCell.ColNum
                    ,
                    RowNum=CurrentCell.RowNum
                    ,
                    ShowTimes=st
                    
                   
                };

                
                foreach (Seat s in CurrentCell.Seats)
                {
                    tc.TimeCellSeats.Add(Translaters.TranseSeatToTimeCellSeat(s));
                }

                curCell.TimeCells.Add(tc);
                context.SaveChanges();

                tvTimeCell.CurrentCell = CurrentCell;

                tcd.Close();
                tcd.Dispose();
            };

            tcd.Show();
        }

        /// <summary>
        /// 修改房间时间段安排信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TimeCell tc= context.TimeCells.FirstOrDefault(c => c.ID == new Guid(tvTimeCell.SelectedNode.Name));

            TimeCellDia tcd = new TimeCellDia();
            tcd.Text = "增加影片播放房间信息";
            tcd.On_Complete += (tb, te, mn,st) =>
            {
                Cell curCell = context.Cells.FirstOrDefault(c => c.ID == CurrentCell.ID);
                
                tc.TimeBe = tb;
                tc.TimeEn = te;
                tc.MovieName = mn;
                tc.ShowTimes = st;

                curCell.TimeCells.Add(tc);
                context.SaveChanges();

                tvTimeCell.CurrentCell = CurrentCell;
            };

            tcd.Show();
        }


        #endregion
        /// <summary>
        /// 删除房间时间段安排信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try {
                TimeCell tc = context.TimeCells.FirstOrDefault(c => c.ID == new Guid(tvTimeCell.SelectedNode.Name));
                if (tc == null)
                {
                    MessageBox.Show("此房间不存在");
                    return;
                }
                else
                {
                    context.TimeCells.Remove(tc);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            tvTimeCell.CurrentCell = CurrentCell;
        }

        private void flSeats_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TS_Resize(object sender, EventArgs e)
        {
            try
            {
                tvCells.Height = this.Height - tvCells.Top - 40;
                tvTimeCell.Height = this.Height - tvTimeCell.Top - 40;
                flSeats.Height = this.Height - flSeats.Top - 40;
                flSeats.Width = this.Width - flSeats.Left - 40;


                Seat s = null;
                TimeCellSeat tcs = null;

                if (flSeats.Controls.Count != 0)
                {
                    Guid guid=Guid.Empty;

                    foreach (SeatButton sb in flSeats.Controls)
                    {
                        guid= sb.SeatId;
                        break;
                    }

                    s=context.Seats.FirstOrDefault(c=>c.ID==guid);
                    tcs=context.TimeCellSeats.FirstOrDefault(c => c.ID == guid);

                    if (s != null)
                    {
                        ShowCellSeats(s.Cell.ID);
                    }

                    if (tcs != null)
                    {
                        tvTimeCell.ShowTimeCellSeats(tcs.TimeCell.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 取消出售此座位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            message = "座位：{0} 取消失败！";
            string seatNum = string.Empty;

            SeatButton xbtn = (SeatButton)SellModeSeatMenu.SourceControl;
            if (xbtn != null)
            {
                xbtn.BackColor = xbtn.SelectedColor;
            }

            foreach (SeatButton btn in flSeats.Controls)
            {
                if (btn.BackColor != btn.SelectedColor) continue;

                bool isset = false;

                try
                {
                    //处理小座位
                    TimeCellSeat tcs = context.TimeCellSeats.FirstOrDefault(c => c.ID == btn.SeatId);

                    if (tcs != null)
                    {
                        if (tcs.IsUsing == false) continue;
                        if (tcs.IsSold == false)
                        {
                            btn.IsSold = tcs.IsSold;
                            tcs.SoldUser = null;
                            btn.CancelSell();
                            continue;
                        }
                        tcs.IsSold = false;
                        tcs.SoldUser = null;
                        isset = true;
                    }

                    if (isset == false)
                    {
                        MessageBox.Show("请重试");
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改信息出错，请重试。");
                    return;
                }
                btn.CancelSell();
            }

            if (seatNum != string.Empty)
            {
                MessageBox.Show(string.Format(message, seatNum));
            }



        }

        private void TS_Shown(object sender, EventArgs e)
        {
            l.Show();
            l.Activate();
        }

        private void btnUserManage_Click(object sender, EventArgs e)
        {
            UserManager UM = new UserManager();
            UM.context = context;
            UM.Show();
        }

        private void 打印小票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            message = "座位：{0} 取消失败！";
            string seatNum = string.Empty;

            SeatButton xbtn = (SeatButton)SellModeSeatMenu.SourceControl;

            if (xbtn != null)
            {
                xbtn.BackColor = xbtn.SelectedColor;
            }

            foreach (SeatButton sbtn in flSeats.Controls)
            {
                if (sbtn.BackColor == sbtn.SelectedColor)
                {
                    xbtn = sbtn;
                    break;
                }
            }

            List<PrintMessage> pml = new List<PrintMessage>();

            foreach (SeatButton btn in flSeats.Controls)
            {
                if (btn.BackColor != btn.SelectedColor) continue;

                PrintMessage pm = new PrintMessage();
                TimeCellSeat tcs = context.TimeCellSeats.FirstOrDefault(c => c.ID == btn.SeatId);

                try
                {
                    //处理小座位

                    if (tcs != null)
                    {
                        if (tcs.IsUsing == false) continue;
                        if (tcs.IsSold == true)
                        {
                            tcs.SeatName = string.Format("{0}排{1}号", tcs.RowIndex, tcs.ColIndex);
                            pm.Seat = tcs.SeatName;
                            pm.Hall = CurrentCell.CellName;
                            pm.Movie = tvTimeCell.CurrentTimeCell.MovieName;
                            pm.No = tcs.ID.ToString().Substring(0,tcs.ID.ToString().IndexOf("-"));
                            pm.SeatType = tcs.SeatType;
                            pm.Showtimes = tcs.TimeCell.ShowTimes;
                            pm.Time = tvTimeCell.CurrentTimeCell.TimeBe.ToShortTimeString();
                            pm.TicketPrice = tcs.TicketPrice.ToString();
                            pm.TicketType = tcs.TicketType;
                            pm.Date = tvTimeCell.CurrentTimeCell.TimeBe.ToShortDateString();
                            pm.UserName = CurrentUser.UserName;

                            pml.Add(pm);
                            continue;
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改信息出错，请重试。");
                    return;
                }
                finally
                {
                    if (tcs == null)
                    {
                        btn.IsUsing = false;
                    }
                    else
                    {
                        btn.IsSold = tcs.IsSold;
                    }
                }
                
            }

            try
            {
                TicketPrinter.printMessages = pml;
                TicketPrinter.Print("xxx", printerName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void flSeats_MouseEnter(object sender, EventArgs e)
        {
            timeFreshSeat.Stop();
        }

        private void flSeats_MouseLeave(object sender, EventArgs e)
        {
            timeFreshSeat.Start();
        }

        private void TimeCellMenu_Opening(object sender, CancelEventArgs e)
        {

        }
        
        /// <summary>
        /// 添加子房间
        /// </summary>
        private void AddNewCell_Click(object sender, EventArgs e)
        {
            if (tvCells.SelectedNode == null)
            {
                MessageBox.Show("请选择一个节点");
                return;
            }

            Guid selectCellId = new Guid(tvCells.SelectedNode.Name);
            Cell selectCell = context.Cells.FirstOrDefault(
                        c => c.ID == selectCellId);

            if (selectCell == null)
            {
                MessageBox.Show("请重新选择");
                return;
            }

            if (selectCell.CellParent != null)
            {
                MessageBox.Show("请选择根节点");
                return;
            }

            InputDialog id = new InputDialog((s) =>
            {
                Guid guid;
                if (tvCells.SelectedNode != null)
                {
                    guid = Guid.NewGuid();
                    //Guid parentGuid = new Guid(tvCells.SelectedNode.Name);

                    //Cell parent = context.Cells.FirstOrDefault(
                    //    c => c.ID == parentGuid);

                    context.Cells.Add(new Cell
                    {
                        ID = guid,
                        CellName = s,
                        CellText = "xx",
                        ColNum = getColNum(),
                        RowNum = getRowNum(),
                        CellParent = selectCell,
                        CreatDate = DateTime.Now.ToString(),
                        User = CurrentUser
                    }
                    );
                    tvCells.SelectedNode.Nodes.Add(guid.ToString(), s);
                }
                context.SaveChanges();


            });

            id.ShowDia();
        }
        /// <summary>
        /// 添加根房间
        /// </summary>
        private void 添加根房间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputDialog id = new InputDialog((s) =>
            {
                try
                {
                    Guid guid;
                    guid = Guid.NewGuid();
                    //tvCells.Nodes.Add(guid.ToString(), s);
                    context.Cells.Add(new Cell
                    {
                        ID = guid,
                        CellName = s,
                        CellText = "",
                        ColNum = getColNum(),
                        RowNum = getRowNum(),
                        CreatDate = DateTime.Now.ToString(),
                        User = CurrentUser
                    });
                    tvCells.Nodes.Add(guid.ToString(), s);

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出错：" + ex.Message);
                }
            });
                
            id.ShowDia();
        }

        private void btnStatic_Click(object sender, EventArgs e)
        {
            FormStatic fs = new FormStatic();
            fs.Show();
        }

        private void 更改信息并打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void SeatTypeManagement_Click(object sender, EventArgs e)
        {
            SeatTypeManager UM = new SeatTypeManager();
            UM.Context = context;
            UM.Show();
        }

        private void tvTimeCell_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void btnTickManagement_Click(object sender, EventArgs e)
        {
            TickTypeManager UM = new TickTypeManager();
            UM.Context = context;
            UM.Show();
        }                 
    }
}
