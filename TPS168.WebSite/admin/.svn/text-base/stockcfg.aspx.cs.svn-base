using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TPS168.BLL;

namespace TPS168.WebSite.admin
{
    public partial class stockcfg : System.Web.UI.Page
    {
        protected String _type;
        protected String _pageStr;
        protected int _pageIndex;

        protected String _urlGetQm;
        protected String _urlGetId;

        protected StockLogic _stockLogic = new StockLogic();
        protected UserLogic _userLogic = new UserLogic();
        protected GameLogic _gameLogic = new GameLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 4) == 0)
            {
                Response.Redirect("error.aspx");
            }

            _type = Request.QueryString["type"];
            _pageStr = Request.QueryString["page"];
            _urlGetQm = Request.QueryString["qm"];
            _urlGetId = Request.QueryString["id"];

            int _pageInt;

            if (int.TryParse(_pageStr, out _pageInt))
            {
                if (_pageInt > 0)
                {
                    _pageIndex = _pageInt;
                }
                else
                {
                    _pageIndex = 1;
                }
            }
            else
            {
                _pageIndex = 1;
            }

            int _queryMode;

            if (!IsPostBack)
            {
                switch (_urlGetQm)
                {
                    case "0": _queryMode = 0; break;
                    case "1": _queryMode = 1; break;
                    case "2": _queryMode = 2; break;
                    case "3": _queryMode = 3; break;
                    default: _queryMode = 0; break;
                }

                switch (_type)
                {
                    case "1": supply_query(_queryMode, _urlGetId, _pageIndex); break; //供货信息查询
                    case "2": stock_query(_queryMode, _urlGetId, _pageIndex); break; //库存信息查询

                    default: supply_query(0, "0", _pageIndex); break;
                }

                get_gameinfo_into_dropdownlist();
            }
        }

        protected void btn_turntopage1_click(object sender, EventArgs e)
        {
            Response.Redirect("stockcfg.aspx?type=1&qm=" + _urlGetQm + "&id=" + _urlGetId + "&page=" + tb_pageindex1.Text);
        }

        protected void btn_turntopage2_click(object sender, EventArgs e)
        {
            Response.Redirect("stockcfg.aspx?type=2&qm=" + _urlGetQm + "&id=" + _urlGetId + "&page=" + tb_pageindex2.Text);
        }

        protected void btn_user_query_click(object sender, EventArgs e)
        {
            if (_type == "1") { Response.Redirect("stockcfg.aspx?type=1&qm=3&id=" + user_id_query(tb_user_query.Text) + "&page=1"); }
            if (_type == "2") { Response.Redirect("stockcfg.aspx?type=2&qm=3&id=" + user_id_query(tb_user_query.Text) + "&page=1"); }
        }

        protected void btn_game_query_click(object sender, EventArgs e)
        {
            if (_type == "1") { Response.Redirect("stockcfg.aspx?type=1&qm=1&id=" + ddl_game_query.SelectedValue + "&page=1"); }
            if (_type == "2") { Response.Redirect("stockcfg.aspx?type=2&qm=1&id=" + ddl_game_query.SelectedValue + "&page=1"); }
        }

        protected void btn_server_query_click(object sender, EventArgs e)
        {
            if (_type == "1") { Response.Redirect("stockcfg.aspx?type=1&qm=2&id=" + ddl_server_query.SelectedValue + "&page=1"); }
            if (_type == "2") { Response.Redirect("stockcfg.aspx?type=2&qm=2&id=" + ddl_server_query.SelectedValue + "&page=1"); }
        }

        protected void ddl_game_query_changed(object sender, EventArgs e)
        {
            gameserver_update(ddl_game_query.SelectedValue);
        }


        //供货信息查询
        protected void supply_query(int _queryMode, String _queryId, int _pageIndex)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();

            String _querytypeStr;

            switch (_queryMode)
            {
                case 0: _querytypeStr = "所有"; break;
                case 3: _querytypeStr = "用户" + tb_user_query.Text + "的"; break;
                case 1: try { _querytypeStr = "游戏" + ddl_game_query.SelectedItem.Text + "的"; }
                    catch { _querytypeStr = "所有"; }; break;
                case 2: _querytypeStr = "服务器" + ddl_server_query.SelectedItem.Text + "的"; break;
                default: _querytypeStr = "所有"; break;
            }

            lb_querytype.Text = _querytypeStr + "供货信息列表";

            _ds = _stockLogic.SupplyQuery(_queryMode, _queryId, _pageIndex, 20, out _pageAmount);

            pn_supply_list.Visible = true;
            query_null1.Visible = false;
            rpt_supply.Visible = true;
            pagination_pageindex1.Visible = true;
            pagination_pageamount1.Visible = true;

            pagination_pageindex1.InnerHtml = "第" + _pageIndex + "页";
            pagination_pageamount1.InnerHtml = "共" + _pageAmount + "页";

            if (_pageAmount > 1)
            {
                pagination_frame1.Visible = true;


                if (_pageIndex == 1)
                {
                    pagination_prev1.InnerHtml = "";
                    pagination_next1.InnerHtml = "<a href=\"stockcfg.aspx?type=1&qm=" + _queryMode + "&id=" + _queryId + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

                if (_pageIndex == _pageAmount)
                {
                    pagination_prev1.InnerHtml = "<a href=\"stockcfg.aspx?type=1&qm=" + _queryMode + "&id=" + _queryId + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next1.InnerHtml = "";
                }

                if (_pageIndex > 1 && _pageIndex < _pageAmount)
                {
                    pagination_prev1.InnerHtml = "<a href=\"stockcfg.aspx?type=1&qm=" + _queryMode + "&id=" + _queryId + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next1.InnerHtml = "<a href=\"stockcfg.aspx?type=1&qm=" + _queryMode + "&id=" + _queryId + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

            }
            else
            {
                pagination_frame1.Visible = false;
            }

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    rpt_supply.DataSource = _ds;
                    rpt_supply.DataBind();
                }
                else
                {
                    rpt_supply.Visible = false;
                    pagination_pageindex1.Visible = false;
                    pagination_pageamount1.Visible = false;
                    query_null1.Visible = true;
                    pagination_frame1.Visible = false;
                }
            }
            else
            {
                rpt_supply.Visible = false;
                pagination_pageindex1.Visible = false;
                pagination_pageamount1.Visible = false;
                query_null1.Visible = true;
                pagination_frame1.Visible = false;
            }

        }



        //库存信息查询
        protected void stock_query(int _queryMode, String _queryId, int _pageIndex)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();

            String _querytypeStr;

            switch (_queryMode)
            {
                case 0: _querytypeStr = "所有"; break;
                case 3: _querytypeStr = "用户" + tb_user_query.Text + "的"; break;
                case 1: _querytypeStr = "游戏" + ddl_game_query.SelectedItem.Text + "的"; break;
                case 2: _querytypeStr = "服务器" + ddl_server_query.SelectedItem.Text + "的"; break;
                default: _querytypeStr = "所有"; break;
            }

            lb_querytype.Text = _querytypeStr + "库存信息列表";

            _ds = _stockLogic.StockQuery(_queryMode, _queryId, _pageIndex, 20, out _pageAmount);

            pn_stock_list.Visible = true;
            query_null2.Visible = false;
            rpt_stock.Visible = true;
            pagination_pageindex2.Visible = true;
            pagination_pageamount2.Visible = true;

            pagination_pageindex2.InnerHtml = "第" + _pageIndex + "页";
            pagination_pageamount2.InnerHtml = "共" + _pageAmount + "页";

            if (_pageAmount > 1)
            {
                pagination_frame2.Visible = true;


                if (_pageIndex == 1)
                {
                    pagination_prev2.InnerHtml = "";
                    pagination_next2.InnerHtml = "<a href=\"stockcfg.aspx?type=2&qm=" + _queryMode + "&id=" + _queryId + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

                if (_pageIndex == _pageAmount)
                {
                    pagination_prev2.InnerHtml = "<a href=\"stockcfg.aspx?type=2&qm=" + _queryMode + "&id=" + _queryId + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next2.InnerHtml = "";
                }

                if (_pageIndex > 1 && _pageIndex < _pageAmount)
                {
                    pagination_prev2.InnerHtml = "<a href=\"stockcfg.aspx?type=2&qm=" + _queryMode + "&id=" + _queryId + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next2.InnerHtml = "<a href=\"stockcfg.aspx?type=2&qm=" + _queryMode + "&id=" + _queryId + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

            }
            else
            {
                pagination_frame2.Visible = false;
            }

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    rpt_stock.DataSource = _ds;
                    rpt_stock.DataBind();
                }
                else
                {
                    rpt_stock.Visible = false;
                    pagination_pageindex2.Visible = false;
                    pagination_pageamount2.Visible = false;
                    query_null2.Visible = true;
                    pagination_frame2.Visible = false;
                }
            }
            else
            {
                rpt_stock.Visible = false;
                pagination_pageindex2.Visible = false;
                pagination_pageamount2.Visible = false;
                query_null2.Visible = true;
                pagination_frame2.Visible = false;
            }

        }




        //获取所有游戏列表并写入Dropdownlist
        protected void get_gameinfo_into_dropdownlist()
        {
            int _pageAmount;
            DataSet _ds = _gameLogic.GameQueryByGameState("-1", 50, 1, 100, out _pageAmount);

            ddl_game_query.Items.Clear();

            ddl_game_query.Items.Add(new ListItem("请选择游戏", "0"));

            if (_ds.Tables.Count > 0)
            {
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    ddl_game_query.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
                }
            }
        }

        //改变选择游戏 更新服务器下拉列表
        protected void gameserver_update(String _gameId)
        {
            int _pageAmount;

            DataSet _ds = _gameLogic.ServerQueryByGameId(_gameId, 1, 100, out _pageAmount);

            ddl_server_query.Items.Clear();

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow _dr in _ds.Tables[0].Rows)
                    {
                        ddl_server_query.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
                    }
                }
                else
                {
                    ddl_server_query.Items.Add(new ListItem("当前游戏暂无服务器信息", "0"));
                }
            }
            else
            {
                ddl_server_query.Items.Add(new ListItem("当前游戏暂无服务器信息", "0"));
            }
        }

        //根据用户名查询用户ID
        protected String user_id_query(String _userName)
        {
            return _userLogic.UserQueryByUserNameOrUserId(_userName, 0)[1];
        }



    }
}