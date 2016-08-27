
var isActive = false;

$(document).ready(function () {
    $("#ProductCompanyShow #textSubCategory").add("#ProductCompanyShow #dropDownListSubCategory").add("#ProductCompanyShow #moreInfoButton").add("#ProductCompanyShow #tablePackage").hide();
    jQuery("#ProductCompanyShow #CategoryDropdown").change(function () {

        $("#ProductCompanyShow #divLoadingsubCategory").css({ 'display': 'block' });
        $("#ProductCompanyShow #Category_IdRoot").val($(this).find('option:selected').attr('value'));

        $('#ProductCompanyShow #MoreInfoDiv').empty();
        $("#ProductCompanyShow #moreInfoButton").hide();

        $("#ProductCompanyShow #textSubCategory").add("#ProductCompanyShow #dropDownListSubCategory").hide();

        jQuery.getJSON($(this).data('address'), { Id: $("#ProductCompanyShow #Category_IdRoot").val() }, function (data) {
            $("#SubCategoryDropdown").empty();
            if (data.length > 0) {
                $("#ProductCompanyShow #SubCategoryDropdown").append($('<option></option>').attr("value", "0").text("انتخاب تکنولوژی توسعه"));
                $("#ProductCompanyShow #textSubCategory").add("#ProductCompanyShow #dropDownListSubCategory").show();
            }
            else {
                $("#ProductCompanyShow #divLoadingsubCategory").css({ 'display': 'none' });
                NoMoreInfo("گروه انتخابی شما هیچ گونه تکنولوژی توسعه ای ندارد");
            }
            jQuery.each(data, function (i) {
                var option = $('<option></option>').attr("value", data[i].Id).text(data[i].Name);
                $("#ProductCompanyShow #SubCategoryDropdown").append(option);
            });
            $("#ProductCompanyShow #divLoadingsubCategory").css({ 'display': 'none' });
        }).error(function () {
            $("#ProductCompanyShow #divLoadingsubCategory").css({ 'display': 'none' });
            HandelError();
        });

    });

    jQuery("#ProductCompanyShow #SubCategoryDropdown").change(function () {
        var categoryId = $(this).find('option:selected').attr('value');
        $("#ProductCompanyShow #Category_Id").val(categoryId);
        $("#ProductCompanyShow #moreInfoButton").show();
        $("#ProductCompanyShow #MoreInfoDiv").empty();
        $("#ProductCompanyShow #PageNumber").val("0");

        if (!isActive)
            $("#ProductCompanyShow #moreInfoButton").detach().prependTo("#ProductCompanyShow #divSearch")
                .html("<span class=\"glyphicon glyphicon-search\"></span>");
        isActive = true;

    });
    $(function () {

        $.get($('#containerDetailsProductBase #ContainerComment').data('addresscomment'), { id: $('#containerDetailsProductBase #ContainerComment').data('productid') }, function (data) {
            jQuery('#containerDetailsProductBase #ContainerComment').append(data);
        })
        var heightShoping = $('#SearchAndShoping').height();
        var heightNavbar = $('.navbar-Top-User').height();
        $(document).scroll(function () {
            if ($('#directPage li:eq(0)').offset().top > $('#ContainerDetailProduct').height() - 135)
                $('#directPage ul.affix').hide();

            if ($('#directPage li:eq(0)').offset().top < $('#ContainerDetailProduct').height() - 135)
                $('#directPage ul.affix').show();

            if ($(document).scrollTop() > (heightShoping + heightNavbar) - 45) {
                $('#directPage ul.affix').css({ 'margin-top': '-90px' });
            }
            else {
                $('#directPage ul.affix').css({ 'margin-top': '0px' });
            }
        });


        $('.scrollToTop').click(function () {
            $('html, body').animate({ scrollTop: 0 }, 800);
            return false;
        });
        $('.alert-status').bootstrapSwitch('state', false);
        $('.alert-status').on('switchChange.bootstrapSwitch', function (event, state) {
        });

        $('#list').click(function () {
            $('.prod-box').animate({ opacity: 0 }, function () {
                $('.grid').removeClass('grid-active');
                $('.list').addClass('list-active');
                $('.prod-box').attr('class', 'prod-box-list col-sm-12 col-md-12 col-lg-12 padingBox');
                $('.prod-box-list').stop().animate({ opacity: 1 });
            });
        });

        $('#grid').click(function () {
            $('.prod-box-list').animate({ opacity: 0 }, function () {
                $('.list').removeClass('list-active');
                $('.grid').addClass('grid-active');
                $('.prod-box-list').attr('class', 'prod-box col-sm-6 col-md-4 col-lg-3 padingBox');
                $('.prod-box').stop().animate({ opacity: 1 });
            });
        });

    });
    $(document).on('click', '#containerDetailsProductBase #btnFavorite_', function () {
        $(this).closest('#containerDetailsProductBase div#containerStatusFavoriteUser').empty()
            .append("<img src=\"/Content/Images/Main/loadingAnimation.gif\" />");

        $.get($(this).data('addressfavorite'), { productId: $(this).data('productid') }, function (data) {
            $('#containerDetailsProductBase #containerStatusFavoriteUser').empty().append(data);
        });
    });

    //swiech Start



    //swiech End




});


