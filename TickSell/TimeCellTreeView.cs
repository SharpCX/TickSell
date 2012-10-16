using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TickSell.Model;

namespace TickSell
{
    public class TimeCellTreeView:TreeView
    {
        public TimeCellTreeView()
        {
            this.NodeMouseDoubleClick += on_NodeMouseDoubleClick;
        }

        private Cell currentCell;
        private TimeCell currentTimeCell;
        private TicksModelContainer context;
        private SeatBtnPanel flSeats;
        private ContextMenuStrip editModeBtnMenuStrip;
        private ContextMenuStrip sellModeBtnMenuStrip;
        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        public TicksModelContainer Context
        {
            get { return context; }
            set { context = value; }
        }
        public TimeCell CurrentTimeCell
        {
            get { return currentTimeCell; }
            set { 
                currentTimeCell = value; 
                
            }
        }
        public Cell CurrentCell
        {
            get { return currentCell; }
            set 
            {
                currentCell = value;
                boundTreeView();
            }
        }
        private void boundTreeView()
        {
            if (currentCell == null) return;
            
            List<TimeCell> timeCellList;

            if (currentUser.UserLevel == "经理")
            {
                timeCellList = currentCell.TimeCells.OrderBy(c => c.TimeBe).ToList();
            }
            else
            {
                timeCellList = currentCell.TimeCells.Where(c => c.TimeBe.Date >= DateTime.Now.Date).OrderBy(c => c.TimeBe).ToList();
            }
            
            bool isadded = false;

            this.Nodes.Clear();

            foreach (TimeCell tc in timeCellList)
            {
                isadded = false;

                foreach (TreeNode n in this.Nodes)
                {
                    if (n.Text == tc.TimeBe.Date.ToString("yyyy-MM-dd"))
                    {
                        n.Nodes.Add(tc.ID.ToString(), tc.TimeBe.ToString("HH:mm")
                            + "-"
                            + tc.TimeEn.ToString("HH:mm*")
                            + tc.ShowTimes
                            + "*"
                            + tc.MovieName); 
                        isadded = true;
                    }
                }

                if (isadded == false)
                {
                    this.Nodes.Add(tc.TimeBe.Date.ToString("yyyy-MM-dd"));

                    foreach (TreeNode n in this.Nodes)
                    {
                        if (DateTime.Parse(n.Text).Date == tc.TimeBe.Date)
                        {
                            n.Nodes.Add(tc.ID.ToString(), tc.TimeBe.ToString("HH:mm") 
                                + "-"
                                + tc.TimeEn.ToString("HH:mm*") 
                                +tc.ShowTimes
                                +"*"
                                + tc.MovieName);
                        }
                    }
                }
            }
        }
        public SeatBtnPanel FlSeats
        {
            get { return flSeats; }
            set { flSeats = value; }
        }
        public ContextMenuStrip SellModeBtnMenuStrip
        {
            get { return sellModeBtnMenuStrip; }
            set { sellModeBtnMenuStrip = value; }
        }
        public ContextMenuStrip EditModeBtnMenuStrip
        {
            get { return editModeBtnMenuStrip; }
            set { editModeBtnMenuStrip = value; }
        }

        #region 功能函数
        /// <summary>
        /// 初始化座位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void on_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Guid guid = new Guid();
            try
            {
                guid = new Guid(this.SelectedNode.Name);
            }
            catch
            {
                return;
            }

            if (guid == null) return;
            ShowTimeCellSeats(guid);
        }
        public void ShowTimeCellSeats(Guid guid)
        {
            if (guid == null) return;
            TimeCell currentTimeCell;
            try
            {
                currentTimeCell = context.TimeCells.FirstOrDefault(c => c.ID == guid);

                if (currentCell == null)
                {
                    return;
                }
                //当前编辑房间的ID号
                CurrentTimeCell = currentTimeCell;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            try
            {
                initialCellSeat(currentTimeCell);
                this.CurrentTimeCell = currentTimeCell;
                flSeats.ContextMenuStrip = null;
                flSeats.BtnSellModeMenu = ((TS)this.Parent).SellModeSeatMenu;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region 辅助函数

        private void initialCellSeat(TimeCell ccurrentTimeCell)
        {
            IEnumerable<TimeCellSeat> cellList = ccurrentTimeCell.TimeCellSeats.OrderBy(c => c.SeatIndex);

            flSeats.Controls.Clear();

            int row = ccurrentTimeCell.RowNum;
            int col = ccurrentTimeCell.ColNum;
            int all = row * col;

            foreach (TimeCellSeat s in cellList)
            {
                SeatButton btn = new SeatButton();

                btn = new SeatButton();

                btn.SetButtonContent(s);

                btn.ContextMenuStrip = EditModeBtnMenuStrip;
                setSeatsSize(btn, flSeats.Size, row, col);

                flSeats.Controls.Add(btn);
            }
        }

        private void setSeatsSize(Control control, Size lsSize, int Row, int Col)
        {
            double lsWidth = (double)lsSize.Width;
            double lsHeight = (double)lsSize.Height;

            double ctWidth;
            double ctHeight;

            int marginwidth;

            ctWidth = Math.Floor(lsWidth / Col);
            ctHeight = Math.Floor(lsHeight / Row);

            marginwidth = (int)Math.Ceiling(ctWidth / 20.0);

            control.Width = ((int)ctWidth) - 2 * marginwidth;
            control.Height = (int)ctHeight - 2 * marginwidth;

            //这里应该加一个验证座位是不是太小的部分
            control.Margin = new Padding(marginwidth);
        }
        #endregion
    }
}
