<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="profile1.aspx.cs" Inherits="TPS168.WebSite.profile1" %>
<%@ Register TagPrefix="profile_bar" TagName="profile_bar" Src="~/profile.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_profile1").addClass("side-current");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">

    <profile_bar:profile_bar runat="server" />

    <div class="main-center">
        <div class="crumb">
            当前位置：<a href="index.aspx">首页</a> > <a class="blue boldface" href="profile1.aspx">我的游戏中心</a>
        </div>

        <div class="main-content">

            <span id="span_profile_tips" runat="server" class="profile-tips" style="display:none;"></span>

            <table cellpadding="0" cellspacing="0" class="profile-update-table">

                <tr>
                    <td class="update-item-name" width="25%"><span class="red">*&nbsp;</span>供货商名称：</td>
                    <td width="30%"><asp:TextBox ID="tb_providername" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td width="45%">如果您不是可以输入您的姓名</td>
                </tr>

                <tr>
                    <td class="update-item-name"><span class="red">*&nbsp;</span>真实姓名：</td>
                    <td><asp:TextBox ID="tb_realname" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td>请填写真实的姓名，以保护您所有的用户权益</td>
                </tr>

                <tr>
                    <td class="update-item-name"><span class="red">*&nbsp;</span>联系电话：</td>
                    <td><asp:TextBox ID="tb_tel" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td>请填写有效电话号码，供货商需要确认身份后才能交易</td>
                </tr>

                <tr>
                    <td class="update-item-name"><span class="red">*&nbsp;</span>手机号码：</td>
                    <td><asp:TextBox ID="tb_mobile" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td>我们需要真实号码来验证账户</td>
                </tr>

                <tr>
                    <td class="update-item-name">QQ：</td>
                    <td><asp:TextBox ID="tb_qq" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td>方便联系请输入真实信息</td>
                </tr>

                <tr>
                    <td class="update-item-name">MSN：</td>
                    <td><asp:TextBox ID="tb_msn" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td>方便联系请输入真实信息</td>
                </tr>

                <tr>
                    <td class="update-item-name">邮政编码：</td>
                    <td><asp:TextBox ID="tb_postcode" runat="server" MaxLength="10"></asp:TextBox></td>
                    <td>请填写正确邮编</td>
                </tr>

                <tr>
                    <td class="update-item-name">详细地址：</td>
                    <td><asp:TextBox ID="tb_address" runat="server" MaxLength="100"></asp:TextBox></td>
                    <td>请输入您详细的联系地址</td>
                </tr>

            </table>

            <div class="profile-btn-frame">
                <asp:ImageButton ID="btn_update_submit" OnClick="btn_update_submit_click" OnClientClick="return user_details_update_validate();" runat="server" ImageUrl="~/images/btn_update_submit.png" />
            </div>

        </div>
    </div>

</asp:Content>
