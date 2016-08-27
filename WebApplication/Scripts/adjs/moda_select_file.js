

jQuery(document).ready(function () {
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
    $(":file").filestyle({
        input: false
    });
    $(".selected").selectpicker();
});



