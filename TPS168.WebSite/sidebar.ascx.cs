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
    public partial class sidebar : System.Web.UI.UserControl
    {
        protected NewsLogic _newsLogic = new NewsLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            int _pageAmount;
            DataSet _ds = _newsLogic.NewsQueryByNewsType("1", 1, 50, 1, 5, out _pageAmount);

            if (_ds.Tables.Count > 0)
            {
                rpt_sidebar_news.DataSource = _ds;
                rpt_sidebar_news.DataBind();
            }
        }
    }
}