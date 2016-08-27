var keepArrayIdCategory = [];
var keepArrayIdCompany = [];
function OnSuccess(data) {
    $('#ContainerCheckBoxCategoryCompany #result').fadeOut(500, function () {
        $(this).empty().append(data).fadeIn(1500);
    });
};
function OnComplete() {
    $('#ContainerCheckBoxCategoryCompany #divLoading').fadeOut(800);
};
function OnBegin() {
    $('#ContainerCheckBoxCategoryCompany #divLoading').show();
};
function disableRightLeft() {
    $('.disableLeft').add('.disableRight').closest('li').addClass('disable').children('a').css({ 'color': 'white' });
}
function disableLeft() {
    $('.disableLeft').closest('li').addClass('disable').children('a').css({ 'color': 'white' });

}
function disableRight() {
    $('.disableRight').closest('li').addClass('disable').children('a').css({ 'color': 'white' });
}
$(document).ready(function () {
    var term = "";
    var defaultNumberAtFirstLoad = $('#ContainerCheckBoxCategoryCompany').data('categoryid');

    var $CategoryTechnology = $('#ContainerCheckBoxCategoryCompany #CategoryTechnology ul li').find("input:checkBox");
    keepArrayIdCategory.push(defaultNumberAtFirstLoad);

    jQuery.each($CategoryTechnology, function () {
        var Id = $(this).attr('id').split('-')[1];
        if (defaultNumberAtFirstLoad == Id) {
            $("#ContainerCheckBoxCategoryCompany #CategoryTechnology ul li").find("input:checkBox#Category-" + Id).attr('checked', 'true');
            $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val(term);
            checkArrayCategory();
        }
    });

    var $CompanyTechnology = $('#ContainerCheckBoxCategoryCompany #CompanyTechnology ul li').find("input:checkBox");
    jQuery.each($CompanyTechnology, function () {
        var Id = $(this).attr('id').split('-')[1];
        $("#ContainerCheckBoxCategoryCompany #CompanyTechnology ul li").find("input:checkBox#Company-" + Id).attr('checked', 'true');
        keepArrayIdCompany.push(Id);
        $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val(term);
    });

    $('#ContainerCheckBoxCategoryCompany #divLoading').css({ 'display': 'block' });

    RunsMethodAJAX(keepArrayIdCategory.toString(), keepArrayIdCompany.toString(), $('#ContainerCheckBoxCategoryCompany #search option:selected').val(), $('#ContainerCheckBoxCategoryCompany #order option:selected').val(), term, $('#ContainerCheckBoxCategoryCompany').data('addresscategory'));

    $('#ContainerCheckBoxCategoryCompany #search').change(function () {
        checkArrayCategory();
        checkArrayCompany();
        $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val(term);
        RunsMethodAJAX(keepArrayIdCategory.toString(), keepArrayIdCompany.toString(), $(this).val(), $('#ContainerCheckBoxCategoryCompany #order option:selected').val(), term, $('#ContainerCheckBoxCategoryCompany').data('addresscategory'));

    });

    $('#ContainerCheckBoxCategoryCompany #order').change(function () {
        checkArrayCategory();
        checkArrayCompany();
        $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val(term);
        RunsMethodAJAX(keepArrayIdCategory.toString(), keepArrayIdCompany.toString(), $('#ContainerCheckBoxCategoryCompany #search option:selected').val(), $(this).val(), term, $('#ContainerCheckBoxCategoryCompany').data('addresscategory'));
    });

    $(document).on('change', '#ContainerCheckBoxCategoryCompany #CategoryTechnology ul li input:checkbox', function () {
        if ($(this).is(':checked')) {

            keepArrayIdCategory.push($(this).attr('id').split('-')[1]);
            $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val(term);
            checkArrayCategory();
            checkArrayCompany();
            RunsMethodAJAX(keepArrayIdCategory.toString(), keepArrayIdCompany.toString(), $('#ContainerCheckBoxCategoryCompany #search option:selected').val(), $('#ContainerCheckBoxCategoryCompany #order option:selected').val(), term, $('#ContainerCheckBoxCategoryCompany').data('addresscategory'));
        }
        else {
            var IdCategory = $(this).attr('id').split('-')[1];
            keepArrayIdCategory = jQuery.grep(keepArrayIdCategory, function (value) {
                return value != IdCategory;
            });
            $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val(term);
            checkArrayCategory();
            checkArrayCompany();
            RunsMethodAJAX(keepArrayIdCategory.toString(), keepArrayIdCompany.toString(), $('#ContainerCheckBoxCategoryCompany #search option:selected').val(), $('#ContainerCheckBoxCategoryCompany #order option:selected').val(), term, $('#ContainerCheckBoxCategoryCompany').data('addresscategory'));
        }
    });

    $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').bind('keypress', function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            term = $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val();
            checkArrayCategory();
            checkArrayCompany();
            RunsMethodAJAX(keepArrayIdCategory.toString(), keepArrayIdCompany.toString(), $('#ContainerCheckBoxCategoryCompany #search option:selected').val(), $('#ContainerCheckBoxCategoryCompany #order option:selected').val(), term, $('#ContainerCheckBoxCategoryCompany').data('addresscategory'));
        }
    });

    $(document).on('change', '#ContainerCheckBoxCategoryCompany #CompanyTechnology ul li input:checkbox', function () {
        if ($(this).is(':checked')) {
            keepArrayIdCompany.push($(this).attr('id').split('-')[1]);
            $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val(term);
            checkArrayCategory();
            checkArrayCompany();
            RunsMethodAJAX(keepArrayIdCategory.toString(), keepArrayIdCompany.toString(), $('#ContainerCheckBoxCategoryCompany #search option:selected').val(), $('#ContainerCheckBoxCategoryCompany #order option:selected').val(), term, $('#ContainerCheckBoxCategoryCompany').data('addresscategory'));
        }
        else {
            var IdCompany = $(this).attr('id').split('-')[1];
            keepArrayIdCompany = jQuery.grep(keepArrayIdCompany, function (value) {
                return value != IdCompany;
            });
            $('#ContainerCheckBoxCategoryCompany #txtSerachProduct').val(term);
            checkArrayCategory();
            checkArrayCompany();
            RunsMethodAJAX(keepArrayIdCategory.toString(), keepArrayIdCompany.toString(), $('#ContainerCheckBoxCategoryCompany #search option:selected').val(), $('#ContainerCheckBoxCategoryCompany #order option:selected').val(), term, $('#ContainerCheckBoxCategoryCompany').data('addresscategory'));
        }

    })

    function RunsMethodAJAX(arrayCategory, arrayCompany, search, order, term, urlAction) {
        StartLoadingRunsMethodAJAX();
        $.ajax({
            cache: false,
            type: "GET",
            url: urlAction,
            data: { arrayCategory: arrayCategory, arrayCompany: arrayCompany, search: search, order: order, term: term },
            success: function (data) {
                $('#ContainerCheckBoxCategoryCompany #result').fadeOut(250, function () {
                    $(this).empty().append(data).fadeIn(250);
                    EndLoadingRunsMethodAJAX();
                });
            },
            error: function (xhr, ajaxOptions, thrownError) {
                EndLoadingRunsMethodAJAX();
            }
        });
    };
    function checkArrayCategory() {
        if (keepArrayIdCategory.length > 0) { return false; }
        else {
            var checkBoxes = $("#ContainerCheckBoxCategoryCompany #CategoryTechnology ul li").find("input:checkBox");
            jQuery.each(checkBoxes, function (index) {
                $(this).attr('checked', 'true');
                keepArrayIdCategory[index] = $(this).attr('id').split('-')[1];
            });
        }
    };
    function checkArrayCompany() {
        if (keepArrayIdCompany.length > 0) { return false; }
        else {
            var checkBoxes = $("#ContainerCheckBoxCategoryCompany #CompanyTechnology ul li").find("input:checkBox");
            jQuery.each(checkBoxes, function (index) {
                $(this).attr('checked', 'true');
                keepArrayIdCompany[index] = $(this).attr('id').split('-')[1];
            });
        }
    };
    function StartLoadingRunsMethodAJAX() {
        $('#ContainerCheckBoxCategoryCompany #divLoading').show();
    };
    function EndLoadingRunsMethodAJAX() {
        $('#ContainerCheckBoxCategoryCompany #divLoading').hide();
    };
    //Start Section Switch
    $(function () {
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

    $(window).resize(function () {
        var viewportWidth = $(window).width();
        if (viewportWidth < 768) {
            $('.list').removeClass('list-active');
            $('.grid').addClass('grid-active');
            $('.prod-box-list').attr('class', 'prod-box col-sm-6 col-md-4 col-lg-3');
            $('.prod-box').stop().animate({ opacity: 1 });
        }
    });
    $('.aside-menu-item').hover(function () {
        $('.dropdown-menu.multi-level', this).fadeIn();
    }, function () {
        $('.dropdown-menu.multi-level', this).fadeOut('fast');
    });

    //End Section Switch
});