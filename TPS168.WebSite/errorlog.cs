using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TPS168.WebSite
{
    public class errorlog : System.Web.UI.Page
    {
        public static string logFile;

        public void WriteAML2Log(string strLog)
        {
            string strPath = string.Empty;
            strPath = Server.MapPath("AML2.log");
            //System.IO.FileStream fs=new System.IO.FileStream(strPath,FileMode.OpenOrCreate,FileAccess.ReadWrite);  

            //System.IO.StreamWriter sw=new System.IO.StreamWriter(fs);  
            System.IO.StreamWriter sw = new System.IO.StreamWriter(strPath, true);
            sw.WriteLine("[" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] " + strLog);
            
            sw.Close();
            //fs.Close();   

            sw = null;
            //fs=null;
        }




        /*
        public static void AML2ImportLogInfo(string logFileName)
        {
            SQLDBClient db = new SQLDBClient();
            db.Open(CITI.ChinaGCG.Common.CommonMethod.getRASConnectionString());
            //前提条件是后台验证日志路径和导入文件在同一个目录下
            DataSet ds = db.ExecuteSPDataSet( "SP_AML2_GET_SYS_PARA", new string[]{"AML2_DE03D后台验证日志路径"});
            if(ds!=null && ds.Tables[0].Rows.Count>0)
            {
                uploadPath = ds.Tables[0].Rows[0][0].ToString();
                if(!Directory.Exists(uploadPath+"\\log\\"))
                {
                    try
                    {
                        Scripting.FileSystemObject fso = new Scripting.FileSystemObjectClass();    
                        fso.CreateFolder(uploadPath+"\\log\\");
                        //Directory.CreateDirectory(uploadPath+"\\log\\");
                    }
                    catch(Exception ex)
                    {
                        throw(ex);
                    }
                }
                logFile = uploadPath+"\\log\\"+logFileName;
            }
            DataSet ds2 = db.ExecuteSPDataSet( "SP_AML2_GET_SYS_PARA", new string[]{"AML2_IMPORTLOG后台控制是否写日志参数"});
			
            if(ds2!=null && ds2.Tables[0].Rows.Count>0)
            {
                isLog = ds2.Tables[0].Rows[0][0].ToString() ;
            }
            db.Close();
        }

        */

        public void WriteLog(string strLog)
        {
            StreamWriter streamWriter = null;
            logFile = Server.MapPath("/") + "\\errorlog\\log" + System.DateTime.Now.Date.ToString("yyMMdd") + ".txt";
            
            try
            {
                FileInfo finfo = new FileInfo(logFile);
                if (finfo.Exists && finfo.Length > 10485760)
                {
                    finfo.Delete();
                }
                streamWriter = new StreamWriter(@logFile, true, System.Text.UnicodeEncoding.Unicode);
                streamWriter.WriteLine(System.DateTime.Now.ToString() + "   " + strLog);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw (new Exception(uae.Message + logFile));
            }
            catch (DirectoryNotFoundException de)
            {
                throw (de);
            }
            catch (System.Security.SecurityException se)
            {
                throw (se);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                streamWriter.Flush();
                streamWriter.Close();
            }


        }

        /*
        /// <summary>
        /// 纪录同步、导入、过滤、上传每步动作的数据路LOG
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmd"></param>
        /// <param name="flag"></param>
        /// <param name="type"></param>
        public static void LogUploadStatus(SqlConnection conn, SqlCommand cmd, string flag, string type, string specialDate, string soeid)
        {

            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@FILE_ID",System.Data.SqlDbType.VarChar),
				new SqlParameter("@TXN_DATE",System.Data.SqlDbType.DateTime),
				new SqlParameter("@MAKER",System.Data.SqlDbType.VarChar),
				new SqlParameter("@TYPE",System.Data.SqlDbType.VarChar)
			};
            parameters[0].Value = flag;
            parameters[1].Value = specialDate;
            parameters[2].Value = soeid;
            parameters[3].Value = type;

            cmd.CommandText = "SP_AML2_LOG_UPLOAD_STATUS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            foreach (SqlParameter p in parameters)
            {
                cmd.Parameters.Add(p);
            }
            cmd.ExecuteNonQuery();

        }

        */


    }

}