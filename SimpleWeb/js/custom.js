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
    var name = $("#member_TruethName").val();
    var phone = $("#member_MobileNum").val();
    var weixin = $("#member_WeixinNum").val();
    var zhifubao = $("#member_AliPayNum").val();
    var area = $("#member_Area").val();
    var pwd = $("#pwd").val();
    var surepwd = $("#member_LogPwd").val();
    var checkv = $("#checkv").val();
    if (name == '') {
        bootbox.alert("������������ʵ������");
        return false;
    }
    if (phone == '') {
        bootbox.alert("����дע���ֻ��ţ�");
        return false;
    }    
    if (weixin == '') {
        bootbox.alert("����д΢����ϵ��ʽ��");
        return false;
    }
    if (zhifubao == '') {
        bootbox.alert("����д֧�����˻���");
        return false;
    }
    if (area == '') {
        bootbox.alert("��ѡ����������");
        return false;
    }
    if (pwd == '') {
        bootbox.alert("�����õ�½���룡");
        return false;
    }
    if (surepwd == '') {
        bootbox.alert("������ȷ�����룡");
        return false;
    }
    if (surepwd != pwd) {
        bootbox.alert("�����������벻һ�£�");
        return false;
    }
    if (checkv == "e")
    {
        bootbox.alert("�ֻ���֤�벻��ȷ����������֤��");
        return false;
    }
    var check = $("#agreementchk").is(':checked');
    if (!check) {
        bootbox.alert("����ͬ��ƽ̨Э���޷����ע��");
        return false;
    }
    document.getElementById('registerform').submit();
    return false;
}
function changlogpwd() {
    if (confirm("�����޸ĵ�½���룬ȷ�ϼ�����")) {
        var pwd = $("#mynewpwd").val();
        $.ajax({
            url: '/WebFrontArea/WebHome/updatepwd',
            dataType: 'Json',
            data: { 'pwd': pwd },
            type: 'POST',
            success: function (data) {
                if (data == "1") {
                    bootbox.alert("���ĳɹ�");
                }
                else {
                    bootbox.alert("����ʧ��")
                }
            }
        });
    }
}
function send() {
    var phone = $("#member_MobileNum").val();
    if (phone.length != 11) {
        bootbox.alert("������ֻ��Ų��Ϸ�");
        return false;
    }
    $.ajax({
        url: '/WebFrontArea/Register/sendsms',
        dataType: 'Json',
        data: { 'phone': phone },
        type: 'POST',
        success: function (data) {
            if (data == "0") {
                bootbox.alert("���ŷ���ʧ�ܣ�������");
            }
            else {
                $("#vid").val(data);
                $("#resultmsg").html("���ͳɹ������:" + data);
            }
        }
    });
}
function checkvcode() {
    var id = $("#vid").val();
    var code = $("#verifivationcode").val();
    if (code.length != 6) {
        return false;
    }
    $.ajax({
        url: '/WebFrontArea/Register/checkverification',
        dataType: 'Json',
        data: { 'id': id, 'code': code },
        type: 'POST',
        success: function (data) {
            if (data == "0") {
                $("#resultmsg").html("��֤�벻��ȷ");
                $("#checkv").val("e");
            }
            else {
                $("#resultmsg").html("��֤��ͨ��");
                $("#checkv").val("s");
            }
        }
    });
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
    var helpmin = parseFloat($("#helpmin").html());//���ٰ������
    var helpmax = parseFloat($("#helpmax").html());//���������
    var helpamount = parseFloat($("#helpamount").val());//�������
    var paytype = $("#paytype").val();
    var code = $("#helpactivecode").val();
    var lastmoney = parseFloat($("#lasthelp").val());//�ϴΰ������
    if (code == '') {
        bootbox.alert("����д�ŵ��ұ��");
        return false;
    }
    if (paytype == '') {
        alert("��ѡ��֧����ʽ");
        return false;
    }
    if (helpamount < helpmin) {
        alert("�ṩ�İ������С��ƽ̨�涨ֵ");
        return false;
    }
    if (helpamount > helpmax) {
        alert("�ṩ�İ���������ƽ̨�涨ֵ");
        return false;
    }
    if (helpamount < lastmoney) {
        alert("�ṩ�Ľ��С���ϴεİ������");
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
                bootbox.alert(data);
            }
        }
    });
}
function accepthelp() {
    var accmin = parseFloat($("#accmin").html());//���ٽ��ܽ��
    var accmax = parseFloat($("#accmax").html());//�����ܽ��
    var accamount = parseFloat($("#acceptamont").val());//���ܽ��
    var paytype = $("#apaytype").val();
    var soucetype = $("#soucetype").val();
    if (soucetype == '') {
        alert("��ѡ���ʽ���Դ");
        return false;
    }
    if (paytype == '') {
        alert("��ѡ��֧����ʽ");
        return false;
    }
    if (accamount < helpmin) {
        alert("���ܵİ������С��ƽ̨�涨ֵ");
        return false;
    }
    if (accamount > helpmax) {
        alert("���ܵİ���������ƽ̨�涨ֵ");
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
                bootbox.alert(data);
            }
        }
    });
}
function activemember() {
    var memberphone = $("#needactivemember").val();
    var activecode = $("#useactivecode").val();
    if (memberphone == '') {
        alert("����д��Ҫ����Ļ�Ա�绰");
        return false;
    }
    if (activecode == '') {
        alert("��ʹ�õļ�����");
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
        alert("����д���ܵĻ�Ա�绰");
        return false;
    }
    if (count == '') {
        alert("���������");
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
function updatehelporder(id) {
    $.ajax({
        url: '/WebFrontArea/WebHome/paymoney',
        dataType: 'Json',
        data: { 'id': id },
        type: 'POST',
        success: function (data) {
            if (data == '1') {
                location.reload();
            }
            else {
                bootbox.alert("����ʧ��")
            }
        }
    });
}
function updateacceptorder(id) {
    $.ajax({
        url: '/WebFrontArea/WebHome/finishorder',
        dataType: 'Json',
        data: { 'id': id },
        type: 'POST',
        success: function (data) {
            if (data == '1') {
                location.reload();
            }
            else {
                bootbox.alert("����ʧ��")
            }
        }
    });
}
function updatesingleorder(aid, hid) {
    $.ajax({
        url: '/WebFrontArea/WebHome/singlefinishorder',
        dataType: 'Json',
        data: { 'aid': aid, 'hid': hid },
        type: 'POST',
        success: function (data) {
            if (data == '1') {
                location.reload();
            }
            else {
                bootbox.alert("����ʧ��")
            }
        }
    });
}
