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
    public partial class profile7 : System.Web.UI.Page
    {
        protected StockLogic _stockLogic = new StockLogic();
        protected GameLogic _gameLogic = new GameLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            String _urlGetAc = Request.QueryString["ac"];
            String _urlGetId = Request.QueryString["id"];

            if (!IsPostBack)
            {
                if (_urlGetAc == "1")
                {
                    int _intNum;
                    if (int.TryParse(_urlGetId, out _intNum))
                    {
                        supply_delete(_urlGetId);
                    }
                }

                get_gameinfo_into_dropdownlist();
                supply_query();
            }
        }

        protected void ddl_gamelist_changed(object sender, EventArgs e)
        {
            gameserver_update(ddl_gamelist.SelectedValue);
        }

        protected void btn_supply_insert_click(object sender, EventArgs e)
        {
            supply_insert();
        }

        //查询供货信息列表
        protected void supply_query()
        {
            DataSet _ds = new DataSet();
            int _pageAmount;

            _ds = _stockLogic.SupplyQuery(3, Session["userid"].ToString(), 1, 200, out _pageAmount);

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    rpt_supply_info.DataSource = _ds;
                    rpt_supply_info.DataBind();
                }
                else
                {
                    tr_querynull.Visible = true;
                }
            }
            else
            {
                tr_querynull.Visible = true;
            }
        }

        //获取所有游戏列表并写入Dropdownlist
        protected void get_gameinfo_into_dropdownlist()
        {
            int _pageAmount;
            DataSet _ds = _gameLogic.GameQueryByGameState("-1", 50, 1, 100, out _pageAmount);

            if (_ds.Tables.Count > 0)
            {
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    ddl_gamelist.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
                }
            }
        }

        //改变选择游戏 更新服务器下拉列表
        protected void gameserver_update(String _gameId)
        {
            int _pageAmount;

            DataSet _ds = _gameLogic.ServerQueryByGameId(_gameId, 1, 100, out _pageAmount);

            ddl_serverlist.Items.Clear();

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow _dr in _ds.Tables[0].Rows)
                    {
                        ddl_serverlist.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
                    }
                }
                else
                {
                    ddl_serverlist.Items.Add(new ListItem("当前游戏暂无服务器信息", "0"));
                }
            }
            else
            {
                ddl_serverlist.Items.Add(new ListItem("当前游戏暂无服务器信息", "0"));
            }
        }

        //添加供货信息
        protected void supply_insert()
        {
            int _retVal;
            String _uid = Session["userid"].ToString();
            String _serverId = ddl_serverlist.SelectedValue;

            span_profile_tips.Attributes["style"] = "";

            if ((ddl_gamelist.SelectedValue == "0") || (ddl_gamelist.SelectedValue == null) || (ddl_serverlist.SelectedValue == "0") || (ddl_serverlist.SelectedValue == null))
            {
                _retVal = -1;
            }
            else
            {
                _retVal = _stockLogic.SupplyInsert(_uid, _serverId);
            }

            switch (_retVal)
            {
                case 2: span_profile_tips.InnerHtml = "该服务器供货记录已存在！"; break;
                case 3: span_profile_tips.InnerHtml = "添加成功！"; tr_querynull.Visible = false; supply_query(); break;
                case -1: span_profile_tips.InnerHtml = "请选择游戏和服务器！"; break;
                default: span_profile_tips.InnerHtml = "添加失败，请稍后再试！"; break;
            }

        }

        //删除供货信息
        protected void supply_delete(String _supplyId)
        {
            int _retVal;

            span_profile_tips.Attributes["style"] = "";

            _retVal = _stockLogic.SupplyDelete(_supplyId);

            if (_retVal == 1)
            {
                span_profile_tips.InnerHtml = "删除成功！";
                supply_query();
            }
            else
            {
                span_profile_tips.InnerHtml = "删除失败，请稍后再试！";
            }
        }

    }
}