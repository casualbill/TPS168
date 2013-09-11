using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPS168.WebSite
{
    public partial class profile : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            String _crtUrl = Server.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery);

            if (!IsPostBack)
            {
                if (Session["userid"] == null)
                {
                    Session.Clear();
                    Response.Redirect("login.aspx?ret=" + _crtUrl);
                }
            }
        }
    }
}