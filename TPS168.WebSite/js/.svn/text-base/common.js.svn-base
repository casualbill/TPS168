
function create_auth_image(_imgDiv1,_imgDiv2) {
    var numkey = Math.random();
    numkey = Math.round(numkey * 10000);
    $('#' + _imgDiv1).html('<img src=\"authimage.aspx?k=' + numkey + '\" width=\"70\" onClick=\"this.src+=Math.random()\" alt=\"图片看不清？点击重新得到验证码\" style=\"cursor:pointer;\" height=\"25\" hspace=\"4\" align=\"absmiddle\" />');
    $('#' + _imgDiv2).html('<img src=\"authimage.aspx?k=' + numkey + '\" width=\"70\" onClick=\"this.src+=Math.random()\" alt=\"图片看不清？点击重新得到验证码\" style=\"cursor:pointer;\" height=\"25\" hspace=\"4\" align=\"absmiddle\" />'); 
}


function addToFavorite(_url, _title) {
    var _ua = navigator.userAgent.toLowerCase();
    if (_ua.indexOf("msie 8") > -1) {
        external.AddToFavoritesBar(_url, _title, ""); //IE8
    } else {
        try {
            window.external.addFavorite(_url, _title);
        } catch (e) {
            try {
                window.sidebar.addPanel(_title, _url, ""); //firefox
            } catch (e) {
                alert("您的浏览器不支持自动添加至收藏夹，请使用\"Ctrl+D\"进行手动添加！");
            }
        }
    }

}

function header_news_roll() {
    var _newsId = new Array;
    var _newsContent = new Array;
    var _newsCount = parseInt($("#ctl00_ipt_news_count").val());
    var _crtNews = 0;

    _newsId[0] = $("#ctl00_ipt_news_id0").val();
    _newsId[1] = $("#ctl00_ipt_news_id1").val();
    _newsId[2] = $("#ctl00_ipt_news_id2").val();
    _newsId[3] = $("#ctl00_ipt_news_id3").val();
    _newsId[4] = $("#ctl00_ipt_news_id4").val();

    _newsContent[0] = $("#ctl00_ipt_news_content0").val();
    _newsContent[1] = $("#ctl00_ipt_news_content1").val();
    _newsContent[2] = $("#ctl00_ipt_news_content2").val();
    _newsContent[3] = $("#ctl00_ipt_news_content3").val();
    _newsContent[4] = $("#ctl00_ipt_news_content4").val();

    news_roll();

    function news_roll() {
        $("#span_news_roll").fadeOut(500, function () { $("#span_news_roll").html("<a class=\"quiet666\" href=\"article.aspx?id=" + _newsId[_crtNews] + "\">" + _newsContent[_crtNews] + "</a>"); });
        $("#span_news_roll").fadeIn(500);

        if (_crtNews == _newsCount - 1) {
            _crtNews = 0;
        }
        else {
            _crtNews++;
        }

        setTimeout(function () { news_roll(); }, 5000);
    }
}

function index_gamelist_toggle() {
    $("#li_gamelist1").click(function () {
        $("#li_gamelist1").addClass("list-current");
        $("#li_gamelist0").removeClass("list-current");
        $("#gamelist-gamestate1").show();
        $("#gamelist-gamestate0").hide();
    });

    $("#li_gamelist0").click(function () {
        $("#li_gamelist0").addClass("list-current");
        $("#li_gamelist1").removeClass("list-current");
        $("#gamelist-gamestate0").show();
        $("#gamelist-gamestate1").hide();
    });
}

function server_state_table_toggle() {
    $(".tr_index_serverlist[js=2]").children("td").addClass("td-bggray");
    $(".tr_index_serverlist[js=2]").children("td").removeClass("td-bluebold");
    $(".tr_index_serverlist[js=2]").children("td").children("input").val("暂停");
    $(".tr_index_serverlist[js=2]").children("td").children("input").attr("disabled", "disabled");

    $(".ipt_place_server").click(function () {
        var _serverId = $(this).attr("js");
        window.location.href = "profile4.aspx?id=" + _serverId;
    });
}

function order_price_calculate() {
    var _purchasePrice = parseFloat($("#ctl00_Site_ContentPlaceHolder_hf_purchase_price").val());

    $("#ctl00_Site_ContentPlaceHolder_tb_amount").blur(function () {    
        var _amount = parseFloat($("#ctl00_Site_ContentPlaceHolder_tb_amount").val());
        var _total = parseFloat(_purchasePrice * _amount);

        if (_total == parseFloat(_total)) {
            $("#ctl00_Site_ContentPlaceHolder_tb_total_price").show();
            $("#ctl00_Site_ContentPlaceHolder_tb_total_price").html(_total.toFixed(2));
        }
        else {
            $("#ctl00_Site_ContentPlaceHolder_tb_total_price").hide();
            $("#ctl00_Site_ContentPlaceHolder_tb_total_price").html("");
        }
    })
}

