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
        url: '/AdminArea/MemberOpera/obtainreagin',
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
        var pwd = $("#newpwd").val();
        $.ajax({
            url: '/WebFrontArea/WebHome/updatepwd',
            dataType: 'Json',
            data: { 'pwd': pwd },
            type: 'POST',
            success: function (data) {
                if (data == "1")
                {
                    alert("更改成功");
                }
            }
        });
    }
}
