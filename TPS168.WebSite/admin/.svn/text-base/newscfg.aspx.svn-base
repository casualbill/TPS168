<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="newscfg.aspx.cs" Inherits="TPS168.WebSite.admin.newscfg" %>
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
            S.Editor("#ctl00_Admin_ContentPlaceHolder_tb_news_content", {
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

    <h2>新闻设置 —— <asp:Label ID="lb_querytype" runat="server"></asp:Label></h2>

    <asp:Panel ID="pn_news_list_table" runat="server" Visible="false">
        <table class="admin-table">
            <tr>
                <th>ID</th>
                <th>标题</th>
                <th>内容</th>
                <th>类型</th>
                <th>编辑用户</th>
                <th>编辑时间</th>
                <th>管理</th>
            </tr>

            <tr>
                <td id="query_null" runat="server" colspan="7" visible="false" style="text-align:center;">没有查询记录</td>
            </tr>

            <asp:Repeater ID="rpt_news" runat="server">
                <ItemTemplate>

                    <tr>
                        <td><%# Eval("Id") %></td>
                        <td><a href="newscfg.aspx?type=10&id=<%# Eval("Id") %>"><%# Eval("NewsTitle") %></a></td>
                        <td><%# Eval("NewsContent") %></td>
                        <td><%# Eval("NewsTypeStr") %></td>
                        <td><%# Eval("UserName") %></td>
                        <td><%# Eval("EditTime") %></td>
                        <td>
                            <a href="newscfg.aspx?type=10&id=<%# Eval("Id") %>">编辑</a>
                            <a style="cursor:pointer;" onclick="news_delete(0,<%# Eval("Id") %>);">删除</a>
                        </td>
                    </tr>
            
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div id="pagination_wrapper">
        
            <span id="pagination_pageindex" runat="server"></span>
            <span id="pagination_pageamount" runat="server"></span>

            <div id="pagination_frame" runat="server" visible="false" style="display:inline;">
                <span id="pagination_prev" runat="server"></span>

                <span id="pagination_next" runat="server"></span>

                转到第<asp:TextBox ID="tb_pageindex" runat="server" Width="20"></asp:TextBox>页
                <asp:Button ID="btn_turntopage" OnClick="btn_turntopage_click" runat="server" Text="确定" />
            </div>
        </div>

    </asp:Panel>

    <asp:Panel ID="pn_news_details" runat="server" Visible="false">
        <div id="div_news_details" runat="server" class="admin-list">
            <ul>
                <li id="li_news_id" runat="server" visible="false"><em>ID：</em><asp:Label ID="lb_news_id" runat="server"></asp:Label></li>
                <li><em>标题：</em><asp:TextBox ID="tb_news_title" runat="server" MaxLength="256" Width="400"></asp:TextBox></li>
                <li><em>内容：</em><asp:TextBox ID="tb_news_content" TextMode="MultiLine" runat="server" MaxLength="50000" style="width:688px; height:300px;"></asp:TextBox></li>
                <li><em>类型：</em><asp:RadioButtonList ID="rbl_news_type" runat="server"><asp:ListItem Text="站内公告" Value="1"></asp:ListItem><asp:ListItem Text="游戏资讯" Value="2"></asp:ListItem><asp:ListItem Text="常见问题" Value="3"></asp:ListItem></asp:RadioButtonList></li>
                <li id="li_news_edituser" runat="server" visible="false"><em>编辑用户：</em><asp:Label ID="lb_news_edituser" runat="server"></asp:Label></li>
                <li id="li_news_edittime" runat="server" visible="false"><em>编辑时间：</em><asp:Label ID="lb_news_edittime" runat="server"></asp:Label></li>

            </ul>

            <asp:Button ID="btn_news_insert" OnClick="btn_news_insert_click" runat="server" Text="添加" Visible="false" />
            <asp:Button ID="btn_news_update" OnClick="btn_news_update_click" runat="server" Text="修改" Visible="false" />
            <asp:Button ID="btn_news_delete" OnClick="btn_news_delete_click" OnClientClick="return news_delete(1,null);" runat="server" Text="删除" Visible="false" />

        </div>

    </asp:Panel>

    <script type="text/javascript">
        function news_delete(_type,_newsId) {
            var _retVal = confirm("确定要删除吗？");

            if (_type == 0) {
                if (_retVal == true) { window.location.href = "newscfg.aspx?type=11&id=" + _newsId; }
            }

            if (_type == 1) {
                return _retVal;
            }
        }
    
    </script>


</asp:Content>
