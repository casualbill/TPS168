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
    public partial class article : System.Web.UI.Page
    {
        protected NewsLogic _newsLogic = new NewsLogic();
        protected String _urlGetId;
        protected String _urlGettype;

        protected void Page_Load(object sender, EventArgs e)
        {
            _urlGettype = Request.QueryString["type"];
            _urlGetId = Request.QueryString["id"];

            if (_urlGettype == null && _urlGetId == null)
            {
                lb_news_null.Visible = true;
            }
            else
            {
                if (_urlGetId != null)
                {
                    news_details_query(_urlGetId);
                }
                else
                {
                    news_list_query(_urlGettype);
                }
            }
            
        }

        protected void news_list_query(String _type)
        {
            int _typeNum;

            if (!int.TryParse(_type, out _typeNum))
            {
                lb_news_null.Visible = true;
            }

            switch (_typeNum)
            {
                case 1: news_list_query_type(1); break;
                case 2: news_list_query_type(2); break;
                case 3: news_list_query_type(3); break;
                default: lb_news_null.Visible = true; break;
            }

        }

        protected void news_details_query(String _newsId)
        {
            String[] _newsStr = new String[10];

            _newsStr = _newsLogic.NewsContentQueryByNewsId(_newsId);

            if (_newsStr[0] == null)
            {
                lb_news_null.Visible = true;
            }
            else
            {
                pn_news_details.Visible = true;

                lb_news_title.Text = _newsStr[1];
                lb_news_time.Text = _newsStr[5];
                lb_news_content.Text = Server.HtmlDecode(_newsStr[2]);

                switch (_newsStr[3])
                {
                    case "1": hl_news_type.Text = "站内公告"; hl_news_type.NavigateUrl = "article.aspx?type=1"; break;
                    case "2": hl_news_type.Text = "游戏资讯"; hl_news_type.NavigateUrl = "article.aspx?type=2"; break;
                    case "3": hl_news_type.Text = "常见问题"; hl_news_type.NavigateUrl = "article.aspx?type=3"; break;
                }
            }
            
        }


        protected void news_list_query_type(int _type)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();

            if (_type == 1)
            {
                hl_news_type.Text = "站内公告";
                hl_news_type.NavigateUrl = "article.aspx?type=1";

                _ds = _newsLogic.NewsQueryByNewsType("1", 1, 100, 1, 8, out _pageAmount);
                if (_ds.Tables.Count > 0)
                {
                    rpt_news_type1.DataSource = _ds;
                    rpt_news_type1.DataBind();
                }

                rpt_news_type1.Visible = true;
                ipt_menu_current.Value = "1"; 
            }

            if (_type == 2)
            {
                hl_news_type.Text = "游戏资讯";
                hl_news_type.NavigateUrl = "article.aspx?type=2";

                _ds = _newsLogic.NewsQueryByNewsType("2", 1, 100, 1, 8, out _pageAmount);
                if (_ds.Tables.Count > 0)
                {
                    rpt_news_type2.DataSource = _ds;
                    rpt_news_type2.DataBind();
                }

                rpt_news_type2.Visible = true;
                ipt_menu_current.Value = "2";

            }

            if (_type == 3)
            {
                hl_news_type.Text = "常见问题";
                hl_news_type.NavigateUrl = "article.aspx?type=3";

                _ds=_newsLogic.NewsQueryByNewsType("3", 1, 100, 1, 8, out _pageAmount);
                if (_ds.Tables.Count > 0)
                {
                    rpt_news_type3.DataSource = _ds;
                    rpt_news_type3.DataBind();
                }

                rpt_news_type3.Visible = true;
                ipt_menu_current.Value = "3";
            }

        }

    }
}