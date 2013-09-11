using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPS168.WebSite.admin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        public String _sessionUsername;
        public String _sessionUsergroupid;
        public String _sessionUsergroupname;

        public void Page_Init(object sender, EventArgs e)
        {
            if (Session["adminid"] == null || Convert.ToInt16(Session["admingroupid"])<1)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                _sessionUsername = Session["adminname"].ToString();
                _sessionUsergroupname = Session["admingroupname"].ToString();
                _sessionUsergroupid = Session["admingroupid"].ToString();

                lbl_username.Text = _sessionUsername;
                lbl_usergroup.Text = _sessionUsergroupname;
            }


        }

        protected void Session_Clear(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Redirect("login.aspx");
        }
    }
}