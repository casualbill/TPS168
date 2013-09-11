using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TPS168.DAL
{
    public class NewsDB
    {
        protected DBHelper _dbHelper = new DBHelper();

        public DataSet NewsQueryByNewsType(String _newsType, String _queryMode, String _pageIndex, String _pageSize, out int _pageAmount)
        {
            _dbHelper.Open();
            _pageAmount = _dbHelper.ExecuteSPNonQuery("dbo.NewsQueryByNewsType", _newsType, _queryMode, _pageIndex, _pageSize, null);
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.NewsQueryByNewsType", _newsType, _queryMode, _pageIndex, _pageSize, null);
            _dbHelper.Close();
            return _ds;
        }

        public DataSet NewsContentQueryByNewsId(String _newsId)
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.NewsContentQueryByNewsId", _newsId, null);
            _dbHelper.Close();
            return _ds;
        }

        public int NewsContentInsert(String[] _newsStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.NewsContentInsert", _newsStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int NewsContentUpdate(String[] _newsStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.NewsContentUpdate", _newsStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int NewsDelete(String _newsId)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.NewsDelete", _newsId, null);
            _dbHelper.Close();
            return _retVal;
        }

    }
}
