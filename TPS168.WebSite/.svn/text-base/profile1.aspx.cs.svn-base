using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS168.BLL;

namespace TPS168.WebSite
{
    public partial class profile1 : System.Web.UI.Page
    {
        protected UserLogic _userLogic = new UserLogic();
        protected String _sessionUserId;
        protected String[] _userStr = new String[30];
        protected String _userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                _sessionUserId = Session["userid"].ToString();
                get_user_details();
            }
        }

        protected void btn_update_submit_click(object sender, EventArgs e)
        {
            update_user_details();
        }


        protected void get_user_details()
        {
            _userStr = _userLogic.UserQueryByUserNameOrUserId(_sessionUserId, 1);

            _userId = _userStr[1];
            tb_providername.Text = _userStr[11];
            tb_realname.Text = _userStr[12];
            tb_tel.Text = _userStr[13];
            tb_mobile.Text = _userStr[14];
            tb_qq.Text = _userStr[15];
            tb_msn.Text = _userStr[16];
            tb_postcode.Text = _userStr[18];
            tb_address.Text = _userStr[17];
                
        }

        protected void update_user_details()
        {
            int _retVal;
            String[] _userUpdateStr = new String[10];

            _userUpdateStr[0] = Session["userid"].ToString(); ;
            _userUpdateStr[1] = tb_providername.Text;
            _userUpdateStr[2] = tb_realname.Text;
            _userUpdateStr[3] = tb_tel.Text;
            _userUpdateStr[4] = tb_mobile.Text;
            _userUpdateStr[5] = tb_qq.Text;
            _userUpdateStr[6] = tb_msn.Text;
            _userUpdateStr[7] = tb_address.Text;
            _userUpdateStr[8] = tb_postcode.Text;

            int _checkinfo = 1;
            for (int i = 1; i < 5; i++)
            {
                if (_userUpdateStr[i].Length < 1)
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
                _retVal = _userLogic.UserDetailsUpdate(_userUpdateStr);
            }

            span_profile_tips.Attributes["style"] = "";

            if (_retVal == 1)
            {
                span_profile_tips.InnerHtml = "修改成功！";
            }
            else
            {
                span_profile_tips.InnerHtml = "修改失败，请稍后重试！";
            }
        }
    }
}