var li = '<li><div style="float:none;margin:5px auto; width:100px;"><img src="/Content/Images/Main/loadingAnimation.gif" /></div></li>';
$(document).ready(function () {
    setInterval(function () {
        var currentdate = new Date();
        var datetime = "ساعت : " + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
        $('#spanTime').text(datetime);
    }, 1000)

    $('#refreshPage').click(function () {
        window.location.reload();
    });

    $(document).on('click', '#btnListContact', function () {
        var $this = $(this);
        $this.closest('div.btn-group').find('div.dropdown-menu > ul.media-list').empty().append(li);
        $.get($this.data('address'), null, function (data) {
            $this.closest('div.btn-group').find('div.dropdown-menu > ul.media-list').empty().append(data);
        });
    });

    $('body').on('click', '.modal-link', function (e) {
        e.preventDefault();
        $(this).attr('data-target', '#modal-container');
        $(this).attr('data-toggle', 'modal');
        $("#result").empty();
    });

    $('body').on('click', '.modal-close-btn', function () {
        $('#modal-container').modal('hide');
    });

    $('#modal-container').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    });


    $(".tile").height($("#tile1").width());
    $(".carousel").height($("#tile1").width());
    $(".item").height($("#tile1").width());


    $(window).resize(function () {
        if (this.resizeTO) clearTimeout(this.resizeTO);
        this.resizeTO = setTimeout(function () {
            $(this).trigger('resizeEnd');
        }, 10);
    });

    $(window).bind('resizeEnd', function () {
        $(".tile").height($("#tile1").width());
        $(".carousel").height($("#tile1").width());
        $(".item").height($("#tile1").width());
    });

});