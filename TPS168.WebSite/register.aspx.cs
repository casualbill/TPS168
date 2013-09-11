using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS168.BLL;

namespace TPS168.WebSite
{
    public partial class register : System.Web.UI.Page
    {
        protected UserLogic _userLogic = new UserLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_reg_click(object sender, EventArgs e)
        {
            String _authCode = tb_authcode.Text.ToLower();

            String _userName = tb_username.Text;
            String _pwd = tb_password.Text;
            String _ipAddress = get_ip_address();
            String[] _userStr = new String[10];

            String[] _userDetailsStr = new String[17];
            int _retVal;

            _userDetailsStr[1] = tb_email.Text;
            _userDetailsStr[2] = tb_providername.Text;
            _userDetailsStr[3] = tb_realname.Text;
            _userDetailsStr[4] = tb_tel.Text;
            _userDetailsStr[5] = tb_mobile.Text;
            _userDetailsStr[6] = tb_qq.Text;
            _userDetailsStr[7] = tb_msn.Text;
            _userDetailsStr[8] = tb_address.Text;
            _userDetailsStr[9] = tb_postcode.Text;
            _userDetailsStr[10] = tb_payeename.Text;
            _userDetailsStr[11] = tb_bankaccount.Text;
            _userDetailsStr[12] = tb_alipayname.Text;
            _userDetailsStr[13] = tb_alipayaccount.Text;
            _userDetailsStr[14] = tb_99billname.Text;
            _userDetailsStr[15] = tb_99bill_account.Text;

            if (_pwd != tb_password_repeat.Text)
            {
                _retVal = -1;
            }
            else
            {
                int _checkinfo = 1;
                for (int i = 1; i < 6; i++)
                {
                    if (_userDetailsStr[i].Length < 1)
                    {
                        _checkinfo = 0;
                    }
                }

                if (_checkinfo == 0)
                {
                    _retVal = -1;
                }
                else
                {
                    if (Session["authcode"] != null)
                    {
                        if (_authCode == Session["authcode"].ToString())
                        {
                            _retVal = _userLogic.UserCreate(_userName, _pwd, _ipAddress, _userDetailsStr, out _userStr);
                        }
                        else
                        {
                            _retVal = -2;
                        }
                    }
                    else
                    {
                        _retVal = -2;
                    }
                }
            }

            if (_retVal == 2)
            {
                Session["userid"] = _userStr[1];
                Session["username"] = _userStr[2];
                Session["usergroupid"] = _userStr[4];
                Session["usergroupname"] = _userStr[5];
                Session["userstate"] = _userStr[2];

                Response.Write("<script type=\"text/javascript\">alert (\"注册成功！\"); window.location.href=\"index.aspx\";</script>");
            }
            else
            {
                span_reg_tips.Attributes["style"] = "";

                if (_retVal == 0)
                {
                    span_reg_tips.InnerHtml = "该用户名已存在！";
                }
                else
                {
                    if (_retVal == -2)
                    {
                        span_reg_tips.InnerHtml = "验证码输入错误，请重新输入！";
                    }
                    else
                    {
                        span_reg_tips.InnerHtml = "注册失败，请正确填写注册信息！";
                    }
                }
            }
        }

        protected String get_ip_address()
        {
            String _ipAddress;

            System.Web.HttpContext _httpContext = System.Web.HttpContext.Current;

            if (_httpContext.Request.ServerVariables["HTTP_VIA"] != null) //判断是否有代理
            {
                _ipAddress = _httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                _ipAddress = _httpContext.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

            return _ipAddress;
        }
    }
}