function reg_validate() {
   
    var _username=$("#ctl00_Site_ContentPlaceHolder_tb_username").val();
    var _pwd1 = $("#ctl00_Site_ContentPlaceHolder_tb_password").val();
    var _pwd2 = $("#ctl00_Site_ContentPlaceHolder_tb_password_repeat").val();
    var _email = $("#ctl00_Site_ContentPlaceHolder_tb_email").val();
    var _provider = $("#ctl00_Site_ContentPlaceHolder_tb_providername").val();
    var _realname = $("#ctl00_Site_ContentPlaceHolder_tb_realname").val();
    var _tel = $("#ctl00_Site_ContentPlaceHolder_tb_tel").val();
    var _mob = $("#ctl00_Site_ContentPlaceHolder_tb_mobile").val();
    var _qq = $("#ctl00_Site_ContentPlaceHolder_tb_qq").val();
    var _msn = $("#ctl00_Site_ContentPlaceHolder_tb_msn").val();
    var _payee = $("#ctl00_Site_ContentPlaceHolder_tb_payeename").val();
    var _bank1 = $("#ctl00_Site_ContentPlaceHolder_tb_bankaccount").val();
    var _bank2 = $("#ctl00_Site_ContentPlaceHolder_tb_bankaccount_repeat").val();
    var _alipayname = $("#ctl00_Site_ContentPlaceHolder_tb_alipayname").val();
    var _alipay1 = $("#ctl00_Site_ContentPlaceHolder_tb_alipayaccount").val();
    var _alipay2 = $("#ctl00_Site_ContentPlaceHolder_tb_alipayaccount_repeat").val();
    var _99billname = $("#ctl00_Site_ContentPlaceHolder_tb_99billname").val();
    var _99bill1 = $("#ctl00_Site_ContentPlaceHolder_tb_99bill_account").val();
    var _99bill2 = $("#ctl00_Site_ContentPlaceHolder_tb_99bill_account_repeat").val();

    $("#ctl00_Site_ContentPlaceHolder_span_reg_tips").hide();

    if (check_length(_username, 4) == -1) {
        show_tips_reg("请正确输入用户名！");
        return false;
    }

    if (check_length(_pwd1, 6) == -1) {
        show_tips_reg("密码长度必须不少于6个字符！");
        return false;
    }

    if (check_compare(_pwd1, _pwd2) == -1) {
        show_tips_reg("两次密码输入不一致，请重新输入！");
        return false;
    }

    if (check_length(_email, 6) == -1) {
        show_tips_reg("请正确输入电子邮件地址！");
        return false;
    }

    if (check_length(_provider, 1) == -1) {
        show_tips_reg("请输入供货商名称！");
        return false;
    }

    if (check_length(_realname, 1) == -1) {
        show_tips_reg("请输入真实姓名！");
        return false;
    }

    if (check_length(_tel, 6) == -1) {
        show_tips_reg("请正确输入联系电话！");
        return false;
    }

    if (check_length(_mob, 11) == -1) {
        show_tips_reg("请正确输入手机号码！");
        return false;
    }

    if ((check_length(_qq, 1) == -1) && (check_length(_msn, 1) == -1))  {
        show_tips_reg("腾讯QQ与MSN账户必须至少填写一个！");
        return false;
    }

    if ((check_length(_qq, 5) == -1) && (check_length(_msn, 1) == -1)) {
        show_tips_reg("请正确输入QQ号！");
        return false;
    }

    if ((check_length(_qq, 1) == -1) && (check_length(_msn, 5) == -1)) {
        show_tips_reg("请正确输入MSN地址！");
        return false;
    }

    var _bankSelect;
    var _alipaySelect;
    var _99billSelect;

    if ((check_length(_payee, 1) == -1) || (check_length(_bank1, 1) == -1)) {
        _bankSelect = 0;
    }
    else {
        _bankSelect = 1;
    }

    if ((check_length(_alipayname, 1) == -1) || (check_length(_alipay1, 1) == -1)) {
        _alipaySelect = 0;
    }
    else {
        _alipaySelect = 1;
    }

    if ((check_length(_99billname, 1) == -1) || (check_length(_99bill1, 1) == -1)) {
        _99billSelect = 0;
    }
    else {
        _99billSelect = 1;
    }

    if ((_bankSelect == 0) && (_alipaySelect == 0) && (_99billSelect == 0)) {
        show_tips_reg("	银行、支付宝或快钱账号必须至少填写一项！");
        return false;
    }

    if (_bankSelect == 1) {
        if (check_length(_bank1, 15) == -1) {
            show_tips_reg("请正确输入银行账号！");
            return false;
        }

        if (check_compare(_bank1, _bank2) == -1) {
            show_tips_reg("两次银行账号输入不一致，请重新输入！");
            return false;
        }
    }

    if (_alipaySelect == 1) {
        if (check_compare(_alipay1, _alipay2) == -1) {
            show_tips_reg("两次支付宝账号输入不一致，请重新输入！");
            return false;
        }
    }

    if (_99billSelect == 1) {
        if (check_compare(_99bill1, _99bill2) == -1) {
            show_tips_reg("两次快钱账号输入不一致，请重新输入！");
            return false;
        }
    }

    return true;
}

