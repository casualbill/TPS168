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
    public partial class site_mp : System.Web.UI.MasterPage
    {
        protected UserLogic _userLogic = new UserLogic();
        protected NewsLogic _newsLogic = new NewsLogic();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userid"] == null)
                {
                    pn_header_login.Visible = true;
                }
                else
                {
                    pn_header_logged.Visible = true;
                    lb_username.Text = Session["username"].ToString();
                }
            }

            header_news_roll();

            remarks.InnerHtml += ("<input type='hidden' value='网站开发：集算网络工作室 Casual Bill' />");
        }


        protected void btn_login_click(object sender, EventArgs e)
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


            if (_retVal[0] == "1")
            {
                Session["userid"] = _retVal[1];
                Session["username"] = _userName;
                Session["usergroupid"] = _retVal[4];
                Session["usergroupname"] = _retVal[5];
                Session["userstate"] = _retVal[3];

                pn_header_login.Visible = false;
                pn_header_logged.Visible = true;
                lb_username.Text = Session["username"].ToString();
            }

            if (_retVal[0] == "0")
            {
                Response.Write("<script type=\"text/javascript\">alert (\"用户名或密码错误，请重新输入！\"); window.location.href=\"login.aspx\";</script>");
            }

            if (_retVal[0] == "2")
            {
                Response.Write("<script type=\"text/javascript\">alert (\"您的账号已被禁止访问，请联系客服！！\"); window.location.href=\"login.aspx\";</script>");
            }

            if (_retVal[0] == "-2")
            {
                Response.Write("<script type=\"text/javascript\">alert (\"验证码输入错误，请重新输入！\");</script>");
            }
        }

        protected void btn_exit_click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Write("<script type=\"text/javascript\">alert (\"您已安全退出系统！\"); window.location.href=\"index.aspx\";</script>");
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

        protected void header_news_roll()
        {
            int _pageAmount;
            int _foreachIndex = 0;
            String[] _newsRollId = new String[5];
            String[] _newsRollContent = new String[5];
            DataSet _ds = _newsLogic.NewsQueryByNewsType("1", 1, 50, 1, 5, out _pageAmount);

            if (_ds.Tables.Count > 0)
            {
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    _newsRollId[_foreachIndex] = _dr[0].ToString();
                    _newsRollContent[_foreachIndex] = _dr[1].ToString() + " [" + _dr[6].ToString() + "]";
                    _foreachIndex++;
                }
            }

            ipt_news_id0.Value = _newsRollId[0];
            ipt_news_id1.Value = _newsRollId[1];
            ipt_news_id2.Value = _newsRollId[2];
            ipt_news_id3.Value = _newsRollId[3];
            ipt_news_id4.Value = _newsRollId[4];

            ipt_news_content0.Value = _newsRollContent[0];
            ipt_news_content1.Value = _newsRollContent[1];
            ipt_news_content2.Value = _newsRollContent[2];
            ipt_news_content3.Value = _newsRollContent[3];
            ipt_news_content4.Value = _newsRollContent[4];

            ipt_news_count.Value = _foreachIndex.ToString();

        }

    }
}