/// <reference path="jquery-2.1.1.min.js" />



function loadMemberShipView(url) {
    clearInterval(interval)
    $.get(url, function (data) {
        $("#Content").fadeOut(600, function () {
            $("#Content").html(data);
            $(".divLoading").css({ display: "none" });
            setTimeout(
                  function () {
                      $("#Content").fadeIn(2000);
                  }, 500);


            preperloadForm();
        });
    });
}
function preperloadForm() {
    var $form = $("#Content").find("form");
    $($form).find(":file").filestyle({ input: false });
    $($form).find(".selected").selectpicker();
    $form.unbind();
    $form.data("validator", null);
    $.validator.unobtrusive.parse(document);
};

jQuery(document).ready(function () {
    //caler item infile tag : Place :moadl form for insert image for text
    $("#calerImageUploaded").click(function () {
        var control = $("#controlUpload");
        clearImageUploaded(control);
    });
    //1
    $("#contaniermoreInfprmationTimes").hide();
    //2
    $("#moreInfprmationTimes").click(function (e) {
        e.preventDefault();
        $("#contaniermoreInfprmationTimes").fadeToggle(1000, function () {
            $("#contaniermoreInfprmationTimes input[type=text]").val("");


        })
    });
    //3
    $("#submitTimes").click(function (e) {
        e.preventDefault();

        var txtMinute = $(this).closest(".col-sm-1").siblings(".col-md-1").find("#txtMinutes").val();
        var txtHour = $(this).closest(".col-sm-1").siblings(".col-sm-1").find("#txtHour").val();


        if ((txtMinute != '') && (txtHour != '')) {
            $("#txtcontainerTimes").val($("#txtHour").val() + ":" + $("#txtMinutes").val() + ":00");
            $("#contaniermoreInfprmationTimes").hide();

        } else
            alert("برای ثبت حتما باید مقداری عددی را وارد نمایید");
    });


    //Preview Image Befor Upload With Details. 1, 2, 3
    //1
    $(".divbtnUploadImage #btnUploadImage").click(function () {

        $(this).closest('div.col-sm-4').find('div.containerFileUploadImg , div.spanBlock , div.divbtnUploadImage').
            css({ 'display': 'none' });
        var control = $(this).closest('div.col-sm-4').find('div.containerFileUpload :file');
        control.replaceWith(control = control.clone(true));

        $(this).closest('div.col-sm-4').find('div.containerFileUpload span:eq(2)').remove();
    });






});

//show item in modal form : Place :moadl form  preview image
function ShowPreviewImageUpload(input) {
    $('#impPrev').attr('src', "");
    if (input.files && input.files[0]) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $('#impPrev').attr('src', e.target.result).css
            ({ "width": "120px", "height": "120px", "float": "left" });
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
};

function clearImageUploaded(controlUpload) {
    var control = $("#controlUpload");
    control.replaceWith(control = control.clone(true));
    $('#impPrev').attr('src', "").css
    ({ "width": 0, "height": 0 });
    control.closest('.col-sm-2').find('span:eq(2)').text("");
};

//Create Alert Without Alert
function createAlert(alert, textAlert) {
    var result = "<div class=\"col-md-12\"><div class=\"alert alert-" + alert + "\">"
    + "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"buttonn\">&times;</button><p>" + textAlert + "</p></div></div>";
    $("#result").html(result);
}

//Create Alert With Return
function createAlert_return(alert, textAlert) {
    var result = "<div class=\"col-md-12\"><div class=\"alert alert-" + alert + "\">"
    + "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"buttonn\">&times;</button><p>" + textAlert + "</p></div></div>";
    return result;
}


//User Just Enter Press Number On KeyBord
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}




//Preview Image Befor Upload With Details. 1, 2, 3
//2
function ShowPreviewBackground(input) {
    if (input.files && input.files[0]) {
        readImage(input.files[0], input);
    }
};

//Preview Image Befor Upload With Details. 1, 2, 3
//3
function readImage(file, input) {
    var $this = input;
    var reader = new FileReader();
    var image = new Image();

    reader.readAsDataURL(file);
    reader.onload = function (_file) {
        image.src = _file.target.result;

        image.onload = function () {
            var w = this.width,
                    h = this.height,
                    t = file.type,
                    n = file.name,
                    wight = ~~(file.size / 1024) + ' KB';
            $($this).closest('div.col-sm-4').find('img').attr('src', this.src).css
                   ({ "width": 230, "height": 200, "float": "left" });
            $($this).closest('div.col-sm-4').find('span:eq(4)').
                text(w + " *" + h).
                css({ 'float': 'left' }).closest('div.col-sm-4').find('span:eq(6)').
                text(wight).
                css({ 'float': 'left' }).closest('div.col-sm-4').find('span:eq(8)').
                text(t).css({ 'float': 'left' });



            $($this).closest('div.col-sm-4').find("div.spanBlock , div.containerFileUploadImg , div.divbtnUploadImage")
                .css({ 'display': 'block' })
        };
        image.onerror = function () {
            alert('Invalid file type: ' + file.type);
        };
    };
};


