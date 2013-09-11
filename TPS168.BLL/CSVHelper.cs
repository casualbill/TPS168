using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;


namespace TPS168.BLL
{
    public class CSVHelper
    {
        public bool SaveToExcel(DataTable dt,string outputFileName)
        {
            return Export(dt, outputFileName,true);
        }
        public bool SaveToCSV(DataTable dt, string outputFileName)
        {
            return Export(dt, outputFileName,false);
        }
        #region 属性
        /// <summary>
        /// 获取或设置要导出的数据源
        /// </summary>
        public DataTable Datatable
        {
            get
            {
                return this._exportData;
            }
            set
            {
                this._exportData = value;
            }
        }


        #region 成员变量声明
        private DataTable _exportData;
        private string _filename = "ExportData.xls";
        private string _title = "";
        private string _extraInfo = "";
        private string title_FontName = "宋体";
        private string title_FontSize = "14";   //Pt
        private bool title_FontBold = true;     //默认粗体

        private const string C_HTTP_HEADER_CONTENT = "Content-Disposition";
        private const string C_HTTP_ATTACHMENT = "attachment;filename=";
        private const string C_HTTP_INLINE = "inline;filename=";
        private const string C_HTTP_CONTENT_TYPE_EXCEL = "application/ms-excel";
        private const string C_HTTP_CONTENT_LENGTH = "Content-Length";
        private const string C_ERROR_NO_RESULT = "Data not found.";

        private const string C_TABLE_START = "<TABLE cellSpacing=1 cellPadding=1   border=1>";
        private const string C_TABLE_END = "</TABLE>";
        #endregion 成员变量声明

