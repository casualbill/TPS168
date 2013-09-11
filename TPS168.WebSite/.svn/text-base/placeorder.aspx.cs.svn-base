using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TPS168.BLL;

namespace TPS168.WebSite
{
    public partial class placeorder : System.Web.UI.Page
    {
        protected GameLogic _gameLogic = new GameLogic();
        protected String _urlGetId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _urlGetId = Request.QueryString["id"];
                int _intNum;

                get_gameinfo_into_dropdownlist();

                if (int.TryParse(_urlGetId, out _intNum))
                {
                    ddl_gamelist.Items.FindByValue(_urlGetId).Selected = true;
                    gameserver_update(_urlGetId);
                    gamecaption_update(_urlGetId);
                }
                else
                {
                    gameserver_update(ddl_gamelist.SelectedValue);
                    gamecaption_update(ddl_gamelist.SelectedValue);
                }

            }
        }

        protected void ddl_gamelist_changed(object sender, EventArgs e)
        {
            gameserver_update(ddl_gamelist.SelectedValue);
            gamecaption_update(ddl_gamelist.SelectedValue);
        }

        //获取开仓游戏列表并写入Dropdownlist
        protected void get_gameinfo_into_dropdownlist()
        {
            ddl_gamelist.Items.Add(new ListItem("所有开放服务器", "-2"));

            int _pageAmount;
            DataSet _ds = _gameLogic.GameQueryByGameState("-1", 50, 1, 1000, out _pageAmount);

            if (_ds.Tables.Count > 0)
            {
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    ddl_gamelist.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
                }
            }
        }

        //改变选择游戏 更新服务器列表
        protected void gameserver_update(String _gameId)
        {
            int _pageAmount;

            DataSet _ds = _gameLogic.ServerQueryByGameIdFront(_gameId, 1, 1000, out _pageAmount);

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    rpt_index_serverlist.DataSource = _ds;
                    rpt_index_serverlist.DataBind();
                    rpt_index_serverlist.Visible = true;
                    tr_querynull.Visible = false;
                }
                else
                {
                    rpt_index_serverlist.Visible = false;
                    tr_querynull.Visible = true;
                }
            }
            else
            {
                rpt_index_serverlist.Visible = false;
                tr_querynull.Visible = true;
            }
        }

        //改变选择游戏 更新游戏简介
        protected void gamecaption_update(String _gameId)
        {
            String[] _gameStr = new String[10];
            _gameStr = _gameLogic.GameQueryByGameId(_gameId);

            hl_game_type.Text = _gameStr[1] + "下单";
            hl_game_type.NavigateUrl = "placeorder.aspx?id=" + _gameStr[0];
            lb_game_caption.Text = Server.HtmlDecode(_gameStr[6]);
        }

    }
}