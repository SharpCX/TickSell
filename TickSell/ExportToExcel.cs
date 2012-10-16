using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;//needed for DataGridView;also must add a reference to WinForms
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime;
using System.Runtime.InteropServices;//for COMExceptions

namespace MyExcelExport
{
    public class GenericFormattedExcel2003Export
    {
        #region fields
        private string fileName = Application.StartupPath + "\\Exported.CSV";
        private string excelName = Application.StartupPath + "\\Exported.XLS";
        private DataGridView dgv = null;
        private List<ColumnConditions> conds = null;
        private List<ColumnRowConditon> rowConds = null;
        private string exportMode = "CSV";
        #endregion


        #region ctor
        //ctor
        public GenericFormattedExcel2003Export(string exportMode, DataGridView dgv, Theme theme,
            List<ColumnConditions> conds, List<ColumnRowConditon> rowConds, string path)
        {
            this.dgv = dgv;
            this.conds = conds;
            this.rowConds = rowConds;
            this.exportMode = exportMode;
            if (!string.IsNullOrEmpty(path))
            {
                fileName = path;
            }

            if (this.exportMode == "XLS")
            {
                switch (theme)
                {
                    case Theme.BlueSky:
                        BlueSky();
                        break;
                    case Theme.ClassicGray:
                        ClassicGray();
                        break;
                    case Theme.GreenIsGood:
                        GreenIsGood();
                        break;
                    case Theme.NiceViolet:
                        NiceViolet();
                        break;
                    case Theme.LadyInRed:
                        LadyInRed();
                        break;
                    case Theme.DarkBlue:
                        DarkBlue();
                        break;
                    case Theme.OrangeWorks:
                        OrangeWorks();
                        break;
                    case Theme.SweetPink:
                        SweetPink();
                        break;
                }
            }
            else
            {
                switch (theme)
                {
                    case Theme.BlueSky:
                        BlueSkyCSV();
                        break;
                    case Theme.ClassicGray:
                        ClassicGreyCSV();
                        break;
                    case Theme.GreenIsGood:
                        GreenIsGood();
                        break;
                    case Theme.NiceViolet:
                        NiceVioletCSV();
                        break;
                    case Theme.LadyInRed:
                        LadyInRedCSV();
                        break;
                    case Theme.DarkBlue:
                        DarkBlueCSV();
                        break;
                    case Theme.OrangeWorks:
                        OrangeWorksCSV();
                        break;
                    case Theme.SweetPink:
                        SweetPinkCSV();
                        break;
                    case Theme.CSV:
                        CSV();
                        break;
                }
            }
        }

        #endregion


        #region HelperMethods
        //aligns the column if any alignement specified
        private void AlignRange(Excel.Range r, Align align)
        {
            switch (align)
            {
                case Align.None:
                    break;
                case Align.Left:
                    r.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    break;
                case Align.Right:
                    r.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    break;
                case Align.Center:
                    r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    break;
            }
        }

        //applies column number/text formats
        private void ApplyColumnFormats(Excel.Worksheet ws)
        {
            if ((this.conds != null) && (this.conds.Count > 0))
            {
                Excel.Range rrr = null;
                foreach (var item in conds)
                {
                    //get the range
                    Excel.Range upperLimit = (Excel.Range)ws.Cells[2, item.Column];
                    Excel.Range lowerLimit = upperLimit.get_End(
                        Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                    rrr = ws.get_Range(upperLimit, lowerLimit);

                    //apply the extra condition
                    switch (item.Cond)
                    {
                        //euro 2 digits
                        case Conditon.CurrencyEuro:
                            rrr.NumberFormat = String.Format("#,##0.00 [$€]");
                            AlignRange(rrr, item.Alignment);
                            break;
                        //euro 4 digits
                        case Conditon.CurrencyEuro4:
                            rrr.NumberFormat = String.Format("#,##0.0000 [$€]");
                            AlignRange(rrr, item.Alignment);
                            break;
                        //USD 2 digits
                        case Conditon.CurrencyDollar:
                            rrr.NumberFormat = "$#,##0.00";
                            AlignRange(rrr, item.Alignment);
                            break;
                        //USD 4 digits
                        case Conditon.CurrencyDollar4:
                            rrr.NumberFormat = "$#,##0.0000";
                            AlignRange(rrr, item.Alignment);
                            break;
                        //Numeric 2 digits
                        case Conditon.Numeric:
                            rrr.NumberFormat = "#,##0.00";
                            AlignRange(rrr, item.Alignment);
                            break;
                        //numeric 3 digits
                        case Conditon.Numeric3:
                            rrr.NumberFormat = "#,##0.000";
                            AlignRange(rrr, item.Alignment);
                            break;
                        //numeric 4 digits
                        case Conditon.Numeric4:
                            rrr.NumberFormat = "#,##0.0000";
                            AlignRange(rrr, item.Alignment);
                            break;
                        //percentage 2 ditits
                        case Conditon.Percentage:
                            rrr.NumberFormat = "0.00%";
                            AlignRange(rrr, item.Alignment);
                            break;
                        //percentage 4 digits
                        case Conditon.Percentage4:
                            rrr.NumberFormat = "0.0000%";
                            AlignRange(rrr, item.Alignment);
                            break;
                        //Europen/German style date time 
                        case Conditon.DateTime:
                            rrr.NumberFormat = "dd.mm.yy;@";
                            AlignRange(rrr, item.Alignment);
                            break;
                        //US style date time
                        case Conditon.USDateTime:
                            rrr.NumberFormat = "mm/dd/yy;@";
                            AlignRange(rrr, item.Alignment);
                            break;
                        case Conditon.Text:
                            rrr.NumberFormat = "@";
                            AlignRange(rrr, item.Alignment);
                            break;
                        default: break;
                    }
                }
            }
        }

        //creates the two dimensional object[,] from the datagridview
        private object[,] CreateTwoDimensionalObject()
        {
            object[,] datas = new object[dgv.Rows.Count + 1, dgv.Rows[0].Cells.Count];

            //add the first row(the column headers) to the array            
            for (int col = 0; col < dgv.Columns.Count; col++)
            {
                datas[0, col] = dgv.Columns[col].HeaderText;
            }

            //copy the actual datas            
            for (int col = 0; col < dgv.Rows[0].Cells.Count; col++)
            {
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    datas[row + 1, col] = dgv.Rows[row].Cells[col].Value.ToString();
                }
            }

            return datas;
        }
        #endregion


