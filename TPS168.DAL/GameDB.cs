using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TPS168.DAL
{
    public class GameDB
    {
        protected DBHelper _dbHelper = new DBHelper();

        public DataSet GameQueryByGameState(String _gameState, String _pageIndex, String _pageSize, out int _pageAmount)
        {
            _dbHelper.Open();
            _pageAmount = _dbHelper.ExecuteSPNonQuery("dbo.GameQueryByGameState", _gameState, _pageIndex, _pageSize, null);
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.GameQueryByGameState", _gameState, _pageIndex, _pageSize, null);
            _dbHelper.Close();
            return _ds;
        }

        public DataSet GameQueryByGameId(String _gameId)
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.GameQueryByGameId", _gameId);
            _dbHelper.Close();
            return _ds;
        }

        public DataSet GameQueryList()
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.GameQueryList");
            _dbHelper.Close();
            return _ds;
        }

        public int GameInsert(String[] _gameStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.GameInsert", _gameStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int GameUpdate(String[] _gameStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.GameUpdate", _gameStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int GameDelete(String _gameId)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.GameDelete", _gameId, null);
            _dbHelper.Close();
            return _retVal;
        }



        public DataSet ServerQueryByGameId(String _gameId, String _pageIndex, String _pageSize, out int _pageAmount)
        {
            _dbHelper.Open();
            _pageAmount = _dbHelper.ExecuteSPNonQuery("dbo.ServerQueryByGameId", _gameId, _pageIndex, _pageSize, null);
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.ServerQueryByGameId", _gameId, _pageIndex, _pageSize, null);
            _dbHelper.Close();
            return _ds;
        }

        public DataSet ServerQueryByServerNameSearch(String _keyWord, String _pageIndex, String _pageSize, out int _pageAmount)
        {
            _dbHelper.Open();
            _pageAmount = _dbHelper.ExecuteSPNonQuery("dbo.ServerQueryByServerNameSearch", _keyWord, _pageIndex, _pageSize, null);
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.ServerQueryByServerNameSearch", _keyWord, _pageIndex, _pageSize, null);
            _dbHelper.Close();
            return _ds;
        }

        public DataSet ServerQueryByServerId(String _serverId)
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.ServerQueryByServerId", _serverId);
            _dbHelper.Close();
            return _ds;
        }

        public int ServerInsert(String[] _serverStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.ServerInsert", _serverStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int ServerUpdate(String[] _serverStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.ServerUpdate", _serverStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int ServerDelete(String _serverId)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.ServerDelete", _serverId, null);
            _dbHelper.Close();
            return _retVal;
        }

    }
}
