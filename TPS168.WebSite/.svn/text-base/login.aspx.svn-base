<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TPS168.WebSite.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            create_auth_image("span_authimage", "span_authimage1");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">

    
        <table cellpadding="0" cellspacing="0" class="reg-table">
            <tr>
                <td width="40%" class="reg-item-name">用户名：</td>
                <td width="60%"><asp:TextBox ID="tb_username" runat="server" MaxLength="32"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="reg-item-name">密码：</td>
                <td><asp:TextBox ID="tb_password" runat="server" TextMode="Password" MaxLength="32"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="reg-item-name">验证码：</td>
                <td>
                    <asp:TextBox ID="tb_authcode" runat="server" MaxLength="4" Width="100"></asp:TextBox>
                    <span id="span_authimage1" style="vertical-align:middle;"></span>
                </td>
            </tr>
            <tr>
                <td class="reg-item-name"></td>
                <td><asp:Button ID="btn_submit" OnClick="btn_submit_Click" runat="server" Text="登录" /></td>
            </tr>
            <tr>
                <td class="reg-item-name"></td>
                <td><asp:Label ID="lb_loginfo" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>


</asp:Content>