        #region ExportCSVMethods
        //exports in the GreenIsGood theme
        private void GreenIsGoodCSV()
        {
            try
            {
                object None = Type.Missing;
                //export to CSV
                CSVToExcel();

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //open the cvs file
                app.Workbooks.OpenText(fileName, None, None, None,
                Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, None,
                None, None, None, None, None, None, None, None, None, None, None, None);

                //set  visible property
                app.Visible = true;

                Excel.Worksheet ws = (Excel.Worksheet)app.ActiveSheet;
                //set the font for first row(column header(s))
                Excel.Range r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.Bold = true;
                r.EntireRow.Font.Italic = true;
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;


                Excel.Range rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);

                r = ws.get_Range("A1",
                    rightLimit.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None));
                //set up the first row(column headers) interior
                r.Interior.ColorIndex = ThemeColors.GreenIsGoodFirstRowInteriorColor;


                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //initialize to first cell("A1")
                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                //get the end of the non empty cells to the right
                Excel.Range bottomRight = upperLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //get the end looking down
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                //get the adress of the lower right cell
                string downAdress = bottomRight.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None);

                //get all the cells
                r = ws.get_Range("A1", downAdress);

                //create the borders for each cell in the exported range
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                r.Font.ColorIndex = ThemeColors.GreenIsGoodFontColor;
                //set the font for first row(column header(s))
                r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                //format(color) just the second row
                Excel.Range upLeft = (Excel.Range)ws.Cells[2, 1];
                Excel.Range dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //color the interior of the second row
                r = ws.get_Range(upLeft, dRight);
                r.Interior.ColorIndex = ThemeColors.GreenIsGoodInteriorColor;
                //now get the second and third row Range
                dRight = upLeft.get_Offset(1, 0);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                Excel.Range toFormat = ws.get_Range(upLeft, dRight);
                toFormat.Copy(None);
                //now get all the rows and columns
                dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                Excel.Range selection2 = ws.get_Range((Excel.Range)ws.Cells[2, 1], dRight);
                //now paste special -> formats
                selection2.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                     Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                     false, false);

                //column formats
                ApplyColumnFormats(ws);

                //now add the extra formatting by row, if any
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }

                r = (Excel.Range)ws.Cells[1, 1];
                r.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //exports in the SweetViolet theme
        private void NiceVioletCSV()
        {
            try
            {
                object None = Type.Missing;
                //export to CSV
                CSVToExcel();

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //open the cvs file
                app.Workbooks.OpenText(fileName, None, None, None,
                Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, None,
                None, None, None, None, None, None, None, None, None, None, None, None);

                //set  visible property
                app.Visible = true;

                Excel.Worksheet ws = (Excel.Worksheet)app.ActiveSheet;
                //set the font for first row(column header(s))
                Excel.Range r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.Bold = true;
                r.EntireRow.Font.Italic = true;
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;


                Excel.Range rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);

                r = ws.get_Range("A1",
                    rightLimit.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None));
                //set up the first row(column headers) interior
                r.Interior.ColorIndex = ThemeColors.NiceVioletFirstRowInteriorColor;


                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //initialize to first cell("A1")
                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                //get the end of the non empty cells to the right
                Excel.Range bottomRight = upperLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //get the end looking down
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                //get the adress of the lower right cell
                string downAdress = bottomRight.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None);

                //get all the cells
                r = ws.get_Range("A1", downAdress);

                //create the borders for each cell in the exported range
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                r.Font.ColorIndex = ThemeColors.NiceVioletFontColor;
                //set the font for first row(column header(s))
                r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                //format(color) just the second row
                Excel.Range upLeft = (Excel.Range)ws.Cells[2, 1];
                Excel.Range dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //color the interior of the second row
                r = ws.get_Range(upLeft, dRight);
                r.Interior.ColorIndex = ThemeColors.NiceVioletInteriorColor;
                //now get the second and third row Range
                dRight = upLeft.get_Offset(1, 0);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                Excel.Range toFormat = ws.get_Range(upLeft, dRight);
                toFormat.Copy(None);
                //now get all the rows and columns
                dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                Excel.Range selection2 = ws.get_Range((Excel.Range)ws.Cells[2, 1], dRight);
                //now paste special -> formats
                selection2.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                     Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                     false, false);


                //NOW ADD THE EXTRA FORMATTING/CONDITIONS by Columns if any
                ApplyColumnFormats(ws);

                //now add the extra formatting by row, if any
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }

                r = (Excel.Range)ws.Cells[1, 1];
                r.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //exports in the blue sky theme
        private void BlueSkyCSV()
        {
            try
            {
                object None = Type.Missing;
                //export to CSV
                CSVToExcel();

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //open the cvs file
                app.Workbooks.OpenText(fileName, None, None, None,
                Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, None,
                None, None, None, None, None, None, None, None, None, None, None, None);

                //set  visible property
                app.Visible = true;

                Excel.Worksheet ws = (Excel.Worksheet)app.ActiveSheet;
                //set the font for first row(column header(s))
                Excel.Range r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.Bold = true;
                r.EntireRow.Font.Italic = true;
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                Excel.Range rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);

                r = ws.get_Range("A1",
                    rightLimit.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None));
                //set up the first row(column headers) interior
                r.Interior.ColorIndex = ThemeColors.BlueSkyFirstRowInteriorColor;


                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //initialize to first cell("A1")
                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                //get the end of the non empty cells to the right
                Excel.Range bottomRight = upperLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //get the end looking down
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                //get the adress of the lower right cell
                string downAdress = bottomRight.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None);

                //get all the cells
                r = ws.get_Range("A1", downAdress);

                //create the borders for each cell in the exported range
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                r.Font.ColorIndex = ThemeColors.BlueSkyFontColor;
                //set the font for first row(column header(s))
                r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                //format(color) just the second row
                Excel.Range upLeft = (Excel.Range)ws.Cells[2, 1];
                Excel.Range dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //color the interior of the second row
                r = ws.get_Range(upLeft, dRight);
                r.Interior.ColorIndex = ThemeColors.BlueSkyInteriorColor;
                //now get the second and third row Range
                dRight = upLeft.get_Offset(1, 0);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                Excel.Range toFormat = ws.get_Range(upLeft, dRight);
                toFormat.Copy(None);
                //now get all the rows and columns
                dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                Excel.Range selection2 = ws.get_Range((Excel.Range)ws.Cells[2, 1], dRight);
                //now paste special -> formats
                selection2.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                     Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                     false, false);


                //NOW ADD THE EXTRA FORMATTING/CONDITIONS by Columns if any
                ApplyColumnFormats(ws);

                //now add the extra formatting by row, if any
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }

                r = (Excel.Range)ws.Cells[1, 1];
                r.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //exports in the classic gray theme; no extra condition(s)
        private void ClassicGreyCSV()
        {
            try
            {
                object None = Type.Missing;
                //export to CSV
                CSVToExcel();

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //open the cvs file
                app.Workbooks.OpenText(fileName, None, None, None,
                Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, None,
                None, None, None, None, None, None, None, None, None, None, None, None);

                //set  visible property
                app.Visible = true;

                Excel.Worksheet ws = (Excel.Worksheet)app.ActiveSheet;
                //set the font for first row(column header(s))
                Excel.Range r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.Bold = true;
                r.EntireRow.Font.Italic = true;
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;


                Excel.Range rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);

                r = ws.get_Range("A1",
                    rightLimit.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None));
                //set up the first row(column headers) interior
                r.Interior.ColorIndex = ThemeColors.ClassicGrayFirstRowInteriorColor;


                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //initialize to first cell("A1")
                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                //get the end of the non empty cells to the right
                Excel.Range bottomRight = upperLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //get the end looking down
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                //get the adress of the lower right cell
                string downAdress = bottomRight.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None);

                //get all the cells
                r = ws.get_Range("A1", downAdress);

                //create the borders for each cell in the exported range
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                r.Font.ColorIndex = ThemeColors.ClasicGaryFontColor;
                //set the font for first row(column header(s))
                r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                //format(color) just the second row
                Excel.Range upLeft = (Excel.Range)ws.Cells[2, 1];
                Excel.Range dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //color the interior of the second row
                r = ws.get_Range(upLeft, dRight);
                r.Interior.ColorIndex = ThemeColors.ClassicGrayInteriorColor;
                //now get the second and third row Range
                dRight = upLeft.get_Offset(1, 0);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                Excel.Range toFormat = ws.get_Range(upLeft, dRight);
                toFormat.Copy(None);
                //now get all the rows and columns
                dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                Excel.Range selection2 = ws.get_Range((Excel.Range)ws.Cells[2, 1], dRight);
                //now paste special -> formats
                selection2.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                     Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                     false, false);


                //NOW ADD THE EXTRA FORMATTING/CONDITIONS by Columns if any
                ApplyColumnFormats(ws);

                //now add the extra formatting by row, if any
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }

                r = (Excel.Range)ws.Cells[1, 1];
                r.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SweetPinkCSV()
        {
            try
            {
                object None = Type.Missing;
                //export to CSV
                CSVToExcel();

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //open the cvs file
                app.Workbooks.OpenText(fileName, None, None, None,
                Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, None,
                None, None, None, None, None, None, None, None, None, None, None, None);

                //set  visible property
                app.Visible = true;

                Excel.Worksheet ws = (Excel.Worksheet)app.ActiveSheet;
                //set the font for first row(column header(s))
                Excel.Range r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.Bold = true;
                r.EntireRow.Font.Italic = true;
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                Excel.Range rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);

                r = ws.get_Range("A1",
                    rightLimit.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None));
                //set up the first row(column headers) interior
                r.Interior.ColorIndex = ThemeColors.SweetPinkFirstRowInterior;


                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //initialize to first cell("A1")
                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                //get the end of the non empty cells to the right
                Excel.Range bottomRight = upperLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //get the end looking down
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                //get the adress of the lower right cell
                string downAdress = bottomRight.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None);

                //get all the cells
                r = ws.get_Range("A1", downAdress);

                //create the borders for each cell in the exported range
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                r.Font.ColorIndex = ThemeColors.SweetPinkFont;
                //set the font for first row(column header(s))
                r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                //format(color) just the second row
                Excel.Range upLeft = (Excel.Range)ws.Cells[2, 1];
                Excel.Range dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //color the interior of the second row
                r = ws.get_Range(upLeft, dRight);
                r.Interior.ColorIndex = ThemeColors.SweetPinkInterior;
                //now get the second and third row Range
                dRight = upLeft.get_Offset(1, 0);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                Excel.Range toFormat = ws.get_Range(upLeft, dRight);
                toFormat.Copy(None);
                //now get all the rows and columns
                dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                Excel.Range selection2 = ws.get_Range((Excel.Range)ws.Cells[2, 1], dRight);
                //now paste special -> formats
                selection2.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                     Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                     false, false);


                //NOW ADD THE EXTRA FORMATTING/CONDITIONS by Columns if any
                ApplyColumnFormats(ws);

                //now add the extra formatting by row, if any
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }

                r = (Excel.Range)ws.Cells[1, 1];
                r.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OrangeWorksCSV()
        {
            try
            {
                object None = Type.Missing;
                //export to CSV
                CSVToExcel();

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //open the cvs file
                app.Workbooks.OpenText(fileName, None, None, None,
                Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, None,
                None, None, None, None, None, None, None, None, None, None, None, None);

                //set  visible property
                app.Visible = true;

                Excel.Worksheet ws = (Excel.Worksheet)app.ActiveSheet;
                //set the font for first row(column header(s))
                Excel.Range r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.Bold = true;
                r.EntireRow.Font.Italic = true;
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                Excel.Range rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);

                r = ws.get_Range("A1",
                    rightLimit.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None));
                //set up the first row(column headers) interior
                r.Interior.ColorIndex = ThemeColors.OrangeWorksFirstRowInterior;


                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //initialize to first cell("A1")
                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                //get the end of the non empty cells to the right
                Excel.Range bottomRight = upperLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //get the end looking down
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                //get the adress of the lower right cell
                string downAdress = bottomRight.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None);

                //get all the cells
                r = ws.get_Range("A1", downAdress);

                //create the borders for each cell in the exported range
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                r.Font.ColorIndex = ThemeColors.OrangeWorksFont;
                //set the font for first row(column header(s))
                r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                //format(color) just the second row
                Excel.Range upLeft = (Excel.Range)ws.Cells[2, 1];
                Excel.Range dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //color the interior of the second row
                r = ws.get_Range(upLeft, dRight);
                r.Interior.ColorIndex = ThemeColors.OrangeWorksInterior;
                //now get the second and third row Range
                dRight = upLeft.get_Offset(1, 0);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                Excel.Range toFormat = ws.get_Range(upLeft, dRight);
                toFormat.Copy(None);
                //now get all the rows and columns
                dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                Excel.Range selection2 = ws.get_Range((Excel.Range)ws.Cells[2, 1], dRight);
                //now paste special -> formats
                selection2.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                     Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                     false, false);


                //NOW ADD THE EXTRA FORMATTING/CONDITIONS by Columns if any
                ApplyColumnFormats(ws);

                //now add the extra formatting by row, if any
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }

                r = (Excel.Range)ws.Cells[1, 1];
                r.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LadyInRedCSV()
        {
            try
            {
                object None = Type.Missing;
                //export to CSV
                CSVToExcel();

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //open the cvs file
                app.Workbooks.OpenText(fileName, None, None, None,
                Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, None,
                None, None, None, None, None, None, None, None, None, None, None, None);

                //set  visible property
                app.Visible = true;

                Excel.Worksheet ws = (Excel.Worksheet)app.ActiveSheet;
                //set the font for first row(column header(s))
                Excel.Range r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.Bold = true;
                r.EntireRow.Font.Italic = true;
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                Excel.Range rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);

                r = ws.get_Range("A1",
                    rightLimit.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None));
                //set up the first row(column headers) interior
                r.Interior.ColorIndex = ThemeColors.LadyInRedFirstRow;


                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //initialize to first cell("A1")
                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                //get the end of the non empty cells to the right
                Excel.Range bottomRight = upperLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //get the end looking down
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                //get the adress of the lower right cell
                string downAdress = bottomRight.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None);

                //get all the cells
                r = ws.get_Range("A1", downAdress);

                //create the borders for each cell in the exported range
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                r.Font.ColorIndex = ThemeColors.LadyInRedFont;
                //set the font for first row(column header(s))
                r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                //format(color) just the second row
                Excel.Range upLeft = (Excel.Range)ws.Cells[2, 1];
                Excel.Range dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //color the interior of the second row
                r = ws.get_Range(upLeft, dRight);
                r.Interior.ColorIndex = ThemeColors.LadyInRedInterior;
                //now get the second and third row Range
                dRight = upLeft.get_Offset(1, 0);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                Excel.Range toFormat = ws.get_Range(upLeft, dRight);
                toFormat.Copy(None);
                //now get all the rows and columns
                dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                Excel.Range selection2 = ws.get_Range((Excel.Range)ws.Cells[2, 1], dRight);
                //now paste special -> formats
                selection2.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                     Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                     false, false);


                //NOW ADD THE EXTRA FORMATTING/CONDITIONS by Columns if any
                ApplyColumnFormats(ws);

                //now add the extra formatting by row, if any
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }

                r = (Excel.Range)ws.Cells[1, 1];
                r.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DarkBlueCSV()
        {
            try
            {
                object None = Type.Missing;
                //export to CSV
                CSVToExcel();

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //open the cvs file
                app.Workbooks.OpenText(fileName, None, None, None,
                Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, None,
                None, None, None, None, None, None, None, None, None, None, None, None);

                //set  visible property
                app.Visible = true;

                Excel.Worksheet ws = (Excel.Worksheet)app.ActiveSheet;
                //set the font for first row(column header(s))
                Excel.Range r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.Bold = true;
                r.EntireRow.Font.Italic = true;
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                Excel.Range rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);

                r = ws.get_Range("A1",
                    rightLimit.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None));
                //set up the first row(column headers) interior
                r.Interior.ColorIndex = ThemeColors.DarkBlueFirstRow;


                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //initialize to first cell("A1")
                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                //get the end of the non empty cells to the right
                Excel.Range bottomRight = upperLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //get the end looking down
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);

                //get the adress of the lower right cell
                string downAdress = bottomRight.get_Address(false, false,
                    Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1, None, None);

                //get all the cells
                r = ws.get_Range("A1", downAdress);

                //create the borders for each cell in the exported range
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                r.Font.ColorIndex = ThemeColors.DarkBlueFont;
                //set the font for first row(column header(s))
                r = (Excel.Range)ws.Cells[1, 1];
                r.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;

                //format(color) just the second row
                Excel.Range upLeft = (Excel.Range)ws.Cells[2, 1];
                Excel.Range dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //color the interior of the second row
                r = ws.get_Range(upLeft, dRight);
                r.Interior.ColorIndex = ThemeColors.DarkBlueInterior;
                //now get the second and third row Range
                dRight = upLeft.get_Offset(1, 0);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                Excel.Range toFormat = ws.get_Range(upLeft, dRight);
                toFormat.Copy(None);
                //now get all the rows and columns
                dRight = upLeft.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                dRight = dRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                Excel.Range selection2 = ws.get_Range((Excel.Range)ws.Cells[2, 1], dRight);
                //now paste special -> formats
                selection2.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                     Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                     false, false);


                //NOW ADD THE EXTRA FORMATTING/CONDITIONS by Columns if any
                ApplyColumnFormats(ws);

                //now add the extra formatting by row, if any
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }

                r = (Excel.Range)ws.Cells[1, 1];
                r.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //exports to a CSV file and opens it using the default program
        // using Process.Start(fileName);
        private void CSV()
        {
            string cultureSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            System.IO.FileStream fs = null;
            System.IO.StreamWriter sw = null;
            try
            {
                fs = System.IO.File.Create(fileName);
                sw = new System.IO.StreamWriter(fs);

                //write the header/column descriptor
                for (int i = 0; i < dgv.Rows[0].Cells.Count; i++)
                {
                    sw.Write(dgv.Columns[i].HeaderText + cultureSeparator);
                }
                sw.WriteLine();

                int columnsPerRow = dgv.Rows[0].Cells.Count;
                int j = 0;

                StringBuilder sb = null;

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sb = new StringBuilder();
                    for (j = 0; j < columnsPerRow; j++)
                    {
                        sb.Append(dgv.Rows[i].Cells[j].Value.ToString() + cultureSeparator);
                    }
                    sw.WriteLine(sb.ToString());
                }

                //clean up
                sw.Flush();

                fs.Flush();
                fs.Close();
                fs.Dispose();

                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally//clean up in case of failure/exception
            {
                sw = null;
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        //creates the csv file to be opened with excel for
        //further formatting
        private void CSVToExcel()
        {
            string cultureSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            System.IO.FileStream fs = null;
            System.IO.StreamWriter sw = null;
            try
            {
                fs = System.IO.File.Create(fileName);
                sw = new System.IO.StreamWriter(fs);

                //write the header/column descriptor
                for (int i = 0; i < dgv.Rows[0].Cells.Count; i++)
                {
                    sw.Write(dgv.Columns[i].HeaderText + cultureSeparator);
                }
                sw.WriteLine();

                int columnsPerRow = dgv.Rows[0].Cells.Count;
                int j = 0;

                StringBuilder sb = null;

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sb = new StringBuilder();
                    for (j = 0; j < columnsPerRow; j++)
                    {
                        sb.Append(dgv.Rows[i].Cells[j].Value.ToString() + cultureSeparator);
                    }
                    sw.WriteLine(sb.ToString());
                }

                //clean up
                sw.Flush();

                fs.Flush();
                fs.Close();
                fs.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally//clean up in case of failure/exception
            {
                sw = null;
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        #endregion


        #region ExportDirectlyToExcel

        private void GreenIsGood()
        {
            try
            {
                object[,] datas = CreateTwoDimensionalObject();

                object None = Type.Missing;

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;

                Excel.Workbook wk = app.Workbooks.Add(None);
                Excel.Worksheet ws = (Excel.Worksheet)wk.ActiveSheet;

                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                Excel.Range rightLimit = upperLeft.get_Offset(0, dgv.Columns.Count - 1);
                Excel.Range bottomRight = rightLimit.get_Offset(dgv.Rows.Count, 0);
                Excel.Range wholeThing = ws.get_Range(upperLeft, bottomRight);
                wholeThing.Value2 = datas;
                //set the font color
                wholeThing.Font.ColorIndex = ThemeColors.GreenIsGoodFontColor;
                //create the borders
                wholeThing.Borders.Weight = Excel.XlBorderWeight.xlThin;
                wholeThing.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                wholeThing.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

                //format first row            
                upperLeft.EntireRow.Font.Bold = true;
                upperLeft.EntireRow.Font.Italic = true;
                upperLeft.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;
                ws.get_Range(upperLeft, rightLimit).Interior.ColorIndex = ThemeColors.GreenIsGoodFirstRowInteriorColor;

                Excel.Range r;
                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //color the 3rd row
                r = (Excel.Range)ws.Cells[3, 1];
                rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                ws.get_Range(r, rightLimit).Interior.ColorIndex = ThemeColors.GreenIsGoodInteriorColor;

                //get the second and third row
                r = (Excel.Range)ws.Cells[2, 1];
                bottomRight = r.get_Offset(1, 0);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //copy the range
                ws.get_Range(r, bottomRight).Copy(None);

                //get the whole range
                r = (Excel.Range)ws.Cells[4, 1];
                bottomRight = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                wholeThing = ws.get_Range(r, bottomRight);
                //now paste special -> formats
                wholeThing.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                         Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                         false, false);

                //apply column formattings, if any
                ApplyColumnFormats(ws);

                //apply rowcolumn(Bold row) formatting, if any                
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }
                //select cell A1
                upperLeft.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BlueSky()
        {
            try
            {
                object[,] datas = CreateTwoDimensionalObject();

                object None = Type.Missing;

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;

                Excel.Workbook wk = app.Workbooks.Add(None);
                Excel.Worksheet ws = (Excel.Worksheet)wk.ActiveSheet;

                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                Excel.Range rightLimit = upperLeft.get_Offset(0, dgv.Columns.Count - 1);
                Excel.Range bottomRight = rightLimit.get_Offset(dgv.Rows.Count, 0);
                Excel.Range wholeThing = ws.get_Range(upperLeft, bottomRight);
                wholeThing.Value2 = datas;
                //set the font color
                wholeThing.Font.ColorIndex = ThemeColors.BlueSkyFontColor;
                //create the borders
                wholeThing.Borders.Weight = Excel.XlBorderWeight.xlThin;
                wholeThing.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                wholeThing.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

                //format first row            
                upperLeft.EntireRow.Font.Bold = true;
                
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void NiceViolet()
        {
            try
            {
                object[,] datas = CreateTwoDimensionalObject();

                object None = Type.Missing;

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;

                Excel.Workbook wk = app.Workbooks.Add(None);
                Excel.Worksheet ws = (Excel.Worksheet)wk.ActiveSheet;

                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                Excel.Range rightLimit = upperLeft.get_Offset(0, dgv.Columns.Count - 1);
                Excel.Range bottomRight = rightLimit.get_Offset(dgv.Rows.Count, 0);
                Excel.Range wholeThing = ws.get_Range(upperLeft, bottomRight);
                wholeThing.Value2 = datas;
                //set the font color
                wholeThing.Font.ColorIndex = ThemeColors.NiceVioletFontColor;
                //create the borders
                wholeThing.Borders.Weight = Excel.XlBorderWeight.xlThin;
                wholeThing.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                wholeThing.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

                //format first row            
                upperLeft.EntireRow.Font.Bold = true;
                upperLeft.EntireRow.Font.Italic = true;
                upperLeft.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;
                ws.get_Range(upperLeft, rightLimit).Interior.ColorIndex = ThemeColors.NiceVioletFirstRowInteriorColor;

                Excel.Range r;
                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //color the 3rd row
                r = (Excel.Range)ws.Cells[3, 1];
                rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                ws.get_Range(r, rightLimit).Interior.ColorIndex = ThemeColors.LadyInRedInterior;

                //get the second and third row
                r = (Excel.Range)ws.Cells[2, 1];
                bottomRight = r.get_Offset(1, 0);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //copy the range
                ws.get_Range(r, bottomRight).Copy(None);

                //get the whole range
                r = (Excel.Range)ws.Cells[4, 1];
                bottomRight = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                wholeThing = ws.get_Range(r, bottomRight);
                //now paste special -> formats
                wholeThing.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                         Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                         false, false);

                //apply column formattings, if any
                ApplyColumnFormats(ws);

                //apply rowcolumn(Bold row) formatting, if any                
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }
                //select cel A1
                upperLeft.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClassicGray()
        {
            try
            {
                object[,] datas = CreateTwoDimensionalObject();

                object None = Type.Missing;

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;

                Excel.Workbook wk = app.Workbooks.Add(None);
                Excel.Worksheet ws = (Excel.Worksheet)wk.ActiveSheet;

                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                Excel.Range rightLimit = upperLeft.get_Offset(0, dgv.Columns.Count - 1);
                Excel.Range bottomRight = rightLimit.get_Offset(dgv.Rows.Count, 0);
                Excel.Range wholeThing = ws.get_Range(upperLeft, bottomRight);
                wholeThing.Value2 = datas;
                //set the font color
                wholeThing.Font.ColorIndex = ThemeColors.ClasicGaryFontColor;
                //create the borders
                wholeThing.Borders.Weight = Excel.XlBorderWeight.xlThin;
                wholeThing.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                wholeThing.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

                //format first row            
                upperLeft.EntireRow.Font.Bold = true;
                upperLeft.EntireRow.Font.Italic = true;
                upperLeft.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;
                ws.get_Range(upperLeft, rightLimit).Interior.ColorIndex = ThemeColors.ClassicGrayFirstRowInteriorColor;

                Excel.Range r;
                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //color the 3rd row
                r = (Excel.Range)ws.Cells[3, 1];
                rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                ws.get_Range(r, rightLimit).Interior.ColorIndex = ThemeColors.ClassicGrayInteriorColor;

                //get the second and third row
                r = (Excel.Range)ws.Cells[2, 1];
                bottomRight = r.get_Offset(1, 0);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //copy the range
                ws.get_Range(r, bottomRight).Copy(None);

                //get the whole range
                r = (Excel.Range)ws.Cells[4, 1];
                bottomRight = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                wholeThing = ws.get_Range(r, bottomRight);
                //now paste special -> formats
                wholeThing.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                         Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                         false, false);

                //apply column formattings, if any
                ApplyColumnFormats(ws);

                //apply rowcolumn(Bold row) formatting, if any                
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }
                //select cel A1
                upperLeft.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OrangeWorks()
        {
            try
            {
                object[,] datas = CreateTwoDimensionalObject();

                object None = Type.Missing;

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;

                Excel.Workbook wk = app.Workbooks.Add(None);
                Excel.Worksheet ws = (Excel.Worksheet)wk.ActiveSheet;

                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                Excel.Range rightLimit = upperLeft.get_Offset(0, dgv.Columns.Count - 1);
                Excel.Range bottomRight = rightLimit.get_Offset(dgv.Rows.Count, 0);
                Excel.Range wholeThing = ws.get_Range(upperLeft, bottomRight);
                wholeThing.Value2 = datas;
                //set the font color
                wholeThing.Font.ColorIndex = ThemeColors.OrangeWorksFont;
                //create the borders
                wholeThing.Borders.Weight = Excel.XlBorderWeight.xlThin;
                wholeThing.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                wholeThing.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

                //format first row            
                upperLeft.EntireRow.Font.Bold = true;
                upperLeft.EntireRow.Font.Italic = true;
                upperLeft.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;
                ws.get_Range(upperLeft, rightLimit).Interior.ColorIndex = ThemeColors.OrangeWorksFirstRowInterior;

                Excel.Range r;
                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //color the 3rd row
                r = (Excel.Range)ws.Cells[3, 1];
                rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                ws.get_Range(r, rightLimit).Interior.ColorIndex = ThemeColors.OrangeWorksInterior;

                //get the second and third row
                r = (Excel.Range)ws.Cells[2, 1];
                bottomRight = r.get_Offset(1, 0);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //copy the range
                ws.get_Range(r, bottomRight).Copy(None);

                //get the whole range
                r = (Excel.Range)ws.Cells[4, 1];
                bottomRight = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                wholeThing = ws.get_Range(r, bottomRight);
                //now paste special -> formats
                wholeThing.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                         Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                         false, false);

                //apply column formattings, if any
                ApplyColumnFormats(ws);

                //apply rowcolumn(Bold row) formatting, if any                
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }
                //select cel A1
                upperLeft.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SweetPink()
        {
            try
            {
                object[,] datas = CreateTwoDimensionalObject();

                object None = Type.Missing;

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;

                Excel.Workbook wk = app.Workbooks.Add(None);
                Excel.Worksheet ws = (Excel.Worksheet)wk.ActiveSheet;

                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                Excel.Range rightLimit = upperLeft.get_Offset(0, dgv.Columns.Count - 1);
                Excel.Range bottomRight = rightLimit.get_Offset(dgv.Rows.Count, 0);
                Excel.Range wholeThing = ws.get_Range(upperLeft, bottomRight);
                wholeThing.Value2 = datas;
                //set the font color
                wholeThing.Font.ColorIndex = ThemeColors.SweetPinkFont;
                //create the borders
                wholeThing.Borders.Weight = Excel.XlBorderWeight.xlThin;
                wholeThing.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                wholeThing.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

                //format first row            
                upperLeft.EntireRow.Font.Bold = true;
                upperLeft.EntireRow.Font.Italic = true;
                upperLeft.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;
                ws.get_Range(upperLeft, rightLimit).Interior.ColorIndex = ThemeColors.SweetPinkFirstRowInterior;

                Excel.Range r;
                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //color the 3rd row
                r = (Excel.Range)ws.Cells[3, 1];
                rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                ws.get_Range(r, rightLimit).Interior.ColorIndex = ThemeColors.SweetPinkInterior;

                //get the second and third row
                r = (Excel.Range)ws.Cells[2, 1];
                bottomRight = r.get_Offset(1, 0);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //copy the range
                ws.get_Range(r, bottomRight).Copy(None);

                //get the whole range
                r = (Excel.Range)ws.Cells[4, 1];
                bottomRight = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                wholeThing = ws.get_Range(r, bottomRight);
                //now paste special -> formats
                wholeThing.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                         Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                         false, false);

                //apply column formattings, if any
                ApplyColumnFormats(ws);

                //apply rowcolumn(Bold row) formatting, if any                
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }
                //select cel A1
                upperLeft.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LadyInRed()
        {
            try
            {
                object[,] datas = CreateTwoDimensionalObject();

                object None = Type.Missing;

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;

                Excel.Workbook wk = app.Workbooks.Add(None);
                Excel.Worksheet ws = (Excel.Worksheet)wk.ActiveSheet;

                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                Excel.Range rightLimit = upperLeft.get_Offset(0, dgv.Columns.Count - 1);
                Excel.Range bottomRight = rightLimit.get_Offset(dgv.Rows.Count, 0);
                Excel.Range wholeThing = ws.get_Range(upperLeft, bottomRight);
                wholeThing.Value2 = datas;
                //set the font color
                wholeThing.Font.ColorIndex = ThemeColors.LadyInRedFont;
                //create the borders
                wholeThing.Borders.Weight = Excel.XlBorderWeight.xlThin;
                wholeThing.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                wholeThing.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

                //format first row            
                upperLeft.EntireRow.Font.Bold = true;
                upperLeft.EntireRow.Font.Italic = true;
                upperLeft.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;
                ws.get_Range(upperLeft, rightLimit).Interior.ColorIndex = ThemeColors.LadyInRedFirstRow;

                Excel.Range r;
                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //color the 3rd row
                r = (Excel.Range)ws.Cells[3, 1];
                rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                ws.get_Range(r, rightLimit).Interior.ColorIndex = ThemeColors.LadyInRedInterior;

                //get the second and third row
                r = (Excel.Range)ws.Cells[2, 1];
                bottomRight = r.get_Offset(1, 0);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //copy the range
                ws.get_Range(r, bottomRight).Copy(None);

                //get the whole range
                r = (Excel.Range)ws.Cells[4, 1];
                bottomRight = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                wholeThing = ws.get_Range(r, bottomRight);
                //now paste special -> formats
                wholeThing.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                         Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                         false, false);

                //apply column formattings, if any
                ApplyColumnFormats(ws);

                //apply rowcolumn(Bold row) formatting, if any                
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }
                //select cel A1
                upperLeft.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DarkBlue()
        {
            try
            {
                object[,] datas = CreateTwoDimensionalObject();

                object None = Type.Missing;

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;

                Excel.Workbook wk = app.Workbooks.Add(None);
                Excel.Worksheet ws = (Excel.Worksheet)wk.ActiveSheet;

                Excel.Range upperLeft = (Excel.Range)ws.Cells[1, 1];
                Excel.Range rightLimit = upperLeft.get_Offset(0, dgv.Columns.Count - 1);
                Excel.Range bottomRight = rightLimit.get_Offset(dgv.Rows.Count, 0);
                Excel.Range wholeThing = ws.get_Range(upperLeft, bottomRight);
                wholeThing.Value2 = datas;
                //set the font color
                wholeThing.Font.ColorIndex = ThemeColors.DarkBlueFont;
                //create the borders
                wholeThing.Borders.Weight = Excel.XlBorderWeight.xlThin;
                wholeThing.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                wholeThing.Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

                //format first row            
                upperLeft.EntireRow.Font.Bold = true;
                upperLeft.EntireRow.Font.Italic = true;
                upperLeft.EntireRow.Font.ColorIndex = ThemeColors.FirstRowFontColor;
                ws.get_Range(upperLeft, rightLimit).Interior.ColorIndex = ThemeColors.DarkBlueFirstRow;

                Excel.Range r;
                //set the auto fit for each column
                for (int i = 1; i <= dgv.Rows[0].Cells.Count; i++)
                {
                    r = (Excel.Range)ws.Cells[1, i];
                    r.EntireColumn.AutoFit();
                }

                //color the 3rd row
                r = (Excel.Range)ws.Cells[3, 1];
                rightLimit = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                ws.get_Range(r, rightLimit).Interior.ColorIndex = ThemeColors.DarkBlueInterior;

                //get the second and third row
                r = (Excel.Range)ws.Cells[2, 1];
                bottomRight = r.get_Offset(1, 0);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                //copy the range
                ws.get_Range(r, bottomRight).Copy(None);

                //get the whole range
                r = (Excel.Range)ws.Cells[4, 1];
                bottomRight = r.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight);
                bottomRight = bottomRight.get_End(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                wholeThing = ws.get_Range(r, bottomRight);
                //now paste special -> formats
                wholeThing.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats,
                         Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                         false, false);

                //apply column formattings, if any
                ApplyColumnFormats(ws);

                //apply rowcolumn(Bold row) formatting, if any                
                if ((this.rowConds != null) && (this.rowConds.Count > 0))
                {
                    Excel.Range rrr = null;
                    //pass each datagridview row only once
                    foreach (DataGridViewRow dgvItem in dgv.Rows)
                    {
                        foreach (var rowCon in rowConds)
                        {
                            if (dgvItem.Cells[rowCon.Column - 1].Value.ToString().Equals(rowCon.ConditionValue))
                            {
                                rrr = (Excel.Range)ws.Cells[dgvItem.Cells[0].RowIndex + 2, dgvItem.Cells[rowCon.Column - 1].ColumnIndex + 1];
                                rrr.EntireRow.Font.Bold = true;
                            }
                        }
                    }
                }
                //select cel A1
                upperLeft.Select();
            }
            catch (COMException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }


    #region ConditionClasses
    public class ColumnConditions
    {
        private int column = 0;
        private Conditon condition = Conditon.None;
        private Align align = Align.None;

        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        public Conditon Cond
        {
            get { return condition; }
            set { condition = value; }
        }

        public Align Alignment
        {
            get { return align; }
            set { align = value; }
        }
    }

    public class ColumnRowConditon
    {
        private int column = 0;
        private string condValue = "None";

        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        public string ConditionValue
        {
            get { return condValue; }
            set { condValue = value; }
        }
    }
    #endregion


    #region enums
    public enum Conditon
    {
        None,//default(no condition)
        Numeric,//2 digits
        Numeric3,//3 digits
        Numeric4,//4 digits
        Percentage,//2 ditits
        Percentage4,//4 digits
        CurrencyEuro,//2 digits
        CurrencyEuro4,//4 digits
        CurrencyDollar,//2 digits
        CurrencyDollar4,//4 digits
        DateTime,//dd/mm/yyyy
        USDateTime,//mm/dd/yyy
        Text//treats everything as text; usefull for columns with both numeric
        //and text values to align all the cells to the left(default text alignment)       
    }

    public enum Align
    {
        None,//default
        Left,
        Right,
        Center
    }

    public enum Theme
    {
        BlueSky,
        ClassicGray,
        GreenIsGood,
        NiceViolet,
        OrangeWorks,
        SweetPink,
        LadyInRed,
        DarkBlue,
        CSV//no theme
    }

    public enum ThemeColors
    {
        //first row FontColor is WHITE for all the themes
        FirstRowFontColor = 2,
        //the BlueSkyColors
        BlueSkyFirstRowInteriorColor = 11,
        BlueSkyInteriorColor = 37,
        BlueSkyFontColor = 11,
        //the ClassicGrayColors
        ClassicGrayFirstRowInteriorColor = 16,
        ClassicGrayInteriorColor = 15,
        ClasicGaryFontColor = 56,
        //the GreenIsGoodColors
        GreenIsGoodFirstRowInteriorColor = 50,
        GreenIsGoodInteriorColor = 35,
        GreenIsGoodFontColor = 10,
        //the NiceViolet colors
        NiceVioletFirstRowInteriorColor = 13,
        NiceVioletInteriorColor = 39,
        NiceVioletFontColor = 13,

        //the OrangwWorks colors
        OrangeWorksFirstRowInterior = 46,
        OrangeWorksFont = 53,
        OrangeWorksInterior = 40,
        //the SweetPink colors
        SweetPinkFirstRowInterior = 7,
        SweetPinkFont = 13,
        SweetPinkInterior = 38,
        //the LadyInRed colors
        LadyInRedFirstRow = 3,
        LadyInRedFont = 3,
        LadyInRedInterior = 38,
        //the DarkBlue colors
        DarkBlueFirstRow = 55,
        DarkBlueFont = 11,
        DarkBlueInterior = 47
    }

    #endregion
}