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
    public partial class profile6 : System.Web.UI.Page
    {
        protected String _pageStr;
        protected int _pageIndex;
        protected String _urlGetId;

        protected OrderLogic _orderLogic = new OrderLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userstate"].ToString() != "3")
            {
                Response.Write("<script type=\"text/javascript\">alert (\"您的账号还未通过审核，请联系客服开通账号！\"); window.location.href=\"index.aspx\"; </script>");
            }

            _pageStr = Request.QueryString["page"];
            _urlGetId = Request.QueryString["id"];

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
                if (_urlGetId == null)
                {
                    order_list_query(_pageIndex);
                }
                else
                {
                    pn_order_list_table.Visible = false;
                    pn_order_details_list.Visible = true;

                    order_details_query(_urlGetId);
                }
            }
        }


        protected void rpt_order_list_btn_click(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btn_rpt_fail")
            {
                String[] _retVal = e.CommandArgument.ToString().Split(',');
                order_fail(_retVal[0], _retVal[1]);    
            }
        }


        //订单列表查询
        protected void order_list_query(int _pageIndex)
        {
            int _pageAmount;
            String _queryId;

            if (Session["userid"] != null)
            {
                _queryId = Session["userid"].ToString();
                DataSet _ds = new DataSet();

                _ds = _orderLogic.OrderQuery(3, _queryId, _pageIndex, 20, out _pageAmount);

                query_null.Visible = false;
                rpt_order_list.Visible = true;
                pagination_pageindex.Visible = true;
                pagination_pageamount.Visible = true;

                pagination_pageindex.InnerHtml = "第" + _pageIndex + "页";
                pagination_pageamount.InnerHtml = "共" + _pageAmount + "页";

                if (_pageAmount > 1)
                {
                    pagination_frame.Visible = true;


                    if (_pageIndex == 1)
                    {
                        pagination_prev.InnerHtml = "";
                        pagination_next.InnerHtml = "<a href=\"profile6.aspx?page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                    }

                    if (_pageIndex == _pageAmount)
                    {
                        pagination_prev.InnerHtml = "<a href=\"profile6.aspx?&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                        pagination_next.InnerHtml = "";
                    }

                    if (_pageIndex > 1 && _pageIndex < _pageAmount)
                    {
                        pagination_prev.InnerHtml = "<a href=\"profile6.aspx?&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                        pagination_next.InnerHtml = "<a href=\"profile6.aspx?&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
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
                        rpt_order_list.DataSource = _ds;
                        rpt_order_list.DataBind();
                    }
                    else
                    {
                        rpt_order_list.Visible = false;
                        pagination_pageindex.Visible = false;
                        pagination_pageamount.Visible = false;
                        query_null.Visible = true;
                        pagination_frame.Visible = false;
                    }
                }
                else
                {
                    rpt_order_list.Visible = false;
                    pagination_pageindex.Visible = false;
                    pagination_pageamount.Visible = false;
                    query_null.Visible = true;
                    pagination_frame.Visible = false;
                }
            }

        }

        //订单详情查询
        protected void order_details_query(String _orderId)
        {
            String[] _orderStr;
            _orderStr = _orderLogic.OrderQueryByOrderId(_orderId);

            pn_order_details_list.Visible = true;

            if (_orderStr[0] == null || _orderStr[1]!=Session["userid"].ToString())
            {
                div_order_details.InnerHtml = "没有符合条件的订单记录！";
            }
            else
            {

                lb_order_details0.Text = _orderStr[0];
                lb_order_details2.Text = _orderStr[2];
                lb_order_details4.Text = _orderStr[4];
                lb_order_details6.Text = _orderStr[6];
                lb_order_details7.Text = _orderStr[7];
                lb_order_details8.Text = _orderStr[8];
                lb_order_details9.Text = _orderStr[9];
                lb_order_details10.Text = _orderStr[10];

                lb_order_details11.Text = _orderStr[25];

                lb_order_details12.Text = _orderStr[12];
                lb_order_details13.Text = _orderStr[13];
                lb_order_details14.Text = _orderStr[14];
                lb_order_details15.Text = _orderStr[15];
                lb_order_details16.Text = _orderStr[16];
                lb_order_details17.Text = Server.HtmlDecode(_orderStr[17]);
                lb_order_details18.Text = _orderStr[18];

                lb_order_details19.Text = _orderStr[24];

                lb_order_details20.Text = _orderStr[20];
                lb_order_details21.Text = _orderStr[21];
                lb_order_details22.Text = _orderStr[22];
                lb_order_details23.Text = _orderStr[23];

                hf_order_state.Value = _orderStr[19];

            }
        }




        //用户订单失败  （功能暂时不显示）
        protected void order_fail(String _orderId, String _orderState)
        {
            int _retVal;

            if (Session["userid"] != null)
            {
                _retVal = _orderLogic.OrderOperation(Session["userid"].ToString(), _orderId, "4", null, "用户操作");
            }
            else
            {
                _retVal = -2;
            }

            switch (_retVal)
            {
                case 0: Response.Write("<script type=\"text/javascript\">alert (\"订单号不存在！\"); </script>"); break;
                case 1: Response.Write("<script type=\"text/javascript\">alert (\"订单信息已过期，请刷新后重试！\"); window.location.href=\"profile6.aspx\"; </script>"); break;
                case 2: Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"profile6.aspx\"; </script>"); break;
                default: Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请稍后重试！\"); </script>"); break;
            }
            
        }

    }
}