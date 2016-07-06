$(function () {
    var linkurl = $("#linkurl").val();
    $('#qrcode').qrcode({
        width: 200,
        height: 200,
        correctLevel: QRErrorCorrectLevel.H,
        render: "table",
        text: linkurl

    });
});
/* ===== Navbar Search ===== */

$('#navbar-search > a').on('click', function () {
    $('#navbar-search > a > i').toggleClass('fa-search fa-times');
    $("#navbar-search-box").toggleClass('show hidden animated fadeInUp');
    return false;
});

/*===== Pricing Bonus ===== */

$('#bonus .pricing-number > .fa-scissors').on('click', function () {
    $(this).css('left', '100%');    /* Cutting */
    setTimeout(function () {          /* Removing the scissors */
        $('#bonus .pricing-number > .fa-scissors').addClass('hidden');
        $('#bonus .pricing-body ul').addClass('animated fadeOutDown');
    }, 2000);
    return false;
});

/* ===== Lost password form ===== */

$('.pwd-lost > .pwd-lost-q > a').on('click', function () {
    $(".pwd-lost > .pwd-lost-q").toggleClass("show hidden");
    $(".pwd-lost > .pwd-lost-f").toggleClass("hidden show animated fadeIn");
    return false;
});

/* ===== Thumbs rating ===== */

$('.rating .voteup').on('click', function () {
    var up = $(this).closest('div').find('.up');
    up.text(parseInt(up.text(), 10) + 1);
    return false;
});
$('.rating .votedown').on('click', function () {
    var down = $(this).closest('div').find('.down');
    down.text(parseInt(down.text(), 10) + 1);
    return false;
});

/* ===== Responsive Showcase ===== */

