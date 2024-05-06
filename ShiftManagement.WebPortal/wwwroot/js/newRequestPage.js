$(function () {

    /* edit_Value is defined in the chtml file NewRequest.cshtml to assign the ViewData value  */
    let divRequestNumber = $('#divRequestNumber');
    let divBtnSubmitNewRequest = $('#divBtnSubmitNewRequest');
    let divResponseDetails = $('#divResponseDetails');
    // if edit_Value = -1 then new request
    if (edit_Value == "-1") {
        // Displaying Request number Div
        divRequestNumber.addClass('d-none');
        // Hiding New request button Div
        divBtnSubmitNewRequest.removeClass('d-none');
        // Hiding Response Details Div
        divResponseDetails.addClass('d-none');
        //alert('New Request');
    }
    else {
        // Hiding Request number Div
        divRequestNumber.removeClass('d-none');
        // Displaying new request button Div
        divBtnSubmitNewRequest.addClass('d-none');
        // Hiding Response Details Div
        divResponseDetails.removeClass('d-none');
        //alert('Editing');

        loadDataForEditAndResponse()
    }


    $('#divBtnSubmitNewRequest').on('click', function () {
        if (newRequestIsValid()) {
           // alert('token:' + $('input[name="__RequestVerificationToken"]').val());
            var data = {                
                'receivedOnDate': $('#date').val(),
                'typeOfReqId': $('#inputTypeOfRequest').val(),
                'lldcCountry': $('#inlineRadioLldcYes').val(),
                'subProgrammeId': $('#inputSubprogramme').val(),
                'staffResponsibleId': $('#inputStaffResponsible').val(),
                'staffResponsible': $('#inputStaffResponsible').text(),
                'requestFrom': $('#inputFrom').val(),
                'requestTo': $('#inputAddressTo').val(),
                'requestSummary': $('#inputSummaryOfRequest').val(),
                //'newRequestDetail': $('#inputCountry').val()
            };

            var selected = [];
            $('#inputCountry  > option:selected').each(function () {
                //alert($(this).text() + ' ' + $(this).val());
                var $this = $(this);
                selected.push({ countryName: $this.text(), countryID: $this.val() });
            });

            data.newRequestDetail = selected;

            $.ajax({
                url: '/NewRequest?handler=SaveNewRequest',
                data: { record : data },
                type: 'POST',
                headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
                error: OnError
            }).done(function (data) {
                if (!data.status) {
                    $('#new-request-validation').html(data.message).show().delay(3000).fadeOut();
                } else {
                    alert(data.message);
                    $('#new-request-validation').html(data.message).show().delay(3000).fadeOut();
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                console.log('error' + errorThrown);
                console.log('error' + textStatus);
                console.log('error' + jqXHR);
               
            }).always(function (jqXHR, textStatus, errorThrown) {
            });
        }
    });

    function OnError(xhr, errorType, exception) {
        var responseText;
        $("#dialog").html("");
        try {
            responseText = jQuery.parseJSON(xhr.responseText);
            $("#dialog").append("<div><b>" + errorType + " " + exception + "</b></div>");
            $("#dialog").append("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
            $("#dialog").append("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
            $("#dialog").append("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");
        } catch (e) {
            responseText = xhr.responseText;
            $("#dialog").html(responseText);
        }
        $("#dialog").dialog({
            title: "jQuery Exception Details",
            width: 700,
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
    }


    function newRequestIsValid() {
        /*
        const userName = $('#userName').val();
        const password = $('#userPassword').val();
        const confirmPassword = $('#userConfirmPassword').val();

        if (userName === '' || password === '' || confirmPassword === '') {
            $('#user-modal-validation').html('All fields are required').show().delay(2000).fadeOut();
            return false;
        }

        if (password !== confirmPassword) {
            $('#user-modal-validation').html('Passwords not matching').show().delay(2000).fadeOut();
            return false;
        }
        */
        return true;
    }

    function loadDataForEditAndResponse() {

        $.ajax({
            url: '/NewRequest?handler=LoadDataForResponseEdit',
            data: { RequestId: edit_Value },
            type: 'POST',
            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
            error: OnError
        }).done(function (data) {
            if (data != null) {
                $('#date').val(data.formattedReceivedOnDate);
                $('#inputRequestNumber').val(data.requestNumber);
                $('#inputRequestNumber').val(data.requestNumber);

                if (data.lldcCountry) {
                    $('#inlineRadioLldcYes').prop('checked', true);
                    $('#inlineRadioLldcNo').prop('checked', false);
                }
                else {
                    $('#inlineRadioLldcYes').prop('checked', false);
                    $('#inlineRadioLldcNo').prop('checked', true);
                }
                $('#inputFrom').val(data.requestFrom);
                $('#inputAddressTo').val(data.requestTo);
                $('#inputSummaryOfRequest').val(data.requestSummary);
                $('#inputStaffResponsible').val(data.staffResponsibleId);

                /*
                "requestId": 26,                    
                "typeOfReqId": 4,                                
                "subProgrammeId": 10,   
                 "newRequestDetail": [],
                 */

               // alert(data.RequestNumber);
               // $('#new-request-validation').html(data.message).show().delay(3000).fadeOut();
            } else {
                alert(data);
               // $('#new-request-validation').html(data.message).show().delay(3000).fadeOut();
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.log('error' + errorThrown);
            console.log('error' + textStatus);
            console.log('error' + jqXHR);

        }).always(function (jqXHR, textStatus, errorThrown) {
        });

    }

});


