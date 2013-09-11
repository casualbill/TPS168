using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TPS168.BLL;

namespace TPS168.WebSite.admin
{
    public partial class ordercsv : System.Web.UI.Page
    {
        protected String _pageStr;

        protected String _urlGetState;
        protected String _urlGetStr;

        protected OrderLogic _orderLogic = new OrderLogic();
        protected UserLogic _userLogic = new UserLogic();

        protected String[] _orderStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 8) == 0)
            {
                Response.Redirect("error.aspx");
            }

            _urlGetState = Request.QueryString["qm"];
            _urlGetStr = Server.HtmlDecode(Request.QueryString["id"]);

            if (_urlGetStr == null)
            {
                _urlGetStr = "";
            }

            int _orderState;

            if (!IsPostBack)
            {
                switch (_urlGetState)
                {
                    case "0": _orderState = 0; break;
                    case "1": _orderState = 1; break;
                    case "2": _orderState = 2; break;
                    case "3": _orderState = 3; break;
                    case "4": _orderState = 4; break;
                    default: _orderState = -1; break;
                }

                int _pageAmount;
                
                if (_orderLogic.OrderExportToCSV(_orderLogic.OrderQueryNew(_orderState.ToString(), _urlGetStr, 1, 10000, out _pageAmount), "order") == false)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"没有可供导出的数据！\"); </script>");
                }

            }
        }
    }
}