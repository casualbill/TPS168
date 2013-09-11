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
    public partial class gamecfg : System.Web.UI.Page
    {
        protected String _type;
        protected String _pageStr;
        protected int _pageIndex;

        protected String _urlGetId;
        protected String _urlGetSt;

        protected GameLogic _gameLogic = new GameLogic();
        protected UserLogic _userLogic = new UserLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 5) == 0)
            {
                Response.Redirect("error.aspx");
            }

            _type = Request.QueryString["type"];
            _pageStr = Request.QueryString["page"];

            _urlGetId = Request.QueryString["id"];
            _urlGetSt = Request.QueryString["st"];

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

            if (!IsPostBack)
            {

                switch (_type)
                {
                    case "1": game_list_query(_urlGetSt, _pageIndex); break; //游戏列表
                    case "2": game_insert(); break; //游戏添加
                    case "3": server_list_query(0,_urlGetSt, _pageIndex); break; //服务器列表
                    case "4": server_insert(); break; //服务器添加

                    case "10": server_list_query(1, _urlGetSt, _pageIndex); break; //服务器列表（根据名称模糊查询）
                    case "11": game_details_query(_urlGetId); break; //游戏详情
                    case "12": game_delete(_urlGetId); break; //游戏删除
                    case "13": server_details_query(_urlGetId); break; //服务器详情
                    case "14": server_delete(_urlGetId); break; //服务器删除

                    default: game_list_query(_urlGetSt, _pageIndex); break;
                }
            }

        }


        protected void btn_turntopage1_click(object sender, EventArgs e)
        {
            Response.Redirect("gamecfg.aspx?type=1&st=" + _urlGetSt + "&page=" + tb_pageindex1.Text);
        }

        protected void btn_turntopage2_click(object sender, EventArgs e)
        {
            Response.Redirect("gamecfg.aspx?type=3&st=" + _urlGetSt + "&page=" + tb_pageindex2.Text);
        }

        protected void btn_game_state_query_click(object sender, EventArgs e)
        {
            String _gameState = rbl_game_state_query.SelectedValue;
            Response.Redirect("gamecfg.aspx?type=1&st=" + _gameState + "&page=1");
        }

        protected void btn_server_query_by_game_click(object sender, EventArgs e)
        {
            String _gameId = ddl_server_query_by_game.SelectedValue;
            if (_gameId == "-1")
            {
                Response.Redirect("gamecfg.aspx?type=3&page=1");
            }
            else
            {
                Response.Redirect("gamecfg.aspx?type=3&st=" + _gameId + "&page=1");
            }
        }

        protected void btn_server_query_by_server_name_search_click(object sender, EventArgs e)
        {
            String _queryString = Server.HtmlEncode(tb_server_query_by_servername_search.Text);
            Response.Redirect("gamecfg.aspx?type=10&st=" + _queryString + "&page=1");
        }

        protected void rbl_server_insert_type_changed(object sender, EventArgs e)
        {
            if (rbl_server_insert_type.SelectedValue == "0")
            {
                tb_server_name.Visible = true;
                tb_server_name_batch.Visible = false;
                em_server_name.InnerHtml = "服务器名称：";
            }

            if (rbl_server_insert_type.SelectedValue == "1")
            {
                tb_server_name.Visible = false;
                tb_server_name_batch.Visible = true;
                em_server_name.InnerHtml = "服务器名称：<br />（输入多条服务器名称时请使用回车间隔）";
            }
        }

        protected void btn_game_insert_click(object sender, EventArgs e)
        {
            game_insert_update(0);
        }

        protected void btn_game_update_click(object sender, EventArgs e)
        {
            game_insert_update(1);
        }

        protected void btn_game_delete_click(object sender, EventArgs e)
        {
            game_delete(lb_game_id.Text);
        }

        protected void btn_server_insert_click(object sender, EventArgs e)
        {
            server_insert_update(0);
        }

        protected void btn_server_update_click(object sender, EventArgs e)
        {
            server_insert_update(1);
        }

        protected void btn_server_delete_click(object sender, EventArgs e)
        {
            server_delete(lb_server_id.Text);
        }

        protected void btn_server_update_batch_click(object sender, EventArgs e)
        {
            server_insert_update(2);
        }


        //游戏列表
        protected void game_list_query(String _gameState,int _pageIndex)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();

            _ds = _gameLogic.GameQueryByGameState(_gameState, 50, _pageIndex, 20, out _pageAmount);

            switch (_gameState)
            {
                case "0": lb_querytype.Text = "未开仓游戏列表"; rbl_game_state_query.Items.FindByValue("0").Selected = true; break;
                case "1": lb_querytype.Text = "已开仓游戏列表"; rbl_game_state_query.Items.FindByValue("1").Selected = true; break;
                default: lb_querytype.Text = "所有游戏列表"; rbl_game_state_query.Items.FindByValue("-1").Selected = true; break;
            }

            
            pn_game_list_table.Visible = true;

            pagination_pageindex1.InnerHtml = "第" + _pageIndex + "页";
            pagination_pageamount1.InnerHtml = "共" + _pageAmount + "页";

            if (_pageAmount > 1)
            {
                pagination_frame1.Visible = true;


                if (_pageIndex == 1)
                {
                    pagination_prev1.InnerHtml = "";
                    pagination_next1.InnerHtml = "<a href=\"gamecfg.aspx?type=1&st=" + _gameState + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

                if (_pageIndex == _pageAmount)
                {
                    pagination_prev1.InnerHtml = "<a href=\"gamecfg.aspx?type=1&st=" + _gameState + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next1.InnerHtml = "";
                }

                if (_pageIndex > 1 && _pageIndex < _pageAmount)
                {
                    pagination_prev1.InnerHtml = "<a href=\"gamecfg.aspx?type=1&st=" + _gameState + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next1.InnerHtml = "<a href=\"gamecfg.aspx?type=1&st=" + _gameState + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
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
                    rpt_game.DataSource = _ds;
                    rpt_game.DataBind();
                }
                else
                {
                    pagination_pageindex1.Visible = false;
                    pagination_pageamount1.Visible = false;
                    query_null1.Visible = true;
                    pagination_frame1.Visible = false;
                }
            }
            else
            {
                pagination_pageindex1.Visible = false;
                pagination_pageamount1.Visible = false;
                query_null1.Visible = true;
                pagination_frame1.Visible = false;
            }
        }


        //服务器列表 _queryMode: 0根据游戏ID查询，1根据服务器名查询
        protected void server_list_query(int _queryMode,String _queryString, int _pageIndex)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();

            get_gameinfo_into_dropdownlist();

            if (_queryMode == 0)
            {
                _ds = _gameLogic.ServerQueryByGameId(_queryString, _pageIndex, 20, out _pageAmount);
            }
            else
            {
                String _serverName = Server.HtmlDecode(_queryString);
                _ds = _gameLogic.ServerQueryByServerNameSearch(_serverName, _pageIndex, 20, out _pageAmount);
            }

            lb_querytype.Text = "服务器列表";

            pn_server_list_table.Visible = true;

            pagination_pageindex2.InnerHtml = "第" + _pageIndex + "页";
            pagination_pageamount2.InnerHtml = "共" + _pageAmount + "页";

            if (_pageAmount > 1)
            {
                pagination_frame2.Visible = true;

                if (_queryMode == 0)
                {
                    if (_pageIndex == 1)
                    {
                        pagination_prev2.InnerHtml = "";
                        pagination_next2.InnerHtml = "<a href=\"gamecfg.aspx?type=3&st=" + _queryString + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                    }

                    if (_pageIndex == _pageAmount)
                    {
                        pagination_prev2.InnerHtml = "<a href=\"gamecfg.aspx?type=3&st=" + _queryString + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                        pagination_next2.InnerHtml = "";
                    }

                    if (_pageIndex > 1 && _pageIndex < _pageAmount)
                    {
                        pagination_prev2.InnerHtml = "<a href=\"gamecfg.aspx?type=3&st=" + _queryString + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                        pagination_next2.InnerHtml = "<a href=\"gamecfg.aspx?type=3&st=" + _queryString + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                    }
                }
                else
                {
                    if (_pageIndex == 1)
                    {
                        pagination_prev2.InnerHtml = "";
                        pagination_next2.InnerHtml = "<a href=\"gamecfg.aspx?type=10&st=" + _queryString + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                    }

                    if (_pageIndex == _pageAmount)
                    {
                        pagination_prev2.InnerHtml = "<a href=\"gamecfg.aspx?type=10&st=" + _queryString + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                        pagination_next2.InnerHtml = "";
                    }

                    if (_pageIndex > 1 && _pageIndex < _pageAmount)
                    {
                        pagination_prev2.InnerHtml = "<a href=\"gamecfg.aspx?type=10&st=" + _queryString + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                        pagination_next2.InnerHtml = "<a href=\"gamecfg.aspx?type=10&st=" + _queryString + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                    }
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
                    rpt_server.DataSource = _ds;
                    rpt_server.DataBind();

                    int _intNum;


                    if (_queryMode == 0)
                    {
                        if (_urlGetSt == "-1" || _urlGetSt == null || !int.TryParse(_urlGetSt, out _intNum))
                        {
                            lb_querytype.Text = "所有服务器列表";
                            ddl_server_query_by_game.Items.FindByValue("-1").Selected = true;
                        }
                        else
                        {
                            lb_querytype.Text += "，所属游戏：" + _ds.Tables[0].Rows[0]["GameName"].ToString();
                            ddl_server_query_by_game.Items.FindByValue(_urlGetSt).Selected = true;
                        }
                    }

                }
                else
                {
                    pagination_pageindex2.Visible = false;
                    pagination_pageamount2.Visible = false;
                    query_null2.Visible = true;
                    pagination_frame2.Visible = false;
                }
            }
            else
            {
                pagination_pageindex2.Visible = false;
                pagination_pageamount2.Visible = false;
                query_null2.Visible = true;
                pagination_frame2.Visible = false;
            }
        }




        //游戏详细信息
        protected void game_details_query(String _gameId)
        {
            String[] _gameStr = _gameLogic.GameQueryByGameId(_gameId);

            pn_game_details.Visible = true;

            if (_gameStr[0] == null)
            {
                lb_querytype.Text = "游戏详细信息";
                div_game_details.InnerHtml = "没有符合条件的游戏记录！";
            }
            else
            {
                lb_querytype.Text = _gameStr[1] + "详细信息";

                li_game_id.Visible = true;

                lb_game_id.Text = _gameStr[0];
                tb_game_name.Text = _gameStr[1];
                tb_monetary_unit.Text = _gameStr[2];
                tb_game_currency_name.Text = _gameStr[3];
                tb_game_currency_unit.Text = _gameStr[4];
                //rbl_game_state.Items[Convert.ToInt16(_gameStr[5])].Selected = true;
                tb_game_caption.Text = Server.HtmlDecode(_gameStr[6]);

                btn_game_update.Visible = true;
                btn_game_delete.Visible = true;
            }

        }

        //服务器详细信息
        protected void server_details_query(String _serverId)
        {
            String[] _serverStr = _gameLogic.ServerQueryByServerId(_serverId);

            pn_server_details.Visible = true;

            if (_serverStr[0] == null)
            {
                lb_querytype.Text = "服务器详细信息";
                div_server_details.InnerHtml = "没有符合条件的服务器记录！";
            }
            else
            {
                em_server_insert_type.Visible = false;
                rbl_server_insert_type.Visible = false;
                tb_server_name_batch.Visible = false;

                lb_querytype.Text = _serverStr[1] + "详细信息";

                li_server_id.Visible = true;
                get_gameinfo_into_dropdownlist();

                lb_server_id.Text = _serverStr[0];
                tb_server_name.Text = _serverStr[1];
                ddl_server_game.Items.FindByValue(_serverStr[2]).Selected = true;

                tb_purchase_price.Text = _serverStr[4];
                tb_amount_min.Text = _serverStr[5];
                tb_amount_max.Text = _serverStr[6];
                rbl_server_state.Items.FindByValue(_serverStr[7]).Selected = true;

                ddl_server_game.Enabled = false;

                btn_server_update.Visible = true;
                btn_server_delete.Visible = true;
            }

        }

        //游戏添加
        protected void game_insert()
        {
            lb_querytype.Text = "添加游戏";

            lb_game_id.Visible = false;

            btn_game_insert.Visible = true;

            pn_game_details.Visible = true;

        }

        //服务器添加
        protected void server_insert()
        {
            lb_querytype.Text = "添加服务器";
            
            lb_server_id.Visible = false;

            btn_server_insert.Visible = true;

            pn_server_details.Visible = true;

            rbl_server_insert_type.Items.FindByValue("0").Selected = true;

            tb_server_name.Visible = true;
            tb_server_name_batch.Visible = false;

            get_gameinfo_into_dropdownlist();
        }



        //游戏添加、修改 _type 0：添加 1：修改
        protected void game_insert_update(int _type)
        {
            String[] _gameUpdateStr = new String[8];
            String[] _gameInsertStr = new String[7];
            int _retVal;

            if (_type == 0)
            {
                _gameInsertStr[0] = tb_game_name.Text;
                _gameInsertStr[1] = tb_monetary_unit.Text;
                _gameInsertStr[2] = tb_game_currency_name.Text;
                _gameInsertStr[3] = tb_game_currency_unit.Text;
                _gameInsertStr[4] = "10";
                _gameInsertStr[5] = tb_game_caption.Text;

                _retVal = _gameLogic.GameInsert(_gameInsertStr);

                if (_retVal == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"游戏名已存在！\"); </script>");
                }
                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"gamecfg.aspx?type=1\"; </script>");
                }
            }


            if (_type == 1)
            {
                _gameUpdateStr[0] = lb_game_id.Text;
                _gameUpdateStr[1] = tb_game_name.Text;
                _gameUpdateStr[2] = tb_monetary_unit.Text;
                _gameUpdateStr[3] = tb_game_currency_name.Text;
                _gameUpdateStr[4] = tb_game_currency_unit.Text;
                _gameUpdateStr[5] = "10";
                _gameUpdateStr[6] = tb_game_caption.Text;

                _retVal = _gameLogic.GameUpdate(_gameUpdateStr);

                if (_retVal == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作失败！\"); </script>");
                }
                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"gamecfg.aspx?type=1\";</script>");
                }
            }

        }

        //游戏删除
        protected void game_delete(String _gameId)
        {
            int _retVal = _gameLogic.GameDelete(_gameId);

            if (_retVal == 0)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"该游戏不存在！\"); window.location.href=\"gamecfg.aspx?type=1\";</script>");
            }
            if (_retVal == 1)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"该游戏内存在关联服务器！请将服务器删除后重试！\"); window.location.href=\"gamecfg.aspx?type=1\";</script>");
            }
            if (_retVal == 2)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"gamecfg.aspx?type=1\";</script>");
            }
            if (_retVal == -1)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请稍后重试！\"); window.location.href=\"gamecfg.aspx?type=3\";</script>");
            }
        }

        //服务器删除
        protected void server_delete(String _serverId)
        {
            int _retVal = _gameLogic.ServerDelete(_serverId);

            if (_retVal == 0)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"服务器不存在！\"); window.history.go(-1);</script>");
            }
            if (_retVal == 1)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.history.go(-1);</script>");
            }
            if (_retVal == -1)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请稍后重试！\"); window.history.go(-1);</script>");
            }

        }


        //服务器添加、修改 _type 0：添加 1：修改 2：批量修改
        protected void server_insert_update(int _type)
        {
            String[] _serverUpdateStr = new String[7];
            String[] _serverInsertStr = new String[7];
            int _retVal = -1;

            if (_type == 0)
            {
                String _serverInsertType = rbl_server_insert_type.SelectedValue;
                String[] _nameStr = new String[100];

                _serverInsertStr[1] = ddl_server_game.SelectedValue;
                _serverInsertStr[2] = tb_purchase_price.Text.Trim();
                _serverInsertStr[3] = tb_amount_min.Text.Trim();
                _serverInsertStr[4] = tb_amount_max.Text.Trim();
                _serverInsertStr[5] = "10";

                if (_serverInsertType == "0")
                {
                    _serverInsertStr[0] = tb_server_name.Text;
                    _retVal = _gameLogic.ServerInsert(_serverInsertStr);
                }
                if (_serverInsertType == "1")
                {
                    _nameStr = tb_server_name_batch.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    _retVal = _gameLogic.ServerInsertBatch(_nameStr, _serverInsertStr);
                }


                if (_serverInsertType == "0")
                {
                    if (_retVal == 0)
                    {
                        Response.Write("<script type=\"text/javascript\">alert (\"服务器名已存在！\"); </script>");
                    }
                    if (_retVal == 1)
                    {
                        Response.Write("<script type=\"text/javascript\">alert (\"所属游戏不存在！\"); </script>");
                    }
                    if (_retVal == 2)
                    {
                        Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"gamecfg.aspx?type=3\"; </script>");
                    }
                    if (_retVal == -1)
                    {
                        Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请检查输入数据是否正确！\"); </script>");
                    }
                }

                if (_serverInsertType == "1")
                {
                    if (_retVal == 0)
                    {
                        Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请检查输入数据是否正确！\"); </script>");
                    }
                    if (_retVal == 1)
                    {
                        Response.Write("<script type=\"text/javascript\">alert (\"服务器添加部分失败，请查询服务器列表后重试！\"); </script>");
                    }
                    if (_retVal == 2)
                    {
                        Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"gamecfg.aspx?type=3\"; </script>");
                    }
                    if (_retVal == -1)
                    {
                        Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请检查输入数据是否正确！\"); </script>");
                    }
                }

            }


            if (_type == 1)
            {
                _serverUpdateStr[0] = lb_server_id.Text;
                _serverUpdateStr[1] = tb_server_name.Text;
                _serverUpdateStr[2] = tb_purchase_price.Text;
                _serverUpdateStr[3] = tb_amount_min.Text;
                _serverUpdateStr[4] = tb_amount_max.Text;
                _serverUpdateStr[5] = "10";

                 _retVal = _gameLogic.ServerUpdate(_serverUpdateStr);

                if (_retVal == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作失败！\"); </script>");
                }
                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"gamecfg.aspx?type=3\";</script>");
                }
                if (_retVal == -1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请检查输入数据是否正确！\"); </script>");
                }
            }

            if (_type == 2)
            {
                DataTable _dt = new DataTable();
                _dt.Columns.Add("serverId");
                _dt.Columns.Add("serverName");
                _dt.Columns.Add("purchasePrice");
                _dt.Columns.Add("amountMin");
                _dt.Columns.Add("amountMax");
                _dt.Columns.Add("serverState");


                foreach (RepeaterItem _item in rpt_server.Items)
                {
                    Label _lb = (Label)_item.FindControl("lb_rpt_serverid");
                    HiddenField _hf1 = (HiddenField)_item.FindControl("hf_rpt_server_name");
                    TextBox _tb1 = (TextBox)_item.FindControl("tb_rpt_price");
                    TextBox _tb2 = (TextBox)_item.FindControl("tb_rpt_min");
                    TextBox _tb3 = (TextBox)_item.FindControl("tb_rpt_max");
                    HiddenField _hf2 = (HiddenField)_item.FindControl("hf_rpt_server_state");

                    _dt.Rows.Add(_lb.Text, _hf1.Value, _tb1.Text, _tb2.Text, _tb3.Text, "10");

                }

                _retVal = _gameLogic.ServerUpdateBatch(_dt);

                if (_retVal == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请检查输入数据是否正确！\"); </script>");
                }
                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"服务器添加部分失败，请查询服务器列表后重试！\"); </script>");
                }
                if (_retVal == 2)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.reload(); </script>");
                }
                if (_retVal == -1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请检查输入数据是否正确！\"); </script>");
                }

            }

        }



        //获取游戏列表并写入Dropdownlist
        protected void get_gameinfo_into_dropdownlist()
        {
            DataSet _ds = _gameLogic.GameQueryList();

            ddl_server_query_by_game.Items.Clear();
            ddl_server_game.Items.Clear();

            ddl_server_query_by_game.Items.Add(new ListItem("所有游戏", "-1"));

            foreach (DataRow _dr in _ds.Tables[0].Rows)
            {
                ddl_server_query_by_game.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
                ddl_server_game.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
            }
        }

    }
}