        /// <summary>
        /// 获取或设置导出的文件名称
        /// </summary>
        public string FileNameToExport
        {
            get
            {
                return this._filename;
            }
            set
            {
                if (value != null && value.Trim() != "")
                {
                    if (value.IndexOf(".xls") < 1 && value.IndexOf(".csv") < 1)
                    {
                        if (value.IndexOf(".csv") < 1)
                        {
                            this._filename = HttpUtility.UrlEncode(value + ".xls", Encoding.UTF8);
                        }
                        else
                        {
                            this._filename = HttpUtility.UrlEncode(value + ".csv", Encoding.UTF8);
                        }
                    }
                    else
                    {
                        this._filename = HttpUtility.UrlEncode(value, Encoding.UTF8);
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置标题字体
        /// 默认宋体
        /// </summary>
        public string Title_FontName
        {
            get
            {
                return this.title_FontName;
            }
            set
            {
                this.title_FontName = value;
            }
        }

        /// <summary>
        /// 获取或设置标题字体大小
        /// 默认14pt
        /// </summary>
        public string Title_FontSize
        {
            get
            {
                return this.title_FontSize;
            }
            set
            {
                this.title_FontSize = value;
            }
        }

        /// <summary>
        /// 获取或设置标题字体是否粗体
        /// 默认为粗体
        /// </summary>
        public bool Title_FontBold
        {
            get
            {
                return this.title_FontBold;
            }
            set
            {
                this.title_FontBold = value;
            }
        }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                if (value != null)
                    this._title = value;
            }
        }

        /// <summary>
        /// 获取或设置除标题外其他的额外信息
        /// </summary>
        public string ExtraInfo
        {
            get
            {
                return this._extraInfo;
            }
            set
            {
                if (value != null)
                    this._extraInfo = value;
            }

        }
        #endregion 属性

        #region DataTable导入到Excel/CSV
        public bool Export(DataTable srcDataTable, string fileName, bool flag)
        {
            try
            {
                FileNameToExport = fileName;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.AddHeader(C_HTTP_HEADER_CONTENT, C_HTTP_ATTACHMENT + this.FileNameToExport);
                response.ContentType = C_HTTP_CONTENT_TYPE_EXCEL;

                string _exportContent = string.Empty;

                if (srcDataTable != null && srcDataTable.Rows.Count > 0)
                {
                    if (flag)
                    {
                        _exportContent = ConvertDataTableToString(srcDataTable);
                    }
                    else
                    {
                        _exportContent = ConvertDataTableToString_CSV(srcDataTable);
                    }
                }

                if (_exportContent.Length <= 0)
                {
                    _exportContent = C_ERROR_NO_RESULT;
                }

                Encoding encoding = Encoding.GetEncoding("GB2312");
                response.AddHeader(C_HTTP_CONTENT_LENGTH, encoding.GetByteCount(_exportContent).ToString());
                response.BinaryWrite(encoding.GetBytes(_exportContent));
                response.Charset = "GB2312";
                response.End();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private string ConvertDataTableToString(DataTable srcData)
        {
            int ColumnCount = srcData.Columns.Count;
            int RowCount = srcData.Rows.Count;

            StringBuilder ResultBuilder = new StringBuilder();
            ResultBuilder.Length = 0;

            ResultBuilder.Append(C_TABLE_START);

            //插入标题
            if (this._title == "")
                this._title = srcData.TableName;
            if (this._title != "")
            {
                ResultBuilder.Append("<TR><TD HEIGHT=40 colSpan=" + ColumnCount + " align=center style=\"FONT-WEIGHT: " + ((this.title_FontBold) ? Convert.ToString("bold") : Convert.ToString("normal")) + "; FONT-SIZE: " + this.title_FontSize + "pt; FONT-FAMILY: " + this.title_FontName + "\">" + _title + "</TD></TR>");
            }

            //插入额外信息

            if (this._extraInfo != "")
            {
                ResultBuilder.Append("<TR><TD colSpan=" + ColumnCount + ">" + _extraInfo + "</TD></TR>");
                ResultBuilder.Append("<BR>");
            }

            ResultBuilder.Append("<TR>");

            //插入列标题
            foreach (DataColumn aCol in srcData.Columns)
            {
                ResultBuilder.Append("<TD align=center style=\"FONT-WEIGHT: bold\">" + this.ToExcelStr(aCol.Caption) + "</TD>");
            }
            ResultBuilder.Append("</TR>");

            //插入明细内容
            foreach (DataRow aRow in srcData.Rows)
            {
                ResultBuilder.Append("<TR>");
                foreach (DataColumn aCol in srcData.Columns)
                {
                    object obj = aRow[aCol.ColumnName];

                    //下面一句的Style属性设置所有的数字都为字符串格式，即身份证能正常显示
                    ResultBuilder.Append("<TD style='vnd.ms-excel.numberformat:@'>" + ((obj == null) ? " " : this.ToExcelStr(obj.ToString())) + "</TD>");
                }
                ResultBuilder.Append("</TR>");
            }

            ResultBuilder.Append(C_TABLE_END);
            if (ResultBuilder != null)
                return ResultBuilder.ToString();
            else
                return string.Empty;
        }

        private string ConvertDataTableToString_CSV(DataTable srcData)
        {
            int ColumnCount = srcData.Columns.Count;
            int RowCount = srcData.Rows.Count;

            StringBuilder ResultBuilder = new StringBuilder();
            ResultBuilder.Length = 0;

            //插入列标题
            foreach (DataColumn aCol in srcData.Columns)
            {
                ResultBuilder.Append(this.ToExcelStr(aCol.Caption) + ",");
            }
            ResultBuilder.Append("\r\n");

            //插入明细内容
            foreach (DataRow aRow in srcData.Rows)
            {
                foreach (DataColumn aCol in srcData.Columns)
                {
                    object obj = aRow[aCol.ColumnName];

                    //下面一句的Style属性设置所有的数字都为字符串格式，即身份证能正常显示
                    ResultBuilder.Append(((obj == null) ? " " : this.ToExcelStr(obj.ToString())) + ",");
                }
                ResultBuilder.Append("\r\n");
            }
            if (ResultBuilder != null)
                return ResultBuilder.ToString();
            else
                return string.Empty;
        }
        private string ToExcelStr(string srcStr)
        {
            if (srcStr == null || srcStr == "")
                return "";
            else
            {
                string rtnStr = srcStr.Replace(Environment.NewLine, "");
                rtnStr = rtnStr.Replace(" ", " ");
                rtnStr = rtnStr.Replace("<", "＜");
                rtnStr = rtnStr.Replace(">", "＞");
                rtnStr = rtnStr.Replace("\n", "");
                rtnStr = rtnStr.Replace("\r", "");
                return rtnStr;
            }
        }
        #endregion

        #region 将DataReader 转为 DataTable
        /// <summary>
        /// 将DataReader 转为 DataTable
        /// </summary>
        /// <param name="DataReader">DataReader</param>
        public static DataTable ConvertDataReaderToDataTable(SqlDataReader dataReader)
        {
            DataTable datatable = new DataTable();
            DataTable schemaTable = dataReader.GetSchemaTable();
            //动态添加列
            try
            {
                if (dataReader.FieldCount == 0)
                {
                    return datatable;
                }
                foreach (DataRow myRow in schemaTable.Rows)
                {
                    DataColumn myDataColumn = new DataColumn();
                    myDataColumn.DataType = System.Type.GetType("System.String");
                    myDataColumn.ColumnName = myRow[0].ToString();
                    datatable.Columns.Add(myDataColumn);
                }
                //添加数据
                while (dataReader.Read())
                {
                    DataRow myDataRow = datatable.NewRow();
                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        myDataRow[i] = dataReader[i].ToString();
                    }
                    datatable.Rows.Add(myDataRow);
                    myDataRow = null;
                }
                schemaTable = null;
                return datatable;

            }
            catch (Exception ex)
            {
                throw new Exception("DataRead转换为DataTable出错!", ex);
            }
            finally
            {
                dataReader.Close();
                dataReader.Dispose();
            }

        }

        #endregion

    
    }
}
