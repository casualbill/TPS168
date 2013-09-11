using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TPS168.DAL;

namespace TPS168.BLL
{
    public class OrderLogic
    {
        protected OrderDB _orderDB = new OrderDB();
        protected CSVHelper _csvHelper = new CSVHelper();

        //查询订单信息
        public DataSet OrderQuery(int _queryMode, String _queryId, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            int _intNum;
            if (!int.TryParse(_queryId, out _intNum))
            {
                _queryId = null;
            }

            DataSet _ds=_orderDB.OrderQuery(_queryMode.ToString(), _queryId, "1", _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);
            int _index = 0;

            if (_ds.Tables.Count != 0)
            {
                _ds.Tables[0].Columns.Add("OrderStateStr", Type.GetType("System.String"));
                _ds.Tables[0].Columns.Add("OrderStateOperation", Type.GetType("System.String"));
                _ds.Tables[0].Columns.Add("OrderStateOperationStr", Type.GetType("System.String"));
                _ds.Tables[0].Columns.Add("OrderOperationBtnState", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _gameStateStr = _dr["OrderState"].ToString();

                    switch (_gameStateStr)
                    {
                        case "0": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "等待审核"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "审核通过"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "1"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = ""; break;
                        case "1": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "审核通过-等待交易"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "交易完成"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "2"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = ""; break;
                        case "2": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "交易完成-等待汇款"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "完成汇款"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "3"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = ""; break;
                        case "3": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "完成汇款"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "已完成"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "0"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = "disabled='disabled'"; break;
                        case "4": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "失败订单"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "已失败"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "0"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = "disabled='disabled'"; break;
                    }

                    _index++;
                }

            }

            return _ds;
        }


        //查询订单信息(新)
        public DataSet OrderQueryNew(String _orderState, String _queryString, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            int _intNum;
            if (!int.TryParse(_orderState, out _intNum))
            {
                _orderState = null;
            }

            DataSet _ds = _orderDB.OrderQueryNew(_orderState, _queryString, _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);
            int _index = 0;

            if (_ds.Tables.Count != 0)
            {
                _ds.Tables[0].Columns.Add("OrderStateStr", Type.GetType("System.String"));
                _ds.Tables[0].Columns.Add("OrderStateOperation", Type.GetType("System.String"));
                _ds.Tables[0].Columns.Add("OrderStateOperationStr", Type.GetType("System.String"));
                _ds.Tables[0].Columns.Add("OrderOperationBtnState", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _gameStateStr = _dr["OrderState"].ToString();


                    switch (_gameStateStr)
                    {
                        case "0": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "等待审核"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "审核通过"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "1"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = ""; break;
                        case "1": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "审核通过-等待交易"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "交易完成"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "2"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = ""; break;
                        case "2": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "交易完成-等待汇款"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "完成汇款"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "3"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = ""; break;
                        case "3": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "完成汇款"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "已完成"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "0"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = "disabled='disabled'"; break;
                        case "4": _ds.Tables[0].Rows[_index]["OrderStateStr"] = "失败订单"; _ds.Tables[0].Rows[_index]["OrderStateOperationStr"] = "已失败"; _ds.Tables[0].Rows[_index]["OrderStateOperation"] = "0"; _ds.Tables[0].Rows[_index]["OrderOperationBtnState"] = "disabled='disabled'"; break;
                    }

                    _index++;
                }

            }

            return _ds;
        }

        //查询订单
        public Double OrderPriceAmount(int _orderState)
        {
            DataSet _ds = _orderDB.OrderPriceAmount(_orderState.ToString());
            Double _priceAmount = 0;

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    _priceAmount = Convert.ToDouble(_ds.Tables[0].Rows[0][0]);
                }
            }

