<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="profile4.aspx.cs" Inherits="TPS168.WebSite.profile4" %>
<%@ Register TagPrefix="profile_bar" TagName="profile_bar" Src="~/profile.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_profile4").addClass("side-current");
            order_price_calculate();
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
            
            

            <asp:Panel ID="pn_placeorder" runat="server">
                <div class="profile-order-list clearfix">
                    <ul>
                        <li><span class="order-item-name">游戏名称：</span><asp:Label ID="lb_game_name" runat="server"></asp:Label></li>
                        <li><span class="order-item-name">服务器名称：</span><asp:Label ID="lb_server_name" runat="server"></asp:Label></li>
                        <li><span class="order-item-name">收货方要求：</span>每笔最低限制：<b><asp:Label ID="lb_amount_min" runat="server"></asp:Label></b></li>
                        <li><span class="order-item-name"></span>每笔最高限制：<b><asp:Label ID="lb_amount_max" runat="server"></asp:Label></b></li>
                        <li><span class="order-item-name"></span>订单采购单价：<b><asp:Label ID="lb_price" runat="server"></asp:Label></b></li>
                        <li><span class="order-item-name">供货商名称：</span><asp:Label ID="lb_provider" runat="server"></asp:Label></li>
                        <li><span class="order-item-name">联系人：</span><asp:Label ID="lb_real_name" runat="server"></asp:Label></li>
                        <li class="list-blank"></li>
                        <li><span class="order-item-name">联系电话1：</span><asp:Label ID="lb_tel" runat="server"></asp:Label></li>
                        <li><span class="order-item-name">联系电话2：</span><asp:Label ID="lb_mob" runat="server"></asp:Label></li>
                        <li><span class="order-item-name">联系QQ号码：</span><asp:Label ID="lb_qq" runat="server"></asp:Label></li>
                        <li><span class="order-item-name">联系MSN地址：</span><asp:Label ID="lb_msn" runat="server"></asp:Label></li>
                        <li class="list-cutline"></li>

                        <li><span id="span_profile_tips" runat="server" class="profile-tips" style="display:none;"></span></li>
                        <li><span class="order-item-name">请选择您的结算方式：</span><asp:RadioButton ID="rb_bank" runat="server" Text="工商银行" GroupName="rb_trade_type" /></li>
                        <li id="li_tradetype1" runat="server" visible="false"><table class="profile-order-innertable">
                                <tr><td class="table-first" width="35%"><b>收款人：</b></td><td width="65%"><asp:Label ID="lb_payee" runat="server"></asp:Label></td></tr>
                                <tr><td class="table-first"><b>银行账号：</b></td><td><asp:Label ID="lb_bank_account" runat="server"></asp:Label></td></tr>
                            </table>
                        </li>
                        <li><span class="order-item-name"></span><asp:RadioButton ID="rb_alipay" runat="server" Text="支付宝" GroupName="rb_trade_type" /></li>
                        <li id="li_tradetype2" runat="server" visible="false"><table class="profile-order-innertable">
                                <tr><td class="table-first" width="35%"><b>支付宝注册姓名：</b></td><td width="65%"><asp:Label ID="lb_alipay_name" runat="server"></asp:Label></td></tr>
                                <tr><td class="table-first"><b>支付宝账号：</b></td><td><asp:Label ID="lb_alipay_account" runat="server"></asp:Label></td></tr>
                            </table>
                        </li>
                        <li><span class="order-item-name"></span><asp:RadioButton ID="rb_99bill" runat="server" Text="快钱" GroupName="rb_trade_type" /></li>
                        <li id="li_tradetype3" runat="server" visible="false"><table class="profile-order-innertable">
                                <tr><td class="table-first" width="35%"><b>快钱注册姓名：</b></td><td width="65%"><asp:Label ID="lb_99bill_name" runat="server"></asp:Label></td></tr>
                                <tr><td class="table-first"><b>快钱账号：</b></td><td><asp:Label ID="lb_99bill_account" runat="server"></asp:Label></td></tr>
                            </table>
                        </li>
                        <li class="list-blank"></li>
                        <li><span class="order-item-name">本次供货量：</span><asp:TextBox ID="tb_amount" runat="server" MaxLength="12"></asp:TextBox><asp:Label ID="lb_currency_unit" runat="server"></asp:Label></li>
                        <li><span class="order-item-name">游戏角色名称：</span><asp:TextBox ID="tb_rolename" runat="server" MaxLength="30"></asp:TextBox></li>
                        <li><span class="order-item-name">总价格：</span><asp:Label ID="tb_total_price" runat="server"></asp:Label></li>
                        <li class="list-blank"></li>
                        <li><span class="order-item-name">交易时间：</span><asp:Textbox ID="tb_trade_time" runat="server" CssClass="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></asp:Textbox></li>
                        <li><span class="order-item-name">备注：<br />（填写游戏内交易地点等<br />相关交易信息）</span><asp:TextBox ID="tb_remarks" runat="server" TextMode="MultiLine" MaxLength="2000" Width="300" Height="100"></asp:TextBox></li>
                        
                        <asp:HiddenField ID="hf_purchase_price" runat="server" />
                        <asp:HiddenField ID="hf_amount_min" runat="server" />
                        <asp:HiddenField ID="hf_amount_max" runat="server" />
                    </ul>
                </div>

                <div class="profile-btn-frame">
                    <asp:ImageButton ID="btn_placeorder" runat="server" ImageUrl="~/images/btn_placeorder.png" OnClick="btn_placeorder_click" OnClientClick="return place_order_validate();" />
                </div>
            </asp:Panel>
            

        </div>
    </div>

</asp:Content>
