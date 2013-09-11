using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TPS168.DAL
{
    public class StockDB
    {
        protected DBHelper _dbHelper = new DBHelper();

        public DataSet SupplyQuery(String _queryMode, String _queryId, String _sortMode, String _pageIndex, String _pageSize, out int _pageAmount)
        {
            _dbHelper.Open();
            _pageAmount = _dbHelper.ExecuteSPNonQuery("dbo.SupplyQuery", _queryMode, _queryId, _sortMode, _pageIndex, _pageSize, null);
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.SupplyQuery", _queryMode, _queryId, _sortMode, _pageIndex, _pageSize, null);
            _dbHelper.Close();
            return _ds;
        }

        public int SupplyInsert(String _uid, String _serverId)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.SupplyInsert", _uid, _serverId, "1", null);
            _dbHelper.Close();
            return _retVal;
        }

        public int SupplyDelete(String _suppplyId)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.SupplyDelete", _suppplyId, null);
            _dbHelper.Close();
            return _retVal;
        }


        public DataSet StockQuery(String _queryMode, String _queryId, String _sortMode, String _pageIndex, String _pageSize, out int _pageAmount)
        {
            _dbHelper.Open();
            _pageAmount = _dbHelper.ExecuteSPNonQuery("dbo.StockQuery", _queryMode, _queryId, _sortMode, _pageIndex, _pageSize, null);
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.StockQuery", _queryMode, _queryId, _sortMode, _pageIndex, _pageSize, null);
            _dbHelper.Close();
            return _ds;
        }

        public int StockInsert(String _uid, String _serverId, String _stockAmount, String _presellPrice)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.StockInsert", _uid, _serverId, _stockAmount, _presellPrice, "1", null);
            _dbHelper.Close();
            return _retVal;
        }

        public int StockDelete(String _stockId)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.StockDelete", _stockId, null);
            _dbHelper.Close();
            return _retVal;
        }
    }
}
