using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.IO;
using TickSell;
using System.Collections.Generic;

public static class TicketPrinter
{
    public static List<PrintMessage> printMessages;
    public static void Print(string text,string printerName)
    {
        using (PrintDocument prn = new PrintDocument())
        {
            prn.PrinterSettings.DefaultPageSettings.Margins = new Margins();
            prn.PrinterSettings.PrinterName = printerName;
            prn.DocumentName = "ticket";

            prn.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("page1", 100, 200);
            prn.PrintPage += prn_PrintPage;

            prn.Print();
        }
    }

    static void prn_PrintPage(object sender, PrintPageEventArgs e)
    {
        if (printMessages == null && printMessages.Count==0) return;
        int count = 0;
        int PageInterval = 336+30;
        foreach (PrintMessage printMessage in printMessages)
        {

            string No = printMessage.No;
            string hall = printMessage.Hall;
            string seat = printMessage.Seat;
            string date = printMessage.Date;
            string time = printMessage.Time;
            string movie = printMessage.Movie;
            string showtimes = printMessage.Showtimes;
            string seatType = printMessage.SeatType;
            string ticketType = printMessage.TicketType;
            string ticketPrice = printMessage.TicketPrice;
            string userName = printMessage.UserName;

            Font bigfont = new Font("宋体", 12, FontStyle.Regular);
            Font midfont = new Font("宋体", 8, FontStyle.Regular);
            Font smallfont = new Font("宋体", 4, FontStyle.Regular);

            Image im = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image.Jpg"));
            //0，0表示左上角的位置，可以调整。
            e.Graphics.DrawImage(im, 0, 0 + PageInterval*count);

            double xpos = 0.0;
            double ypos = 70;
            double xposCenter = 0.0;
            //SizeF size;

            //size = e.Graphics.MeasureString(hall, font);
            //e.Graphics.DrawString(hall, font, Brushes.Black,new Point(getPix(10),getPix(10)));
            #region 存根
            //号码
            e.Graphics.DrawString(No, bigfont, Brushes.Black, 47, 66 + 10 + PageInterval * count);
            //影厅//座位
            e.Graphics.DrawString(hall, bigfont, Brushes.Black, 47, 114 + 10 + PageInterval * count);
            e.Graphics.DrawString(seat, bigfont, Brushes.Black, 160, 114 + 10 + PageInterval * count);
            //影片
            e.Graphics.DrawString(movie, bigfont, Brushes.Black, 47, 153 + 10 + PageInterval * count);
            //日期//时间
            e.Graphics.DrawString(date, midfont, Brushes.Black, 55, 194 + 18 + PageInterval * count);
            e.Graphics.DrawString(time, midfont, Brushes.Black, 160, 194 + 18 + PageInterval * count);
            //座类//票类//票价
            e.Graphics.DrawString(seatType, midfont, Brushes.Black, 47, 235 + 18 + PageInterval * count);
            e.Graphics.DrawString(ticketType, midfont, Brushes.Black, 117, 235 + 18 + PageInterval * count);
            e.Graphics.DrawString(ticketPrice, midfont, Brushes.Black, 189, 235 + 18 + PageInterval * count);
            //人名
            e.Graphics.DrawString(userName, midfont, Brushes.Black, 47, 285 + 18 + PageInterval * count);
            #endregion

            #region 存根
            //号码
            //e.Graphics.DrawString(No, bigfont, Brushes.Black, 47, 66 + 10);
            //影厅//座位
            e.Graphics.DrawString(hall, midfont, Brushes.Black, 250 + 5, 79 + 10 + PageInterval * count);
            e.Graphics.DrawString(seat, midfont, Brushes.Black, 250 + 5, 131 + 10 + PageInterval * count);
            //影片
            //e.Graphics.DrawString(movie, bigfont, Brushes.Black, 250 + 5, 153 + 10);
            //日期//时间
            e.Graphics.DrawString(date, midfont, Brushes.Black, 250 + 5, 180 + 18 + PageInterval * count);
            e.Graphics.DrawString(time, midfont, Brushes.Black, 250 + 5, 235 + 18 + PageInterval * count);
            //座类//票类//票价
            //e.Graphics.DrawString(seatType, midfont, Brushes.Black, 250 + 5, 235 + 18);
            //e.Graphics.DrawString(ticketType, midfont, Brushes.Black, 250 + 5, 235 + 18);
            //e.Graphics.DrawString(ticketPrice, midfont, Brushes.Black, 250 + 5, 235 + 18);
            //场次
            e.Graphics.DrawString(showtimes, midfont, Brushes.Black, 250 + 5, 282 + 18 + PageInterval * count);
            e.Graphics.DrawString(userName, midfont, Brushes.Black, 250 + 5, 310 + 18 + PageInterval * count);
            count++;
        }
        #endregion
    }
}