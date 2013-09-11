<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="profile6.aspx.cs" Inherits="TPS168.WebSite.profile6" %>
<%@ Register TagPrefix="profile_bar" TagName="profile_bar" Src="~/profile.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">
    <link href="css/admin.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_profile6").addClass("side-current");

            $("input[js=btn_order_cancel][state=2]").attr("disabled", "disabled");
            $("input[js=btn_order_cancel][state=3]").attr("disabled", "disabled");
            $("input[js=btn_order_cancel][state=4]").attr("disabled", "disabled");
        });

        function order_cancel_check() {
            return confirm("本次操作将使订单失败，确定要继续吗？");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Site_ContentPlaceHolder" runat="server">

    <profile_bar:profile_bar ID="Profile_bar1" runat="server" />

    <div class="main-center">
        <div class="crumb">
            当前位置：<a href="index.aspx">首页</a> > <a class="blue boldface" href="profile1.aspx">我的游戏中心</a>
        </div>

        <div class="main-content">
            
        <asp:Panel ID="pn_order_list_table" runat="server">
            <span id="span_profile_tips" runat="server" class="profile-tips" style="display:none;"></span>

            <div class="profile-stock-table">
                <table>
                    <tr>
                        <th class="table-first">订单号</th>
                        <th>游戏</th>
                        <th>服务器</th>
                        <th>数量</th>
                        <th>总价</th>
                        <th>订单状态</th>
                        <!--th class="table-last">操作</th-->
                    </tr>

                    <tr>
                        <td id="query_null" runat="server" colspan="7" visible="false" style="text-align:center;">抱歉，没有符合条件的订单记录，您可以点击这里<a class="green boldface" href="profile4.aspx">下单</a></td>
                    </tr>

                    <asp:Repeater ID="rpt_order_list" runat="server" OnItemCommand="rpt_order_list_btn_click">
                        <ItemTemplate>

                            <tr class="tr_order_list">
                                <td class="table-first"><a href="profile6.aspx?id=<%# Eval("OrderId") %>"><%# Eval("OrderId") %></a></td>
                                <td><%# Eval("GameName") %></td>
                                <td><%# Eval("ServerName") %></td>
                                <td><%# Eval("Amount") %></td>
                                <td><%# Eval("TotalPrice") %></td>
                                <td><%# Eval("OrderStateStr") %></th>
                                <!--td class="table-last">
                                    <asp:Button ID="btn_rpt_order_fail" runat="server" CommandArgument='<%# Eval("OrderId")+","+Eval("OrderState") %>' CommandName="btn_rpt_fail" Text="取消" OnClientClick="return order_cancel_check();" js="btn_order_cancel" state='<%# Eval("OrderState") %>' />
                                </td-->
                            </tr>
            
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>

            <div id="pagination_wrapper" class="pagination-profile">
        
                <span id="pagination_pageindex" runat="server"></span>
                <span id="pagination_pageamount" runat="server"></span>

                <div id="pagination_frame" runat="server" visible="false" style="display:inline;">
                    <span id="pagination_prev" runat="server"></span>

                    <span id="pagination_next" runat="server"></span>

                </div>
            </div>

        </asp:Panel>

        <asp:Panel ID="pn_order_details_list" runat="server" Visible="false">
            <div id="div_order_details" runat="server" class="admin-list">
                <ul>
                    <li><em></em></li>
                    <li><em>订单号：</em><asp:Label ID="lb_order_details0" runat="server"></asp:Label></li>
                    <li><em>用户名：</em><asp:Label ID="lb_order_details2" runat="server"></asp:Label></li>
                    <li><em>服务器名：</em><asp:Label ID="lb_order_details4" runat="server"></asp:Label></li>
                    <li><em>游戏名：</em><asp:Label ID="lb_order_details6" runat="server"></asp:Label></li>
                    <li><em>角色名：</em><asp:Label ID="lb_order_details7" runat="server"></asp:Label></li>
                    <li><em>供货数量：</em><asp:Label ID="lb_order_details8" runat="server"></asp:Label></li>
                    <li><em>单价：</em><asp:Label ID="lb_order_details9" runat="server"></asp:Label></li>
                    <li><em>总价：</em><asp:Label ID="lb_order_details10" runat="server"></asp:Label></li>

                    <li><em>QQ：</em><asp:Label ID="lb_order_details20" runat="server"></asp:Label></li>
                    <li><em>MSN：</em><asp:Label ID="lb_order_details21" runat="server"></asp:Label></li>
                    <li><em>电话：</em><asp:Label ID="lb_order_details22" runat="server"></asp:Label></li>
                    <li><em>手机：</em><asp:Label ID="lb_order_details23" runat="server"></asp:Label></li>

                    <li><em>交易类型：</em><asp:Label ID="lb_order_details11" runat="server"></asp:Label></li>
                    <li><em>收款人姓名：</em><asp:Label ID="lb_order_details12" runat="server"></asp:Label></li>
                    <li><em>收款账号：</em><asp:Label ID="lb_order_details13" runat="server"></asp:Label></li>
                    <li><em>交易时间：</em><asp:Label ID="lb_order_details14" runat="server"></asp:Label></li>
                    <li><em>下单时间：</em><asp:Label ID="lb_order_details15" runat="server"></asp:Label></li>
                    <li><em>完成时间：</em><asp:Label ID="lb_order_details16" runat="server"></asp:Label></li>
                    <li><em>备注：</em><asp:Label ID="lb_order_details17" runat="server"></asp:Label></li>
                    <li><em>流水号：</em><asp:Label ID="lb_order_details18" runat="server"></asp:Label></li>
                    <li><em>当前订单状态：</em><asp:Label ID="lb_order_details19" runat="server"></asp:Label></li>
                    <li><em></em></li>
                </ul>
                <asp:HiddenField ID="hf_order_state" runat="server" />
            </div>

        </asp:Panel>

        </div>

    </div>

</asp:Content>
