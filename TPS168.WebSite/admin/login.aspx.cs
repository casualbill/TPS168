using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS168.BLL;

namespace TPS168.WebSite.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected UserLogic _userLogic = new UserLogic();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {

            String _userName = tb_username.Text;
            String _pwd = tb_password.Text;
            String _ipAddress = get_ip_address();

            String[] _retVal = _userLogic.UserLoginValidate(1, _userName, _pwd, _ipAddress);

            if (_retVal[0] == "1")
            {
                Session["adminid"] = _retVal[1];
                Session["adminname"] = _userName;
                Session["admingroupid"] = _retVal[4];
                Session["admingroupname"] = _retVal[5];
                Session["adminstate"] = _retVal[3];
                Session["token"] = _userLogic.CreateRandomCode(8);

                Response.Redirect("index.aspx");
            }

            if (_retVal[0] == "0")
            {
                lb_loginfo.Text = "用户名或密码错误，请重新输入！";
            }

            if (_retVal[0] == "2")
            {
                lb_loginfo.Text = "您的账号已被禁止访问，请联系客服！";
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