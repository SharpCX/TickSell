using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Configuration;
using System.Xml;

namespace TickSell
{
    public partial class SelectPrinter : Form
    {
        public SelectPrinter()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            System.Configuration.ConfigurationManager.AppSettings["printerName"] = "";

            //System.Configuration.ConfigurationSettings.AppSettings["printerName"] = "";
        }

        private void SelectPrinter_Load(object sender, EventArgs e)
        {
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                comPrinter.Items.Add(strPrinter);
            }
        }

        public bool CheckPrinter(PrintDocument objPrintDocument)
        {
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                if (objPrintDocument.PrinterSettings.PrinterName == null)
                {
                    MessageBox.Show("没有检测到可以使用的打印机！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    objPrintDocument.PrinterSettings.PrinterName = strPrinter;

                    PrinterStatus PrinterStatuss = new PrinterStatus();
                    PrinterStatuss = GetPrinterStat(objPrintDocument.PrinterSettings.PrinterName);
                    if (PrinterStatuss.ToString().Equals("空闲"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        enum PrinterStatus 
        {
        其他状态= 1,
        未知,
        空闲,
        正在打印,
        预热,
        停止打印,
        打印中,
        离线
        }
        private PrinterStatus GetPrinterStat(string PrinterDevice)
        {
            PrinterStatus ret = 0;
            string path = @"win32_printer.DeviceId='" + PrinterDevice + "'";
            ManagementObject printer = new ManagementObject(path);
            printer.Get();
            ret = (PrinterStatus)Convert.ToInt32(printer.Properties["PrinterStatus"].Value);
            return ret;
        }

    }
}
