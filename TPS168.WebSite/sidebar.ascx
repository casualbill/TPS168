<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sidebar.ascx.cs" Inherits="TPS168.WebSite.sidebar" %>

<div class="sidebar">

    <div class="side-frame">
        <div class="side-title">
            <span class="side-more"><a href="#">更多&gt;&gt;</a></span>
            <span class="icon-common-2"></span><span>站内公告</span>
        </div>

        <div class="side-content side-news">
            <ul>
            <asp:Repeater ID="rpt_sidebar_news" runat="server">
                <ItemTemplate>
                    <li><a href="article.aspx?id=<%# Eval("Id") %>"><%# Eval("NewsTitle") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
        </div>
    
    </div>


    <div class="side-frame">
        <div class="side-title">
            <span class="icon-common-2"></span><span>客服支持</span>
        </div>

        <div class="side-content side-custom-service">
            <ul>
                <li>请选择相应联系客服，也可根据游戏选择相应负责的客服QQ，谢谢。<br />（加Q时请注明 供货游戏 方便我们分组 谢谢）</li>
                <li>
                    <b>QQ 68221950 魔兽世界 暗黑3/Diablo3</b><br />
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=68221950&site=qq&menu=yes"><img border="0" src="images/ico_qq_online.jpg" alt="点击这里给我发消息" title="点击这里给我发消息" /></a>
                </li>
                <li>
                    <b>QQ 199720829 王者世界 CABAL rappelz 飞飞</b><br />
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=199720829&site=qq&menu=yes"><img border="0" src="images/ico_qq_online.jpg" alt="点击这里给我发消息" title="点击这里给我发消息" /></a>
                </li>
                <li>
                    <b>QQ 704829 冒险岛 DOFUS EVE</b><br />
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=704829&site=qq&menu=yes"><img border="0" src="images/ico_qq_online.jpg" alt="点击这里给我发消息" title="点击这里给我发消息" /></a>
                </li>
                <li>
                    <b>QQ 6578871 科南时代 AION 战锤</b><br />
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=6578871&site=qq&menu=yes"><img border="0" src="images/ico_qq_online.jpg" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                </li>
                <li>
                    <b>QQ 470174 Tera EQ2 魔戒 COH</b><br />
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=470174&site=qq&menu=yes"><img border="0" src="images/ico_qq_online.jpg" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                </li>
                <li>
                    <b>QQ 272370888 星球大战SWTOR</b><br />
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=272370888&site=qq&menu=yes"><img border="0" src="images/ico_qq_online.jpg" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                </li>
                <li>
                    <b>QQ 268017557 RIFT</b><strong>RS江湖 龙之谷</strong><br />
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=268017557&site=qq&menu=yes"><img border="0" src="images/ico_qq_online.jpg" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                </li>
                <li class="li-special">
                    <b>QQ 268027557 渠道主管QQ<br />深度业务合作 投诉</b><br />
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=268027557&site=qq&menu=yes"><img border="0" src="images/ico_qq_online.jpg" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                </li>
            </ul>
        </div>
    </div>

    <div class="side-frame">
        <div class="side-title">
            <span class="icon-common-2"></span><span>合作伙伴</span>
        </div>

        <div class="side-content side-ad">
            <ul>
                <li><a href="http://www.alipay.com"><img src="Images/ad_alipay.png" /></a></li>
                <li><a href="http://www.tenpay.com"><img src="Images/ad_tenpay.png" /></a></li>
                <li><a href="http://www.zhuanyewanjia.com/"><img src="Images/ad_zywj.png" /></a></li>
                <li><a href="http://www.icbc.com.cn"><img src="Images/ad_icbc.png" /></a></li>
            </ul>
        </div>
    </div>

</div>
