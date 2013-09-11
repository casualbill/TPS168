<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="gamecfg.aspx.cs" Inherits="TPS168.WebSite.admin.gamecfg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Admin_Head" runat="server">

    <link href="../css/editor-pkg-min-datauri.css" rel="stylesheet" type="text/css" />
    <script src="../js/kissy-1.1.7-min.js" type="text/javascript"></script>
    <script src="../js/uibase-pkg-min.js" type="text/javascript"></script>
    <script src="../js/dd-pkg-min.js" type="text/javascript"></script>
    <script src="../js/overlay-pkg-min.js" type="text/javascript"></script>
    <script src="../js/editor-all-pkg-min.js" type="text/javascript"></script>
    <script src="../js/editor-plugin-pkg-min.js" type="text/javascript"></script>

    <script type="text/javascript">
        KISSY.ready(function (S) {
            S.Editor("#ctl00_Admin_ContentPlaceHolder_tb_game_caption", {
                // 是否监控编辑器所属的表单提交
                "attachForm": true,
                // 编辑器内弹窗z-index下限, 防止互相覆盖, 请保证这个数字整个页面最高
                "baseZIndex": 10000,
                // 是否编辑器初始自动聚焦(光标位于编辑器内)

                focus: true
            }).use("font,separator,color,separator,list,separator,justify,separator,resize,checkbox-sourcearea,preview,undo");
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Admin_ContentPlaceHolder" runat="server">
    <h2>游戏管理 —— <asp:Label ID="lb_querytype" runat="server"></asp:Label></h2>

    <asp:Panel ID="pn_game_list_table" runat="server" Visible="false">

    <div class="admin-search-bar">
        根据游戏状态查询：<asp:RadioButtonList ID="rbl_game_state_query" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"><asp:ListItem Text="所有" Value="-1"></asp:ListItem><asp:ListItem Text="未开仓" Value="0"></asp:ListItem><asp:ListItem Text="已开仓" Value="1"></asp:ListItem></asp:RadioButtonList> 
        <asp:Button ID="btn_game_state_query" runat="server" OnClick="btn_game_state_query_click" Text="查询" />
    </div>

        <table class="admin-table">
            <tr>
                <th width="4%">ID</th>
                <th width="12%">游戏名称</th>
                <th width="8%">价格单位</th>
                <th width="9%">游戏货币名称</th>
                <th width="8%">货币单位后缀</th>
                <th width="9%">游戏状态</th>
                <th width="40%">游戏介绍说明</th>
                <th width="20">管理</th>
            </tr>

            <tr>
                <td id="query_null1" runat="server" colspan="8" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_game" runat="server">
                <ItemTemplate>

                    <tr>
                        <td><%# Eval("Id") %></td>
                        <td><a href="gamecfg.aspx?type=11&id=<%# Eval("Id") %>"><%# Eval("GameName") %></a></td>
                        <td><%# Eval("MonetaryUnit") %></td>
                        <td><%# Eval("GameCurrencyName") %></td>
                        <td><%# Eval("GameCurrencyUnit") %></td>
                        <td><%# Eval("GameStateStr") %></td>
                        <td><%# Eval("GameCaption") %></td>
                        <td>
                            <a href="gamecfg.aspx?type=11&id=<%# Eval("Id") %>">编辑</a>
                            <a style="cursor:pointer;" onclick="game_delete(0,<%# Eval("Id") %>);">删除</a>
                        </td>
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div id="pagination_wrapper1">
        
            <span id="pagination_pageindex1" runat="server"></span>
            <span id="pagination_pageamount1" runat="server"></span>

            <div id="pagination_frame1" runat="server" visible="false" style="display:inline;">
                <span id="pagination_prev1" runat="server"></span>

                <span id="pagination_next1" runat="server"></span>

                转到第<asp:TextBox ID="tb_pageindex1" runat="server" Width="20"></asp:TextBox>页
                <asp:Button ID="btn_turntopage1" OnClick="btn_turntopage1_click" runat="server" Text="确定" />
            </div>
        </div>

    </asp:Panel>






    <asp:Panel ID="pn_server_list_table" runat="server" Visible="false">
        <div class="admin-search-bar">
            根据服务器名查询：<asp:TextBox ID="tb_server_query_by_servername_search" runat="server"></asp:TextBox><asp:Button ID="btn_server_query_by_server_name_search" runat="server" OnClick="btn_server_query_by_server_name_search_click" Text="查询" />

        </div>

        <div class="admin-search-bar">
            根据游戏查询：<asp:DropDownList ID="ddl_server_query_by_game" runat="server"></asp:DropDownList><asp:Button id="btn_server_query_by_game" OnClick="btn_server_query_by_game_click" runat="server" Text="查询" />
        </div>

        <table class="admin-table">
            <tr>
                <th width="4%">ID</th>
                <th width="17%">服务器名称</th>
                <th width="11%">所属游戏ID</th>
                <th width="15%">所属游戏名称</th>
                <th width="10%">收货价格</th>
                <th width="11%">最小下单数</th>
                <th width="11%">最大下单数</th>
                <th width="11%">服务器状态</th>
                <th width="10%">管理</th>
            </tr>

            <tr>
                <td id="query_null2" runat="server" colspan="9" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_server" runat="server">
                <ItemTemplate>

                    <tr>
                        <td><asp:Label ID="lb_rpt_serverid" runat="server" Text='<%# Eval("Id") %>'></asp:Label></td>
                        <td><a href="gamecfg.aspx?type=13&id=<%# Eval("Id") %>"><%# Eval("ServerName") %></a></td>
                        <td><%# Eval("GameId") %></td>
                        <td><a href="gamecfg.aspx?type=3&st=<%# Eval("GameId") %>"><%# Eval("GameName") %></td>
                        <td><asp:TextBox ID="tb_rpt_price" runat="server" Text='<%# Eval("PurchasePrice") %>' Width="65"></asp:TextBox></td>
                        <td><asp:TextBox ID="tb_rpt_min" runat="server" Text='<%# Eval("AmountMin") %>' Width="70"></asp:TextBox></td>
                        <td><asp:TextBox ID="tb_rpt_max" runat="server" Text='<%# Eval("AmountMax") %>' Width="70"></asp:TextBox></td>
                        <td><%# Eval("ServerStateStr") %></td>
                        <td>
                            <a href="gamecfg.aspx?type=13&id=<%# Eval("Id") %>">编辑</a>
                            <a style="cursor:pointer;" onclick="server_delete(0,<%# Eval("Id") %>);">删除</a>
                        </td>
                        <asp:HiddenField ID="hf_rpt_server_name" Value='<%# Eval("ServerName") %>' runat="server" />
                        <asp:HiddenField ID="hf_rpt_server_state" Value='<%# Eval("ServerState") %>' runat="server" />
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div id="pagination_wrapper2">
        
            <span id="pagination_pageindex2" runat="server"></span>
            <span id="pagination_pageamount2" runat="server"></span>

            <div id="pagination_frame2" runat="server" visible="false" style="display:inline;">
                <span id="pagination_prev2" runat="server"></span>

                <span id="pagination_next2" runat="server"></span>

                转到第<asp:TextBox ID="tb_pageindex2" runat="server" Width="20"></asp:TextBox>页
                <asp:Button ID="btn_turntopage2" OnClick="btn_turntopage2_click" runat="server" Text="确定" />
            </div>
        </div>

        <div class="a-center margin-top15e margin-btm15e">
            <asp:Button ID="btn_server_update_batch" runat="server" OnClick="btn_server_update_batch_click" Text="服务器批量更改" />
        </div>

    </asp:Panel>



    <asp:Panel ID="pn_game_details" runat="server" Visible="false">
        <div id="div_game_details" runat="server" class="admin-list">
            <ul>
                <li id="li_game_id" runat="server" visible="false"><em>ID：</em><asp:Label ID="lb_game_id" runat="server"></asp:Label></li>
                <li><em>游戏名称：</em><asp:TextBox ID="tb_game_name" runat="server" MaxLength="30"></asp:TextBox></li>
                <li><em>价格单位：</em><asp:TextBox ID="tb_monetary_unit" runat="server" MaxLength="10"></asp:TextBox></li>
                <li><em>游戏货币名称：</em><asp:TextBox ID="tb_game_currency_name" runat="server" MaxLength="30"></asp:TextBox></li>
                <li><em>货币单位后缀：</em><asp:TextBox ID="tb_game_currency_unit" runat="server" MaxLength="30"></asp:TextBox></li>
                <li><em>游戏状态：</em><asp:RadioButtonList ID="rbl_game_state" runat="server" Enabled="false"><asp:ListItem Text="未开仓" Value="0"></asp:ListItem><asp:ListItem Text="已开仓" Value="1"></asp:ListItem></asp:RadioButtonList></li>
                <li><em>游戏介绍说明：</em><asp:TextBox ID="tb_game_caption" TextMode="MultiLine" runat="server" MaxLength="30000" style="width:688px; height:300px;"></asp:TextBox></li>
            </ul>

            <asp:Button ID="btn_game_insert" OnClick="btn_game_insert_click" runat="server" Text="添加" Visible="false" />
            <asp:Button ID="btn_game_update" OnClick="btn_game_update_click" runat="server" Text="修改" Visible="false" />
            <asp:Button ID="btn_game_delete" OnClick="btn_game_delete_click" OnClientClick="return game_delete(1,null);" runat="server" Text="删除" Visible="false" />

        </div>

    </asp:Panel>


    <asp:Panel ID="pn_server_details" runat="server" Visible="false">
        <div id="div_server_details" runat="server" class="admin-list">
            <ul>
                <li id="li_server_id" runat="server" visible="false"><em>ID：</em><asp:Label ID="lb_server_id" runat="server"></asp:Label></li>
                <li><em id="em_server_insert_type" runat="server">添加方式：</em><asp:RadioButtonList ID="rbl_server_insert_type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_server_insert_type_changed"><asp:ListItem Text="单个添加" Value="0"></asp:ListItem><asp:ListItem Text="批量添加" Value="1"></asp:ListItem></asp:RadioButtonList></li>

                <li><em id="em_server_name" runat="server">服务器名称：</em><asp:TextBox ID="tb_server_name" runat="server" MaxLength="100"></asp:TextBox><asp:TextBox ID="tb_server_name_batch" runat="server" TextMode="MultiLine" MaxLength="10000" CssClass="small-textarea"></asp:TextBox></li>
                <li><em>所属游戏：</em><asp:DropDownList ID="ddl_server_game" runat="server"></asp:DropDownList></li>
                <li><em>收货价格：</em><asp:TextBox ID="tb_purchase_price" runat="server" MaxLength="16"></asp:TextBox></li>
                <li><em>最小下单数：</em><asp:TextBox ID="tb_amount_min" runat="server" MaxLength="12"></asp:TextBox></li>
                <li><em>最大下单数：</em><asp:TextBox ID="tb_amount_max" runat="server" MaxLength="12"></asp:TextBox></li>
                <li><em>服务器状态：</em><asp:RadioButtonList ID="rbl_server_state" runat="server" Enabled="false"><asp:ListItem Text="开放" Value="1"></asp:ListItem><asp:ListItem Text="满仓" Value="2"></asp:ListItem></asp:RadioButtonList></li>
            </ul>

            <asp:Button ID="btn_server_insert" OnClick="btn_server_insert_click" runat="server" Text="添加" Visible="false" />
            <asp:Button ID="btn_server_update" OnClick="btn_server_update_click" runat="server" Text="修改" Visible="false" />
            <asp:Button ID="btn_server_delete" OnClick="btn_server_delete_click" OnClientClick="return server_delete(1,null);" runat="server" Text="删除" Visible="false" />

        </div>

    </asp:Panel>

    <script type="text/javascript">
        function game_delete(_type, _gameId) {
            var _retVal = confirm("确定要删除吗？");

            if (_type == 0) {
                if (_retVal == true) { window.location.href = "gamecfg.aspx?type=12&id=" + _gameId; }
            }

            if (_type == 1) {
                return _retVal;
            }
        }

        function server_delete(_type, _serverId) {
            var _retVal = confirm("确定要删除吗？");

            if (_type == 0) {
                if (_retVal == true) { window.location.href = "gamecfg.aspx?type=14&id=" + _serverId; }
            }

            if (_type == 1) {
                return _retVal;
            }
        }
    
    </script>

</asp:Content>
