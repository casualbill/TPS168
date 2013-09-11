<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="article.aspx.cs" Inherits="TPS168.WebSite.article" %>
<%@ Register TagPrefix="sidebar" TagName="sidebar" Src="~/sidebar.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var _currentFlag = $("#ctl00_Site_ContentPlaceHolder_ipt_menu_current").val();

            if (_currentFlag == "1") { $("#menu_nav2").addClass("mainmenu-current"); }
            if (_currentFlag == "2") { $("#menu_nav3").addClass("mainmenu-current"); }
            if (_currentFlag == "3") { $("#menu_nav4").addClass("mainmenu-current"); }
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">
    
    <input type="hidden" id="ipt_menu_current" runat="server" />
    
    <sidebar:sidebar runat="server" />

    <div class="main-center">
        <div class="crumb">
            当前位置：<a href="index.aspx">首页</a> >
            <asp:HyperLink ID="hl_news_type" runat="server" CssClass="blue boldface"></asp:HyperLink>
        </div>
    
        <div class="main-content">
            <div class="content-news">
                <asp:Label ID="lb_news_null" runat="server" Text="没有符合条件的新闻信息！" Visible="false"></asp:Label>
                
                <asp:Panel ID="pn_news_details" CssClass="a-center" runat="server" Visible="false">
                    <asp:Label ID="lb_news_title" runat="server" Style="margin:0 auto; font-weight:bold; font-size:14px;"></asp:Label>
                    <br /><br />
                    <asp:Label ID="lb_news_time" runat="server" Style="margin:0 auto; color:#666;"></asp:Label>
                    <br /><br />
                    <asp:Label ID="lb_news_content" runat="server" Style="display:block; font-size:14px; text-align:left;"></asp:Label>
                </asp:Panel>

                <ul>
                    <asp:Repeater ID="rpt_news_type1" runat="server" Visible="false">
                        <ItemTemplate>
                            <li>
                                <span class="icon-common-5"></span>
                                <span class="news-title"><a href="article.aspx?id=<%# Eval("Id") %>"><%# Eval("NewsTitle") %></a></span><%# Eval("EditTime") %>
                                <br /><br />
                                <%# Eval("NewsContent") %>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Repeater ID="rpt_news_type2" runat="server" Visible="false">
                        <ItemTemplate>
                            <li>
                                <span class="icon-common-4"></span>
                                <span class="news-title"><a href="article.aspx?id=<%# Eval("Id") %>"><%# Eval("NewsTitle") %></a></span><%# Eval("EditTime") %>
                                <br /><br />
                                <%# Eval("NewsContent") %>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Repeater ID="rpt_news_type3" runat="server" Visible="false">
                        <ItemTemplate>
                            <li>
                                <span class="blue boldface">[常见问题]</span>
                                <span class="news-title"><a href="article.aspx?id=<%# Eval("Id") %>"><%# Eval("NewsTitle") %></a></span><%# Eval("EditTime") %>
                                <br /><br />
                                <%# Eval("NewsContent") %>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    
    </div>

</asp:Content>
