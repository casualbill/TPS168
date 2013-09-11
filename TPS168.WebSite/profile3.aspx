<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="profile3.aspx.cs" Inherits="TPS168.WebSite.profile3" %>
<%@ Register TagPrefix="profile_bar" TagName="profile_bar" Src="~/profile.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_profile3").addClass("side-current");
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
                    <td class="update-item-name" width="25%">收款人：</td>
                    <td width="30%"><asp:TextBox ID="tb_payeename" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td width="45%">您工商银行账号收款人姓名</td>
                </tr>

                <tr>
                    <td class="update-item-name">银行账号：</td>
                    <td><asp:TextBox ID="tb_bankaccount" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td>您工商银行账号</td>
                </tr>

                <tr>
                    <td class="update-item-name">支付宝注册姓名：</td>
                    <td><asp:TextBox ID="tb_alipayname" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td>您支付宝账号真实姓名</td>
                </tr>

                <tr>
                    <td class="update-item-name">支付宝账号：</td>
                    <td><asp:TextBox ID="tb_alipayaccount" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td></td>
                </tr>

                <tr>
                    <td class="update-item-name">快钱注册姓名：</td>
                    <td><asp:TextBox ID="tb_99name" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td>您快钱账号真实姓名</td>
                </tr>

                <tr>
                    <td class="update-item-name">快钱账号：</td>
                    <td><asp:TextBox ID="tb_99account" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td></td>
                </tr>

            </table>

            <div class="profile-btn-frame">
                <asp:ImageButton ID="btn_update_submit" OnClick="btn_update_submit_click" OnClientClick="return user_details_update_validate();" runat="server" ImageUrl="~/images/btn_update_submit.png" />
            </div>

        </div>
    </div>
</asp:Content>
