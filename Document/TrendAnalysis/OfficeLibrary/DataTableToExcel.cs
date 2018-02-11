using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeLibrary
{
    /// <summary>
    /// 数据表转为Excel文档
    /// </summary>
    public class DataTableToExcel
    {
        /// <summary>
        /// 当前操作工作表
        /// </summary>
        protected ExcelWorksheet Worksheet { get; private set; }

        public string StyleFontName { get; set; } = "宋体";

        //public string ExcelStyleNumberFormat { get; set; } = "#,##0.00";

        public string StyleDateFormat { get; set; } = "yyyy-MM-dd";

        public int StyleFontSize { get; set; } = 10;

        public int StyleIndent { get; set; } = 3;


        /// <summary>
        ///     设置表格行列格式
        /// </summary>
        /// <param name="columns"></param>
        protected virtual void SetRowsColumnsStyle(DataColumnCollection columns)
        {
            for (int i = 0, len = columns.Count; i < len; i++)
            {
                var tableColumn = columns[i];
                var column = Worksheet.Column(i + 1);

                column.Style.Font.Name =StyleFontName;
                column.Style.Indent = StyleIndent;
                column.Style.Font.Size = StyleFontSize * 1.2F;

                //默认水平居中,垂直居中
                //column.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                column.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                var columnType = tableColumn.DataType;
                if (columnType == null) continue;
                if (columnType == typeof(DateTime) || columnType == typeof(DateTime?))
                {
                    column.Style.Numberformat.Format = StyleDateFormat;
                }
                //else if (BasicDataExcelHelper.IsNumberType(propertyType))
                //{
                //    column.Style.Numberformat.Format = ExcelStyleNumberFormat;

                //    //column.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}
            }

            //默认行高20
            Worksheet.DefaultRowHeight =StyleFontSize * 2;
        }
        /// <summary>
        ///     写入表格表头
        /// </summary>
        /// <param name="sheetTitle">表头标题</param>
        /// <param name="columnCount">表头总列数</param>
        /// <returns>下一开始行号</returns>
        protected virtual int WriteSheetHeader(string sheetTitle, int columnCount)
        {
            var rowIndex = 1;
            if (columnCount == 0) //不处理表头
                return rowIndex;

            Worksheet.Cells[rowIndex, 1, rowIndex, columnCount].Merge = true;

            var tableHeader = Worksheet.Cells[rowIndex, 1];
            tableHeader.Value = sheetTitle;
            tableHeader.Style.Font.Size = StyleFontSize * 2F;
            tableHeader.Style.Font.Bold = true;
            tableHeader.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            Worksheet.Row(rowIndex).Height = StyleFontSize * 5;

            //返回下一开始行号
            return ++rowIndex;
        }

        public void Export(DataTable table, string fileName,string headerTitle)
        {
            if (table == null || table.Rows.Count == 0)
                throw new Exception("导出错误，数据表为空！");

            //如果文件存在，删除文件
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            using (var bs = new BufferedStream(fs))
            {
                using (var package = new ExcelPackage(bs))
                {
                    var workbook = package.Workbook;
                    var sheetName = string.IsNullOrWhiteSpace(table.TableName) ? "Sheet1" : table.TableName;
                    Worksheet = workbook.Worksheets.Add(sheetName);
                    var columnCount=table.Columns.Count;
                    //设置行列格式
                    SetRowsColumnsStyle(table.Columns);
                    //写入表头
                    var rowIndex=WriteSheetHeader(headerTitle, columnCount);
                    Worksheet.Cells[rowIndex, 1].LoadFromDataTable(table, true);

                    var startRowIndex = rowIndex;
                    var endRowIndex = rowIndex + table.Rows.Count;
                    for (var i = startRowIndex; i <= endRowIndex; i++)
                    {
                        Worksheet.Row(i).Height =StyleFontSize * 2;
                    }
                    var range = Worksheet.Cells[startRowIndex, 1, endRowIndex, columnCount];
                    range.AutoFitColumns();

                    //设置边框
                    SetRangeBorderStyle(range);
                    rowIndex = endRowIndex++;
                    //写入表尾
                    WriteSheetFooter(rowIndex);
                    package.Save();
                    bs.Flush();
                    fs.Flush();
                    bs.Close();
                    fs.Close();                    
                }
            }
        }

        /// <summary>
        ///     写入表格尾部
        /// </summary>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns>下一开始行号</returns>
        protected virtual int WriteSheetFooter(int startRowIndex)
        {
            return startRowIndex;
        }
        /// <summary>
        ///     设置一个区域单元格边框格式
        /// </summary>
        /// <param name="range"></param>
        internal static void SetRangeBorderStyle(ExcelRange range)
        {
            range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }

    }

}
