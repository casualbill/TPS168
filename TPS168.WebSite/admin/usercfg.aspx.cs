﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TPS168.BLL;

namespace TPS168.WebSite.admin
{
    public partial class usercfg : System.Web.UI.Page
    {
        protected String _type;
        protected String _pageStr;
        protected int _pageIndex;
        protected String _uid;
        
        protected UserLogic _userLogic = new UserLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 4) == 0)
            {
                Response.Redirect("error.aspx");
            }
            
            _type = Request.QueryString["type"];
            _pageStr = Request.QueryString["page"];

            _uid = Request.QueryString["uid"];
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
                    case "1": user_query(1, _pageIndex); break; //所有用户
                    case "2": user_query(2, _pageIndex); break; //待审用户
                    case "3": user_query(3, _pageIndex); break; //黑名单
                    case "10": user_details_query(_uid); break; //用户详情
                    case "11": user_state_update(_uid, 3); break;   //通过审核
                    case "12": user_state_update(_uid, 4); break;   //禁止用户
                    default: user_query(1, _pageIndex); break;
                }
            }


        }

        //更改密码
        protected void btn_password_click(object sender, EventArgs e)
        {
            String _pwd = tb_password.Text.Trim();
            int _retVal;

            if (_pwd.Length==0)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"密码不能为空！\");</script>");
            }
            else
            {
                _retVal = _userLogic.UserPasswordUpdateAdmin(_uid, _pwd);

                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"修改成功！\");</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"修改失败！\");</script>");
                }
            }
        }


        //搜索用户
        protected void btn_userquery_click(object sender, EventArgs e)
        {
            String _uid = user_id_query(tb_username.Text);
            Response.Redirect("usercfg.aspx?type=10&uid=" + _uid);
        }

        //转到页数
        protected void btn_turntopage_click(object sender, EventArgs e)
        {
            Response.Redirect("usercfg.aspx?type=" + _type + "&page=" + tb_pageindex.Text);

        }

        //根据用户名查询用户ID
        protected String user_id_query(String _userName)
        {
            return _userLogic.UserQueryByUserNameOrUserId(_userName, 0)[1];
        }



        //用户分页列表
        protected void user_query(int _queryType,int _pageIndex)
        {
            int _pageAmount;
            DataSet _ds = new DataSet();

            switch (_queryType)
            {
                case 1: _ds = _userLogic.UserQueryByUserState(10, 1, _pageIndex, out _pageAmount); lb_querytype.Text = "所有用户列表"; break;
                case 2: _ds = _userLogic.UserQueryByUserState(1, 1, _pageIndex, out _pageAmount); lb_querytype.Text = "待审用户列表"; break;
                case 3: _ds = _userLogic.UserQueryByUserState(4, 1, _pageIndex, out _pageAmount); lb_querytype.Text = "黑名单列表"; break;
                default: _ds = _userLogic.UserQueryByUserState(10, 1, _pageIndex, out _pageAmount); lb_querytype.Text = "所有用户列表"; break;
            }

            pagination_pageindex.InnerHtml = "第" + _pageIndex + "页";
            pagination_pageamount.InnerHtml = "共" + _pageAmount + "页";

            if (_pageAmount > 1)
            {
                pagination_frame.Visible = true;


                if (_pageIndex == 1)
                {
                    pagination_prev.InnerHtml = "";
                    pagination_next.InnerHtml = "<a href=\"usercfg.aspx?type=" + _queryType.ToString() + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
                }

                if (_pageIndex == _pageAmount)
                {
                    pagination_prev.InnerHtml = "<a href=\"usercfg.aspx?type=" + _queryType.ToString() + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next.InnerHtml = "";
                }

                if (_pageIndex > 1 && _pageIndex < _pageAmount)
                {
                    pagination_prev.InnerHtml = "<a href=\"usercfg.aspx?type=" + _queryType.ToString() + "&page=" + (_pageIndex - 1).ToString() + "\">上一页</a>";
                    pagination_next.InnerHtml = "<a href=\"usercfg.aspx?type=" + _queryType.ToString() + "&page=" + (_pageIndex + 1).ToString() + "\">下一页</a>";
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
                    rpt_user.DataSource = _ds;
                    rpt_user.DataBind();
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

        //用户详细信息查询
        protected void user_details_query(String _uid)
        {
            pn_user_list_table.Visible = false;

            user_details_list.Visible = true;

            String[] _userDetails = _userLogic.UserQueryByUserNameOrUserId(_uid, 1);

            if (_userDetails[1] == null)
            {
                lb_querytype.Text = "用户详细信息";
                user_details_list.InnerHtml = "没有符合条件的用户！";
            }
            else
            {
                lb_querytype.Text = _userDetails[2] + "的详细信息";

                lb_userdetails_1.Text = _userDetails[1];
                lb_userdetails_2.Text = _userDetails[2];
                lb_userdetails_3.Text = _userDetails[5];
                lb_userdetails_4.Text = _userDetails[25];
                lb_userdetails_5.Text = _userDetails[6];
                lb_userdetails_6.Text = _userDetails[7];
                lb_userdetails_7.Text = _userDetails[9];
                lb_userdetails_8.Text = _userDetails[8];
                lb_userdetails_9.Text = _userDetails[10];
                lb_userdetails_10.Text = _userDetails[11];
                lb_userdetails_11.Text = _userDetails[12];
                lb_userdetails_12.Text = _userDetails[13];
                lb_userdetails_13.Text = _userDetails[14];
                lb_userdetails_14.Text = _userDetails[15];
                lb_userdetails_15.Text = _userDetails[16];
                lb_userdetails_16.Text = _userDetails[17];
                lb_userdetails_17.Text = _userDetails[18];
                lb_userdetails_18.Text = _userDetails[19];
                lb_userdetails_19.Text = _userDetails[20];
                lb_userdetails_20.Text = _userDetails[21];
                lb_userdetails_21.Text = _userDetails[22];
                lb_userdetails_22.Text = _userDetails[23];
                lb_userdetails_23.Text = _userDetails[24];

                lb_manageoperation.Text = "<a href=\"usercfg.aspx?type=11&uid=" + _userDetails[1] + "\">通过</a>&nbsp;&nbsp;<a href=\"usercfg.aspx?type=12&uid=" + _userDetails[1] + "\">禁止</a>";
            }
        }


        //更新用户状态
        protected void user_state_update(String _uid,int _userState)
        {
            String _adminId = null;
            String _backUrl = null;

            if (Session["adminid"] != null)
            {
                _adminId = Session["adminid"].ToString();
            }

            if (Request.UrlReferrer != null)
            {
                _backUrl = Request.UrlReferrer.ToString();
            }
            
            int _retVal = _userLogic.UserStateUpdate(_adminId, _uid, _userState);

            if (_retVal == 1)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"您无法对管理员执行该操作！\"); window.location.href=\"" + _backUrl + "\";</script>");
            }

            if (_retVal == 2)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"" + _backUrl + "\";</script>");
            }
        }
        
    }
}