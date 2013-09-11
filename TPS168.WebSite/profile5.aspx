<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="profile5.aspx.cs" Inherits="TPS168.WebSite.profile5" %>
<%@ Register TagPrefix="profile_bar" TagName="profile_bar" Src="~/profile.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_profile5").addClass("side-current");
        });

        function stock_delete(_stockId) {
            var _retVal = confirm("确定要删除吗？");

            if (_retVal == true) { window.location.href = "profile5.aspx?ac=1&id=" + _stockId; }
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
            
            <span id="span_profile_tips" runat="server" class="profile-tips" style="display:none;"></span>

            <div class="profile-stock-list clearfix">
                <ul>
                    <li>
                        <span>请选择一个游戏</span><asp:DropDownList ID="ddl_gamelist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_gamelist_changed" Width="200"><asp:ListItem Text="请选择游戏" Value="0"></asp:ListItem></asp:DropDownList>
                    </li>
                    <li>
                        <span>请选择一个服务器</span><asp:DropDownList ID="ddl_serverlist" runat="server"><asp:ListItem Text="请先选择游戏" Value="0"></asp:ListItem></asp:DropDownList>
                    </li>
                    <li>
                        <span>可供货量</span><asp:TextBox ID="tb_stock_amount" runat="server" Height="20"></asp:TextBox>
                    </li>
                    <li>
                        <span>预售价格</span><asp:TextBox ID="tb_presell_price" runat="server" Height="20"></asp:TextBox>
                    </li>
                    <li>
                        <span></span><asp:Button ID="btn_stock_insert" runat="server" Text="添加库存信息" OnClick="btn_stock_insert_click" />
                    </li>
                </ul>
            </div>


            <div class="profile-stock-table">
                <table>
                    <tr>
                        <th class="table-first" width="22%">游戏</th>
                        <th width="28%">服务器</th>
                        <th width="10%">数量</th>
                        <th width="10%">预售价格</th>
                        <th width="25%">登记时间</th>
                        <th class="table-last" width="5%">操作</th>
                    </tr>

                    <tr id="tr_querynull" runat="server" visible="false" style="text-align:center;"><td colspan="6">没有库存记录</td></tr>
                
                    <asp:Repeater ID="rpt_stock_info" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="table-first"><%# Eval("GameName") %></td>
                                <td><%# Eval("ServerName") %></td>
                                <td><%# Eval("StockAmount")%></td>
                                <td><%# Eval("PresellPrice")%></td>
                                <td><%# Eval("RegTime")%></td>
                                <td class="table-last"><a class="blue" style="cursor:pointer;" onclick="stock_delete(<%# Eval("Id")%>);">删除</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>

        </div>
    </div>


</asp:Content>
