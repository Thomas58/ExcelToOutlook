using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Excel
{
    public class ExcelManager : IDisposable
    {
        private IWorkbook Workbook;

        #region Constructors
        public ExcelManager(FileInfo file)
        {
            if (file.Extension.ToLower().Equals(".xlsx"))
                Workbook = new XSSFWorkbook(file.OpenRead());
            else if (file.Extension.ToLower().Equals(".xls"))
                Workbook = new HSSFWorkbook(file.OpenRead());
            else throw new Exception("Unknown file extension.");
        }
        #endregion

        public int FindDateColumn()
        {
            ISheet sheet = Workbook.GetSheetAt(0);
            int[] counters = new int[sheet.GetRow(0).LastCellNum];

            try
            {
                IRow row = Workbook.GetSheetAt(0).GetRow(0);
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    if (DateUtil.IsCellDateFormatted(row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK)))
                        counters[i]++;
                }
            }
            catch (Exception e) {}

            try
            {
                IRow row = Workbook.GetSheetAt(0).GetRow(sheet.LastRowNum / 4);
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    if (DateUtil.IsCellDateFormatted(row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK)))
                        counters[i]++;
                }
            }
            catch (Exception e) {}

            try
            {
                IRow row = Workbook.GetSheetAt(0).GetRow(sheet.LastRowNum / 2);
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    if (DateUtil.IsCellDateFormatted(row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK)))
                        counters[i]++;
                }
            }
            catch (Exception e) {}

            try
            {
                IRow row = Workbook.GetSheetAt(0).GetRow((sheet.LastRowNum / 2) + (sheet.LastRowNum / 4));
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    if (DateUtil.IsCellDateFormatted(row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK)))
                        counters[i]++;
                }
            }
            catch (Exception e) {}

            try
            {
                IRow row = Workbook.GetSheetAt(0).GetRow(sheet.LastRowNum);
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    if (DateUtil.IsCellDateFormatted(row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK)))
                        counters[i]++;
                }
            }
            catch (Exception e) {}

            int maxIndex = -1;
            int max = 0;
            for (int i = 0; i < counters.Length; i++)
            {
                if (max < counters[i])
                {
                    maxIndex = i;
                    max = counters[i];
                }
            }
            return maxIndex;
        }
        
        public bool HasHeader = true;

        // Function for reading data from Excel worksheet into DataTable
        public DataTable WorksheetToDataTable(bool hasHeader = true)
        {
            ISheet sheet = Workbook.GetSheetAt(0); // zero-based index of your target sheet
            DataTable dt = new DataTable(sheet.SheetName);

            // write header row
            if (hasHeader)
            {
                IRow headerRow = sheet.GetRow(0);
                foreach (ICell headerCell in headerRow)
                {
                    try
                    {
                        string name = cellToString(headerCell);
                        Regex rgx = new Regex("[^ÆØÅæøåa-zA-Z0-9 -]");
                        name = rgx.Replace(name, "");
                        dt.Columns.Add(name);
                        dt.Columns[dt.Columns.Count - 1].Caption = name;
                    }
                    catch (DuplicateNameException)
                    {
                        var name = cellToString(headerCell);
                        dt.Columns[name].ColumnName = name + 1;
                        dt.Columns.Add(name + 2);
                    }
                }
            }
            else
            {
                for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
                {
                    dt.Columns.Add();
                }
            }

            // write the rest
            for (int i = hasHeader ? 1 : 0; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) break;
                DataRow dataRow = dt.NewRow();
                string[] rowStrings = new string[dataRow.ItemArray.Length];
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    ICell cell = row.GetCell(j, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    rowStrings[j] = cellToString(cell);
                }
                dataRow.ItemArray = rowStrings;
                dt.Rows.Add(dataRow);
            }

            return dt;
        }

        public DateTime ExtractCellToDate(int rowIndex, int columnIndex)
        {
            ISheet sheet = Workbook.GetSheetAt(0);
            IRow row = sheet.GetRow(rowIndex);
            ICell cell = row.GetCell(columnIndex);
            //ICell cell = Workbook.GetSheetAt(0).GetRow(rowIndex).GetCell(columnIndex);
            return (cell != null && cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell)) ? cell.DateCellValue : new DateTime();
        }

        public List<DateTime> ExtractColumnToDates(int columnIndex, bool hasHeader = true)
        {
            ISheet sheet = Workbook.GetSheetAt(0);
            List<DateTime> column = new List<DateTime>();
            
            for (int i = hasHeader ? 1 : 0; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) break;
                ICell cell = row.GetCell(columnIndex);
                if (cell != null && cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                {
                    column.Add(cell.DateCellValue);
                }
                else
                {
                    column.Add(new DateTime());
                }
            }

            return column;
        }

        public string ExtractCellToString(int rowIndex, int columnIndex)
        {
            ICell cell = Workbook.GetSheetAt(0).GetRow(rowIndex).GetCell(columnIndex);
            return (cell != null) ? cellToString(cell) : "";
        }

        public List<string> ExtractColumnToString(int columnIndex, bool hasHeader = true)
        {
            ISheet sheet = Workbook.GetSheetAt(0);
            List<string> column = new List<string>();

            for (int i = hasHeader ? 1 : 0; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) break;
                ICell cell = row.GetCell(columnIndex);
                if (cell != null)
                {
                    column.Add(cellToString(cell));
                }
                else
                {
                    column.Add("");
                }
            }

            return column;
        }

        public int RowCount => Workbook.GetSheetAt(0).LastRowNum + 1;

        public static string cellToString(ICell cell)
        {
            if (cell == null) return "";
            switch (cell.CellType)
            {
                case CellType.Numeric: //Cell has Numeric content.
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue.ToLongDateString();
                    else
                        return NumberToTextConverter.ToText(cell.NumericCellValue);
                case CellType.Formula: //Cell contains a formula.
                    if (cell.CachedFormulaResultType == CellType.String)
                        return cell.StringCellValue;
                    else
                    {
                        if (DateUtil.IsCellDateFormatted(cell))
                            return cell.DateCellValue.ToLongDateString();
                        else
                            return NumberToTextConverter.ToText(cell.NumericCellValue);
                    }
                case CellType.String: //Cell has String content.
                case CellType.Error: //Cell contains an error.
                case CellType.Boolean: //Cell contains a boolean.
                    return cell.StringCellValue;
                default: //Cell is blank.
                    return cell.StringCellValue;
            }
        }

        #region Dispose and Destructor
        public void Dispose()
        {
            if (Workbook != null)
                Workbook.Close();
        }

        ~ExcelManager()
        {
            Dispose();
        }
        #endregion
    }
}
