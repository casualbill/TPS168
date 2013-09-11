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
    public partial class index : System.Web.UI.Page
    {
        protected NewsLogic _newsLogic = new NewsLogic();
        protected GameLogic _gameLogic = new GameLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            int _pageAmount;
            DataSet _ds;

            if (!IsPostBack)
            {
                
                _ds = _newsLogic.NewsQueryByNewsType("3", 1, 50, 1, 5, out _pageAmount);
                if (_ds.Tables.Count > 0)
                {
                    rpt_sidebar_faq.DataSource = _ds;
                    rpt_sidebar_faq.DataBind();
                }

                _ds=_gameLogic.GameQueryByGameState("-1", 50, 1, 1000, out _pageAmount);
                if (_ds.Tables.Count > 0)
                {
                    rpt_sidebar_gamelist.DataSource = _ds;
                    rpt_sidebar_gamelist.DataBind();
                }

                _ds = _gameLogic.GameQueryByGameState("1", 50, 1, 1000, out _pageAmount);
                if (_ds.Tables.Count > 0)
                {
                    rpt_index_gamelist1.DataSource = _ds;
                    rpt_index_gamelist1.DataBind();
                }

                _ds = _gameLogic.GameQueryByGameState("0", 50, 1, 1000, out _pageAmount);
                if (_ds.Tables.Count > 0)
                {
                    rpt_index_gamelist0.DataSource = _ds;
                    rpt_index_gamelist0.DataBind();
                }


                get_gameinfo_into_dropdownlist();
                gamelist_changed(ddl_gamelist.SelectedValue);
            }

        }

        protected void ddl_gamelist_changed(object sender, EventArgs e)
        {
            gamelist_changed(ddl_gamelist.SelectedValue);

        }

        //获取开仓游戏列表并写入Dropdownlist
        protected void get_gameinfo_into_dropdownlist()
        {
            ddl_gamelist.Items.Add(new ListItem("所有开放服务器", "-2"));

            int _pageAmount;
            DataSet _ds = _gameLogic.GameQueryByGameState("1", 50, 1, 1000, out _pageAmount);

            if (_ds.Tables.Count > 0)
            {
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    ddl_gamelist.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
                }
            }
        }

        //改变选择游戏 更新服务器列表
        protected void gamelist_changed(String _gameId)
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
    }
}