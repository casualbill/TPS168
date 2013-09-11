using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS168.BLL;

namespace TPS168.WebSite
{
    public partial class login : System.Web.UI.Page
    {
        protected UserLogic _userLogic = new UserLogic();
        protected String _retUrl;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {

            String _userName = tb_username.Text;
            String _pwd = tb_password.Text;
            String _authCode = tb_authcode.Text.ToLower();
            String _ipAddress = get_ip_address();

            String[] _retVal = new String[10];

            if (Session["authcode"] != null)
            {
                if (_authCode == Session["authcode"].ToString())
                {
                     _retVal = _userLogic.UserLoginValidate(0, _userName, _pwd, _ipAddress);
                }
                else
                {
                    _retVal[0] = "-2";
                }
            }
            else
            {
                _retVal[0] = "-2";
            }

            _retUrl = Request.QueryString["ret"];

            if (_retUrl == null)
            {
                _retUrl = "\\index.aspx";
            }
            else
            {
                _retUrl = Server.UrlDecode(_retUrl);
            }

            if (_retVal[0] == "1")
            {
                Session["userid"] = _retVal[1];
                Session["username"] = _userName;
                Session["usergroupid"] = _retVal[4];
                Session["usergroupname"] = _retVal[5];
                Session["userstate"] = _retVal[3];

                Response.Redirect(_retUrl);
            }

            if (_retVal[0] == "0")
            {
                lb_loginfo.Text = "用户名或密码错误，请重新输入！";
            }

            if (_retVal[0] == "2")
            {
                lb_loginfo.Text = "您的账号已被禁止访问，请联系客服！";
            }

            if (_retVal[0] == "-2")
            {
                lb_loginfo.Text = "验证码输入错误，请重新输入！";
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