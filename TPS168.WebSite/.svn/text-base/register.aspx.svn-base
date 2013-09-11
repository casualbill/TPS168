<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="TPS168.WebSite.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            create_auth_image("span_authimage", "span_authimage1");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">

    <span id="span_reg_tips" runat="server" class="reg-tips" style="display:none;"></span>

    <table cellpadding="0" cellspacing="0" class="reg-table">
        <tr>
            <td width="20%" class="reg-item-name"><span class="red">*&nbsp;</span>用户名：</td>
            <td width="35%"><asp:TextBox ID="tb_username" runat="server" MaxLength="32"></asp:TextBox></td>
            <td width="45%">请输入4-32个字符，注册成功后不可修改</td>
        </tr>

        <tr>
            <td class="reg-item-name"><span class="red">*&nbsp;</span>密码：</td>
            <td><asp:TextBox ID="tb_password" runat="server" TextMode="Password" MaxLength="32"></asp:TextBox></td>
            <td>密码长度为6-32个字符</td>
        </tr>

        <tr>
            <td class="reg-item-name"><span class="red">*&nbsp;</span>重复密码：</td>
            <td><asp:TextBox ID="tb_password_repeat" runat="server" TextMode="Password" MaxLength="32"></asp:TextBox></td>
            <td>请重复输入</td>
        </tr>

        <tr></tr>

        <tr>
            <td class="reg-item-name"><span class="red">*&nbsp;</span>电子邮件：</td>
            <td><asp:TextBox ID="tb_email" runat="server" MaxLength="50"></asp:TextBox></td>
            <td>邮箱是您取回密码的重要途径，请正确填写</td>
        </tr>

        <tr>
            <td class="reg-item-name"><span class="red">*&nbsp;</span>供货商名称：</td>
            <td><asp:TextBox ID="tb_providername" runat="server" MaxLength="50"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr>
            <td class="reg-item-name"><span class="red">*&nbsp;</span>真实姓名：</td>
            <td><asp:TextBox ID="tb_realname" runat="server" MaxLength="20"></asp:TextBox></td>
            <td>请填写真实的姓名，以保护您所有的用户权益</td>
        </tr>

        <tr>
            <td class="reg-item-name"><span class="red">*&nbsp;</span>联系电话：</td>
            <td><asp:TextBox ID="tb_tel" runat="server" MaxLength="20"></asp:TextBox></td>
            <td>请填写有效电话号码，供货商需要确认身份后才能交易</td>
        </tr>

        <tr>
            <td class="reg-item-name"><span class="red">*&nbsp;</span>手机号码：</td>
            <td><asp:TextBox ID="tb_mobile" runat="server" MaxLength="20"></asp:TextBox></td>
            <td>请输入有效的手机号</td>
        </tr>

        <tr>
            <td class="reg-item-name">QQ：</td>
            <td><asp:TextBox ID="tb_qq" runat="server" MaxLength="20"></asp:TextBox></td>
            <td>腾讯QQ与MSN账户必须至少填写一个</td>
        </tr>

        <tr>
            <td class="reg-item-name">MSN：</td>
            <td><asp:TextBox ID="tb_msn" runat="server" MaxLength="50"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr>
            <td class="reg-item-name">邮政编码：</td>
            <td><asp:TextBox ID="tb_postcode" runat="server" MaxLength="10"></asp:TextBox></td>
            <td>请填写正确邮编</td>
        </tr>

        <tr>
            <td class="reg-item-name">详细地址：</td>
            <td><asp:TextBox ID="tb_address" runat="server" MaxLength="100"></asp:TextBox></td>
            <td>请输入您详细的联系地址</td>
        </tr>

        <tr>
            <td class="reg-item-name"><span class="red">*&nbsp;</span>收款方式：</td>
            <td colspan="2">银行、支付宝或快钱账号必须至少填写一项</td>
        </tr>

        <tr>
            <td class="reg-item-name">收款人：</td>
            <td><asp:TextBox ID="tb_payeename" runat="server" MaxLength="50"></asp:TextBox></td>
            <td>您工商银行账号收款人姓名</td>
        </tr>

        <tr>
            <td class="reg-item-name">银行账号：</td>
            <td><asp:TextBox ID="tb_bankaccount" runat="server" MaxLength="50"></asp:TextBox></td>
            <td>您工商银行账号</td>
        </tr>

        <tr>
            <td class="reg-item-name">重复银行账号：</td>
            <td><asp:TextBox ID="tb_bankaccount_repeat" runat="server" MaxLength="50"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr>
            <td class="reg-item-name">支付宝注册姓名：</td>
            <td><asp:TextBox ID="tb_alipayname" runat="server" MaxLength="50"></asp:TextBox></td>
            <td>您支付宝账号真实姓名</td>
        </tr>

        <tr>
            <td class="reg-item-name">支付宝账号：</td>
            <td><asp:TextBox ID="tb_alipayaccount" runat="server" MaxLength="50"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr>
            <td class="reg-item-name">重复支付宝账号：</td>
            <td><asp:TextBox ID="tb_alipayaccount_repeat" runat="server" MaxLength="50"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr>
            <td class="reg-item-name">快钱注册姓名：</td>
            <td><asp:TextBox ID="tb_99billname" runat="server" MaxLength="20"></asp:TextBox></td>
            <td>您快钱账号真实姓名</td>
        </tr>

        <tr>
            <td class="reg-item-name">快钱账号：</td>
            <td><asp:TextBox ID="tb_99bill_account" runat="server" MaxLength="20"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr>
            <td class="reg-item-name">重复快钱账号：</td>
            <td><asp:TextBox ID="tb_99bill_account_repeat" runat="server" MaxLength="20"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr></tr>

        <tr>
            <td class="reg-item-name">验证码：</td>
            <td>
                <asp:TextBox ID="tb_authcode" runat="server" MaxLength="4" width="100"></asp:TextBox>
                <span id="span_authimage1" style="vertical-align:middle;"></span>
            </td>
            <td></td>
        </tr>

    </table>

    <div style="text-align:center; margin:35px 0 35px 0;">
        <asp:ImageButton ID="btn_reg" runat="server" ImageUrl="~/Images/btn_reg_submit.png" OnClick="btn_reg_click" OnClientClick="return reg_validate();" />
    </div>

</asp:Content>
