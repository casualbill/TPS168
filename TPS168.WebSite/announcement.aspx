<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="announcement.aspx.cs" Inherits="TPS168.WebSite.announcement" %>
<%@ Register TagPrefix="sidebar" TagName="sidebar" Src="~/sidebar.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var _currentFlag = $("#ctl00_Site_ContentPlaceHolder_ipt_menu_current").val();

            if (_currentFlag == "5") { $("#menu_nav5").addClass("mainmenu-current"); }
            if (_currentFlag == "6") { $("#menu_nav6").addClass("mainmenu-current"); }
            
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">

<input type="hidden" id="ipt_menu_current" runat="server" />
    
    <sidebar:sidebar ID="Sidebar1" runat="server" />

    <div class="main-center">
        <div class="crumb">
            当前位置：<a href="index.aspx">首页</a> >
            <asp:HyperLink ID="hl_announce_type" runat="server" CssClass="blue boldface"></asp:HyperLink>
        </div>

        <div class="main-content">
            <div class="announcement-content">
                <asp:Label ID="lb_announcement" runat="server"></asp:Label>
            </div>
        </div>
    </div>


</asp:Content>
