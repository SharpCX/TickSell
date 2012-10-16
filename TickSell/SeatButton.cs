using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TickSell.Model;

namespace TickSell
{
    public class SeatButton:Button
    {
        private void propertyChange()
        {
            this.Text = this.RowIndex + "行" + this.ColIndex+"号"+ Environment.NewLine
                + this.SeatType + Environment.NewLine
                + this.TicketType + Environment.NewLine
                + this.TicketPrice;
        }

        public SeatButton()
            : base()
        { 
            backBackColor=Color.White;
            selectedColor = Color.Blue;
        }

        public void CancelSell()
        { 
            this.BackColor=(new Button()).BackColor;
        }

        private Guid seatId;
        public Guid SeatId
        {
            get {
                if (seatId == null)
                {
                    seatId = Guid.NewGuid();
                }
                
                return seatId; 
            }
            set
            {
                if (value == null)
                {
                    seatId = Guid.NewGuid();
                }
                seatId = value;
            }
        }

        private string ticketType;
        public string TicketType { get { return ticketType; } set { ticketType=value;propertyChange(); } }

        private string seatType;
        public string SeatType { get { return seatType; } set { seatType = value; propertyChange(); } }

        private double ticketPrice;
        public double TicketPrice { get { return ticketPrice; } set { ticketPrice = value; propertyChange(); } }

        private int colIndex;
        public int ColIndex { get { return colIndex; } set { colIndex = value; propertyChange(); } }

        private int rowIndex;
        public int RowIndex { get { return rowIndex; } set { rowIndex = value; propertyChange(); } }

        private bool isUsing;
        public bool IsUsing
        {
            get
            {
                return !(this.Text == "");
            }
            set
            {
                if (value == false)
                {
                    isUsing = false;

                    this.BackColor = (new Button()).BackColor;
                    this.Text = "";
                    this.FlatStyle = FlatStyle.Flat;
                    this.FlatAppearance.BorderColor=(new Button()).BackColor;
                    this.Enabled = false;

                    this.Visible = true;
                }
            }
        }

        private bool isSold;
        public bool IsSold
        {
            get { return (this.BackColor == Color.Red); }
            set {
                if(value==true)
                    this.BackColor = Color.Red;
            }
        }

        private int seatIndex;
        public int SeatIndex
        {
            get { return seatIndex; }
            set { seatIndex = value; }
        }

        private Color backBackColor;
        private Color selectedColor;

        public Color BackBackColor
        {
            get { return backBackColor; }
            set { backBackColor = value; }
        }
        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        public void SetButtonContent(Seat s)
        {
            this.RowIndex = s.RowIndex;
            this.ColIndex = s.ColIndex;
            this.SeatType = s.SeatType;
            this.TicketType = s.TicketType;
            this.TicketPrice = s.TicketPrice??0;

            if (this.isSold)
            {
                this.Text = s.RowIndex + "行" + s.ColIndex + "号" + Environment.NewLine
                    + s.SeatType + Environment.NewLine
                    + s.TicketType + Environment.NewLine
                    + s.TicketPrice;
            }
            else
            {
                this.Text = s.RowIndex + "行" + s.ColIndex + "号" + Environment.NewLine
                    + s.SeatType + Environment.NewLine
                    + s.TicketPrice;
            }

            this.SeatId = s.ID;
            this.IsSold = s.IsSold;
            this.IsUsing = s.IsUsing;

            if (s.IsUsing == true && s.IsSold == false) this.BackColor = (new Button()).BackColor;
            
        }

        public void SetButtonContent(TimeCellSeat s)
        {
            this.RowIndex = s.RowIndex;
            this.ColIndex = s.ColIndex;
            this.SeatType = s.SeatType;
            this.TicketType = s.TicketType;
            this.TicketPrice = s.TicketPrice ?? 0;

            if (s.IsSold)
            {
                this.Text = s.RowIndex + "行" + s.ColIndex + "号" + Environment.NewLine
                    + s.SeatType + Environment.NewLine
                    + s.TicketType + Environment.NewLine
                    + s.TicketPrice;
            }
            else
            {
                this.Text = s.RowIndex + "行" + s.ColIndex + "号" + Environment.NewLine
                    + s.SeatType + Environment.NewLine
                    + s.TicketPrice;
            }

            this.SeatId = s.ID;
            this.IsSold = s.IsSold;
            this.IsUsing = s.IsUsing;

            if (s.IsUsing == true && s.IsSold == false) this.BackColor = (new Button()).BackColor;
        }
    }
}
