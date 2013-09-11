using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using TPS168.DAL;


namespace TPS168.BLL
{
    public class GameLogic
    {
        protected GameDB _gameDB = new GameDB();

        //根据状态查询游戏信息（分页列表）  _gameState：-1所有，0未开仓，1已开仓
        public DataSet GameQueryByGameState(String _gameState, int _lengthLimit, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            DataSet _ds;
            int _index = 0;

            int _intNum;

            if (!int.TryParse(_gameState, out _intNum))
            {
                _gameState = "-1";
            }

            _ds = _gameDB.GameQueryByGameState(_gameState, _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);

            if (_ds.Tables.Count != 0)
            {
                _ds.Tables[0].Columns.Add("GameStateStr", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _gameStateStr = _dr[5].ToString();
                    String _noHtmlStr = remove_html_tag(_ds.Tables[0].Rows[_index][6].ToString());
                    int _length = _noHtmlStr.Length;

                    switch (_gameStateStr)
                    {
                        case "0": _ds.Tables[0].Rows[_index]["GameStateStr"] = "未开仓"; break;
                        case "1": _ds.Tables[0].Rows[_index]["GameStateStr"] = "已开仓"; break;
                    }

                    if (_length > _lengthLimit)
                    {
                        _noHtmlStr = _noHtmlStr.Substring(0, _lengthLimit);
                        _noHtmlStr += "...";
                    }

                    _ds.Tables[0].Rows[_index][6] = _noHtmlStr;

                    _index++;
                }

            }

            return _ds;
        }