function user_details_update_validate() {

    var _provider = $("#ctl00_Site_ContentPlaceHolder_tb_providername").val();
    var _realname = $("#ctl00_Site_ContentPlaceHolder_tb_realname").val();
    var _tel = $("#ctl00_Site_ContentPlaceHolder_tb_tel").val();
    var _mob = $("#ctl00_Site_ContentPlaceHolder_tb_mobile").val();
    var _qq = $("#ctl00_Site_ContentPlaceHolder_tb_qq").val();
    var _msn = $("#ctl00_Site_ContentPlaceHolder_tb_msn").val();

    if (check_length(_provider, 1) == -1) {
        show_tips_profile("请输入供货商名称！");
        return false;
    }

    if (check_length(_realname, 1) == -1) {
        show_tips_profile("请输入真实姓名！");
        return false;
    }

    if (check_length(_tel, 6) == -1) {
        show_tips_profile("请正确输入联系电话！");
        return false;
    }

    if (check_length(_mob, 11) == -1) {
        show_tips_profile("请正确输入手机号码！");
        return false;
    }

    if ((check_length(_qq, 1) == -1) && (check_length(_msn, 1) == -1)) {
        show_tips_profile("腾讯QQ与MSN账户必须至少填写一个！");
        return false;
    }

    if ((check_length(_qq, 5) == -1) && (check_length(_msn, 1) == -1)) {
        show_tips_profile("请正确输入QQ号！");
        return false;
    }

    if ((check_length(_qq, 1) == -1) && (check_length(_msn, 5) == -1)) {
        show_tips_profile("请正确输入MSN地址！");
        return false;
    }

    return true;
}

function place_order_validate() {
    var _radio0 = $("#ctl00_Site_ContentPlaceHolder_rb_bank").attr("checked");
    var _radio1 = $("#ctl00_Site_ContentPlaceHolder_rb_alipay").attr("checked");
    var _radio2 = $("#ctl00_Site_ContentPlaceHolder_rb_99bill").attr("checked");

    var _amount = $("#ctl00_Site_ContentPlaceHolder_tb_amount").val();
    var _amountMinStr=$("#ctl00_Site_ContentPlaceHolder_lb_amount_min").html();
    var _amountMaxStr = $("#ctl00_Site_ContentPlaceHolder_lb_amount_max").html();
    var _amountMin= parseInt($("#ctl00_Site_ContentPlaceHolder_hf_amount_min").val());
    var _amountMax = parseInt( $("#ctl00_Site_ContentPlaceHolder_hf_amount_max").val());

    var _roleName = $("#ctl00_Site_ContentPlaceHolder_tb_rolename").val();
    var _tradeTime = $("#ctl00_Site_ContentPlaceHolder_tb_trade_time").val();

    if (_radio0 != "checked" && _radio1 != "checked" && _radio2 != "checked") {
        show_tips_profile("请选择您的结算方式！");
        return false;
    }

    if (check_length(_amount, 1) == -1) {
        show_tips_profile("请输入供货量！");
        return false;
    }
    else {
        _amount = parseInt(_amount);
    }

    if (_amount < _amountMin) {
        show_tips_profile("供货量必须大于每笔交易最低限制（"+_amountMinStr+"）！");
        return false;
    }

    if (_amount > _amountMax) {
        show_tips_profile("供货量必须小于每笔交易最高限制（" + _amountMaxStr + "）！");
        return false;
    }

    if (check_length(_roleName, 1) == -1) {
        show_tips_profile("请输入游戏角色名称！");
        return false;
    }

    return true;
}



function show_tips_reg(_str) {
    $("#ctl00_Site_ContentPlaceHolder_span_reg_tips").show();
    $("#ctl00_Site_ContentPlaceHolder_span_reg_tips").html(_str);
    window.scrollTo(0, 0);
}

function show_tips_profile(_str) {
    $("#ctl00_Site_ContentPlaceHolder_span_profile_tips").show();
    $("#ctl00_Site_ContentPlaceHolder_span_profile_tips").html(_str);
}


function check_length(_str, _length) {
    if (_str.length >= _length) { return 1; }
    else return -1;
}

function check_compare(_str1, _str2) {
    if ((_str1 == _str2) && (_str1 != null) && (_str1 != "")) {
        return 1;
    }
    else return -1;
}

