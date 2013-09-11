<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="ordercfg.aspx.cs" Inherits="TPS168.WebSite.admin.ordercfg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Admin_Head" runat="server">
    <script type="text/javascript">
        function onclick_submit(_strType) {
            var _str
            if (_strType == 1) { _str = "本次操作将更新订单状态，确定要继续吗？"; }
            if (_strType == 2) { _str = "本次操作将使订单失败，确定要继续吗？"; }
            if (_strType == 3) { _str = "本次操作将改变订单交易供货数量，确定要继续吗？"; }

            return confirm(_str);
        }

        $(document).ready(function () {

            $('[name="OrderShortcutBtn"]').click(function () {
                var _token = $("#ctl00_Admin_ContentPlaceHolder_hf_token").val();
                var _thisBtn = $(this);
                var _orderId = _thisBtn.attr("js1");
                var _orderState = _thisBtn.attr("js2");
                var _serial = $.trim(_thisBtn.parent().children('[name="OrderShortcutTb"]').val());

                if (_orderState == "3" && _serial == "") {
                    alert("请输入流水号！");
                }
                else {
                    $.ajax({
                        url: "ordercfg.asmx/OrderOperationShortcut",
                        type: "POST",
                        data: "{_token:'" + _token + "',_orderId:'" + _orderId + "',_orderState:'" + _orderState + "',_serial:'" + _serial + "'}",
                        dataType: "json",
                        contentType: "application/json",
                        success: function (r) {
                            if (r.d == 2) {
                                _thisBtn.attr("disabled", "disabled");
                                _thisBtn.parent().parent().children('[js="OrderStateTd"]').html(order_state_str(_orderState));
                                _thisBtn.val(order_state_btn_str(_orderState));
                            }
                            else {
                                alert("操作失败，请稍后重试！");
                            }
                        },
                        error: function () {
                            alert("操作失败，请稍后重试！");
                        }
                    });
                }

                function order_state_str(_state) {
                    var _retVal;
                    switch (_state) {
                        case "1": _retVal = "审核通过-等待交易"; break;
                        case "2": _retVal = "交易完成-等待汇款"; break;
                        case "3": _retVal = "完成汇款"; break;
                    }
                    return _retVal;
                }

                function order_state_btn_str(_state) {
                    var _retVal;
                    switch (_state) {
                        case "1": _retVal = "审核通过"; break;
                        case "2": _retVal = "交易完成"; break;
                        case "3": _retVal = "完成汇款"; break;
                    }
                    return _retVal;
                }
            });
        });

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Admin_ContentPlaceHolder" runat="server">
    <asp:HiddenField ID="hf_token" runat="server" />
    <h2>订单管理 —— <asp:Label ID="lb_querytype" runat="server"></asp:Label><asp:Button ID="btn_export_to_csv" runat="server" Visible="false" OnClick="btn_export_to_csv_click" Text="导出至CSV文件" /></h2>

    <asp:Panel ID="pn_order_list_table" runat="server" Visible="false">

        <div class="admin-search-bar">
            <ul style="display:none;">
                <li>根据订单号查询：<asp:TextBox ID="tb_orderid_query" runat="server"></asp:TextBox><asp:Button id="btn_orderid_query" OnClick="btn_orderid_query_click" runat="server" Text="查询" /></li>
                <li>根据用户名查询：<asp:TextBox ID="tb_user_query" runat="server"></asp:TextBox><asp:Button id="btn_user_query" OnClick="btn_user_query_click" runat="server" Text="查询" /></li>
                <li>根据游戏查询：<asp:DropDownList ID="ddl_game_query" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_game_query_changed"><asp:ListItem Text="请选择游戏" Value="0"></asp:ListItem></asp:DropDownList><asp:Button id="btn_game_query" OnClick="btn_game_query_click" runat="server" Text="查询" /></li>
                <li>根据服务器查询：<asp:DropDownList ID="ddl_server_query" runat="server"><asp:ListItem Text="请先选择游戏" Value="0"></asp:ListItem></asp:DropDownList><asp:Button id="btn_server_query" OnClick="btn_server_query_click" runat="server" Text="查询" /></li>
            </ul>

            <ul>
                <li><asp:TextBox ID="tb_search_new" runat="server" Width="300" MaxLength="255"></asp:TextBox><asp:Button ID="btn_search_new" OnClick="btn_search_new_click" runat="server" Text="搜索" /></li>
            </ul>
        </div>

        <table class="admin-table">
            <tr>
                <th width="12%">订单号</th>
                <th width="8%">游戏</th>
                <th width="8%">服务器</th>
                <th width="8%">用户名</th>
                <th width="5%">供货量</th>
                <th width="5%">单价</th>
                <th width="7%">总价</th>
                <th width="9%">游戏角色名</th>
                <th width="9%">下单时间</th>
                <th width="9%">完成时间</th>
                <th width="9%">订单状态</th>
                <th width="11%">管理</th>
            </tr>

            <tr>
                <td id="query_null1" runat="server" colspan="12" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_order_list" runat="server">
                <ItemTemplate>

                    <tr class="tr_order_list">
                        <td><a href="ordercfg.aspx?type=2&id=<%# Eval("OrderId") %>"><%# Eval("OrderId") %></a></td>
                        <td><%# Eval("GameName") %></td>
                        <td><%# Eval("ServerName") %></td>
                        <td><%# Eval("UserName") %></td>
                        <td><%# Eval("Amount") %></td>
                        <td><%# Eval("PurchasePrice") %></td>
                        <td><%# Eval("TotalPrice") %></td>
                        <td><%# Eval("RoleName") %></th>
                        <td><%# Eval("PlaceTime") %></th>
                        <td><%# Eval("CloseTime") %></th>
                        <td js="OrderStateTd"><%# Eval("OrderStateStr") %></th>
                        <td>
                            <a href="ordercfg.aspx?type=2&id=<%# Eval("OrderId") %>">详情管理</a><br />
                            <a href="ordercfg.aspx?type=3&id=<%# Eval("OrderId") %>">操作记录</a><br />
                            流水号：<input type="text" style="width:60px;" name="OrderShortcutTb" <%# Eval("OrderOperationBtnState") %> /><br />
                            <input type="button" name="OrderShortcutBtn" js1="<%# Eval("OrderId") %>" js2="<%# Eval("OrderStateOperation") %>" value="<%# Eval("OrderStateOperationStr") %>" <%# Eval("OrderOperationBtnState") %> />
                        </td>
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <asp:Label ID="lb_totalamount" runat="server" Visible="false"></asp:Label>

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


    <asp:Panel ID="pn_order_details_list" runat="server" Visible="false">
        <div id="div_order_details" runat="server" class="admin-list">
            <ul>
                <li><em>订单号：</em><asp:Label ID="lb_order_details0" runat="server"></asp:Label></li>
                <li><em>用户ID：</em><asp:Label ID="lb_order_details1" runat="server"></asp:Label></li>
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
                <li><em></em></li>
                <li><em>流水号：</em><asp:TextBox ID="tb_order_serial_number" runat="server"></asp:TextBox></li>
                <li><em>更新状态：</em><asp:Button ID="btn_order_state_update" runat="server" OnClick="btn_order_state_update_click" OnClientClick="return onclick_submit(1);" /><asp:Button ID="btn_order_fail" runat="server" Text="失败" OnClick="btn_order_fail_click"  OnClientClick="return onclick_submit(2);" /></li>
                <li><em>更改数量：</em><asp:TextBox ID="tb_order_amount" runat="server"></asp:TextBox><asp:Button ID="btn_order_amount_update" runat="server" Text="更改" OnClick="btn_order_amount_update_click" OnClientClick="return onclick_submit(3);" /></li>
                <li><em>备注：</em><asp:TextBox ID="tb_remarks" runat="server" CssClass="small-textarea" TextMode="MultiLine" MaxLength="250"></asp:TextBox></li>
            </ul>
            <asp:HiddenField ID="hf_order_state" runat="server" />
        </div>

    </asp:Panel>


    <asp:Panel ID="pn_order_log_list" runat="server" Visible="false">
        <div class="admin-search-bar">
            订单号：<asp:TextBox ID="tb_orderid" runat="server" MaxLength="20"></asp:TextBox>
        <asp:Button ID="btn_orderlog_query" OnClick="btn_orderlog_query_click" runat="server" Text="查询" />
        </div>

        <table class="admin-table">
            <tr>
                <th width="6%">记录编号</th>
                <th width="12%">订单号</th>
                <th width="8%">操作员</th>
                <th width="8%">服务器</th>
                <th width="8%">总价</th>
                <th width="12%">原状态</th>
                <th width="12%">更改后状态</th>
                <th width="12%">操作时间</th>
                <th width="22%">备注</th>
            </tr>

            <tr>
                <td id="query_null2" runat="server" colspan="9" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_order_log" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("LogId") %></td>
                        <td><%# Eval("OrderId") %></td>
                        <td><%# Eval("UserName") %></td>
                        <td><%# Eval("ServerName")%></td>
                        <td><%# Eval("Totalprice")%></td>
                        <td><%# Eval("FormerStateStr")%></td>
                        <td><%# Eval("UpdateStateStr")%></td>
                        <td><%# Eval("OperationTime")%></td>
                        <td><%# Eval("Remarks")%></td>
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

    </asp:Panel>


</asp:Content>
