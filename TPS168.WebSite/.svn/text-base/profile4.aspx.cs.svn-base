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
    public partial class profile4 : System.Web.UI.Page
    {
        protected OrderLogic _orderLogic = new OrderLogic();
        protected UserLogic _userLogic = new UserLogic();
        protected GameLogic _gameLogic = new GameLogic();

        String[] _userStr;
        String[] _gameStr;
        String[] _serverStr;
        String _urlGetId;

        protected void Page_Load(object sender, EventArgs e)
        {
            _urlGetId = Request.QueryString["id"];

            if (Session["userstate"].ToString() != "3")
            {
                Response.Write("<script type=\"text/javascript\">alert (\"您的账号还未通过审核，请联系客服开通账号！\"); window.location.href=\"index.aspx\"; </script>");          
            }

            if (!IsPostBack)
            {
                if (_urlGetId != null)
                {
                    int _intNum;
                    if (int.TryParse(_urlGetId, out _intNum))
                    {
                        order_details_query(_urlGetId);
                    }
                    else
                    {
                        Response.Redirect("placeorder.aspx");
                    }
                }
                else
                {
                    Response.Redirect("placeorder.aspx");
                }
            }
        }

        protected void btn_placeorder_click(object sender, EventArgs e)
        {
            place_order(_urlGetId);
        }


        //下单相关信息显示
        protected void order_details_query(String _serverId)
        {

            _serverStr = _gameLogic.ServerQueryByServerId(_serverId);

            if (Session["userid"] != null)
            {
                _userStr = _userLogic.UserQueryByUserNameOrUserId(Session["userid"].ToString(), 1);


                if (_serverStr[0] == null)
                {
                    span_profile_tips.Attributes["Style"] = "margin-bottom:25px;";
                    span_profile_tips.InnerHtml = "未找到服务器，请重新下单！";
                    pn_placeorder.Visible = false;
                }
                else
                {
                    if (_serverStr[7] != "1")
                    {
                        span_profile_tips.Attributes["Style"] = "margin-bottom:25px;";
                        span_profile_tips.InnerHtml = "很抱歉，服务器暂未开放！";
                        pn_placeorder.Visible = false;
                    }
                    else
                    {
                        _gameStr = _gameLogic.GameQueryByGameId(_serverStr[2]);

                        lb_game_name.Text = _serverStr[3];
                        lb_server_name.Text = _serverStr[1];
                        lb_amount_min.Text = _serverStr[5] + _gameStr[3];
                        lb_amount_max.Text = _serverStr[6] + _gameStr[3];
                        lb_price.Text = _gameStr[2] + " " + _serverStr[4] + "/" + _gameStr[3];

                        hf_purchase_price.Value = _serverStr[4];
                        hf_amount_min.Value = _serverStr[5];
                        hf_amount_max.Value = _serverStr[6];

                        lb_provider.Text = _userStr[11];
                        lb_real_name.Text = _userStr[12];
                        lb_tel.Text = _userStr[13];
                        lb_mob.Text = _userStr[14];
                        lb_qq.Text = _userStr[15];
                        lb_msn.Text = _userStr[16];

                        if (_userStr[19].Length < 1 || _userStr[20].Length < 1)
                        {
                            li_tradetype1.Visible = false;
                            rb_bank.Enabled = false;
                        }
                        else
                        {
                            li_tradetype1.Visible = true;
                            lb_payee.Text = _userStr[19];
                            lb_bank_account.Text = _userStr[20];
                        }

                        if (_userStr[21].Length < 1 || _userStr[22].Length < 1)
                        {
                            li_tradetype2.Visible = false;
                            rb_alipay.Enabled = false;
                        }
                        else
                        {
                            li_tradetype2.Visible = true;
                            lb_alipay_name.Text = _userStr[21];
                            lb_alipay_account.Text = _userStr[22];
                        }

                        if (_userStr[23].Length < 1 || _userStr[24].Length < 1)
                        {
                            li_tradetype3.Visible = false;
                            rb_99bill.Enabled = false;
                        }
                        else
                        {
                            li_tradetype3.Visible = true;
                            lb_99bill_name.Text = _userStr[23];
                            lb_99bill_account.Text = _userStr[24];
                        }

                        lb_currency_unit.Text = " " + _gameStr[3];
                    }
                }
            }
        }

        //生成订单
        protected void place_order(String _serverId)
        {
            int _flag = 1;
            int _retVal;
            String[] _orderStr = new String[11];

            _serverStr = _gameLogic.ServerQueryByServerId(_serverId);

            if (Session["userid"] == null)
            {
                String _crtUrl = Server.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery);
                Session.Clear();
                Response.Redirect("login.aspx?ret=" + _crtUrl);
            }
            else
            {
                _userStr = _userLogic.UserQueryByUserNameOrUserId(Session["userid"].ToString(), 1);


                _orderStr[1] = Session["userid"].ToString();
                _orderStr[2] = _serverStr[0];
                _orderStr[3] = tb_rolename.Text;

                if (_orderStr[3].Length < 1) { _flag = 0; }

                _orderStr[4] = tb_amount.Text;

                int _orderAmount;
                try
                {
                    _orderAmount = Convert.ToInt32(_orderStr[4]);

                    if ((_orderAmount < Convert.ToInt32(_serverStr[5])) || (_orderAmount > Convert.ToInt32(_serverStr[6])))
                    {
                        _flag = 0;
                    }
                }
                catch
                {
                    _flag = 0;
                }


                if (rb_bank.Checked == true)
                {
                    _orderStr[5] = "0";
                    _orderStr[6] = _userStr[19];
                    _orderStr[7] = _userStr[20];
                }
                if (rb_alipay.Checked == true)
                {
                    _orderStr[5] = "1";
                    _orderStr[6] = _userStr[21];
                    _orderStr[7] = _userStr[22];
                }
                if (rb_99bill.Checked == true)
                {
                    _orderStr[5] = "2";
                    _orderStr[6] = _userStr[23];
                    _orderStr[7] = _userStr[24];
                }

                if (rb_bank.Checked == false && rb_alipay.Checked == false && rb_99bill.Checked == false) { _flag = 0; }

                _orderStr[8] = tb_trade_time.Text;

                if (_orderStr[8].Length < 1)
                {
                    _orderStr[8] = null;
                }

                _orderStr[9] = Server.HtmlEncode(tb_remarks.Text);

                if (_flag == 1)
                {
                    _retVal = _orderLogic.OrderCreate(_orderStr);
                }
                else
                {
                    _retVal = -2;
                }

                span_profile_tips.Attributes["Style"] = "";


                switch (_retVal)
                {
                    case 3: span_profile_tips.InnerHtml = "服务器不存在！"; break;
                    case 4: span_profile_tips.InnerHtml = "服务器未开放！"; break;
                    case 5: Response.Write("<script type=\"text/javascript\">alert (\"订单已生成！\"); window.location.href=\"profile4.aspx\"; </script>"); break;
                    case -2: span_profile_tips.InnerHtml = "请正确输入订单信息！"; break;
                    default: span_profile_tips.InnerHtml = "操作失败，请稍后再试！"; break;
                }

            }
        }

    }
}