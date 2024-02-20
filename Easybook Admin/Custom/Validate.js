





//function validateSubmission() {

//    $(".form-control").removeClass("mustFill");


//    $('input[type=text].required').each(function () {

//        if ($(this).val() == "") {
//            $(this).addClass("mustFill");
//        }
//    })

//    $('.select2.required').each(function () {
      
//        var id = $(this).attr('id') + "_chosen";
//        if ($(this).val() == "Select") {
//            $('#' + id).css({ "border": "1px solid red", "margin-bottom": "0px" });
//    }
//    })
//    $('.chosen-container.chosen-container-single').each(function () {

//        var id = $(this).attr('id') + "_chosen";
//        if ($(this).val() == "Select") {
//            $('#' + id).css({ "border": "1px solid red", "margin-bottom": "0px" });
//        }
//    })
    
//    //$('textarea.required').each(function () {

//    //    if ($(this).val() == "") {
//    //        $(this).addClass("mustFill");
//    //    }
//    //})
//    //$('select.required').each(function () {

//    //    if ($(this).val() == "") {
//    //        $(this).addClass("mustFill");
//    //    }
//    //})
//    //$('input[type=file].required').each(function () {

//    //    if ($(this).val() == "") {
//    //        $(this).addClass("mustFill");
//    //    }
//    //})

//    //$('html,body').animate({ scrollTop: $('input[type=file].required').eq(0).offset().top - 100 });


//    if ($('input[type=text].required').length > 0) {
//        $('html,body').animate({ scrollTop: $('input[type=text].mustFill').eq(0).offset().top - 100 });
//        return false;
//    }
//    return true;

//}



function validateUserinfo() {

    if ($('#pagecontent_txtUserFirstName').val().length < 3 || $('#pagecontent_txtUserFirstName').val().length > 16) {
        $('#pagecontent_txtUserFirstName').focus();
        $('#pagecontent_txtUserFirstName').addClass("is-invalid");
        return false;
    }
    else { $('#pagecontent_txtUserFirstName').removeClass("is-invalid"); }

    if ($('#pagecontent_txtUserPhoneNumber').val().length < 10 || $('#pagecontent_txtUserPhoneNumber').val().length > 10) {
        $('#pagecontent_txtUserPhoneNumber').focus();
        $('#pagecontent_txtUserPhoneNumber').addClass("is-invalid");
        return false;
    }
    else { $('#pagecontent_txtUserPhoneNumber').removeClass("is-invalid"); }


    if ($('#pagecontent_txtEmailid').val().length < 8 || $('#pagecontent_txtEmailid').val().length > 50) {
        $('#pagecontent_txtEmailid').focus();
        $('#pagecontent_txtEmailid').addClass("is-invalid");
        return false;
    }
    else { $('#pagecontent_txtEmailid').removeClass("is-invalid"); }

    if ($('#pagecontent_txtPassword').val().length < 8 || $('#pagecontent_txtPassword').val().length > 10) {
        $('#pagecontent_txtPassword').focus();
        $('#pagecontent_txtPassword').addClass("is-invalid");
        return false;
    }
    else { $('#pagecontent_txtPassword').removeClass("is-invalid"); }


    return true;
}

function Number(e) {
    isIE = document.all ? 1 : 0
    keyEntry = !isIE ? e.which : event.keyCode;
    if (((keyEntry >= '48') && (keyEntry <= '57')))
        return true;
    else {
        return false;
    }
}

function character(e) {
    isIE = document.all ? 1 : 0
    keyEntry = !isIE ? e.which : event.keyCode;
    if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || keyEntry == '44')
        return true;
    else {
        return false;
    }
}

function character_and_Number(e) {
    isIE = document.all ? 1 : 0
    keyEntry = !isIE ? e.which : event.keyCode;
    if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '8') || (keyEntry == '32') || (keyEntry == '45') || (keyEntry == '95') || (keyEntry == '46') || ((keyEntry >= '48') && (keyEntry <= '57')))
        return true;
    else {
        return false;
    }
}

function DateValidation(e) {
    isIE = document.all ? 1 : 0
    keyEntry = !isIE ? e.which : event.keyCode;
    if (((keyEntry >= '48') && (keyEntry <= '57') && keyEntry == '47'))
        return true;
    else {
        return false;
    }
}

function isNumberKey(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        //Check if the text already contains the . character
        if (txt.value.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (charCode > 31 &&
            (charCode < 48 || charCode > 57))
            return false;
    }
    return true;
}
//$(document).ready(function () {
//    // allow only  Alphabets A-Z a-z _ and space
//    function Number(e) {
//        isIE = document.all ? 1 : 0
//        keyEntry = !isIE ? e.which : event.keyCode;
//        if (((keyEntry >= '48') && (keyEntry <= '57')))
//            return true;
//        else {
//            return false;
//        }
//    }
//});


//function validateDropDowninfo() {

//    if ($('#ContentPlaceHolder1_ddlDropdownType').val() == "Select") {
//        $('#ContentPlaceHolder1_ddlDropdownType').focus();
//        $('#ContentPlaceHolder1_ddlDropdownType').addClass("is-invalid");
//        return false;
//    } else { $('#ContentPlaceHolder1_ddlDropdownType').removeClass("is-invalid"); }

//    if ($('#ContentPlaceHolder1_txtDropdownItemName').val().length < 1) {
//        $('#ContentPlaceHolder1_txtDropdownItemName').focus();
//        $('#ContentPlaceHolder1_txtDropdownItemName').addClass("is-invalid");
//        return false;
//    }
//    else { $('#ContentPlaceHolder1_txtDropdownItemName').removeClass("is-invalid"); }

//    if ($('#ContentPlaceHolder1_txtDropdownItemAlias').val().length < 1) {
//        $('#ContentPlaceHolder1_txtDropdownItemAlias').focus();
//        $('#ContentPlaceHolder1_txtDropdownItemAlias').addClass("is-invalid");
//        return false;
//    }
//    else { $('#ContentPlaceHolder1_txtDropdownItemAlias').removeClass("is-invalid"); }

   
//    return true;
//}









//function validateLogininfo() {

//    if ($('#user').val().length < 1) {
//        $('#user').focus();
//        $('#user').addClass("is-invalid");
//        return false;
//    } else { $('#user').removeClass("is-invalid"); }


//    if ($('#password').val().length < 1) {
//        $('#password').focus();
//        $('#password').addClass("is-invalid");
//        return false;
//    }
//    else { $('#password').removeClass("is-invalid"); }


//    return true;
//}





//return (!((event.keyCode >= 65) && event.keyCode != 32) || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105));"