$('.responsive-showcase ul > li > i').on('click', function () {
    var device = $(this).data('device');
    $('.responsive-showcase ul > li > i').addClass("inactive");
    $(this).removeClass("inactive");
    $('.responsive-showcase img').removeClass("show");
    $('.responsive-showcase img').addClass("hidden");
    $('.responsive-showcase img' + device).toggleClass("hidden show");
    $('.responsive-showcase img' + device).addClass("animated fadeIn");
    return false;
});
/*=======register page===========*/
$("#redrpprovince").change(function () {
    var provincename = $("#redrpprovince").find("option:selected").text();
    var provinceid = $("#redrpprovince").val();
    webchang(provinceid, 'redrpcity');
    $("#member_Province").val(provincename);
});
$("#redrpcity").change(function () {
    var cityname = $("#redrpcity").find("option:selected").text();
    var cityid = $("#redrpcity").val();
    webchang(cityid, 'redrparea');
    $("#member_City").val(cityname);
});
$("#redrparea").change(function () {
    var areaname = $("#redrparea").find("option:selected").text();
    var areaid = $("#redrparea").val();
    $("#member_Area").val(areaname);
});
function webchang(id, control) {
    $.ajax({
        url: '/public/obtainreagin',
        dataType: 'Json',
        data: { 'id': id },
        type: 'POST',
        success: function (data) {
            var optionstr = '<option value="0"></option>';
            $.each(data, function (i, item) {
                optionstr = optionstr + '<option value="' + item.REGION_ID + '">' + item.REGION_NAME + '</option>';
            });
            $("#" + control).html('');
            $("#" + control).html(optionstr);
            if (control == 'redrpcity') {
                $("#member_City").val('');
                $("#redrparea").html('<option value="0"></option>');
                $("#member_Area").val('');
            }
        }
    });
}
function subchange() {
    document.getElementById('registerform').submit();
    return false;
}
function changlogpwd() {
    if (confirm("即将修改登陆密码，确认继续？")) {
        var pwd = $("#mynewpwd").val();
        $.ajax({
            url: '/WebFrontArea/WebHome/updatepwd',
            dataType: 'Json',
            data: { 'pwd': pwd },
            type: 'POST',
            success: function (data) {
                if (data == "1") {
                    bootbox.alert("更改成功");
                }
                else {
                    bootbox.alert("更改失败")
                }
            }
        });
    }
}
/*=======mycapital page===========*/
$("#chkweixin").change(function () {
    var weixin = $(this).val();
    var check = $(this).is(':checked');
    var paytype = $("#paytype").val();
    if (check) {
        paytype = weixin + "," + paytype;
    }
    else {
        paytype = paytype.replace(weixin + ",", "");
    }
    $("#paytype").val(paytype);
});
$("#chkalipay").change(function () {
    var alipay = $(this).val();
    var check = $(this).is(':checked');
    var paytype = $("#paytype").val();
    if (check) {
        paytype = paytype + alipay + ",";
    }
    else {
        paytype = paytype.replace(alipay + ",", "");
    }
    $("#paytype").val(paytype);
});
$("#achkweixin").change(function () {
    var alipay = $(this).val();
    var check = $(this).is(':checked');
    var paytype = $("#apaytype").val();
    if (check) {
        paytype = paytype + alipay + ",";
    }
    else {
        paytype = paytype.replace(alipay + ",", "");
    }
    $("#apaytype").val(paytype);
});
$("#achkalipay").change(function () {
    var alipay = $(this).val();
    var check = $(this).is(':checked');
    var paytype = $("#apaytype").val();
    if (check) {
        paytype = paytype + alipay + ",";
    }
    else {
        paytype = paytype.replace(alipay + ",", "");
    }
    $("#apaytype").val(paytype);
});
$("input:radio[name=optionsRadios1]").change(function () {
    var raval = $("input:radio[name=optionsRadios1]:checked").val();
    $("#soucetype").val(raval);
    if (raval == 1) {
        $("#acceptamontenable").val($("#staticmoney").html());
    }
    else {
        $("#acceptamontenable").val($("#dymicmoney").html());
    }
});
function providehelp() {
    var helpmin = parseFloat($("#helpmin").html());//最少帮助金额
    var helpmax = parseFloat($("#helpmax").html());//最大帮助金额
    var helpamount = parseFloat($("#helpamount").val());//帮助金额
    var paytype = $("#paytype").val();
    var code = $("#helpactivecode").val();
    var lastmoney = parseFloat($("#lasthelp").val());//上次帮助金额
    if (code == '') {
        bootbox.alert("请填写排单币编号");
        return false;
    }
    if (paytype == '') {
        alert("请选择支付方式");
        return false;
    }
    if (helpamount < helpmin) {
        alert("提供的帮助金额小于平台规定值");
        return false;
    }
    if (helpamount > helpmax) {
        alert("提供的帮助金额大于平台规定值");
        return false;
    }
    if (helpamount < lastmoney) {
        alert("提供的金额小于上次的帮助金额");
        return false;
    }
    $.ajax({
        url: "/WebFrontArea/WebHome/addhelporder",
        datatype: "json",
        async: false,
        type: "POST",
        data: { 'helpactivecode': code, "helpamount": helpamount, "paytype": paytype },
        success: function (data) {
            if (data == '1') {
                location.reload();
            }
            else {
                bootbox.alert("操作失败");
            }
        }
    });
}
function accepthelp() {
    var accmin = parseFloat($("#accmin").html());//最少接受金额
    var accmax = parseFloat($("#accmax").html());//最大接受金额
    var accamount = parseFloat($("#acceptamont").val());//接受金额
    var paytype = $("#apaytype").val();
    var soucetype = $("#soucetype").val();
    if (soucetype == '') {
        alert("请选择资金来源");
        return false;
    }
    if (paytype == '') {
        alert("请选择支付方式");
        return false;
    }
    if (accamount < helpmin) {
        alert("接受的帮助金额小于平台规定值");
        return false;
    }
    if (accamount > helpmax) {
        alert("接受的帮助金额大于平台规定值");
        return false;
    }
    $.ajax({
        url: '/WebFrontArea/WebHome/addacceptorder',
        dataType: 'Json',
        data: { 'soucetype': soucetype, "money": accamount, "paytype": paytype },
        type: 'POST',
        success: function (data) {
            if (data == '1') {
                location.reload();
            }
            else {
                alert("操作失败");
            }
        }
    });
}
function activemember() {
    var memberphone = $("#needactivemember").val();
    var activecode = $("#useactivecode").val();
    if (memberphone == '') {
        alert("请填写需要激活的会员电话");
        return false;
    }
    if (activecode == '') {
        alert("请使用的激活码");
        return false;
    }
    $.ajax({
        url: '/WebFrontArea/WebHome/activemember',
        dataType: 'Json',
        data: { 'memberphone': memberphone, "code": activecode },
        type: 'POST',
        success: function (data) {
            if (data == '1') {
                location.reload();
            }
            else {
                $("#activemsg").html(data);
            }
        }
    });
}
function activegive() {
    var memberphone = $("#acceptcodemember").val();
    var count = $("#acceptcodecount").val();
    var typenum = $("#codetype").val();
    if (memberphone == '') {
        alert("请填写接受的会员电话");
        return false;
    }
    if (count == '') {
        alert("请输入个数");
        return false;
    }
    $.ajax({
        url: '/WebFrontArea/WebHome/MemberGife',
        dataType: 'Json',
        data: { 'phone': memberphone, "count": count, "type": typenum },
        type: 'POST',
        success: function (data) {
            if (data == '1') {
                location.reload();
            }
            else {
                $("#passmsg").html(data);
            }
        }
    });
}
