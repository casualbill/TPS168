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
    public partial class admincfg : System.Web.UI.Page
    {
        protected String _type;

        protected UserLogic _userLogic = new UserLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_userLogic.AdminConfigAuthority(Session["admingroupid"].ToString(), 2) == 0)
            {
                Response.Redirect("error.aspx");
            }

            _type = Request.QueryString["type"];
            
            String _urlGetId = Request.QueryString["id"];
            String _urlGetGp = Request.QueryString["gp"];

            if (!IsPostBack)
            {

                switch (_type)
                {
                    case "1": group_query(); break; //用户组查询
                    case "2": group_insert(); break;    //用户组添加
                    case "3": admin_query(_urlGetId); break;    //管理员查询与设置
                    case "4": admin_add(_urlGetId); break;    //管理员添加
                    case "11": group_details(_urlGetId); break; //用户组详情
                    case "12": user_usergroup_update(_urlGetId, _urlGetGp); break;  //更改用户所在用户组
                    case "13": group_delete(_urlGetId); break; //用户组删除
                    default: break;
                }
            }
        }
        

        protected void btn_group_insert_click(object sender, EventArgs e)
        {
            group_info_insert_update(0);
        }


        protected void btn_group_update_click(object sender, EventArgs e)
        {
            group_info_insert_update(1);
        }

        protected void btn_group_delete_click(object sender, EventArgs e)
        {
            group_delete(lb_groupid.Text);
        }

        protected void btn_userquery_click(object sender, EventArgs e)
        {
            user_query_and_update_group();
        }

        protected void btn_update_user_usergroup_click(object sender, EventArgs e)
        {
            Response.Redirect("admincfg.aspx?type=12&id=" + lb_userid.Text + "&gp=" + ddl_objgroup.SelectedValue);
        }

        //所有用户组列表
        protected void group_query()
        {
            DataSet _ds = _userLogic.UserGroupQueryList();

            pn_usergroup_table.Visible = true;

            if (_ds.Tables.Count > 0)
            {
                rpt_group.DataSource = _ds;
                rpt_group.DataBind();
            }
            else
            {
                td_query_null1.Visible = true;
            }

            lb_admincfgtype.Text = "用户组查询与设置";
        }

        //用户组详情
        protected void group_details(String _groupId)
        {
            String[] _groupInfoStr = _userLogic.UserGroupQueryDetails(_groupId);

            pn_usergroup_update.Visible = true;

            if (_groupInfoStr[0] == null)
            {
                lb_admincfgtype.Text = "用户组详细信息";
                div_group_details_list.InnerHtml = "没有符合条件的用户组！";
            }
            else
            {
                lb_admincfgtype.Text = _groupInfoStr[1] + "的详细信息";

                lb_groupid.Text = _groupInfoStr[0];
                tb_groupname.Text = _groupInfoStr[1];
                if (_groupInfoStr[2] == "1") { cb_groupoptions2.Checked = true; }
                if (_groupInfoStr[3] == "1") { cb_groupoptions3.Checked = true; }
                if (_groupInfoStr[4] == "1") { cb_groupoptions4.Checked = true; }
                if (_groupInfoStr[5] == "1") { cb_groupoptions5.Checked = true; }
                if (_groupInfoStr[6] == "1") { cb_groupoptions6.Checked = true; }
                if (_groupInfoStr[7] == "1") { cb_groupoptions7.Checked = true; }

                if (_groupInfoStr[8] == "0") { rbl_groupoptions8.Items.FindByValue("0").Selected = true; }
                if (_groupInfoStr[8] == "1") { rbl_groupoptions8.Items.FindByValue("1").Selected = true; }
                if (_groupInfoStr[8] == "2") { rbl_groupoptions8.Items.FindByValue("2").Selected = true; }

                btn_group_update.Visible = true;
                btn_group_delete.Visible = true;
            }
        }

        //用户组添加页面
        protected void group_insert()
        {
            lb_admincfgtype.Text = "添加用户组";
            pn_usergroup_update.Visible = true;
            li_group_id.Visible = false;
            btn_group_insert.Visible = true;

        }

        //管理员查询列表
        protected void admin_query(String _groupId)
        {
            DataSet _ds = _userLogic.UserQueryByUserGroupId(_groupId);

            pn_admin_table.Visible = true;

            if (_ds.Tables.Count > 0)
            {
                rpt_user.DataSource = _ds;
                rpt_user.DataBind();

                if (_ds.Tables[0].Rows.Count == 0) { td_query_null2.Visible = true; }
            }
            else
            {
                td_query_null2.Visible = true;
            }

            lb_admincfgtype.Text = "管理员查询与设置";

        }


        //管理员添加与更改
        protected void admin_add(String _uid)
        {
            String[] _userDetailsStr = new String[30];
            DataSet _ds = new DataSet();
            int _intNum;

            lb_admincfgtype.Text = "管理员添加与更改";
            pn_admin_add.Visible = true;

            if (int.TryParse(_uid, out _intNum))
            {
                _userDetailsStr = _userLogic.UserQueryByUserNameOrUserId(_uid, 1);

                div_group_user_details.Visible = true;
                lb_userid.Text = _userDetailsStr[1];
                lb_username.Text = _userDetailsStr[2];
                lb_currentgroup.Text = _userDetailsStr[5];

                _ds = _userLogic.UserGroupQueryList();

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    ddl_objgroup.Items.Add(new ListItem(_dr[1].ToString(), _dr[0].ToString()));
                }
            }
            else
            {
                div_group_user_details.Visible = false;
            }
            


        }


        //查询用户
        protected void user_query_and_update_group()
        {
            String _uid = _userLogic.UserQueryByUserNameOrUserId(tb_username.Text, 0)[1];
            Response.Redirect("admincfg.aspx?type=4&id=" + _uid);
        }


        //更改用户所在用户组
        protected void user_usergroup_update(String _uid,String _groupId)
        {
            int _retVal;

            _retVal = _userLogic.UserGroupOfUserInfoUpdate(_uid, _groupId);

            if (_retVal == 0)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作失败！\"); window.location.href=\"admincfg.aspx?type=4&id=" + _uid + "\" </script>");
            }

            if (_retVal == 1)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"admincfg.aspx?type=3\";</script>");
            }

        }


        //用户组信息添加、修改 _type 0：添加 1：修改
        protected void group_info_insert_update(int _type)
        {
            String[] _groupUpdateStr = new String[10];
            int _retVal;

            _groupUpdateStr[0] = lb_groupid.Text;
            _groupUpdateStr[1] = tb_groupname.Text;
            if (cb_groupoptions2.Checked == true) { _groupUpdateStr[2] = "1"; } else { _groupUpdateStr[2] = "0"; }
            if (cb_groupoptions3.Checked == true) { _groupUpdateStr[3] = "1"; } else { _groupUpdateStr[3] = "0"; }
            if (cb_groupoptions4.Checked == true) { _groupUpdateStr[4] = "1"; } else { _groupUpdateStr[4] = "0"; }
            if (cb_groupoptions5.Checked == true) { _groupUpdateStr[5] = "1"; } else { _groupUpdateStr[5] = "0"; }
            if (cb_groupoptions6.Checked == true) { _groupUpdateStr[6] = "1"; } else { _groupUpdateStr[6] = "0"; }
            if (cb_groupoptions7.Checked == true) { _groupUpdateStr[7] = "1"; } else { _groupUpdateStr[7] = "0"; }

            if (rbl_groupoptions8.SelectedValue == "0") { _groupUpdateStr[8] = "0"; }
            if (rbl_groupoptions8.SelectedValue == "1") { _groupUpdateStr[8] = "1"; }
            if (rbl_groupoptions8.SelectedValue == "2") { _groupUpdateStr[8] = "2"; }

            if (_type == 0)
            {
                String[] _groupInsertStr = new String[9];

                for (int i = 0; i < 9; i++)
                {
                    _groupInsertStr[i] = _groupUpdateStr[i + 1];
                }

                _retVal = _userLogic.GroupInfoInsert(_groupInsertStr);

                if (_retVal == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"用户组已存在！\"); window.location.href=\"admincfg.aspx?type=2\";</script>");
                }
                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"admincfg.aspx?type=1\";</script>");
                }
            }

            if (_type == 1)
            {
                _retVal = _userLogic.GroupInfoUpdate(_groupUpdateStr);

                if (_retVal == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作失败，请重试！\"); </script>");
                }
                if (_retVal == 1)
                {
                    Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"admincfg.aspx?type=1\"; </script>");
                }
            }

        }

        //用户组删除
        protected void group_delete(String _groupId)
        {
            int _retVal = _userLogic.GroupInfoDelete(_groupId);

            if (_retVal == 0)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"用户组不存在！\"); window.location.href=\"admincfg.aspx?type=1\";</script>");
            }
            if (_retVal == 1)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"该用户组内存在关联用户！请将用户移出该用户组后重试！\"); window.location.href=\"admincfg.aspx?type=1\";</script>");
            }
            if (_retVal == 2)
            {
                Response.Write("<script type=\"text/javascript\">alert (\"操作成功！\"); window.location.href=\"admincfg.aspx?type=1\";</script>");
            }

        }


    }
}