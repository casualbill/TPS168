<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="usercfg.aspx.cs" Inherits="TPS168.WebSite.admin.usercfg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Admin_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Admin_ContentPlaceHolder" runat="server">
    <h2>用户管理 —— <asp:Label ID="lb_querytype" runat="server"></asp:Label></h2>

    <div class="admin-search-bar">
        搜索用户：<asp:TextBox ID="tb_username" runat="server" MaxLength="32"></asp:TextBox>
        <asp:Button ID="btn_userquery" OnClick="btn_userquery_click" runat="server" Text="查询" />
    </div>

    <asp:Panel ID="pn_user_list_table" runat="server">
        <table class="admin-table">
            <tr>
                <th>ID</th>
                <th>用户名</th>
                <th>用户组</th>
                <th>用户状态</th>
                <th>注册时间</th>
                <th>注册IP</th>
                <th>上次登录IP</th>
                <th>上次登录时间</th>
                <th>验证码</th>
                <th>管理</th>
            </tr>

            <tr>
                <td id="query_null" runat="server" colspan="10" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_user" runat="server">
                <ItemTemplate>

                    <tr>
                        <td><%# Eval("Id") %></td>
                        <td><a href="usercfg.aspx?type=10&uid=<%# Eval("Id") %>"><%# Eval("UserName") %></a></td>
                        <td><%# Eval("GroupName") %></td>
                        <td><%# Eval("UserStateStr") %></td>
                        <td><%# Eval("RegTime") %></td>
                        <td><%# Eval("RegIP") %></td>
                        <td><%# Eval("LatestLoginIP") %></td>
                        <td><%# Eval("LatestLoginTime") %></td>
                        <td><%# Eval("ValidationCode") %></td>
                        <td>
                            <a href="usercfg.aspx?type=10&uid=<%# Eval("Id") %>">查看</a>
                            <a href="usercfg.aspx?type=11&uid=<%# Eval("Id") %>">通过</a>
                            <a href="usercfg.aspx?type=12&uid=<%# Eval("Id") %>">禁止</a>
                        </td>
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div id="pagination_wrapper">
        
            <span id="pagination_pageindex" runat="server"></span>
            <span id="pagination_pageamount" runat="server"></span>

            <div id="pagination_frame" runat="server" visible="false" style="display:inline;">
                <span id="pagination_prev" runat="server"></span>

                <span id="pagination_next" runat="server"></span>

                转到第<asp:TextBox ID="tb_pageindex" runat="server" Width="20" MaxLength="4"></asp:TextBox>页
                <asp:Button ID="btn_turntopage" OnClick="btn_turntopage_click" runat="server" Text="确定" />
            </div>
        </div>

    </asp:Panel>


    <div id="user_details_list" runat="server" visible="false" class="admin-list">
        <ul>
            <li><em>ID：</em><asp:Label ID="lb_userdetails_1" runat="server"></asp:Label></li>
            <li><em>用户名：</em><asp:Label ID="lb_userdetails_2" runat="server"></asp:Label></li>
            <li><em>用户组：</em><asp:Label ID="lb_userdetails_3" runat="server"></asp:Label></li>
            <li><em>用户状态：</em><asp:Label ID="lb_userdetails_4" runat="server"></asp:Label></li>
            <li><em>注册时间：</em><asp:Label ID="lb_userdetails_5" runat="server"></asp:Label></li>
            <li><em>注册IP：</em><asp:Label ID="lb_userdetails_6" runat="server"></asp:Label></li>
            <li><em>上次登录时间：</em><asp:Label ID="lb_userdetails_7" runat="server"></asp:Label></li>
            <li><em>上次登录IP：</em><asp:Label ID="lb_userdetails_8" runat="server"></asp:Label></li>
            <li><em>E-mail地址：</em><asp:Label ID="lb_userdetails_9" runat="server"></asp:Label></li>
            <li><em>供货商姓名：</em><asp:Label ID="lb_userdetails_10" runat="server"></asp:Label></li>
            <li><em>真实姓名：</em><asp:Label ID="lb_userdetails_11" runat="server"></asp:Label></li>
            <li><em>电话号码：</em><asp:Label ID="lb_userdetails_12" runat="server"></asp:Label></li>
            <li><em>手机号码：</em><asp:Label ID="lb_userdetails_13" runat="server"></asp:Label></li>
            <li><em>QQ号码：</em><asp:Label ID="lb_userdetails_14" runat="server"></asp:Label></li>
            <li><em>MSN：</em><asp:Label ID="lb_userdetails_15" runat="server"></asp:Label></li>
            <li><em>邮政编码：</em><asp:Label ID="lb_userdetails_16" runat="server"></asp:Label></li>
            <li><em>地址：</em><asp:Label ID="lb_userdetails_17" runat="server"></asp:Label></li>
            <li><em>收款人姓名：</em><asp:Label ID="lb_userdetails_18" runat="server"></asp:Label></li>
            <li><em>银行账号：</em><asp:Label ID="lb_userdetails_19" runat="server"></asp:Label></li>
            <li><em>支付宝注册姓名：</em><asp:Label ID="lb_userdetails_20" runat="server"></asp:Label></li>
            <li><em>支付宝账号：</em><asp:Label ID="lb_userdetails_21" runat="server"></asp:Label></li>
            <li><em>快钱注册姓名：</em><asp:Label ID="lb_userdetails_22" runat="server"></asp:Label></li>
            <li><em>快钱账号：</em><asp:Label ID="lb_userdetails_23" runat="server"></asp:Label></li>
            <li></li>
            <li><em>修改密码：</em><asp:TextBox ID="tb_password" MaxLength="32" runat="server"></asp:TextBox><asp:Button ID="btn_password" runat="server" OnClick="btn_password_click" Text="提交" /></li>
        </ul>
    
        <asp:Label ID="lb_manageoperation" runat="server"></asp:Label>
    </div>



</asp:Content>
