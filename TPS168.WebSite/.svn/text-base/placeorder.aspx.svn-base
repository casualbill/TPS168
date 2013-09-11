<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="placeorder.aspx.cs" Inherits="TPS168.WebSite.placeorder" %>
<%@ Register TagPrefix="sidebar" TagName="sidebar" Src="~/sidebar.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_nav1").addClass("mainmenu-current");
            server_state_table_toggle();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">
    
    <sidebar:sidebar ID="sidebar1" runat="server" />

    <div class="main-center">
        <div class="crumb">
            当前位置：<a href="index.aspx">首页</a> >
            <asp:HyperLink ID="hl_game_type" runat="server" CssClass="blue boldface"></asp:HyperLink>
        </div>

        <div class="main-content">
            <div class="placeorder-caption"><b>出货前请参考本网站截图规范：</b><a href="#">http://www.tps168.com</a></div>
            <div class="cutline-blue"></div>

            <div class="placeorder-title clearfix"><span>选择游戏 <asp:DropDownList ID="ddl_gamelist" AutoPostBack="true" OnSelectedIndexChanged="ddl_gamelist_changed" runat="server"></asp:DropDownList></span>下单列表</div>

            <div class="serverlist-table serverlist-placeorder">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="25%">游戏</th>
                        <th width="31%">服务器</th>
                        <th width="14%">价格</th>
                        <th width="14%">收货量</th>
                        <th width="8%">状态</th>
                        <th width="8%" class="th-last">下单</th>
                    </tr>
                
                    <tr id="tr_querynull" runat="server" visible="false"><td colspan="6" style="text-align:center;">没有服务器信息</td></tr>

                    <asp:Repeater ID="rpt_index_serverlist" runat="server" Visible="false">
                        <ItemTemplate>
                            <tr class="tr_index_serverlist" js="<%# Eval("ServerState") %>">
                                <td><%# Eval("GameName") %></td>
                                <td><%# Eval("ServerName") %></td>
                                <td><%# Eval("PurchasePriceStr") %></td>
                                <td><%# Eval("AmountMax") %></td>
                                <td class="td-bluebold"><%# Eval("ServerStateStr") %></td>
                                <td><input type="button" value="下单" class="ipt_place_server" js="<%# Eval("Id") %>" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>

            <div class="cutline-blue"></div>

            <div class="placeorder-title">游戏简介</div>
            
            <div class="placeorder-text">
                <asp:Label ID="lb_game_caption" runat="server"></asp:Label>
            </div>

        </div>
    </div>
    
</asp:Content>
