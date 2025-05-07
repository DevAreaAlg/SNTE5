function initializeJS() {

    //custom scrollbar
    //for html
    jQuery(".wrapper").niceScroll({ styler: "fb", cursorcolor: "#696462	", horizrailenabled: false, cursorwidth: '6', autohidemode: false, cursorborderradius: '10px', background: '#fff', cursorborder: '#696462', zindex: '1000' });
    jQuery('#sidebar').niceScroll({ styler: "fb", cursorcolor: "transparent", horizrailenabled: false, cursorwidth: '6', autohidemode: true, cursorborderradius: '10px', background: 'transparent', cursorborder: '#886F4F', zindex: '1000' });
    jQuery('.overflow_x').niceScroll({ styler: "fb", cursorcolor: "#696462	", horizrailenabled: false, cursorwidth: '6', autohidemode: true, cursorborderradius: '10px', background: 'transparent', cursorborder: '#A0935F', zindex: '1000', railpadding: { top: 0, right: 0, left: 5, bottom: 0 } });
    //sidebar dropdown menu
    jQuery('#sidebar .sub-menu > a').click(function () {

        var last = jQuery('.sub-menu.open', jQuery('#sidebar'));
        $(this).children('.menu-arrow').removeClass('arrow_carrot-right');
        jQuery('.sub', last).slideUp(200, function () { jQuery("#sidebar").getNiceScroll().resize(); });
        var sub = jQuery(this).next();
        if (sub.is(":visible")) {
            $(this).children('.menu-arrow').addClass('arrow_carrot-right');
            sub.slideUp(200, function () { jQuery("#sidebar").getNiceScroll().resize(); });
            $(this).css('background-color', '')
        } else {
            $(this).css('background-color', '#696462 !important')

            $(this).children('.menu-arrow').addClass('arrow_carrot-down');
            sub.slideDown(200, function () { jQuery("#sidebar").getNiceScroll().resize(); });
        }

    });




    $(document).ready(function () {
        if (window.location.pathname == "/" || window.location.pathname == "/NOTIFICACIONES.aspx") {
            jQuery('#sidebar > ul').is(":visible") === false;
            jQuery('#main-content').css({
                'margin-left': '225px'
            });
            jQuery('#sidebar > ul').show();
            jQuery('#sidebar').css({
                'margin-left': '10px'
            });
            jQuery("#container").removeClass("sidebar-closed");;
        }
        else {
            jQuery('#sidebar > ul').is(":visible") === true;
            jQuery('#main-content').css({
                'margin-left': '10px'
            });
            jQuery('#sidebar').css({
                'margin-left': '-210px'
            });
            jQuery('#sidebar > ul').hide();
            jQuery("#container").addClass("sidebar-closed");
        }
    });
    //// sidebar menu toggle
    //jQuery(function() {
    //    function responsiveView() {
    //        var wSize = jQuery(window).width();
    //        if (wSize <= 768) {
    //            jQuery('#container').addClass('sidebar-close');
    //            jQuery('#sidebar > ul').hide();
    //        }

    //        if (wSize > 768) {
    //            jQuery('#container').removeClass('sidebar-close');
    //            jQuery('#sidebar > ul').show();
    //        }
    //    }
    //    jQuery(window).on('load', responsiveView);
    //    jQuery(window).on('resize', responsiveView);
    //});

    jQuery(window).on('resize', responsiveView);
    var s = 1
    function responsiveView() {
        if ($(window).width() <= 768 && s == 1 && jQuery('#sidebar > ul').is(":visible")) {
            s = 2
            jQuery('.overflow_x').getNiceScroll().remove()

            jQuery(".wrapper").getNiceScroll().remove()
        } else if ((s == 2 && !jQuery('#sidebar > ul').is(":visible")) || ($(window).width() > 768)) {
            jQuery(".wrapper").niceScroll({ styler: "fb", cursorcolor: "#886F4F", horizrailenabled: false, cursorwidth: '6', autohidemode: false, cursorborderradius: '10px', background: '#fff', cursorborder: '#886F4F', zindex: '1000' });
            jQuery('.overflow_x').niceScroll({ styler: "fb", cursorcolor: "#886F4F", horizrailenabled: false, cursorwidth: '6', autohidemode: true, cursorborderradius: '10px', background: 'transparent', cursorborder: '#886F4F', zindex: '1000', railpadding: { top: 0, right: 0, left: 5, bottom: 0 } });
            s = 1
        }

    }

    responsiveView()
    $('.lbl_notif').click(function () {

        if ($(this).hasClass('cefecto-click-topbar')) {
            $(this).removeClass('cefecto-click-topbar');
            $(this).addClass('refecto-click-topbar');
        }
        else {
            $(this).removeClass('refecto-click-topbar');
            $(this).addClass('cefecto-click-topbar');
        }
    }
    );

    jQuery('.toggle-nav').click(function () {
        if (jQuery('#sidebar > ul').is(":visible") === true) {
            jQuery('#main-content').css({
                'margin-left': '10px'
            });
            jQuery('#sidebar').css({
                'margin-left': '-210px'
            });
            jQuery('#sidebar > ul').hide();
            jQuery("#container").addClass("sidebar-closed");

            $(this).children('.icon-reorder').removeClass('cefecto-click-topbar');
            $(this).children('.icon-reorder').addClass('refecto-click-topbar');
        } else {
            jQuery('#main-content').css({
                'margin-left': '225px'
            });
            jQuery('#sidebar > ul').show();
            jQuery('#sidebar').css({
                'margin-left': '10px'
            });
            jQuery("#container").removeClass("sidebar-closed");


            $(this).children('.icon-reorder').removeClass('refecto-click-topbar');
            $(this).children('.icon-reorder').addClass('cefecto-click-topbar');
        }
        responsiveView()
    });


    //bar chart
    if (jQuery(".custom-custom-bar-chart")) {
        jQuery(".bar").each(function () {
            var i = jQuery(this).find(".value").html();
            jQuery(this).find(".value").html("");
            jQuery(this).find(".value").animate({
                height: i
            }, 2000)
        })
    }
    //panel folder toogle

    $('.panel_header_folder').click(function (event) {
        var folder_content = $(this).siblings('.panel-body').children('.panel-body_content');
        var toogle = $(this).children('.panel_folder_toogle');
        if (toogle.hasClass('up')) {

            toogle.removeClass('up');
            toogle.addClass('down');
            folder_content.show('6666', function () { jQuery('.wrapper').getNiceScroll().resize(); });
            toogle.parent().css({ 'background': '#696462 !important', 'color': '#fff', 'border': 'solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px' });
        } else if (toogle.hasClass('down')) {
            toogle.removeClass('down');
            folder_content.hide('333', function () { jQuery('.wrapper').getNiceScroll().resize(); });
            toogle.addClass('up');
            toogle.parent().css({ 'background': '#696462 !important', 'color': 'inherit', 'border': 'solid 1px #c0cdd5', 'border-radius': '4px' });
        }

    });
}


