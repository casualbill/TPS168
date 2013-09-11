<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TPS168.WebSite.index" %>
<%@ Register TagPrefix="sidebar" TagName="sidebar" Src="~/sidebar.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            index_gamelist_toggle();
            $("#menu_nav0").addClass("mainmenu-current");
            server_state_table_toggle();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">
    
    <div class="sidebar">

        <div class="side-frame">
            <div class="side-title">
                <span class="icon-common-2"></span><span>所有游戏列表</span>
            </div>

            <div class="side-content side-game">
                <ul>
                <asp:Repeater ID="rpt_sidebar_gamelist" runat="server">
                    <ItemTemplate>
                        <li><a href="placeorder.aspx?id=<%# Eval("Id") %>"><%# Eval("GameName") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                </ul>
            </div>
        </div>

        <div class="side-frame">
            <div class="side-title">
                <span class="side-more"><a href="#">更多&gt;&gt;</a></span>
                <span class="icon-common-2"></span><span>常见问题</span>
            </div>

            <div class="side-content side-news">
                <ul>
                <asp:Repeater ID="rpt_sidebar_faq" runat="server">
                    <ItemTemplate>
                        <li><a href="article.aspx?id=<%# Eval("Id") %>"><%# Eval("NewsTitle") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                </ul>
            </div>
    
        </div>
    </div>

    <div class="main-center-index clearfix">
        <div class="index-menu">
            <ul>
                <li id="li_gamelist1" class="list-current">已开仓游戏列表</li>
                <li id="li_gamelist0">未开仓游戏列表</li>
            </ul>

            <span><a href="#" class="red boldface">[24小时在线收购]</a></span>
        </div>

        <div id="gamelist-gamestate1" class="index-game-list">
            <ul class="clearfix">
                <asp:Repeater ID="rpt_index_gamelist1" runat="server">
                    <ItemTemplate>
                        <li><span class="icon-common-3"></span><a href="placeorder.aspx?id=<%# Eval("Id") %>"><%# Eval("GameName") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>        
        </div>

        <div id="gamelist-gamestate0" class="index-game-list" style="display:none;">
            <ul class="clearfix">
                <asp:Repeater ID="rpt_index_gamelist0" runat="server">
                    <ItemTemplate>
                        <li><span class="icon-common-3"></span><a href="placeorder.aspx?id=<%# Eval("Id") %>"><%# Eval("GameName") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>        
        </div>

        <div class="index-menu">
            <ul>
                <li class="list-current">所有开仓的游戏</li>
            </ul>

            <span class="boldface">选择游戏 <asp:DropDownList ID="ddl_gamelist" AutoPostBack="true" OnSelectedIndexChanged="ddl_gamelist_changed" runat="server"></asp:DropDownList></span>
        </div>

        <div class="serverlist-table serverlist-index">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <th width="40%">服务器</th>
                    <th width="20%">价格</th>
                    <th width="20%">收货量</th>
                    <th width="10%">状态</th>
                    <th width="10%" class="th-last">下单</th>
                </tr>
                
                <tr id="tr_querynull" runat="server" visible="false"><td colspan="5" style="text-align:center;">没有服务器信息</td></tr>

                <asp:Repeater ID="rpt_index_serverlist" runat="server" Visible="false">
                    <ItemTemplate>
                        <tr class="tr_index_serverlist" js="<%# Eval("ServerState") %>">
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

    </div>




    <sidebar:sidebar runat="server" />

</asp:Content>
