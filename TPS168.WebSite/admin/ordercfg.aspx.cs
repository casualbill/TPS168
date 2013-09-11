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
    public partial class ordercfg : System.Web.UI.Page
    {
        protected String _type;
        protected String _pageStr;
        protected int _pageIndex;

        protected String _urlGetState;
        protected String _urlGetStr;

        protected OrderLogic _orderLogic = new OrderLogic();
        protected UserLogic _userLogic = new UserLogic();
        protected GameLogic _gameLogic = new GameLogic();

        protected String[] _orderStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 8) == 0)
            {
                Response.Redirect("error.aspx");
            }

            _type = Request.QueryString["type"];
            _pageStr = Request.QueryString["page"];
            _urlGetState = Request.QueryString["qm"];
            _urlGetStr = Server.HtmlDecode(Request.QueryString["id"]);

            if (_urlGetStr == null)
            {
                _urlGetStr = "";
            }

            if (Session["token"] != null)
            {
                hf_token.Value = Session["token"].ToString();
            }

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

            int _orderState;

            if (!IsPostBack)
            {
                switch (_urlGetState)
                {
                    case "0": _orderState = 0; break;
                    case "1": _orderState = 1; break;
                    case "2": _orderState = 2; break;
                    case "3": _orderState = 3; break;
                    case "4": _orderState = 4; break;
                    default: _orderState = -1; break;
                }

                switch (_type)
                {
                    case "1": order_list_query(_orderState, _urlGetStr, _pageIndex); break; //订单列表查询
                    case "2": order_details_query(_urlGetStr); break; //订单详情查询
                    case "3": order_log_query(_urlGetStr, _pageIndex); break; //订单操作记录查询
                    default: order_list_query(_orderState, _urlGetStr, _pageIndex); break;
                }

                get_gameinfo_into_dropdownlist();

                if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 8) != 2)
                {
                    btn_order_amount_update.Enabled = false;
                }
            }
        }

        protected void btn_search_new_click(object sender, EventArgs e)
        {
            Response.Redirect("ordercfg.aspx?type=1&qm=" + _urlGetState + "&id=" + Server.HtmlEncode(tb_search_new.Text.Trim()));
        }


        protected void btn_turntopage1_click(object sender, EventArgs e)
        {
            Response.Redirect("ordercfg.aspx?type=1&qm=" + _urlGetState + "&id=" + Server.HtmlEncode(_urlGetStr) + "&page=" + tb_pageindex1.Text);
        }

        protected void btn_turntopage2_click(object sender, EventArgs e)
        {
            Response.Redirect("ordercfg.aspx?type=3&id=" + _urlGetStr + "&page=" + tb_pageindex2.Text.Trim());
        }

        //以下4个为旧版查询
        protected void btn_orderid_query_click(object sender, EventArgs e)
        {
            Response.Redirect("ordercfg.aspx?type=2&id=" + tb_orderid_query.Text);
        }

        protected void btn_user_query_click(object sender, EventArgs e)
        {
            Response.Redirect("ordercfg.aspx?type=1&qm=3&id=" + user_id_query(tb_user_query.Text) + "&page=1");

        }

        protected void btn_game_query_click(object sender, EventArgs e)
        {
            Response.Redirect("ordercfg.aspx?type=1&qm=1&id=" + ddl_game_query.SelectedValue + "&page=1");

        }

        protected void btn_server_query_click(object sender, EventArgs e)
        {
            Response.Redirect("ordercfg.aspx?type=1&qm=2&id=" + ddl_server_query.SelectedValue + "&page=1");

        }

        protected void btn_orderlog_query_click(object sender, EventArgs e)
        {
            Response.Redirect("ordercfg.aspx?type=3&id=" + tb_orderid.Text);
        }

        protected void btn_order_state_update_click(object sender, EventArgs e)
        {
            order_state_update(true);
        }

        protected void btn_order_fail_click(object sender, EventArgs e)
        {
            order_state_update(false);
        }

        protected void btn_order_amount_update_click(object sender, EventArgs e)
        {
            order_amount_update();
        }

        protected void ddl_game_query_changed(object sender, EventArgs e)
        {
            gameserver_update(ddl_game_query.SelectedValue);
        }

        protected void btn_export_to_csv_click(object sender, EventArgs e)
        {
            /*
            if (_orderLogic.OrderExportToCSV((DataSet)ViewState["dsExport"], "order") == false)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"没有可供导出的数据！\"); </script>");
            }
            */
            Response.Redirect("ordercsv.aspx?qm=" + _urlGetState + "&id=" + _urlGetStr);
        }


        //订单列表查询
        protected void order_list_query(int _orderState, String _queryString, int _pageIndex)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();
            //DataSet _dsExport = new DataSet();

            String _querytypeStr;

            switch (_orderState)
            {
                case 0: _querytypeStr = "订单列表：等待审核"; break;
                case 1: _querytypeStr = "订单列表：审核通过-等待交易"; break;
                case 2: _querytypeStr = "订单列表：交易完成-等待汇款"; break;
                case 3: _querytypeStr = "订单列表：完成汇款"; break;
                case 4: _querytypeStr = "订单列表：失败订单"; break;
                default: _querytypeStr = "所有订单列表"; break;
            }

            

            if (_queryString != "")
            {
                _querytypeStr += " 关键词：" + _queryString.Trim();
            }

            lb_querytype.Text = _querytypeStr;

            //_dsExport = _orderLogic.OrderQueryNew(_orderState.ToString(), _queryString, 1, 10000, out _pageAmount);
            //ViewState["dsExport"] = _dsExport;

            _ds = _orderLogic.OrderQueryNew(_orderState.ToString(), _queryString, _pageIndex, 20, out _pageAmount);

            btn_export_to_csv.Visible = true;

            pn_order_list_table.Visible = true;
            query_null1.Visible = false;
            rpt_order_list.Visible = true;
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
                    pagination_next1.InnerHtml = "<a href=\"ordercfg.aspx?type=1&qm=" + _orderState.ToString() + "&id=" + _queryString + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

                if (_pageIndex == _pageAmount)
                {
                    pagination_prev1.InnerHtml = "<a href=\"ordercfg.aspx?type=1&qm=" + _orderState.ToString() + "&id=" + _queryString + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next1.InnerHtml = "";
                }

                if (_pageIndex > 1 && _pageIndex < _pageAmount)
                {
                    pagination_prev1.InnerHtml = "<a href=\"ordercfg.aspx?type=1&qm=" + _orderState.ToString() + "&id=" + _queryString + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next1.InnerHtml = "<a href=\"ordercfg.aspx?type=1&qm=" + _orderState.ToString() + "&id=" + _queryString + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
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
                    rpt_order_list.DataSource = _ds;
                    rpt_order_list.DataBind();



                    lb_totalamount.Visible = true;
                    Double _totalAmount = _orderLogic.OrderPriceAmount(_orderState);

                    lb_totalamount.Text = "累计金额为" + _totalAmount.ToString() + "元";
                    

                }
                else
                {
                    rpt_order_list.Visible = false;
                    pagination_pageindex1.Visible = false;
                    pagination_pageamount1.Visible = false;
                    query_null1.Visible = true;
                    pagination_frame1.Visible = false;
                }
            }
            else
            {
                rpt_order_list.Visible = false;
                pagination_pageindex1.Visible = false;
                pagination_pageamount1.Visible = false;
                query_null1.Visible = true;
                pagination_frame1.Visible = false;
            }

        }


        //订单详情查询
        protected void order_details_query(String _orderId)
        {
            _orderStr = _orderLogic.OrderQueryByOrderId(_orderId);

            pn_order_details_list.Visible = true;

            if (_orderStr[0] == null)
            {
                lb_querytype.Text = "订单详细信息";
                div_order_details.InnerHtml = "没有符合条件的订单记录！";
            }
            else
            {
                lb_querytype.Text = "订单详细信息：订单号" + _orderStr[0];

                lb_order_details0.Text = _orderStr[0];
                lb_order_details1.Text = _orderStr[1];
                lb_order_details2.Text = _orderStr[2];
                //lb_order_details3.Text = _orderStr[3];
                lb_order_details4.Text = _orderStr[4];
                //lb_order_details5.Text = _orderStr[5];
                lb_order_details6.Text = _orderStr[6];
                lb_order_details7.Text = _orderStr[7];
                lb_order_details8.Text = _orderStr[8];
                lb_order_details9.Text = _orderStr[9];
                lb_order_details10.Text = _orderStr[10];

                lb_order_details11.Text = _orderStr[25];

                lb_order_details12.Text = _orderStr[12];
                lb_order_details13.Text = _orderStr[13];
                lb_order_details14.Text = _orderStr[14];
                lb_order_details15.Text = _orderStr[15];
                lb_order_details16.Text = _orderStr[16];
                lb_order_details17.Text = Server.HtmlDecode(_orderStr[17]);
                lb_order_details18.Text = _orderStr[18];

                lb_order_details19.Text = _orderStr[24];

                lb_order_details20.Text = _orderStr[20];
                lb_order_details21.Text = _orderStr[21];
                lb_order_details22.Text = _orderStr[22];
                lb_order_details23.Text = _orderStr[23];

                hf_order_state.Value = _orderStr[19];

                switch (_orderStr[19])
                {
                    case "0": btn_order_state_update.Text = "审核通过-等待交易"; tb_order_serial_number.Enabled = false; break;
                    case "1": btn_order_state_update.Text = "交易完成-等待汇款"; tb_order_serial_number.Enabled = false; break;
                    case "2": btn_order_state_update.Text = "完成汇款"; break;
                    case "3": btn_order_state_update.Text = "更改流水号"; break;
                    default: btn_order_state_update.Visible = false; tb_order_serial_number.Enabled = false; break;
                }

                if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 8) < 2)
                {
                    tb_order_amount.Enabled = false;
                    btn_order_amount_update.Enabled = false;
                }

            }
        }



        //订单操作记录查询
        protected void order_log_query(String _orderId, int _pageIndex)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();

            if (_orderId != "")
            {
                lb_querytype.Text = "订单操作记录查询";

                long _longNum;
                if (!long.TryParse(_orderId, out _longNum))
                {
                    _orderId = null;
                }
                else
                {
                    lb_querytype.Text += "：订单号" + _orderId;
                }
            }
            else
            {
                _orderId = "-1";
                lb_querytype.Text = "所有订单操作记录查询";
            }


            _ds = _orderLogic.OrderLogQueryByOrderId(_orderId, _pageIndex, 20, out _pageAmount);

            pn_order_log_list.Visible = true;
            query_null2.Visible = false;
            rpt_order_log.Visible = true;
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
                    pagination_next2.InnerHtml = "<a href=\"ordercfg.aspx?type=3&id=" + _orderId + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

                if (_pageIndex == _pageAmount)
                {
                    pagination_prev2.InnerHtml = "<a href=\"ordercfg.aspx?type=3&id=" + _orderId + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next2.InnerHtml = "";
                }

                if (_pageIndex > 1 && _pageIndex < _pageAmount)
                {
                    pagination_prev2.InnerHtml = "<a href=\"ordercfg.aspx?type=3&id=" + _orderId + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next2.InnerHtml = "<a href=\"ordercfg.aspx?type=3&id=" + _orderId + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
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
                    rpt_order_log.DataSource = _ds;
                    rpt_order_log.DataBind();
                }
                else
                {
                    rpt_order_log.Visible = false;
                    pagination_pageindex2.Visible = false;
                    pagination_pageamount2.Visible = false;
                    query_null2.Visible = true;
                    pagination_frame2.Visible = false;
                }
            }
            else
            {
                rpt_order_log.Visible = false;
                pagination_pageindex2.Visible = false;
                pagination_pageamount2.Visible = false;
                query_null2.Visible = true;
                pagination_frame2.Visible = false;
            }

        }


        //订单管理：更改订单状态   _stateType true：更新，false：失败
        protected void order_state_update(bool _stateType)
        {
            int _retVal;

            String _orderId = lb_order_details0.Text;
            String _orderUpdateState;
            String _serialNumber;
            String _remarks;

            if (_stateType == true)
            {
                switch (hf_order_state.Value)
                {
                    case "0": _orderUpdateState = "1"; break;
                    case "1": _orderUpdateState = "2"; break;
                    case "2": _orderUpdateState = "3"; break;
                    case "3": _orderUpdateState = "3"; break;
                    default: _orderUpdateState = null; break;
                }
            }
            else
            {
                _orderUpdateState = "4";
            }

            _serialNumber = tb_order_serial_number.Text;
            _remarks = Server.HtmlEncode(tb_remarks.Text);


            if (_orderUpdateState == "3" && _serialNumber.Length < 1)
            {
                _retVal = -2;
            }
            else
            {
                if (Session["adminid"] != null)
                {
                    _retVal = _orderLogic.OrderOperation(Session["adminid"].ToString(), _orderId, _orderUpdateState, _serialNumber, _remarks);
                }
                else
                {
                    _retVal = -3;
                }
            }

            switch (_retVal)
            {
                case 0: Response.Write("<script type=\"text/javascript\">alert (\"订单号不存在！\"); </script>"); break;
                case 1: Response.Write("<script type=\"text/javascript\">alert (\"订单信息已过期，请刷新后重试！\"); window.location.href=\"ordercfg.aspx?type=2&id=" + _orderId + "\"; </script>"); break;
                case 2: Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"ordercfg.aspx?type=2&id=" + _orderId + "\"; </script>"); break;
                case -2: Response.Write("<script type=\"text/javascript\">alert (\"请输入流水号！\"); </script>"); break;
                default: Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请稍后重试！\"); </script>"); break;
            }

        }

        //订单管理：更改数量
        protected void order_amount_update()
        {
            int _retVal;
            String _orderId = lb_order_details0.Text;
            String _amount = tb_order_amount.Text;
            String _remarks = Server.HtmlEncode(tb_remarks.Text);

            if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 8) == 2)
            {
            
                long _longNum;
                if (!long.TryParse(_amount, out _longNum))
                {
                    _retVal = -2;
                }
                else
                {
                    _retVal = _orderLogic.OrderAmountUpdate(Session["adminid"].ToString(), _orderId, _amount, _remarks);
                }
            }
            else
            {
                _retVal = -3;
            }
            switch (_retVal)
            {
                case 0: Response.Write("<script type=\"text/javascript\">alert (\"订单号不存在！\"); </script>"); break;
                case 1: Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"ordercfg.aspx?type=2&id=" + _orderId + "\"; </script>"); break;
                case -2: Response.Write("<script type=\"text/javascript\">alert (\"请正确输入数量值！\"); </script>"); break;
                case -3: Response.Write("<script type=\"text/javascript\">alert (\"你没有此项操作权限！\"); </script>"); break;
                default: Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请稍后重试！\"); </script>"); break;
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