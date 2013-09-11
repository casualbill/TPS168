<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gamelist.aspx.cs" Inherits="TPS168.WebSite.gamelist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <th>游戏名称</th>
            <th>服务器名称</th>
            <th>价格</th>
            <th>数量</th>
            <th>其他数据</th>
        </tr>

        <asp:Repeater ID="rpt_serverlist" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("GameName") %></td>
                    <td><%# Eval("ServerName")%></td>
                    <td><%# Eval("PurchasePrice")%></td>
                    <td><%# Eval("AmountMin")%>/<%# Eval("AmountMax")%></td>
                    <td><%# Eval("ServerStateStr")%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    
    </table>
    </form>
</body>
</html>
