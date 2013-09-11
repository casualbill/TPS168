using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPS168.BLL;

namespace TPS168.WebSite
{
    public partial class profile3 : System.Web.UI.Page
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

            if (_userStr[19].Trim() != "")
            {
                tb_payeename.Text = _userStr[19];
                //tb_payeename.Enabled = false;
            }

            if (_userStr[20].Trim() != "")
            {
                tb_bankaccount.Text = _userStr[20];
                //tb_bankaccount.Enabled = false;
            }

            if (_userStr[21].Trim() != "")
            {
                tb_alipayname.Text = _userStr[21];
                //tb_alipayname.Enabled = false;
            }

            if (_userStr[22].Trim() != "")
            {
                tb_alipayaccount.Text = _userStr[22];
                //tb_alipayaccount.Enabled = false;
            }

            if (_userStr[23].Trim() != "")
            {
                tb_99name.Text = _userStr[23];
                //tb_99name.Enabled = false;
            }

            if (_userStr[24].Trim() != "")
            {
                tb_99account.Text = _userStr[24];
                //tb_99account.Enabled = false;
            }

        }

        protected void update_user_details()
        {
            int _retVal;
            String[] _userUpdateStr = new String[8];

            _userUpdateStr[0] = Session["userid"].ToString();
            _userUpdateStr[1] = tb_payeename.Text.Trim();
            _userUpdateStr[2] = tb_bankaccount.Text.Trim();
            _userUpdateStr[3] = tb_alipayname.Text.Trim();
            _userUpdateStr[4] = tb_alipayaccount.Text.Trim();
            _userUpdateStr[5] = tb_99name.Text.Trim();
            _userUpdateStr[6] = tb_99account.Text.Trim();

            _retVal = _userLogic.UserPayInfoUpdate(_userUpdateStr);

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