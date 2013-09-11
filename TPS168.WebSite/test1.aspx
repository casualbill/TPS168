<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="TPS168.WebSite.test1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script src="js/WdatePicker.js" type="text/javascript"></script>
<script src="js/common.js" type="text/javascript"></script>
<script src="js/jquery-1.6.2.min.js" type="text/javascript"></script>
    <title></title>


<script type="text/javascript">
    $(document).ready(function () {
        create_auth_image("span_authimage");
    });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_changed"></asp:DropDownList>
        <asp:Label ID="lb" runat="server" Text="aa"></asp:Label>

        <asp:Label ID="lb2" runat="server" Text="bb"></asp:Label>
        <asp:Button ID="btn" OnClick="btn_click" runat="server" />
        

        <span id="span_authimage" style=" vertical-align:middle;"></span>

        <asp:DataGrid ID="dg" runat="server"></asp:DataGrid>

    </div>
    </form>
</body>
</html>
