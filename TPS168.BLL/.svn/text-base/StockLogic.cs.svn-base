using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TPS168.DAL;

namespace TPS168.BLL
{
    public class StockLogic
    {
        protected StockDB _stockDB = new StockDB();

        //查询供货信息
        public DataSet SupplyQuery(int _queryMode, String _queryId, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            int _intNum;
            if (!int.TryParse(_queryId, out _intNum))
            {
                _queryId = null;
            }

            return _stockDB.SupplyQuery(_queryMode.ToString(), _queryId, "1", _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);
        }

        //供货信息添加
        public int SupplyInsert(String _uid, String _serverId)
        {
            int _intNum;
            if (!int.TryParse(_serverId, out _intNum))
            {
                _serverId = null;
            }
            return _stockDB.SupplyInsert(_uid, _serverId);
        }

        //供货信息删除
        public int SupplyDelete(String _supplyId)
        {
            int _intNum;
            if (!int.TryParse(_supplyId, out _intNum))
            {
                _supplyId = null;
            }
            return _stockDB.SupplyDelete(_supplyId);
        }

        //查询库存信息
        public DataSet StockQuery(int _queryMode, String _queryId, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            int _intNum;
            if (!int.TryParse(_queryId, out _intNum))
            {
                _queryId = null;
            }

            return _stockDB.StockQuery(_queryMode.ToString(), _queryId, "1", _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);
        }

        //库存信息添加
        public int StockInsert(String _uid, String _serverId, String _stockAmount, String _presellPrice)
        {
            int _intNum;
            if (!int.TryParse(_serverId, out _intNum))
            {
                _serverId = null;
            }
            return _stockDB.StockInsert(_uid, _serverId, _stockAmount, _presellPrice);
        }

        //库存信息删除
        public int StockDelete(String _stockId)
        {
            int _intNum;
            if (!int.TryParse(_stockId, out _intNum))
            {
                _stockId = null;
            }
            return _stockDB.StockDelete(_stockId);
        }

    }
}
