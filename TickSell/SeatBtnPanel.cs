using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TickSell
{
    public class SeatBtnPanel:FlowLayoutPanel
    {
        bool MouseIsDown = false;
        Rectangle MouseRect = Rectangle.Empty;

        ContextMenuStrip btnEditModeMenu;
        ContextMenuStrip btnSellModeMenu;
        ContextMenuStrip panelEditModeMenu;
        ContextMenuStrip panelSellModeMenu;

        public ContextMenuStrip BtnEditModeMenu
        {
            get { return btnEditModeMenu; }
            set { btnEditModeMenu = value; }
        }
        public ContextMenuStrip BtnSellModeMenu
        {
            get { return btnSellModeMenu; }
            set { btnSellModeMenu = value; }
        }
        public ContextMenuStrip PanelEditModeMenu
        {
            get { return panelEditModeMenu; }
            set { panelEditModeMenu = value; }
        }
        public ContextMenuStrip PanelSellModeMenu
        {
            get { return panelEditModeMenu; }
            set { panelEditModeMenu = value; }
        }

        public SeatBtnPanel()
            : base()
        {
            this.MouseDown += seatBtnPanel_MouseDown;
            this.MouseMove += seatBtnPanel_MouseMove;
            this.MouseUp += seatBtnPanel_MouseUp;
        }

        void seatBtnPanel_MouseUp(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            Cursor.Clip = Rectangle.Empty;
            MouseIsDown = false;
            DrawRectangle();
            if (MouseRect.Size.Width >= 50 && MouseRect.Size.Width >= 50&& this.btnSellModeMenu!=null)
            {
                this.BtnSellModeMenu.Show();
                this.BtnSellModeMenu.Top = e.Location.Y + this.Parent.Location.Y + this.Location.Y;
                this.BtnSellModeMenu.Left = e.Location.X + this.Parent.Location.X + this.Location.X;
            }
            MouseRect = Rectangle.Empty;
            //j选择结束后弹出右键菜单

        }

        void seatBtnPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(MouseIsDown)
            {
                ResizeToRectangle(e.Location);

                int centerHeight = 0;
                int centerWidth = 0;
                int rectTop = MouseRect.Top < MouseRect.Bottom ? MouseRect.Top : MouseRect.Bottom;
                int rectLeft = MouseRect.Left < MouseRect.Right ? MouseRect.Left : MouseRect.Right ;
                int rectBottom = MouseRect.Bottom>MouseRect.Top?MouseRect.Bottom:MouseRect.Top;
                int rectRight = MouseRect.Right>MouseRect.Left?MouseRect.Right:MouseRect.Left;

                //圈定座位
                foreach (SeatButton c in this.Controls)
                {
                    //计算中心
                    centerHeight = c.Top + c.Height / 2;
                    centerWidth = c.Left + c.Width / 2;
                    if (centerHeight > rectTop && centerHeight < rectBottom 
                        && centerWidth > rectLeft && centerWidth < rectRight)
                    {
                        if (c.BackColor == c.SelectedColor)
                        {
                            c.BackColor = c.SelectedColor;
                        }
                        else
                        {
                            c.BackBackColor = c.BackColor;
                            c.BackColor = c.SelectedColor;
                        }
                    }
                    else
                    {
                        if (c.BackColor == c.SelectedColor)
                        {
                            c.BackColor = c.BackBackColor;
                        }
                        else
                        {
                            c.BackColor = c.BackColor;
                        }
                    }
                }
            }
        }

        void seatBtnPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
            DrawStart(e.Location);
        }

        private void DrawRectangle()
        {
            Rectangle rect = this.RectangleToScreen(MouseRect);
            ControlPaint.DrawReversibleFrame(rect,Color.White,FrameStyle.Dashed);
        }

        private void ResizeToRectangle(Point point)
        {
            DrawRectangle();
            MouseRect.Width = point.X - MouseRect.Left;
            MouseRect.Height = point.Y - MouseRect.Top;
            DrawRectangle();
        }

        private void DrawStart(Point point)
        {
            this.Capture = true;
            Cursor.Clip = this.RectangleToScreen(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
            MouseRect = new Rectangle(point.X, point.Y, 0, 0);
        }
    }
}