$('.panel-body_content.init_show').parent().siblings('.panel_header_folder').css({ 'background': '#696462 !important', 'color': '#fff', 'border': ' solid 1px transparent', 'border-radius': ' 4px 4px 0px 0px' });

//estatus show panel


$('.estatus_link').click(function (event) {
    var panel_to_go = $(this).attr('href');
    var folder_content = $(panel_to_go).children('.panel-body').children('.panel-body_content');
    var folder_toogle_arrow = $(panel_to_go).children('.panel_header_folder').children('.panel_folder_toogle');
    if (folder_toogle_arrow.hasClass('up')) {

        folder_toogle_arrow.removeClass('up');
        folder_toogle_arrow.addClass('down');
        folder_content.show('333', null);

    }

    var page = $('html, body');
    page.on("scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove", function () {
        page.stop();
    });

    page.animate({ scrollTop: $(panel_to_go).position().top - 100 }, 1000, function () {
        page.off("scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove");
    });



});

//dropdown menu 

$('.dropdown-menu').children('li').children('a,span ').click(function (event) {
    var opcionSelected = $(this).html();
    var valor_dropdown = $(this).parent().parent().siblings(".dropdown_label").children('.dropdown_value');

    valor_dropdown.attr('Text', opcionSelected);
    valor_dropdown.html(opcionSelected);

});

// nav bar to fixed


/*----customm tree view ----*/


$('.nodeCross').click(function () {

    $(this).toggleClass("active");
    $(this).parent().next(".subDiv").toggleClass("active");
});







jQuery(document).ready(function () {
    initializeJS();
});