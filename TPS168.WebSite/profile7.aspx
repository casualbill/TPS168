<%@ Page Title="" Language="C#" MasterPageFile="~/site_mp.Master" AutoEventWireup="true" CodeBehind="profile7.aspx.cs" Inherits="TPS168.WebSite.profile7" %>
<%@ Register TagPrefix="profile_bar" TagName="profile_bar" Src="~/profile.ascx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Site_Head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_profile7").addClass("side-current");
        });

        function supply_delete(_supplyId) {
            var _retVal = confirm("确定要删除吗？");

            if (_retVal == true) { window.location.href = "profile7.aspx?ac=1&id=" + _supplyId; }
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
                        <span>请选择一个游戏</span><asp:DropDownList ID="ddl_gamelist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_gamelist_changed"><asp:ListItem Text="请选择游戏" Value="0"></asp:ListItem></asp:DropDownList>
                    </li>
                    <li>
                        <span>请选择一个服务器</span><asp:DropDownList ID="ddl_serverlist" runat="server"><asp:ListItem Text="请先选择游戏" Value="0"></asp:ListItem></asp:DropDownList>
                    </li>
                    <li>
                        <span></span><asp:Button ID="btn_supply_insert" runat="server" Text="添加供货信息" OnClick="btn_supply_insert_click" />
                    </li>
                </ul>
            </div>


            <div class="profile-stock-table">
                <table>
                    <tr>
                        <th class="table-first" width="40%">游戏</th>
                        <th width="50%">服务器</th>
                        <th class="table-last" width="10%">操作</th>
                    </tr>

                    <tr id="tr_querynull" runat="server" visible="false" style="text-align:center;"><td colspan="3">没有供货记录</td></tr>
                
                    <asp:Repeater ID="rpt_supply_info" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="table-first"><%# Eval("GameName") %></td>
                                <td><%# Eval("ServerName") %></td>
                                <td class="table-last"><a class="blue" style="cursor:pointer;" onclick="supply_delete(<%# Eval("Id")%>);">删除</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>

        </div>
    </div>


</asp:Content>

