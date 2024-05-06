


$(function () {

    new PerfectScrollbar('.customers-list');

    $('.deleteclient').on('click', function () {
        const btn = $(this);
        const clientId = btn.attr('data-clientid');
        const clientName = btn.attr('data-clientname');
        if (confirm('Are you sure to delete client "' + clientName +'" ?')) {
            
            alert('client ' + clientName + ' marked for deletion !!!');

            //$.ajax({
            //    url: '/Admin/Settings?handler=ShowPassword',
            //    data: { id: userId },
            //    type: 'POST',
            //    headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
            //}).done(function (data) {
            //    spanElem.text(data);
            //    btn.hide();
            //}).fail(function () {
            //    console.log('error')
            //});
        }
    });

    $('.viewclient').on('click', function () {
        const btn = $(this);
        const clientId = btn.attr('data-clientid');
        const clientName = btn.attr('data-clientname');       
        $('#hinp_client_profile_modal_clientid').val(clientId);
        $('#hinp_client_profile_modal_clientname').val(clientName);
        $('#client-profile-modal').modal('show');       
    });

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
        });
    });

});