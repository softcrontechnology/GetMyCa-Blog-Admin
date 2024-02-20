function notification(message, type) {

    $.bootstrapGrowl(message, {
        type: type,
        align: 'right',
        width: 'auto',
        allow_dismiss: false
    });
};
function showDocumentAlert(message, type, pos) {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>danger!</h4> <p><b>' + message + '</b></p>', {
            type: type,
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showDocumentAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Attached Mandatory Documents!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function showMarkSuccess() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Mark Running Successfully!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });
}
function showProductionSubmitSuccess() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Production Planning Added Successfully!</b></p>', {
            type: 'success',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });
}


function CategoryCountToast() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Change Position of Other category  !</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function showImageAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Select Image !</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showDrawingImageAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Select Any Drawing Image !</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showPartyAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Select Party SaleOrder !</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showTimeAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>FromTime and to Time Can not be same !</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function AllreadyRegister() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Email Id Already Exist!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function showError() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please fill Mandatory field!</b></p>', {
            type: 'warning',
           
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showNoRecordFound() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>No Record Found!</b></p>', {
            type: 'warning',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function showErrorFillValue() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Fill Po Number!</b></p>', {
            type: 'warning',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showBookSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Appointment Booked Successfully!</b></p>', {
            type: 'success',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });

}
function showSuccess() {
   

    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Data Added Successfully!</b></p>', {
            type: 'success',
          
            delay: 2500,            
            allow_dismiss: true
        },2000);
      
        $(this).prop('disabled', true);
      
    });

}

function showPartySuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Party Added Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });

}

function showPurchaseSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Purchase Items Added Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });

}


function showUpdate() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

       
        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Data Updated Successfully!</b></p>', {
            type: 'success',
           
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });
}



function showEmployeeUpdate() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */


        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Employee Data Updated Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });
}



function showSaleOrderUpdate() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */


        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Sale Order Status Updated Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });
}

function showItemUpdate() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */


        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Item Data Updated Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });
}


function showPartyUpdate() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */


        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Party Data Updated Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });
}


function showTourUpdate() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */


        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Tour Data Updated Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });
}

function showAllreadyEmployee() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Employee  Already Exist!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


function showAlreadyUser() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>User  Already Exist!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


function showAllreadyTimeSheet() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Project Time Details Already Exist!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}



function showAlreadyTourMaster() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Tour Already Exist !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}



function showAlreadyItemMaster() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Item Already Exist !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


//function showAlreadyInProduction() {
//    $(document).ready(function () {
//        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

//        var growlType = $(this).data('growl');

//        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Item Already In Production. If You want to Stop, Click on Hold !</b></p>', {
//            type: 'danger',
//            delay: 2500,
//            allow_dismiss: true
//        }, 2000);

//        $(this).prop('disabled', true);
//    });

//}



function showDelete() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

      
        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Data Deleted Successfully!</b></p>', {
            type: 'danger',
           // align: 'center',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function showException() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

       
        $.bootstrapGrowl('<h4>Oops!</h4> <p><b>Something went Go Wrong!</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showAlreadyPartyMaster() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Party Already Exist !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showAlreadyPurchaseItemMaster() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b> Item Already Exist !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


function showAlreadySaleOrderItemMaster() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b> Item Already Exist !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


function showAlreadyDropdownMaster() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>DropdownType Already Exist !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    }); 

}
function showAlreadySupplierMaster() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Supplier Already Exist !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    }); 

}

function showAddMenuSuccess() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Menu Successfully Added.</b></p>', {
            type: 'success',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showUpdateMenuSuccess() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Menu Master Successfully Updated.</b></p>', {
            type: 'success',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


function showAlertDropDownType() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Select Dropdown Type!</b></p>', {
            type: 'danger',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}


//Production Start//


function showProductionSubmitSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Plan Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });

}
function showErrorSheet() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */


        $.bootstrapGrowl('<h4>Oops!</h4> <p><b>Invalid Input Value!</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function showCuttingDetailError() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Oops!</h4> <p><b>Please Fill Details Proper!</b></p>', {
            type: 'danger',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}

function showTemperingProductionTimeError() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Oops!</h4> <p><b>Please Fill Production Start & End Time!</b></p>', {
            type: 'danger',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}

function showItemValidationAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Soory You can not select Items more then received Item Quantity !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function showItemQuantityValidationAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Fill Proper Items Quantity like ( Breakage, Reject and Ok Pcs Quantity) !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}
function showAlreadyInProduction() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Item Already In Production. If You want to Stop, Click on Hold !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showNotPermission() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Item Production Currently On Hold !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showShiftAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Select  Production Shift !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}

function showPlannedDateAlert() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>Please Select  Production Planned Date !</b></p>', {
            type: 'danger',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


function showStoreProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Store Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showStoreProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Store Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}

function showPackagingProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Packaging Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showPackagingProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Packaging Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}

function showTemperingProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Tempering Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showTemperingProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Tempering Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}

function showDfgPrintProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>DFG Print Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showDfgPrintProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>DFG Print Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}

function showPaintProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Paint Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showPaintProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Paint Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}


function showWashingProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Washing Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showWashingProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Washing Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}


function showHoleProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Hole Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showHoleProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Hole Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}


function showWashingOneProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Washing One Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showWashingOneProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Washing One Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showGrindingProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Grinding Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showGrindingProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Grinding Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}


function showCuttingProcessStart() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Cutting Production Start Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}
function showCuttingProcessSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Cutting Production Submitted Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}

//Production End//

// Drawing Start//

function showDrawingSuccess() {


    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */
        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Success!</h4> <p><b>Drawing Added Successfully!</b></p>', {
            type: 'success',

            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);

    });
}


function showDrawingAlready() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>This Drawing Details Already Exist!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


function showDrawingPngJpg() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>image should be png or jpg!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}


function showDrawingImageSize() {
    $(document).ready(function () {
        /* Grawl Notifications with Bootstrap-growl plugin, check out more examples at http://ifightcrime.github.io/bootstrap-growl/ */

        var growlType = $(this).data('growl');

        $.bootstrapGrowl('<h4>Warning!</h4> <p><b>image Size should be 1 MB!</b></p>', {
            type: 'warning',
            delay: 2500,
            allow_dismiss: true
        }, 2000);

        $(this).prop('disabled', true);
    });

}




   