            return _priceAmount;
        }

        //根据订单号查询订单
        public String[] OrderQueryByOrderId(String _orderId)
        {
            long _longNum;
            if (!long.TryParse(_orderId, out _longNum))
            {
                _orderId = null;
            }

            String[] _orderStr = new String[26];
            DataSet _ds = _orderDB.OrderQueryByOrderId(_orderId);

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        _orderStr[i] = _ds.Tables[0].Rows[0][i].ToString();
                    }

                    switch (_orderStr[11])
                    {
                        case "0": _orderStr[25] = "银行汇款"; break;
                        case "1": _orderStr[25] = "支付宝"; break;
                        case "2": _orderStr[25] = "快钱"; break;
                    }

                    switch (_orderStr[19])
                    {
                        case "0": _orderStr[24] = "等待审核"; break;
                        case "1": _orderStr[24] = "审核通过-等待交易"; break;
                        case "2": _orderStr[24] = "交易完成-等待汇款"; break;
                        case "3": _orderStr[24] = "完成汇款"; break;
                        case "4": _orderStr[24] = "失败订单"; break;
                    }
                }
                else
                {
                    _orderStr[0] = null;
                }
            }
            else
            {
                _orderStr[0] = null;
            }

            return _orderStr;
        }


        //订单生成
        public int OrderCreate(String[] _orderStr)
        {
            int _retVal = 1;

            while (_retVal == 1)
            {
                _orderStr[0] = DateTime.Now.ToString("yyyyMMddhhmmss") + RandomNumber(0, 1000).ToString("000");
                _retVal = _orderDB.OrderCreate(_orderStr);
            }

            return _retVal;
        }


        //订单操作（更改状态）
        public int OrderOperation(String _adminId, String _orderId, String _orderState, String _serialNumber, String _remarks)
        {
            long _longNum;

            if (!long.TryParse(_orderId, out _longNum) || !long.TryParse(_adminId, out _longNum) || !long.TryParse(_orderState, out _longNum))
            {
                return -1;
            }
            else
            {
                return _orderDB.OrderOperation(_adminId, _orderId, _orderState, _serialNumber, _remarks);
            }

        }

        //订单操作（更改数量）
        public int OrderAmountUpdate(String _adminId, String _orderId, String _amount, String _remarks)
        {
            long _longNum;

            if (!long.TryParse(_orderId, out _longNum) || !long.TryParse(_adminId, out _longNum) || !long.TryParse(_amount, out _longNum))
            {
                return -1;
            }
            else
            {
                return _orderDB.OrderAmountUpdate(_adminId, _orderId, _amount, _remarks);
            }
        }


        //查询订单操作记录
        public DataSet OrderLogQueryByOrderId(String _orderId, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            long _longNum;
            if (!long.TryParse(_orderId, out _longNum))
            {
                _orderId = null;
            }

            DataSet _ds = _orderDB.OrderLogQueryByOrderId(_orderId, "1", _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);
            int _index = 0;

            if (_ds.Tables.Count != 0)
            {
                _ds.Tables[0].Columns.Add("FormerStateStr", Type.GetType("System.String"));
                _ds.Tables[0].Columns.Add("UpdateStateStr", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _formerStateStr = _dr[7].ToString();
                    String _updateStateStr = _dr[8].ToString();

                    switch (_formerStateStr)
                    {
                        case "0": _ds.Tables[0].Rows[_index]["FormerStateStr"] = "等待审核"; break;
                        case "1": _ds.Tables[0].Rows[_index]["FormerStateStr"] = "审核通过-等待交易"; break;
                        case "2": _ds.Tables[0].Rows[_index]["FormerStateStr"] = "交易完成-等待汇款"; break;
                        case "3": _ds.Tables[0].Rows[_index]["FormerStateStr"] = "完成汇款"; break;
                        case "4": _ds.Tables[0].Rows[_index]["FormerStateStr"] = "失败订单"; break;
                    }

                    switch (_updateStateStr)
                    {
                        case "0": _ds.Tables[0].Rows[_index]["UpdateStateStr"] = "等待审核"; break;
                        case "1": _ds.Tables[0].Rows[_index]["UpdateStateStr"] = "审核通过-等待交易"; break;
                        case "2": _ds.Tables[0].Rows[_index]["UpdateStateStr"] = "交易完成-等待汇款"; break;
                        case "3": _ds.Tables[0].Rows[_index]["UpdateStateStr"] = "完成汇款"; break;
                        case "4": _ds.Tables[0].Rows[_index]["UpdateStateStr"] = "失败订单"; break;
                    }

                    _index++;
                }

            }

            return _ds;
        }


        //输出订单操作记录到csv文件
        public bool OrderExportToCSV(DataSet _ds, String _fileName)
        {

            String _timeStr = DateTime.Now.ToString("yyMMdd");

            if (_ds.Tables.Count == 0)
            {
                return false;
            }
            else
            {
                DataTable _dt = _ds.Tables[0];

                foreach (DataRow _dr in _dt.Rows)
                {
                    _dr["OrderId"] = "ID" + _dr["OrderId"];
                }

                _dt.Columns["OrderId"].ColumnName = "订单号";
                _dt.Columns["UserName"].ColumnName = "用户名";
                _dt.Columns["GameName"].ColumnName = "游戏";
                _dt.Columns["ServerName"].ColumnName = "服务器";
                _dt.Columns["RoleName"].ColumnName = "游戏角色名";
                _dt.Columns["Amount"].ColumnName = "数量";
                _dt.Columns["PurchasePrice"].ColumnName = "单价";
                _dt.Columns["TotalPrice"].ColumnName = "总价";
                _dt.Columns["TradeName"].ColumnName = "交易名称";
                _dt.Columns["TradeAccount"].ColumnName = "交易账号";
                _dt.Columns["TradeTime"].ColumnName = "交易时间";
                _dt.Columns["PlaceTime"].ColumnName = "下单时间";
                _dt.Columns["CloseTime"].ColumnName = "结束时间";
                _dt.Columns["OrderStateStr"].ColumnName = "状态";
                _dt.Columns["Remarks"].ColumnName = "备注";

                _dt.Columns.Remove("UserId");
                _dt.Columns.Remove("TradeType");
                _dt.Columns.Remove("OrderState");
                _dt.Columns.Remove("OrderStateOperation");
                _dt.Columns.Remove("OrderStateOperationStr");
                _dt.Columns.Remove("SerialNumber");
                _dt.Columns.Remove("OrderOperationBtnState");

                _fileName += _timeStr + ".csv";
                _csvHelper.SaveToCSV(_dt, _fileName);

                return true;
            }
        }


        //生成随机数
        protected int RandomNumber(int _min, int _max)
        {
            Random _rand=new Random();
            int _retVal = _rand.Next(_min, _max);

            return _retVal;
        }
    }
}