$("#ProductCompanyShow #moreInfoButton").InfiniteScrollMain({
    moreInfoDiv: '#ProductCompanyShow #MoreInfoDiv',
    progressDiv: '#ProductCompanyShow #ProgressDiv',
    loadInfoUrl: $('#ProductCompanyShow #moreInfoButton').data('loadaddress'),
    loginUrl: '/login',
    errorHandler: function () {
        $("#ProductCompanyShow #moreInfoButton").hide();
        HandelError();
    },
    completeHandler: function () {
        $("#ProductCompanyShow #PageNumber").val(parseInt($("#ProductCompanyShow #PageNumber").val()) + 1);
        $("#ProductCompanyShow #tablePackage").add("#ProductCompanyShow #MoreInfoDiv").show();
        $("#ProductCompanyShow #tablePackage").show();
        if (isActive) {
            $("#ProductCompanyShow #moreInfoButton").detach().prependTo("#ProductCompanyShow #Destination")
                .addClass('col-md-9').addClass('btn-block').text("موارد بیشتر");
            isActive = false;
        }
    },
    noMoreInfoHandler: function () {
        $("#ProductCompanyShow #moreInfoButton").hide();
    }
});

function HandelError() {
    swal("برنامه در بازیابی اطلاعات دچار مشکل شده است", "صفحه را مجدد بار گذاری نمایید ! با تشکر");
}
function NoMoreInfo(falt) {
    swal(falt);
}


function generate(type, text) {
    var n = noty({
        text: text,
        type: type,
        dismissQueue: true,
        layout: 'topLeft',
        closeWith: ['click'],
        theme: 'relax',
        maxVisible: 10,
        animation: {
            open: 'animated bounceInLeft',
            close: 'animated bounceOutLeft',
            easing: 'swing',
            speed: 500
        }
    });
    console.log('html: ' + n.options.id);
}

function generateAPI(type, text) {
    var n = noty({
        text: text,
        type: type,
        dismissQueue: false,
        layout: 'topCenter',
        theme: 'defaultTheme'
    });
    console.log(type + ' - ' + n.options.id);
    return n;
}
var result = '<div class="activity-item" style="padding:4px 8px;"><p style="text-align:center">سبد خرید</p><span class="glyphicon glyphicon-shopping-cart pull-right"></span><p class="pull-right">محصول با موفقیت به سبد خرید اضافه شد</p><div class="clearfix"></div><p style="text-align:left;"><span class="glyphicon glyphicon-eye-open"></span><span><a href="#" class="decoration_a">مشاهده سبد خرید</a></span></p></div>';
function generateAll() {
    generate('success', result);
}
function generateAPI_(type, text) {
    generateAPI(type, text);
};

function OnBeginAddToCart() {
    generateAPI_('information', '<div>ارسال اطلاعات به سرور</div>');
    setTimeout(function () {
        $.noty.closeAll();
    }, 4000);
};

function OnSuccessAddToCart(data) {
    $.noty.closeAll();
    if (data.Status != "duplicate") {
        generateAll();
        setTimeout(function () {
            $.noty.closeAll();
        }, 7000);
    } else {
        generateAPI_('information', '<div>این محصول قبلا به لیست خرید شما اضافه شده است</div>');
        setTimeout(function () {
            $.noty.closeAll();
        }, 4000);
    }
};

function OnFailureAddToCart() {
    $.noty.closeAll();
    generateAPI_('warning', '<div>در پردازش در خواست خطایی ایجاد شده است</div>');
    setTimeout(function () {
        $.noty.closeAll();
    }, 2500);
};







