﻿var hidden_toast = function () {
    $('#toast-container').css('display', 'none');
};

var tooltip = function () {
    $("[data-toggle='tooltip']").tooltip();
};

var checkbox = function () {
    $(':checkbox').radiocheck();
    $(':radio').radiocheck();
};

var carousel = function () {
    $('.carousel').carousel();
};

var selectbox = function () {
    $('select').select2({ dropdownCssClass: 'dropdown-inverse' });
};

var tags = function () {
    $(".tagsinput").tagsinput();
};

var toastr = function () {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "positionClass": "toast-bottom-right",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
};

var toastrAutoClose = function () {
    if ($("#toast-container").length > 0) {
        if ($('#toast-container').css('display') == 'block') {
            setTimeout(function () {
                $('#toast-container').css('display', 'none');
            }, '3000')
        }
    }
};
var toastrShow = function (status, msg) {
    if ($("#toast-container").length > 0 && msg != '') {
        if ($('#toast-container').css('display') == 'none') {
            $('#toast-container').css('display', 'block');
            var msgclass = status == 1 ? 'toast-success' : (status == 0 ? 'toast-error' : (status == 3 ? 'toast-warning' : 'toast-info'));
            var html = '<div class="toast ' + msgclass + '"><div class="toast-message">' + msg + '</div></div>';
            $('#toast-container').html(html);

            setTimeout(function () {
                $('#toast-container').css('display', 'none');
            }, '3000')
        }
    }
};

var mCustomScrollbar = function () {
    if ($('.content').length > 0) {
        $('.content').mCustomScrollbar({
            scrollButtons: {
                enable: true
            }
        });
    }
};

var SmoothScroll = function () {
    // Enable Smooth Scroll only on Chrome and only on Win and Linux Systems
    var platform = navigator.platform.toLowerCase();
    if ((platform.indexOf('win') == 0 || platform.indexOf('linux') == 0) && !Modernizr.touch) {
        if ($.browser.webkit) {
            $.webkitSmoothScroll();
            // console.log("hello webkit");
        }
    }
};


var windowScrollDetection = function () {
    $(window).scroll(function () {
        if ($(document).scrollTop() <= 0) {
            MsgBox.Alert("滚动条已经到达顶部为0");
        }

        if ($(document).scrollTop() >= $(document).height() - $(window).height()) {
            MsgBox.Alert("滚动条已经到达底部为" + $(document).scrollTop());
        }
    });
}

var menuFocusDetection = function () {
    // 菜单定位
    var lis = $(".menu_list").find("a");
    var href = location.href;
    var flag = false;

    lis.each(function () {
        if (href.indexOf(this.href) > -1) {
            //$(this).parent().addClass("cur");
            $(this).children().addClass("cur");
            flag = true;
            return false;
        }
    });
    if (!flag) {
        lis.first().children().addClass("cur");
    }
}

var Plugins = function () {
    "use strict";
    return {
        init: function () {
            tooltip();
            checkbox();
            carousel();
            selectbox();
            tags();
            toastr();
            toastrAutoClose();
            //mCustomScrollbar();
            SmoothScroll();
            menuFocusDetection();
        }
    }
}();