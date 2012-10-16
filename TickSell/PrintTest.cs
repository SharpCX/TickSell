using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TickSell
{
    public partial class PrintTest : Form
    {
        public PrintTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> sc=new List<string>();
            string sx = string.Empty;
            foreach(string s in PrinterSettings.InstalledPrinters)
            {
                sc.Add(s);
                sx += s;
            }
            //MessageBox.Show(sx);
            List<PrintMessage> pml = new List<PrintMessage>();

            PrintMessage pm = new PrintMessage
            {
                Hall = "一号大厅",
                Date = "2012-1-1",
                Movie = "钢铁侠",
                No = "1212121",
                Seat = "1号座位",
                SeatType = "一等座",
                Showtimes = "一次",
                TicketPrice = "50.0",
                TicketType = "一等票",
                Time = "12:00-1:00"
            };

            PrintMessage pm2 = new PrintMessage
            {
                Hall = "2号大厅",
                Date = "2012-1-1",
                Movie = "钢铁侠",
                No = "1212121",
                Seat = "1号座位",
                SeatType = "一等座",
                Showtimes = "一次",
                TicketPrice = "50.0",
                TicketType = "一等票",
                Time = "12:00-1:00"
            };
            pml.Add(pm);
            pml.Add(pm2);
            TicketPrinter.printMessages = pml;
            TicketPrinter.Print("xxx", "HP LaserJet 1018");
        }
    }
}
