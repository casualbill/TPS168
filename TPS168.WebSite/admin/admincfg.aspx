<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="admincfg.aspx.cs" Inherits="TPS168.WebSite.admin.admincfg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Admin_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Admin_ContentPlaceHolder" runat="server">
    
    <h2>系统用户管理 —— <asp:Label ID="lb_admincfgtype" runat="server"></asp:Label></h2>

    <asp:Panel ID="pn_usergroup_table" runat="server" Visible="false">
        <table class="admin-table">
            <tr>
                <th>ID</th>
                <th>用户组名称</th>
                <th>系统用户管理</th>
                <th>基本设置</th>
                <th>用户管理</th>
                <th>游戏管理</th>
                <th>新闻设置</th>
                <th>公告设置</th>
                <th>订单处理</th>
                <th>管理</th>
            </tr>

            <tr>
                <td id="td_query_null1" runat="server" colspan="10" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_group" runat="server">
                <ItemTemplate>

                    <tr>
                        <td><%# Eval("Id") %></td>
                        <td><a href="admincfg.aspx?type=11&id=<%# Eval("Id") %>"><%# Eval("GroupName") %></a></td>
                        <td><%# Eval("AdminManagement") %></td>
                        <td><%# Eval("BasicSettings") %></td>
                        <td><%# Eval("UserManagement") %></td>
                        <td><%# Eval("GameManagement") %></td>
                        <td><%# Eval("NewsSettings") %></td>
                        <td><%# Eval("NoticeBoard") %></td>
                        <td><%# Eval("OrderProcess") %></td>
                        <td>
                            <a href="admincfg.aspx?type=11&id=<%# Eval("Id") %>">编辑</a>
                            <a style="cursor:pointer;" onclick="group_delete(0,<%# Eval("Id") %>);">删除</a>
                        </td>
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>

        </table>
    </asp:Panel>


    <asp:Panel ID="pn_admin_table" runat="server" Visible="false">
        <table class="admin-table">
            <tr>
                <th>ID</th>
                <th>用户名</th>
                <th>当前所在用户组</th>
                <th>管理</th>
            </tr>

            <tr>
                <td id="td_query_null2" runat="server" colspan="4" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_user" runat="server">
                <ItemTemplate>

                    <tr>
                        <td><%# Eval("UserId") %></td>
                        <td><a href="usercfg.aspx?type=10&uid=<%# Eval("UserId") %>"><%# Eval("UserName") %></a></td>
                        <td><a href="admincfg.aspx?type=3&id=<%# Eval("GroupId") %>"><%# Eval("GroupName") %></a></td>
                        <td>
                            <a href="admincfg.aspx?type=4&id=<%# Eval("UserId") %>">更改用户组</a>
                        </td>
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>

        </table>
    </asp:Panel>

    <asp:Panel ID="pn_usergroup_update" runat="server" Visible="false">
        <div id="div_group_details_list" runat="server" class="admin-list">
            <ul>
                <li id="li_group_id" runat="server"><em>用户组ID：</em><asp:Label ID="lb_groupid" runat="server"></asp:Label></li>
                <li><em>用户组名称：</em><asp:TextBox ID="tb_groupname" runat="server" MaxLength="20"></asp:TextBox></li>
                <li><em>系统用户管理：</em><asp:CheckBox ID="cb_groupoptions2" runat="server" /></li>
                <li><em>基本设置：</em><asp:CheckBox ID="cb_groupoptions3" runat="server" /></li>
                <li><em>用户管理：</em><asp:CheckBox ID="cb_groupoptions4" runat="server" /></li>
                <li><em>游戏管理：</em><asp:CheckBox ID="cb_groupoptions5" runat="server" /></li>
                <li><em>新闻设置：</em><asp:CheckBox ID="cb_groupoptions6" runat="server" /></li>
                <li><em>公告设置：</em><asp:CheckBox ID="cb_groupoptions7" runat="server" /></li>
                <li><em>订单处理：</em><asp:RadioButtonList ID="rbl_groupoptions8" runat="server"><asp:ListItem Text="无权限" Value="0"></asp:ListItem><asp:ListItem Text="基本操作权限" Value="1"></asp:ListItem><asp:ListItem Text="完全管理权限" Value="2"></asp:ListItem></asp:RadioButtonList></li>
            
            </ul>

            <asp:Button ID="btn_group_insert" OnClick="btn_group_insert_click" runat="server" Text="添加" Visible="false" />
            <asp:Button ID="btn_group_update" OnClick="btn_group_update_click" runat="server" Text="修改" Visible="false" />
            <asp:Button ID="btn_group_delete" OnClick="btn_group_delete_click" OnClientClick="return group_delete(1,null);" runat="server" Text="删除" Visible="false" />

        </div>

    </asp:Panel>

    <asp:Panel ID="pn_admin_add" runat="server" Visible="false">
        <div class="admin-search-bar">
            搜索用户：<asp:TextBox ID="tb_username" runat="server" MaxLength="32"></asp:TextBox>
            <asp:Button ID="btn_userquery" OnClick="btn_userquery_click" runat="server" Text="查询" />
        </div>
        
        <div id="div_group_user_details" runat="server" class="admin-list" visible="false">
            <ul>
                <li><em>ID：</em><asp:Label ID="lb_userid" runat="server"></asp:Label></li>
                <li><em>用户名：</em><asp:Label ID="lb_username" runat="server"></asp:Label></li>
                <li><em>当前所在用户组：</em><asp:Label ID="lb_currentgroup" runat="server"></asp:Label></li>
                <li><em>更改至用户组：</em><asp:DropDownList ID="ddl_objgroup" runat="server"></asp:DropDownList></li>
            </ul>
            
            <asp:Button ID="btn_update_user_usergroup" OnClick="btn_update_user_usergroup_click" runat="server" Text="修改" />
        </div>
            
    </asp:Panel>

    <script type="text/javascript">
        function group_delete(_type, _groupId) {
            var _retVal = confirm("确定要删除吗？");

            if (_type == 0) {
                if (_retVal == true) { window.location.href = "admincfg.aspx?type=13&id=" + _groupId; }
            }

            if (_type == 1) {
                return _retVal;
            }
        }
    
    </script>



</asp:Content>
