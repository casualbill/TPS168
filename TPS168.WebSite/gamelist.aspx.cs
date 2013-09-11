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
    public partial class gamelist : System.Web.UI.Page
    {
        protected GameLogic _gameLogic = new GameLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            int _pageAmount;
            DataSet _ds = _gameLogic.ServerQueryByGameId("-2", 1, 30000, out _pageAmount);

            if (_ds.Tables.Count > 0)
            {
                rpt_serverlist.DataSource = _ds;
                rpt_serverlist.DataBind();
            }
        }
    }
}