        //根据游戏ID查询游戏信息
        public String[] GameQueryByGameId(String _gameId)
        {
            int _intNum;

            if (!int.TryParse(_gameId, out _intNum))
            {
                _gameId = null;
            }

            String[] _gameStr = new String[10];
            DataSet _ds = _gameDB.GameQueryByGameId(_gameId);

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        _gameStr[i] = _ds.Tables[0].Rows[0][i].ToString();
                    }
                }
                else
                {
                    _gameStr[0] = null;
                }
            }
            else
            {
                _gameStr[0] = null;
            }

            return _gameStr;
        }

        //查询所有游戏信息
        public DataSet GameQueryList()
        {
            return _gameDB.GameQueryList();
        }


        //游戏添加
        public int GameInsert(String[] _gameStr)
        {
            return _gameDB.GameInsert(_gameStr);
        }

        //游戏信息修改
        public int GameUpdate(String[] _gameStr)
        {
            return _gameDB.GameUpdate(_gameStr);
        }

        //游戏删除
        public int GameDelete(String _gameId)
        {
            int _intNum;

            if (!int.TryParse(_gameId, out _intNum))
            {
                _gameId = null;
            }

            return _gameDB.GameDelete(_gameId);
        }




        //根据游戏ID查询服务器信息（分页列表）   _gameId：-1所有，-2：所有开放
        public DataSet ServerQueryByGameId(String _gameId, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            int _intNum;
            if (!int.TryParse(_gameId, out _intNum))
            {
                _gameId = "-1";
            }

            DataSet _ds = _gameDB.ServerQueryByGameId(_gameId, _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);
            int _index = 0;

            if (_ds.Tables.Count != 0)
            {
                _ds.Tables[0].Columns.Add("ServerStateStr", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _gameStateStr = _dr[7].ToString();


                    switch (_gameStateStr)
                    {
                        case "1": _ds.Tables[0].Rows[_index]["ServerStateStr"] = "开放"; break;
                        case "2": _ds.Tables[0].Rows[_index]["ServerStateStr"] = "满仓"; break;
                    }

                    _index++;
                }

            }

            return _ds;
        }



        //根据游戏ID查询服务器信息（***前台显示*** 分页列表）   _gameId：-1所有，-2：所有开放
        public DataSet ServerQueryByGameIdFront(String _gameId, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            int _intNum;
            if (!int.TryParse(_gameId, out _intNum))
            {
                _gameId = "-1";
            }

            DataSet _ds = _gameDB.ServerQueryByGameId(_gameId, _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);
            int _index = 0;

            if (_ds.Tables.Count != 0)
            {

                _ds.Tables[0].Columns.Add("ServerStateStr", Type.GetType("System.String"));
                _ds.Tables[0].Columns.Add("PurchasePriceStr", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _gameStateStr = _dr[7].ToString();
                    DataSet _ds2 = _gameDB.GameQueryByGameId(_dr["GameId"].ToString());
                    String _currencyUnit = _ds2.Tables[0].Rows[0]["GameCurrencyUnit"].ToString();

                    _ds.Tables[0].Rows[_index]["PurchasePriceStr"] = _ds.Tables[0].Rows[_index]["PurchasePrice"] + _currencyUnit;

                    switch (_gameStateStr)
                    {
                        case "1": _ds.Tables[0].Rows[_index]["ServerStateStr"] = "开放"; break;
                        case "2": _ds.Tables[0].Rows[_index]["ServerStateStr"] = "满仓"; break;
                    }

                    _index++;
                }

            }

            return _ds;
        }


        //根据服务器名称（模糊）查询服务器信息（分页列表）
        public DataSet ServerQueryByServerNameSearch(String _keyWord, int _pageIndex, int _pageSize, out int _pageAmount)
        {

            DataSet _ds = _gameDB.ServerQueryByServerNameSearch(_keyWord, _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);
            int _index = 0;

            if (_ds.Tables.Count != 0)
            {
                _ds.Tables[0].Columns.Add("ServerStateStr", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _gameStateStr = _dr[7].ToString();


                    switch (_gameStateStr)
                    {
                        case "1": _ds.Tables[0].Rows[_index]["ServerStateStr"] = "开放"; break;
                        case "2": _ds.Tables[0].Rows[_index]["ServerStateStr"] = "满仓"; break;
                    }

                    _index++;
                }

            }

            return _ds;
        }


        //根据服务器ID查询服务器信息
        public String[] ServerQueryByServerId(String _serverId)
        {
            int _intNum;

            if (!int.TryParse(_serverId, out _intNum))
            {
                _serverId = null;
            }

            String[] _serverStr = new String[10];
            DataSet _ds = _gameDB.ServerQueryByServerId(_serverId);

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        _serverStr[i] = _ds.Tables[0].Rows[0][i].ToString();
                    }
                }
                else
                {
                    _serverStr[0] = null;
                }
            }
            else
            {
                _serverStr[0] = null;
            }

            return _serverStr;
        }


        //服务器添加
        public int ServerInsert(String[] _serverStr)
        {
            return _gameDB.ServerInsert(_serverStr);
        }

        //服务器批量添加
        public int ServerInsertBatch(String[] _serverNameStr, String[] _serverStr)
        {
            int _retVal = 0;
            int _count = 0;
            int _sucCount = 0;

            foreach (String _nameStr in _serverNameStr)
            {
                if (_nameStr == "\r" || _nameStr == "")
                {
                    _sucCount++;
                }
                else
                {
                    _serverStr[0] = _nameStr.Trim();

                    if (_gameDB.ServerInsert(_serverStr) == 2)
                    {
                        _sucCount++;
                    }
                }

                _count++;
            }

            if (_sucCount == _count && _sucCount != 0)
            {
                _retVal = 2;
            }

            if (_sucCount < _count && _sucCount > 0)
            {
                _retVal = 1;
            }

            return _retVal;
        }

        //服务器信息修改
        public int ServerUpdate(String[] _serverStr)
        {
            return _gameDB.ServerUpdate(_serverStr);
        }

        //服务器批量修改
        public int ServerUpdateBatch(DataTable _dt)
        {
            int _retVal = 0;
            int _count = 0;
            int _sucCount = 0;
            String[] _serverStr = new String[7];

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in _dt.Rows)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        _serverStr[i] = _dr[i].ToString();
                    }

                    if (_gameDB.ServerUpdate(_serverStr) == 1)
                    {
                        _sucCount++;
                    }
                    _count++;
                }

            }


            if (_sucCount == _count && _sucCount!=0)
            {
                _retVal = 2;
            }

            if (_sucCount < _count && _sucCount > 0)
            {
                _retVal = 1;
            }

            return _retVal;
        }

        //服务器删除
        public int ServerDelete(String _serverId)
        {
            int _intNum;

            if (!int.TryParse(_serverId, out _intNum))
            {
                _serverId = null;
            }

            return _gameDB.ServerDelete(_serverId);
        }

        //去除html标签元素
        public static string remove_html_tag(string _htmlString)
        {
            _htmlString = Regex.Replace(_htmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            _htmlString = Regex.Replace(_htmlString, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"-->", "", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);

            _htmlString = Regex.Replace(_htmlString, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            _htmlString.Replace("<", "");
            _htmlString.Replace(">", "");
            _htmlString.Replace("\r\n", "");
            _htmlString = HttpContext.Current.Server.HtmlEncode(_htmlString).Trim();

            return _htmlString;
        }

    }
}
