


$(function () {

    new PerfectScrollbar('.customers-list');
  
     
    $(document).on('click', '.deleteclient', function () {
        const btn = $(this);
        const clientId = btn.data('clientid');
        const clientName = btn.data('clientname');
        $('#inpClientidDeleteConfirmModal').val(clientId);
        $('#msgDeleteConfirmModal').html('Are you sure to delete client "' + clientName + '" ?');
        $('#DeleteConfirmModal').modal('show');
    });
    $('#btnDeleteConfirmModal').on('click', function () {
        var clientToDeleteId = $('#inpClientidDeleteConfirmModal').val();
        $('#DeleteConfirmModal').modal('hide');
        deleteClient(clientToDeleteId);
    });
    // alert('client ' + clientName + ' marked for deletion !!!');


$("#DeleteConfirmModal").on("hidden.bs.modal", function () {
    $('#inpClientidDeleteConfirmModal').val('');
});

    function deleteClient(clientToDeleteId) {
        $.ajax({
            url: '/SAdminIndex?handler=DeleteClientDetails',
            data: { id: clientToDeleteId },
            type: 'POST',
            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
        }).done(function (data) {
            if (data.success) {
                //Dttbl_role_settings_MR.ajax.reload();
                showSuccessModalSmall("Delete Client", data.message);
            }
            else {
                showErrorModalSmall("Delete Client", data.message);
            }
        }).fail(function () {
            console.log('error');
        });
    }      

    $('.viewclient').on('click', function () {
        const btn = $(this);
        const clientId = btn.attr('data-clientid');
        const clientName = btn.attr('data-clientname');       
        $('#hinp_client_profile_modal_clientid').val(clientId);
        $('#hinp_client_profile_modal_clientname').val(clientName);
        $('#client-profile-modal').modal('show'); 
        // Disable all interactive elements within the modal
        $('#client-profile-modal :input').prop('disabled', true);
        // Disable all input elements within the modal
        $('#client-profile-modal input').prop('disabled', true);

    });
    $('.saveclient').on('click', function () {
        const btn = $(this);
        const clientId = btn.attr('data-clientid');
        const clientName = btn.attr('data-clientname');
        $('#hinp_client_profile_modal_clientid').val('');
        $('#hinp_client_profile_modal_clientname').val('');
        $('#client-profile-modal').modal('show');
        if (clientName.trim() === '') {
            alert('Please provide a client name.');
            return;
        }
       
    }); 
     
    //function saveClient(clientId, clientName) {
    //    //  AJAX call to save client details
    //    $.ajax({
    //        url: '/SAdminIndex?handler=SaveClientDetails',
    //        data: { id: clientId, name: clientName },
    //        type: 'POST',
    //        headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
    //    }).done(function (data) {
    //        if (data.success) {
    //            // Show success message
    //            showSuccessModalSmall("Save Client", data.message);
    //            $('#clientNameElement').text(clientName);
    //        }
    //        else {
    //            // Show error message if save fails
    //            showErrorModalSmall("Save Client", data.message);
    //        }
    //    }).fail(function () {
    //        console.log('error');
    //    });
    //}

    $('.editclient').on('click', function () {
        const btn = $(this);
        const clientId = btn.attr('data-clientid');
        const clientName = btn.attr('data-clientname');
        $('#hinp_client_profile_modal_clientid').val(clientId);
        $('#hinp_client_profile_modal_clientname').val(clientName);
        $('#client-profile-modal').modal('show');
        
    });


    $('#client-profile-modal').on('shown.bs.modal', function (event) {
        $('#div_client_settings').html('');
        var csnme = $('#hinp_client_profile_modal_clientname').val();
        var csid = $('#hinp_client_profile_modal_clientid').val();
        $('#mdl_client_name').text(csnme)

        $('#div_client_profile_settings').load('/SAdminIndex?handler=ClientProfileSettings&clientId=' + csid, function () {
            // This function will be executed after the content is loaded
            // window.sharedVariable = button.data('cs-id');
            // console.log('Load operation completed!');
            // You can add your additional code or actions here
            // console.log(csnme);    
            $('.btnsave_me').on('click', function (e) {
                var data = {
                    'id':csid,
                    'Name': $('#Name').val(),
                    'emails': $('#Emails').val(),
                    'phone': $('#Phone').val(),
                    'address': $('#Address').val(),
                };
                    $.ajax({
                        url: '/SAdminIndex?handler=SaveUpdateClientDetails',
                    data: { record: data },
                    type: 'POST',
                    headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
                }).done(function (data) {
                    if (data.success) {
                        
                        showSuccessModalSmall("Update Client", data.message);
                    }
                    else {
                        showErrorModalSmall("Update Client", data.message);
                    }
                }).fail(function () {
                    console.log('error');
                });
            });
        });
               

          
        });
       
   

});


