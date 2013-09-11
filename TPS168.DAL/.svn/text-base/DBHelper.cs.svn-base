

using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TPS168.DAL
{
	#region struct SQLConnectionString
	public struct SQLConnectionString
	{
		public int ConnectTimeout;
		public string DatabaseName;
		public string IntegratedSecurity;
		public int MaxPoolSize;
		public int MinPoolSize;
		public string Password;
		public bool PersistSecurityInfo;
		public bool Pooling;
		public string ServerName;
		public string UserID;
	}
	#endregion
	
	#region struct RecordsRestrictInfo
	public struct RecordsRestrictInfo
	{
		public string PrimaryKey;
		public int StartPosition;
		public int RecordsCount;
	}
	#endregion
	
	#region DataSetChangesType
	public enum DataSetChangesType
	{ INSERT, MODIFIED, DELETE, OTHERS }
	#endregion

    #region Class DBHelper
    /// <summary>
	/// Description of SQLDBClient.
	/// </summary>
	public class DBHelper : IDisposable
	{
		#region protected variables
		protected SqlConnection sqlDbConnection;
        protected string connectionString;
		protected SqlDataAdapter dataAdapter;
		protected SqlCommandBuilder sqlCommandBuilder;
		protected SqlCommand command;
		protected ArrayList sqlDBClientError;
		protected bool bShowErrorDialog;
		protected ArrayList lstSensitiveKeyWords;
		#endregion
		
		#region Contructor/Destructor
		public DBHelper()
		{
            this.sqlDbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ToString());

            this.connectionString = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
			this.bShowErrorDialog = false;
			this.sqlDBClientError = new ArrayList();
			
			this.dataAdapter = new SqlDataAdapter();
			this.sqlCommandBuilder = new SqlCommandBuilder( this.dataAdapter );
			this.command = new SqlCommand();
			
			// Define the sensitive keywords for function RestrictReturnRecords
			// TODO: Developers can modify here and insert more common sensitive keywords
			this.lstSensitiveKeyWords = new ArrayList();
			this.lstSensitiveKeyWords.Add( "GROUP BY" );
			this.lstSensitiveKeyWords.Add( "HAVING" );
			this.lstSensitiveKeyWords.Add( "ORDER BY" );

		}
		
		~DBHelper()
		{
			Dispose( false );
		}
		#endregion
		
		#region Properties
		public SqlConnection SqlDbConnection
		{
			get{ return this.sqlDbConnection; }
		}
		
		public string ConnectionString
		{
			set{ this.connectionString = value; }
			get{ return this.connectionString; }
		}
		
		public ArrayList SQLDBClientError
		{
			get{ return this.sqlDBClientError; }
		}
		
		public bool ShowErrorDialog
		{
			get{ return this.bShowErrorDialog; }
			set{ this.bShowErrorDialog = value; }
		}
		
		public ArrayList SensitiveKeywords
		{
			get{ return this.lstSensitiveKeyWords; }
			set{ this.lstSensitiveKeyWords = value; }
		}
		#endregion
		
		#region Dispose Pattern
		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( true );
		}
		
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="bool disposing"></param>
		protected virtual void Dispose( bool disposing )
		{
			if ( disposing )
			{
				// Managed Resources
				if( this.sqlDBClientError != null )
				{
					this.sqlDBClientError.Clear();
					this.sqlDBClientError = null;
				}
		    
				if( this.lstSensitiveKeyWords != null )
				{
					this.lstSensitiveKeyWords.Clear();
					this.lstSensitiveKeyWords = null;
				}

				if( this.command != null )
				{
					this.command.Dispose();
				}
				
				if( this.sqlCommandBuilder != null )
				{
					this.sqlCommandBuilder.Dispose();
				}
	    
				if( this.dataAdapter != null )
				{
					this.dataAdapter.Dispose();
				}
	    
				if( this.sqlDbConnection != null )
				{
					this.sqlDbConnection.Dispose();
				}
			}
		
			// Unmanaged Resources
            Close();
		}
		#endregion
		
		#region ToString
		/// <summary>
		/// ToString
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return base.ToString();
		}
		#endregion
		
		#region Open database
		/// <summary>
		/// Open
		/// </summary>
		/// <returns>bool</returns>
		public bool Open()
		{
			try
			{
				this.sqlDbConnection.Open();
				
				return true;
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
				
				return false;
			}
		}
		
		/// <summary>
		/// Open
		/// </summary>
		/// <param name="string strConnectionString"></param>
		/// <returns>bool</returns>
		public bool Open( string strConnectionString )
		{
			try
			{
				this.connectionString = strConnectionString;
				
				this.sqlDbConnection = new SqlConnection();
				this.sqlDbConnection.ConnectionString = this.connectionString;
				
				this.Open();
				
				return true;
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
				
				return false;
			}
		}
		
		/// <summary>
		/// Open
		/// </summary>
		/// <param name="SQLConnectionString"></param>
		/// <returns>bool</returns>
		public bool Open( SQLConnectionString sqlConnectionString )
		{
			try
			{
				if( sqlConnectionString.ServerName == string.Empty || sqlConnectionString.ServerName == null || sqlConnectionString.DatabaseName == string.Empty || sqlConnectionString.DatabaseName == null )
				{
					throw new Exception( "ConnectionString missing ServerName or DatabaseName value." );
				}
				
				this.connectionString = this.ConnectionStringBuilder( sqlConnectionString );
				
				this.sqlDbConnection = new SqlConnection();
				this.sqlDbConnection.ConnectionString = this.connectionString;
				
				this.Open();
							
				return true;
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
				
				return false;
			}
		}
		#endregion
		
		#region Close database connection
		/// <summary>
		/// Close
		/// </summary>
		public void Close()
		{
			this.sqlDBClientError.Clear();
			
			if( this.sqlDbConnection == null )
			{
				return;
			}
			
			try
			{
				if( this.sqlDbConnection.State != ConnectionState.Closed )
				{
					this.sqlDbConnection.Close();
				}
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
			}
		}
		#endregion
		
		#region Execute SQL command with Update, Insert, and Delete
		/// <summary>
		/// ExecuteNonQuery
		/// </summary
		/// <param name="CommandType commandType"></param>
		/// <param name="string commandText"></param>
		/// <returns>int</returns>
		public int ExecuteNonQuery( string commandText, params SqlParameter [] parameters )
		{
			int iResult = -1;

			try
			{
				if( this.command == null )
				{
					this.command = new SqlCommand();
				}

				if( this.sqlDbConnection.State == ConnectionState.Closed )
				{
					throw new Exception( "Database Connection is closed." );
				}
			
				this.command.Connection = this.sqlDbConnection;
				this.command.CommandType = CommandType.Text;
				this.command.CommandText = commandText;
				this.command.CommandTimeout=300; //extend the timeout from default 30 to 300

				if( parameters != null )
				{
					if( !this.AttachSQLParameters( this.command, parameters ) )
					{
						throw new Exception( "Failed to attach the SQL parameters to command." );
					}
				}
				
				iResult = this.command.ExecuteNonQuery();
				return iResult;
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
				return -1;
			}
		}
		#endregion
		
		#region Execute SQL command with Select script
		/// <summary>
		/// ExecuteCommandScript
		/// </summary
		/// <param name="CommandType commandType"></param>
		/// <param name="string commandText"></param>
		/// <returns>DataSet</returns>
		public DataSet ExecuteCommandScript( string commandText, RecordsRestrictInfo recordsRestrictInfo, params SqlParameter [] parameters )
		{
			string tmpCommand = string.Empty;
			bool bConnectionOpened = true;
			DataSet dsResult = new DataSet();
			
			if( this.sqlDbConnection.State == ConnectionState.Closed )
			{
				throw new Exception( "Database Connection is closed." );
			}

			if( this.dataAdapter == null )
			{
				this.dataAdapter = new SqlDataAdapter();
			}

			if( this.command == null )
			{
				this.command = new SqlCommand();
			}
			
			SqlTransaction transaction = this.sqlDbConnection.BeginTransaction();
			
			tmpCommand = this.RestrictReturnRecords( commandText, recordsRestrictInfo );
			
			try
			{
				this.command.Connection = this.sqlDbConnection;
				this.command.Transaction = transaction;
				this.command.CommandType = CommandType.Text;
				this.command.CommandText = tmpCommand;
				this.command.CommandTimeout=300; //extend the timeout from default 30 to 300
				
				if( parameters != null )
				{
					if( !this.AttachSQLParameters( this.command, parameters ) )
					{
						throw new Exception( "Failed to attach the SQL parameters to command." );
					}
				}
				
				this.dataAdapter.SelectCommand = this.command;
				this.dataAdapter.Fill( dsResult );
				
				transaction.Commit();
				
				if( !bConnectionOpened )
				{
					this.sqlDbConnection.Close();
				}
				
				return dsResult;
			}
			catch( Exception ex )
			{
				try
				{
					transaction.Rollback();
				}
				catch( Exception exc )
				{
					this.ErrorLog( exc );
				}
				
				this.ErrorLog( ex );
				return null;
			}
			finally
			{
				dsResult.Dispose();
			}
		}
		#endregion
		
		#region Execute SQL command with stored proc and params, used by Insert, Update, and Delete
		/// <summary>
		/// ExecuteCommandScript
		/// </summary
		/// <param name="CommandType commandType"></param>
		/// <param name="string commandText"></param>
		/// <returns>DataSet</returns>
		public int ExecuteSPNonQuery( string strStoredProc, params string[] parameters )
		{
			int iResult = -1;
            int retVal;

			SqlParameter [] sqlParameter;
			
			try
			{
				if( this.sqlDbConnection.State == ConnectionState.Closed )
				{
					throw new Exception( "Database Connection is closed." );
				}

				if( this.command == null )
				{
					this.command = new SqlCommand();
				}
				
				this.command.Connection = this.sqlDbConnection;
				this.command.CommandType = CommandType.StoredProcedure;
				this.command.CommandText = strStoredProc;
				this.command.CommandTimeout=1500; //extend the timeout from default 300 to 1500 modified by qiujiaqi 20110217
				
				if( parameters != null )
				{
					sqlParameter = this.SQLParametersMapping( strStoredProc, parameters );
					if( sqlParameter == null )
					{
						throw new Exception( "SQL parameters mapping returned null." );
					}
					
					if( !this.AttachSQLParameters( this.command, sqlParameter ) )
					{
						throw new Exception( "Failed to attach the SQL parameters to command." );
					}
				}
				

                this.command.Parameters["@retVal"].Direction = ParameterDirection.Output;
                iResult = this.command.ExecuteNonQuery();

                retVal = Convert.ToInt16(this.command.Parameters["@retVal"].Value);

                return retVal;
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
				return -1;
			}
		}
		#endregion
		
		#region Execute SQL command with stored proc and params, used by Select
		/// <summary>
		/// ExecuteCommandScript
		/// </summary
		/// <param name="CommandType commandType"></param>
		/// <param name="string commandText"></param>
		/// <returns>DataSet</returns>
		public DataSet ExecuteSPDataSet( string strStoredProc, params string[] parameters )
		{
			SqlParameter [] sqlParameter;
			DataSet dsResult = new DataSet();
			
			try
			{
				if( this.sqlDbConnection.State == ConnectionState.Closed )
				{
					throw new Exception( "Database Connection is closed." );
				}

				if( this.dataAdapter == null )
				{
					this.dataAdapter = new SqlDataAdapter();
				}

				if( this.command == null )
				{
					this.command = new SqlCommand();
				}
				
				this.command.Connection = this.sqlDbConnection;
				this.command.CommandType = CommandType.StoredProcedure;
				this.command.CommandText = strStoredProc;
				this.command.CommandTimeout=300; //extend the timeout from default 30 to 300
				
				if( parameters != null )
				{
					sqlParameter = this.SQLParametersMapping( strStoredProc, parameters );
					if( sqlParameter == null )
					{
						throw new Exception( "SQL parameters mapping returned null." );
					}
					
					if( !this.AttachSQLParameters( this.command, sqlParameter ) )
					{
						throw new Exception( "Failed to attach the SQL parameters to command." );
					}
				}
				else
				{
					this.command.Parameters.Clear();
				}
				
				this.dataAdapter.SelectCommand = this.command;
				
				this.dataAdapter.Fill( dsResult );
				
				return dsResult;
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
				throw ex;// jin cheng 20100507
				//return null;
			}
			finally
			{
				dsResult.Dispose();
			}
		}
		#endregion
		
		#region Update the Dataset changes into database
		/// <summary>
		/// ExecuteDataSetChanges
		/// </summary>
		/// <param name="DataSet dataSet"></param>
		/// <param name="DataSetChangesType dsChangesType"></param>
		/// <returns>int</returns>
		public int ExecuteDataSetChanges( DataSet dataSet, DataSetChangesType dsChangesType )
		{
			int iResult = -1;
			DataSet dsChanged = new DataSet();
			
			try
			{
				if( this.sqlDbConnection.State == ConnectionState.Closed )
				{
					throw new Exception( "Database Connection is closed." );
				}
				
				if( dataSet.HasChanges())
				{
					switch( dsChangesType )
					{
						case DataSetChangesType.INSERT: dsChanged = dataSet.GetChanges( DataRowState.Added ); break;
						case DataSetChangesType.DELETE: dsChanged = dataSet.GetChanges( DataRowState.Deleted ); break;
						case DataSetChangesType.MODIFIED: dsChanged = dataSet.GetChanges( DataRowState.Modified ); break;
						default: iResult = this.dataAdapter.Update( dataSet ); dataSet.AcceptChanges(); return iResult;
					}
					
					iResult = this.dataAdapter.Update( dsChanged );
					dataSet.AcceptChanges();
				}
				
				return iResult;
			}
			catch( Exception ex )
			{
				dataSet.RejectChanges();
				this.ErrorLog( ex );
				return -1;
			}
			finally
			{
				dsChanged.Dispose();
			}
		}
		#endregion
		
		#region Build the connection string
		/// <summary>
		/// ConnectionStringBuilder
		/// </summary>
		/// <param name="SQLConnectionString"></param>
		/// <returns>string</returns>
		protected string ConnectionStringBuilder( SQLConnectionString sqlConnectionString )
		{
			StringBuilder tmpString = new StringBuilder();
			string tmpResult;
			
			tmpString.Append( "Persist Security Info=" );
			tmpString.Append(  sqlConnectionString.PersistSecurityInfo );
			
			tmpString.Append( ";Integrated Security=" );
			if( sqlConnectionString.IntegratedSecurity == string.Empty )
			{
				tmpString.Append( "SSPI" );
			}
			else
			{
				tmpString.Append( sqlConnectionString.IntegratedSecurity );
				
				if( sqlConnectionString.IntegratedSecurity == "false" )
				{
					tmpString.Append( ";UID=" );
					tmpString.Append( sqlConnectionString.UserID );
					tmpString.Append( ";Password=" );
					tmpString.Append( sqlConnectionString.Password );
				}
			}
			
			tmpString.Append( ";database=" );
			tmpString.Append( sqlConnectionString.DatabaseName );
			tmpString.Append( ";server=" );
			tmpString.Append( sqlConnectionString.ServerName );
			
			if( sqlConnectionString.ConnectTimeout != 0 )
			{
				tmpString.Append( ";Connect Timeout=" );
				tmpString.Append( sqlConnectionString.ConnectTimeout );
			}
			
			if( sqlConnectionString.Pooling )
			{
				tmpString.Append( ";Pooling=" );
				tmpString.Append( sqlConnectionString.Pooling );
				
				if( sqlConnectionString.MaxPoolSize != 0 )
				{
					tmpString.Append( ";Max Pool Size=" );
					tmpString.Append( sqlConnectionString.MaxPoolSize );
				}
			}
			
			tmpResult = tmpString.ToString();
			tmpString.Remove( 0, tmpString.Length );
			tmpString = null;
			
			return tmpResult;
		}
		#endregion
		
		#region Attach the parameters to SqlCommand
		/// <summary>
		/// AttachSQLParameters
		/// </summary>
		/// <param name="SqlCommand sqlCommand"></param>
		/// <param name="SqlParameter[] parameters"></param>
		/// <returns>bool</returns>
		protected bool AttachSQLParameters( SqlCommand sqlCommand, SqlParameter[] parameters )
		{
			if( sqlCommand == null )
			{
				throw new Exception( "SQL parameters cannot be null when pass to command." );
			}
			
			/*
			for( int i = 0; i < parameters.Length; i++ )
			{
				if( parameters[i].Value.ToString() == string.Empty )
				{
					throw new Exception( "At lease Value must be provided in SqlParameter, and the ParameterName is also required when using SQL script." );
				}
			}
			*/
			
			try
			{
				if( parameters != null )
				{
					if( sqlCommand.Parameters != null )
					{
						sqlCommand.Parameters.Clear();
					}
					
					foreach( SqlParameter param in parameters )
					{
						if( param != null )
						{
							if(( param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Input ) && param.Value == null )
							{
								param.Value = DBNull.Value;
							}
							
							// Everytime use SqlParameter, a new instance must be created to avoid the following error:
							// "The SqlParameter with ParameterName 'xxx' is already contained by another SqlParamter collection."
							SqlParameter tmpParam = (SqlParameter)((ICloneable)param).Clone();

							sqlCommand.Parameters.Add( tmpParam );
						}
					}
				}
				
				return true;
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
				
				return false;
			}
		}
		#endregion
		
		#region Map the incoming string parameters to SqlParameter
		/// <summary>
		/// SQLParametersMapping
		/// </summary>
		/// <param name="string strSPName"></param>
		/// <param name="string [] strParams"></param>
		/// <returns>SqlParameter []</returns>
		protected SqlParameter [] SQLParametersMapping( string strStoredProc, params string [] strParams )
		{
			SqlParameter [] sqlParameter;
			
			sqlParameter = this.RetrieveSTParameters( strStoredProc, false );
			if( sqlParameter == null )
			{
				throw new Exception( "Failed to retrive parameters for the stored procedure." );
			}
			
			if( sqlParameter.Length != strParams.Length )
			{
				throw new Exception( "SQLParametersMapping Error for parameters don't match." );
			}
			
			try
			{
				for( int i = 0; i < sqlParameter.Length; i++ )
				{
					if( strParams[i] == null )
					{
						sqlParameter[i].Value = DBNull.Value;
					}
					else
					{
						sqlParameter[i].Value = strParams[i];
					}
				}
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );
				
				return null;
			}
			
			return sqlParameter;
		}
		#endregion
		
		#region Retrieve stored procedure parameter list from database
		/// <summary>
		/// RetrieveSTParameters
		/// </summary>
		/// <param name="string strSPName"></param>
		/// <param name="bool bHasReturnParameter"></param>
		/// <returns>SqlParameter []</returns>
		protected SqlParameter [] RetrieveSTParameters( string strSPName, bool bHasReturnParameter )
		{
			SqlCommand tmpCmd;
			SqlParameter [] sqlParameter;
			bool bConnectionOpened = true;
			
			if( this.sqlDbConnection == null )
			{
				throw new Exception( "Database does not be connected." );
			}
			
			tmpCmd = new SqlCommand( strSPName, this.sqlDbConnection );
			tmpCmd.CommandType = CommandType.StoredProcedure;
			
			try
			{
				if( this.sqlDbConnection.State == ConnectionState.Closed )
				{
					bConnectionOpened = false;
					this.sqlDbConnection.Open();
				}
				
				SqlCommandBuilder.DeriveParameters( tmpCmd );
				
				if( !bConnectionOpened )
				{
					this.sqlDbConnection.Close();
				}
				
				if( !bHasReturnParameter )
				{
					tmpCmd.Parameters.RemoveAt( 0 );
				}
				
				sqlParameter = new SqlParameter[ tmpCmd.Parameters.Count ];
				tmpCmd.Parameters.CopyTo( sqlParameter, 0 );
				
				if( tmpCmd != null )
				{
					tmpCmd.Dispose();
				}

				return sqlParameter;
			}
			catch( Exception ex )
			{
				this.ErrorLog( ex );

				if( tmpCmd != null )
				{
					tmpCmd.Dispose();
				}
				
				if( !bConnectionOpened && ( this.sqlDbConnection.State != ConnectionState.Closed ))
				{
					this.sqlDbConnection.Close();
				}
				
				return null;
			}
		}
		#endregion
		
		#region Restrct the return records
		// only support clause "GROUP", "HAVING", "ORDER"
		// Or, users can modify public property SensitiveKeywords to add more
		// Or, developer can modify the initialization of the protected variable strSensitiveKeywords
		// to enable more clauses in the class constructor
		/// <summary>
		/// RestrictReturnRecords
		/// </summary>
		/// <param name="string strCommandText"></param>
		/// <param name="RecordsRestrictnInfo"></param>
		/// <returns>string</returns>
		protected string RestrictReturnRecords( string strCommandText, RecordsRestrictInfo recordsRestrictInfo )
		{
			ArrayList lstSensitiveKeywords;
			StringBuilder tmpString = new StringBuilder();
			string strTemp = string.Empty, strTableShortName = string.Empty;
			
			lstSensitiveKeywords = this.CheckSensitiveKeywords( strCommandText, this.lstSensitiveKeyWords );
			
			strCommandText = strCommandText.ToUpper();
			tmpString.Append( "SELECT TOP " );
			tmpString.Append( recordsRestrictInfo.RecordsCount );
			tmpString.Append( " " );
			
			if( strCommandText.IndexOf( "WHERE" ) != -1 )
			{
				tmpString.Append( strCommandText.Substring( strCommandText.IndexOf( "SELECT" ) + 7, strCommandText.IndexOf( "WHERE") ));
			}
			else
			{
				if( lstSensitiveKeywords.Count == 0 )
				{
					tmpString.Append( strCommandText.Substring( strCommandText.IndexOf( "SELECT" ) + 7 ));
				}
				else
				{
					tmpString.Append( strCommandText.Substring( strCommandText.IndexOf( "SELECT") + 7, strCommandText.IndexOf( lstSensitiveKeywords[0].ToString() ) - lstSensitiveKeywords[0].ToString().Length - 2 ));
				}
			}
			
			if( recordsRestrictInfo.PrimaryKey != string.Empty && recordsRestrictInfo.PrimaryKey != null )
			{
				if( strCommandText.IndexOf( "WHERE" ) != -1 )
				{
					strTemp = strCommandText.Substring( strCommandText.IndexOf( "FROM" ), strCommandText.IndexOf( "WHERE" ));
				}
				else
				{
					strTemp = strCommandText.Substring( strCommandText.IndexOf( "FROM" ));
				}
				
				if( strCommandText.IndexOf( "WHERE" ) == -1 )
				{
					tmpString.Append( " WHERE " );
				}
				
				// Get the table short-cut name
				if( strTemp.IndexOf( "," ) != -1 )
				{
					strTableShortName = strTemp.Substring( 0, strTemp.IndexOf(",") );
				}
				else
				{
					strTableShortName = strTemp;
				}
				
				strTableShortName = strTableShortName.Substring( strTableShortName.IndexOf( "FROM " ) + 6);
				strTableShortName = strTableShortName.Substring( strTableShortName.IndexOf( " " ) + 1);
				
				if( strTemp.IndexOf( "," ) != -1 )
				{
					tmpString.Append( strTableShortName + "." );
				}
				
				tmpString.Append( recordsRestrictInfo.PrimaryKey + " NOT IN (SELECT TOP " );
				tmpString.Append( recordsRestrictInfo.StartPosition );
				
				if( strTemp.IndexOf( "," ) != -1 )
				{
					tmpString.Append( " " + strTableShortName + "." );
				}
				else
				{
					tmpString.Append( " " );
				}
				
				tmpString.Append( recordsRestrictInfo.PrimaryKey + " " );				
				tmpString.Append( strCommandText.Substring( strCommandText.IndexOf( "FROM " ) ));
				tmpString.Append( ")" );
				
				if( strCommandText.IndexOf( "WHERE" ) != -1 )
				{
					tmpString.Append( " AND " + strCommandText.Substring( strCommandText.IndexOf( "WHERE " ) + 6 ));
				}
				
				strCommandText = tmpString.ToString();
			}
			
			tmpString.Remove( 0, tmpString.Length );
			tmpString = null;
			
			return strCommandText;
		}
		#endregion
		
		#region Is the command text contains sensitive keywords
		/// <summary>
		/// CheckSensitiveKeywords
		/// </summary>
		/// <param name="string strCommandText"></param>
		/// <param name="ArrayList lstSensitiveKeywords"></param>
		/// <returns>ArrayList</returns>
		protected ArrayList CheckSensitiveKeywords( string strCommandText, ArrayList lstSensitiveKeywords )
		{
			ArrayList lstResult = new ArrayList();
			
			for( int i = 0; i < lstSensitiveKeywords.Count; i++ )
			{
				if( strCommandText.IndexOf( lstSensitiveKeywords[i].ToString() ) != -1 )
				{
					lstResult.Add( lstSensitiveKeywords[i] );
				}
			}
			
			return lstResult;
		}
		#endregion
		
		#region Error handling
		/// <summary>
		/// ErrorLog
		/// </summary>
		/// <param name="Exception ex"></param>
		/// <returns>void</returns>
		protected virtual void ErrorLog( Exception ex )
		{
			string tmpString = string.Empty, strFunctionName = string.Empty, strFileName = string.Empty;
			string strClassName = string.Empty, strCodeLine = string.Empty;
			StringBuilder tmpStr = new StringBuilder();
			
			tmpString = ex.StackTrace;
			
			if( ex.StackTrace.IndexOf( "at " ) >= 0 )
			{
				tmpString = ex.StackTrace.Substring( ex.StackTrace.LastIndexOf( "at " ) + 3 );
				
				if( tmpString.IndexOf( " in " ) >= 0 )
				{
					if( tmpString.IndexOf( ":line" ) >= 0 )
					{
						strCodeLine = tmpString.Substring( tmpString.IndexOf( ":line" ) + 6 ).Trim();
					
						strFileName = tmpString.Substring( tmpString.IndexOf( " in " ) + 3, tmpString.IndexOf( ":line" ) - tmpString.IndexOf( " in " ) - 3).Trim();
					}
					else
					{
						strFileName = tmpString.Substring( tmpString.IndexOf( " in " ) + 3 );
					}
				}
				
				strClassName = tmpString.Substring( 0, tmpString.IndexOf( ")" ) + 1);
				strFunctionName = strClassName.Substring( strClassName.LastIndexOf( "." ) + 1 ).Trim();
				strClassName = strClassName.Substring( 0, strClassName.LastIndexOf( "." ) ).Trim();
				strClassName = strClassName.Substring( strClassName.LastIndexOf( "." ) + 1 ).Trim();
			}
			
			tmpStr.AppendFormat( "[Exception] {0}\n", ex.GetType().ToString() );
			
			if( strFileName != string.Empty )
			{
				tmpStr.AppendFormat( "[File] {0}\n", strFileName );
			}
			
			if( strClassName != string.Empty )
			{
				tmpStr.AppendFormat( "[Class] {0}\n", strClassName );
			}
			
			if( strCodeLine != string.Empty )
			{
				tmpStr.AppendFormat( "[Code Line] {0}\n", strCodeLine );
			}
			
			if( strFunctionName != string.Empty )
			{
				tmpStr.AppendFormat( "[Function] {0}\n", strFunctionName );
			}
			
			tmpStr.AppendFormat( "[Message] {0}", ex.Message );
			tmpString = tmpStr.ToString();
			
			tmpStr.Remove( 0, tmpStr.Length );
			tmpStr = null;
			
			// Keep the most inside error is at the top of the error ArrayList
			this.sqlDBClientError.Insert( 0, tmpString );
		}
		#endregion

		
	}

}
#endregion