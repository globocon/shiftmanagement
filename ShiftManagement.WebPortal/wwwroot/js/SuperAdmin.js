


$(function () {

    new PerfectScrollbar('.customers-list');

    $('#add-new-company').on('click', function () {
        $('#add-new-company-modal').modal('show');
    });

    $('#btn-mdl-add-new-company').on('click', function () {
        if (isNewCompanyEntryValid()) {
            $.ajax({
                url: '/SAdminIndex?handler=NewCompany',
                type: 'POST',
                data: $('#frmNewCompany').serialize(),
                headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
            }).done(function (data) {
                if (data.success) {
                    //location.reload(true);
                    $('#add-new-company-modal').modal('hide');
                    alert_success_with_refresh_window(data.message);
                } else {                    
                    alert_error(data.message);
                }
            }).fail(function () {
                //showStatusNotification(false, 'Something went wrong');
            });
        }
    });
    $('#add-new-company-modal').on('show.bs.modal', function (event) {
        $('#frmNewCompany').removeClass('was-validated');
        $('#frmNewCompany').removeClass('invalid');
        $("#newCompany_CompanyName").val('');
        $("newCompany_CompanyNameID").val('');
        $('#mdl-add-new-company-checkId-available').html('.');     
    });

    function isNewCompanyEntryValid() {
        $('#frmNewCompany').removeClass('was-validated');
        $('#frmNewCompany').removeClass('invalid');

        let valdfail = false;
        let msg = "";

        var pCompanyName = document.getElementById("newCompany_CompanyName");
        var pCompanyID = document.getElementById("newCompany_CompanyNameID");
               
        if (pCompanyName.value == '' || pCompanyID.value == '') {
            valdfail = true;
        }

        if (valdfail == true) {
            $('#frmNewCompany').addClass('was-validated');
            $('#frmNewCompany').addClass('invalid');         
            return false;
        }
        return true;
    }

    $('#newCompany_CompanyNameID').on('keyup', function () {
        const txt = $(this);
        const disp = $('#mdl-add-new-company-checkId-available');  
        disp.html('.');
        if (txt.val().length >= 3) {
            $.ajax({
                url: '/SAdminIndex?handler=CheckIdAvailable',
                type: 'POST',
                data: { companyId: txt.val() },
                headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
            }).done(function (data) {
                disp.removeClass('text-success').removeClass('text-danger');
                disp.html(data.message);
                if (data.success) {
                    disp.addClass('text-success'); 
                } else {
                    disp.addClass('text-danger');
                }              
            }).fail(function () {
                //disp.addClass('d-none');
            });            
        } 
    });


    $('.deletecompany').on('click', function () {
        const btn = $(this);
        const companyId = btn.attr('data-companyid');
        const companyName = btn.attr('data-companyname');
        var qus = 'Are you sure to delete company "' + companyName + '" ?';
        confirm_with_callback(qus, companyId, deleteCompany);
    });

    function deleteCompany(companyId) {
        $.ajax({
            url: '/SAdminIndex?handler=DeleteCompany',
            data: { companyid: companyId },
            type: 'POST',
            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
        }).done(function (data) {
            if (data.success) {
                alert_success_with_refresh_window(data.message);
            } else {
                alert_error(data.message);
            }
        }).fail(function () {
            console.log('error')
        });
    }

    $('.viewcompany').on('click', function () {
        const btn = $(this);
        const clientId = btn.attr('data-clientid');
        const clientName = btn.attr('data-clientname');       
        $('#hinp_client_profile_modal_clientid').val(clientId);
        $('#hinp_client_profile_modal_clientname').val(clientName);
        $('#client-profile-modal').modal('show');       
    });

    $('.editcompany').on('click', function () {
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
        });
    });

});