using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS168.BLL;

namespace TPS168.WebSite
{
    public partial class profile2 : System.Web.UI.Page
    {
        protected UserLogic _userLogic = new UserLogic();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_update_submit_click(object sender, EventArgs e)
        {
            int _retVal;

            String _pwdOld = tb_passwprd_old.Text;
            String _pwdNew = tb_password_new.Text;

            if ((_pwdNew.Length < 6) || (_pwdOld.Length < 6))
            {
                _retVal = -2;
            }
            else
            {
                if (_pwdNew != tb_password_repeat.Text)
                {
                    _retVal = -1;
                }
                else
                {
                    _retVal = _userLogic.UserPasswordUpdate(Session["userid"].ToString(), _pwdOld, _pwdNew);
                }
            }


            switch (_retVal)
            {
                case 1: Session.Clear(); Response.Write("<script type=\"text/javascript\">alert (\"修改成功！请使用新密码重新登录！\"); window.location.href=\"index.aspx\";</script>"); break;
                case -1: span_profile_tips.Attributes["style"] = ""; span_profile_tips.InnerHtml = "两次密码输入不一致，请重新输入！"; break;
                case -2: span_profile_tips.Attributes["style"] = ""; span_profile_tips.InnerHtml = "密码长度必须不少于6个字符！"; break;
                default: span_profile_tips.Attributes["style"] = ""; span_profile_tips.InnerHtml = "原密码错误！"; break;

            }

        }
    }
}