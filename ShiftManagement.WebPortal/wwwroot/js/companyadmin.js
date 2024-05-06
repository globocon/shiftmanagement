
$(function () {

    new PerfectScrollbar('.customers-list');

    $('.deleteclient').on('click', function () {
        const btn = $(this);
        const clientId = btn.attr('data-clientid');
        const clientName = btn.attr('data-clientname');
        if (confirm('Are you sure to delete client "' + clientName + '" ?')) {

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

        $('#div_client_profile_settings').load('/CAdminIndex?handler=ClientProfileSettings&clientId=' + csid, function () {
            // This function will be executed after the content is loaded
            // window.sharedVariable = button.data('cs-id');
            // console.log('Load operation completed!');
            // You can add your additional code or actions here
            // console.log(csnme);    
        });
    });

    let Dttbl_staffTable = $('#staffTable').DataTable({
        lengthMenu: [[5, 10, 25, 50, 100, 1000], [5, 10, 25, 50, 100, 1000]],
        paging: true,
        pagingType: "full_numbers",
        ordering: true,
        scrollCollapse: true,
        fixedHeader: false,
        order: [[1, "asc"]],
        info: false,
        scrollX: true,
        searching: true,
        autoWidth: false,
        ajax: {
            url: '/CAdminIndex?handler=DataForStaffTable',
            dataSrc: ''
        },
        columns: [
            { data: 'id', visible: false, title:'StaffId' },
            {
                data: 'name', title:'Name',
                "render": function (data, type, row) {
                    var rtn = '<div class="d-flex align-items-center">';
                    rtn += '    <div class="">';
                    var staffimg = '/Images/Employees/Noimage.jfif';
                    if (row.imageExtn != null) {
                        staffimg = '/Images/Employees/' + row.id + row.imageExtn + '?t=' + Date.now(); // Timestamp added to overcome browser cache issue
                    }
                    rtn += '<img src="' + staffimg + '" class="rounded-circle" width="46" height="46" alt="StaffImage_' + row.id +'" asp-append-version="true" />';
                    rtn += '    </div>';
                    rtn += '    <div class="ms-2">';
                    rtn += '        <h6 class="mb-1 font-14">' + data +'</h6>';
                    rtn += '        <p class="mb-0 font-13 text-secondary">Employee Id # ' + row.id +'</p>';
                    rtn += '    </div>';
                    rtn += '</div>';
                    return rtn;
                }
            },
            { data: 'formattedDOB', title:'Date Of Birth' },
            { data: 'phone', title:'Phone'},
            { data: 'genderDesc', title:'Gender' },
            {
                data: 'employementTypeId', title: 'Employement Type',
                "render": function (data, type, row) {
                    console.log(data);
                    var rtn = 'bg-danger';
                    if (data == 1) rtn = 'bg-warning';
                    else if (data == 0) rtn = 'bg-success';
                    else if (data == 2) rtn = 'bg-secondary';
                    else if (data == 3) rtn = 'bg-primary';
                    return '<div class="badge rounded-pill ' + rtn + ' w-100">' + row.employementTypeDesc + '</div>';;
                }
            },  
            { data: 'formattedDOJ', title: 'Date Of Join' },       
            //{
            //    targets: -1,
            //    orderable: false,
            //    width: '4%',
            //    data: 'id',
            //    "render": function (data, type, row) {
            //        var rtn = '<div class="d-flex order-actions">' +
            //            '<button type="button" class="btn btn-sm btn-outline-primary px-2 mx-2" id="btn_view_cs_key" onclick=location.href="/newrequest?EditValue=' + data + '"><i class="fa fa-eye mr-2"></i>View</button>' +
            //            '</div>'
            //        return rtn;

            //    }               

            //},
        ],
    });

});