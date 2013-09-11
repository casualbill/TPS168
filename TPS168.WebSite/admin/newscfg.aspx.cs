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
    public partial class newscfg : System.Web.UI.Page
    {
        protected String _type;
        protected String _pageStr;
        protected int _pageIndex;

        protected NewsLogic _newsLogic=new NewsLogic();
        protected UserLogic _userLogic = new UserLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 6) == 0)
            {
                Response.Redirect("error.aspx");
            }

            _type = Request.QueryString["type"];
            _pageStr = Request.QueryString["page"];

            String _urlGetId = Request.QueryString["id"];

            int _pageInt;

            if (int.TryParse(_pageStr, out _pageInt))
            {
                if (_pageInt > 0)
                {
                    _pageIndex = _pageInt;
                }
                else
                {
                    _pageIndex = 1;
                }
            }
            else
            {
                _pageIndex = 1;
            }

            if (!IsPostBack)
            {
                switch (_type)
                {
                    case "1": news_list_query(_pageIndex); break; //新闻列表
                    case "2": news_insert(); break; //新闻添加
                    case "10": news_details_query(_urlGetId); break; //新闻详情
                    case "11": news_delete(_urlGetId); break; //新闻删除

                    default: news_list_query(_pageIndex); break;
                }
            }


        }



        //转到页数
        protected void btn_turntopage_click(object sender, EventArgs e)
        {
            Response.Redirect("newscfg.aspx?type=1&page=" + tb_pageindex.Text);

        }


        protected void btn_news_insert_click(object sender, EventArgs e)
        {
            news_insert_update(0);
        }

        protected void btn_news_update_click(object sender, EventArgs e)
        {
            news_insert_update(1);
        }

        protected void btn_news_delete_click(object sender, EventArgs e)
        {
            news_delete(lb_news_id.Text);
        }


        //新闻列表
        protected void news_list_query(int _pageIndex)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();

            _ds = _newsLogic.NewsQueryByNewsType("-1", 1, 50, _pageIndex, 20, out _pageAmount);

            lb_querytype.Text = "新闻列表";
            pn_news_list_table.Visible = true;

            pagination_pageindex.InnerHtml = "第" + _pageIndex + "页";
            pagination_pageamount.InnerHtml = "共" + _pageAmount + "页";

            if (_pageAmount > 1)
            {
                pagination_frame.Visible = true;


                if (_pageIndex == 1)
                {
                    pagination_prev.InnerHtml = "";
                    pagination_next.InnerHtml = "<a href=\"newscfg.aspx?type=1&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

                if (_pageIndex == _pageAmount)
                {
                    pagination_prev.InnerHtml = "<a href=\"newscfg.aspx?type=1&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next.InnerHtml = "";
                }

                if (_pageIndex > 1 && _pageIndex < _pageAmount)
                {
                    pagination_prev.InnerHtml = "<a href=\"newscfg.aspx?type=1&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next.InnerHtml = "<a href=\"newscfg.aspx?type=1&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }


            }
            else
            {
                pagination_frame.Visible = false;
            }


            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    rpt_news.DataSource = _ds;
                    rpt_news.DataBind();
                }
                else
                {
                    pagination_pageindex.Visible = false;
                    pagination_pageamount.Visible = false;
                    query_null.Visible = true;
                    pagination_frame.Visible = false;
                }
            }
            else
            {
                pagination_pageindex.Visible = false;
                pagination_pageamount.Visible = false;
                query_null.Visible = true;
                pagination_frame.Visible = false;
            }
        }

        //新闻详细信息
        protected void news_details_query(String _newsId)
        {
            String[] _newsStr = _newsLogic.NewsContentQueryByNewsId(_newsId);

            pn_news_details.Visible = true;

            if (_newsStr[0] == null)
            {
                lb_querytype.Text = "新闻详细信息";
                div_news_details.InnerHtml = "没有符合条件的新闻记录！";
            }
            else
            {
                lb_querytype.Text = _newsStr[1] + "详细信息";

                li_news_id.Visible = true;
                li_news_edittime.Visible = true;
                li_news_edituser.Visible = true;

                lb_news_id.Text = _newsStr[0];
                tb_news_title.Text = _newsStr[1];
                tb_news_content.Text = Server.HtmlDecode(_newsStr[2]);
                rbl_news_type.Items[Convert.ToInt16(_newsStr[3])-1].Selected = true;
                lb_news_edituser.Text = _newsStr[4];
                lb_news_edittime.Text = _newsStr[5];

                btn_news_update.Visible = true;
                btn_news_delete.Visible = true;
            }

        }

        //新闻添加
        protected void news_insert()
        {
            lb_querytype.Text = "添加新闻";

            lb_news_id.Visible = false;
            lb_news_edituser.Visible = false;
            lb_news_edittime.Visible = false;

            btn_news_insert.Visible = true;

            pn_news_details.Visible = true;

        }


        //新闻信息添加、修改 _type 0：添加 1：修改
        protected void news_insert_update(int _type)
        {
            String[] _newsUpdateStr = new String[6];
            String[] _newsInsertStr = new String[5];
            int _retVal;

            if (_type == 0)
            {
                _newsInsertStr[0] = tb_news_title.Text;
                _newsInsertStr[1] = rbl_news_type.SelectedValue;
                _newsInsertStr[2] = tb_news_content.Text;
                _newsInsertStr[3] = Session["adminid"].ToString();

                _retVal = _newsLogic.NewsContentInsert(_newsInsertStr);

                if (_retVal == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"新闻标题已存在！\"); </script>");
                }
                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"newscfg.aspx?type=1\"; </script>");
                }
            }


            if (_type == 1)
            {
                _newsUpdateStr[0] = lb_news_id.Text;
                _newsUpdateStr[1] = tb_news_title.Text;
                _newsUpdateStr[3] = tb_news_content.Text;
                _newsUpdateStr[2] = rbl_news_type.SelectedValue;
                _newsUpdateStr[4] = Session["adminid"].ToString();

                _retVal = _newsLogic.NewsContentUpdate(_newsUpdateStr);

                if (_retVal == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作失败！\"); </script>");
                }
                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"新闻标题已存在！\"); </script>");
                }
                if (_retVal == 2)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"newscfg.aspx?type=1\";</script>");
                }
            }

        }

        //新闻删除
        protected void news_delete(String _newsId)
        {
            int _retVal = _newsLogic.NewsDelete(_newsId);

            if (_retVal == 0)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"新闻不存在！\"); window.location.href=\"newscfg.aspx?type=1\";</script>");
            }
            if (_retVal == 1)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"newscfg.aspx?type=1\";</script>");
            }

        }

    }
}