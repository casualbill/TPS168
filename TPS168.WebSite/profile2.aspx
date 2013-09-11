<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="profile2.aspx.cs" Inherits="TPS168.WebSite.profile2" %>
<%@ Register TagPrefix="profile_bar" TagName="profile_bar" Src="~/profile.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_profile2").addClass("side-current");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">

    <profile_bar:profile_bar ID="Profile_bar1" runat="server" />

    <div class="main-center">
        <div class="crumb">
            当前位置：<a href="index.aspx">首页</a> > <a class="blue boldface" href="profile1.aspx">我的游戏中心</a>
        </div>

        <div class="main-content">

            <span id="span_profile_tips" runat="server" class="profile-tips" style="display:none;"></span>

            <table cellpadding="0" cellspacing="0" class="profile-update-table">

                <tr>
                    <td class="update-item-name" width="25%">原密码：</td>
                    <td width="30%"><asp:TextBox ID="tb_passwprd_old" runat="server" TextMode="Password" MaxLength="32"></asp:TextBox></td>
                    <td width="45%">请输入您的当前密码</td>
                </tr>

                <tr>
                    <td class="update-item-name">新密码：</td>
                    <td><asp:TextBox ID="tb_password_new" runat="server" TextMode="Password" MaxLength="32"></asp:TextBox></td>
                    <td>请输入新密码，密码长度为6-32个字符</td>
                </tr>

                <tr>
                    <td class="update-item-name">重复新密码：</td>
                    <td><asp:TextBox ID="tb_password_repeat" runat="server" TextMode="Password" MaxLength="32"></asp:TextBox></td>
                    <td>请重复输入</td>
                </tr>

            </table>

            <div class="profile-btn-frame">
                <asp:ImageButton ID="btn_update_submit" OnClick="btn_update_submit_click" runat="server" ImageUrl="~/images/btn_update_submit.png" />
            </div>

        </div>
    </div>

</asp:Content>
