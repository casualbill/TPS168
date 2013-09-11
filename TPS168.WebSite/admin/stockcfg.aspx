<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="stockcfg.aspx.cs" Inherits="TPS168.WebSite.admin.stockcfg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Admin_Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Admin_ContentPlaceHolder" runat="server">
    <h2>供货库存管理 —— <asp:Label ID="lb_querytype" runat="server"></asp:Label></h2>
        
    <div class="admin-search-bar">
        <ul>
            <li>根据用户名查询：<asp:TextBox ID="tb_user_query" runat="server"></asp:TextBox><asp:Button id="btn_user_query" OnClick="btn_user_query_click" runat="server" Text="查询" /></li>
            <li>根据游戏查询：<asp:DropDownList ID="ddl_game_query" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_game_query_changed"></asp:DropDownList><asp:Button id="btn_game_query" OnClick="btn_game_query_click" runat="server" Text="查询" /></li>
            <li>根据服务器查询：<asp:DropDownList ID="ddl_server_query" runat="server"><asp:ListItem Text="请先选择游戏" Value="0"></asp:ListItem></asp:DropDownList><asp:Button id="btn_server_query" OnClick="btn_server_query_click" runat="server" Text="查询" /></li>
        </ul>
    </div>

    <asp:Panel ID="pn_supply_list" runat="server" Visible="false">

        <table class="admin-table">
            <tr>
                <th width="30%">游戏</th>
                <th width="35%">服务器</th>
                <th width="35%">用户</th>
            </tr>

            <tr>
                <td id="query_null1" runat="server" colspan="3" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_supply" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("GameName") %></td>
                        <td><%# Eval("ServerName") %></td>
                        <td><%# Eval("UserName") %></td>
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div id="pagination_wrapper1">
        
            <span id="pagination_pageindex1" runat="server"></span>
            <span id="pagination_pageamount1" runat="server"></span>

            <div id="pagination_frame1" runat="server" visible="false" style="display:inline;">
                <span id="pagination_prev1" runat="server"></span>

                <span id="pagination_next1" runat="server"></span>

                转到第<asp:TextBox ID="tb_pageindex1" runat="server" Width="20"></asp:TextBox>页
                <asp:Button ID="btn_turntopage1" OnClick="btn_turntopage1_click" runat="server" Text="确定" />
            </div>
        </div>

    </asp:Panel>



    <asp:Panel ID="pn_stock_list" runat="server" Visible="false">

        <table class="admin-table">
            <tr>
                <th width="20%">游戏</th>
                <th width="24%">服务器</th>
                <th width="20%">用户</th>
                <th width="10%">数量</th>
                <th width="10%">预售价格</th>
                <th width="16%">登记时间</th>
            </tr>

            <tr>
                <td id="query_null2" runat="server" colspan="6" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_stock" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("GameName") %></td>
                        <td><%# Eval("ServerName") %></td>
                        <td><%# Eval("UserName") %></td>
                        <td><%# Eval("StockAmount")%></td>
                        <td><%# Eval("PresellPrice")%></td>
                        <td><%# Eval("RegTime")%></td>
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div id="pagination_wrapper2">
        
            <span id="pagination_pageindex2" runat="server"></span>
            <span id="pagination_pageamount2" runat="server"></span>

            <div id="pagination_frame2" runat="server" visible="false" style="display:inline;">
                <span id="pagination_prev2" runat="server"></span>

                <span id="pagination_next2" runat="server"></span>

                转到第<asp:TextBox ID="tb_pageindex2" runat="server" Width="20"></asp:TextBox>页
                <asp:Button ID="btn_turntopage2" OnClick="btn_turntopage2_click" runat="server" Text="确定" />
            </div>
        </div>

    </asp:Panel>

</asp:Content>
