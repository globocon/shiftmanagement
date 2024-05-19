
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
                    var staffimg = '/Images/Employees/NoImage.png';
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

    let shiftDataToDisplayDate = new Date();
    let shiftDataToDisplayDateString = convertDateFormat(shiftDataToDisplayDate, 'yyyy-MM-dd');
    let Dttbl_shiftTable = $('#staffShiftTable').DataTable({
        paging: true,
        pagingType: "full_numbers",
        ordering: true,
        order: [[5, "asc"]],
        fixedHeader: false,
        info: true,
        scrollX: true,
        autoWidth: false,
        searching: true,

        ajax: {
            url: '/CAdminIndex?handler=EmployeeShiftData',
            data: function (d) {
                d.shiftDate = shiftDataToDisplayDateString;
            },
            type: 'GET',
            dataType: 'json',
            dataSrc: 'shiftData'
        },
        columns: [
            { data: 'id', visible: false },
            { data: 'calanderId', visible: false },
            { data: 'employeeId', visible: false },
            { data: 'clientId', visible: false },
            { data: 'shiftTypeID', visible: false },
            {
                data: 'employees', visible: true, title: 'Employee',
                "render": function (data, type, row) {
                    var rtn = '<div class="d-flex align-items-center">';
                    rtn += '    <div class="">';
                    var staffimg = '/Images/Employees/NoImage.png';
                    if (row.employees.imageExtn != null) {
                        staffimg = '/Images/Employees/' + row.employeeId + row.employees.imageExtn + '?t=' + Date.now(); // Timestamp added to overcome browser cache issue
                    }
                    rtn += '<img src="' + staffimg + '" class="rounded-circle" width="46" height="46" alt="StaffImage_' + row.employeeId + '" asp-append-version="true" />';
                    rtn += '    </div>';
                    rtn += '    <div class="ms-2">';
                    rtn += '        <h6 class="mb-1 font-14">' + row.employees.name + '</h6>';
                    rtn += '        <p class="mb-0 font-13 text-secondary">Employee Id # ' + row.employeeId + '</p>';
                    rtn += '    </div>';
                    rtn += '</div>';
                    return rtn;
                }
            },
            {
                data: 'clients', visible: true, title: 'Client',
                "render": function (data, type, row) {
                    var rtn = '<div class="d-flex align-items-center">';
                    rtn += '    <div class="recent-product-img">';
                    var clientimg = '/Images/Clients/NoImage2.png';
                    if (row.clients.imageExtn != null) {
                        clientimg = '/Images/Clients/' + row.clientId + row.clients.imageExtn + '?t=' + Date.now(); // Timestamp added to overcome browser cache issue
                    }
                    rtn += '<img src="' + clientimg + '" alt="CI_' + row.clients.name + '" asp-append-version="true" />';
                    rtn += '    </div>';
                    rtn += '    <div class="ms-2">';
                    rtn += '        <h6 class="mb-1 font-14">' + row.clients.name + '</h6>';
                    rtn += '    </div>';
                    rtn += '</div>';
                    return rtn;
                }
            },
            { data: 'shiftType.shiftTypeName', visible: true, title: 'Shift Type' },
            { data: 'formattedShiftDate', visible: true, title: 'Shift Date' },
            { data: 'formattedStartTime', visible: true, title: 'Shift Start' },
            { data: 'formattedEndTime', visible: true, title: 'Shift End' },
            {
                data: 'endTime', visible: true, title: 'Shift Status',
                "render": function (data, type, row) {
                    return shiftStatus(data, type, row);
                },
            }
        ],
        drawCallback: function (settings) {
            $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
            var api = this.api();
            var rj = api.ajax.json();
            if (rj != undefined && rj != 'undefined') {
                var hdr = 'Shift (' + rj.startDate + ' ~ ' + rj.endDate + ')'
                $('#shiftdaterange').html(hdr);
            }
        }
    });

    function shiftStatus(value, type, row) {

        var DateTime = luxon.DateTime;

        var cdtm = new DateTime.now();
        const edate = new Date(value);
        const sdate = new Date(row.startTime);
        var enddtm = DateTime.fromJSDate(edate);
        var srtdtm = DateTime.fromJSDate(sdate);

        var clas = 'text-primary';
        var sts = 'InProgress';
        if (enddtm < cdtm) { clas = 'text-warning'; sts = 'Completed'; }
        else if (enddtm > cdtm && srtdtm > cdtm) { clas = 'text-success'; sts = 'Upcoming'; }

        var rtn = "<div class='d-flex align-items-center " + clas + " '>";
        rtn += "    <i class='bx bx-radio-circle-marked bx-burst bx-rotate-90 align-middle font-18 me-1'></i>";
        rtn += "    <span>" + sts + "</span>";
        rtn += "</div>";

        return rtn;
    }

    function convertDateFormat(dte, format) {
        if (dte != null) {
            const date = new Date(dte);
            var DateTime = luxon.DateTime;
            var dt1 = DateTime.fromJSDate(date);
            var dt = dt1.toFormat(format);
            return dt;
        }
        else {
            return null;
        }
    }

    $('#btn_refresh_table_data').on('click', function (e) {
        Dttbl_shiftTable.ajax.reload();
    });

    $('#btn_previous_table_data').on('click', function (e) {
        shiftDataToDisplayDate = new Date(shiftDataToDisplayDate);
        shiftDataToDisplayDate.setDate(shiftDataToDisplayDate.getDate() - 7);
        shiftDataToDisplayDateString = convertDateFormat(shiftDataToDisplayDate, 'yyyy-MM-dd');
        Dttbl_shiftTable.ajax.reload();
    });

    $('#btn_next_table_data').on('click', function (e) {
        shiftDataToDisplayDate = new Date(shiftDataToDisplayDate);
        shiftDataToDisplayDate.setDate(shiftDataToDisplayDate.getDate() + 7);
        shiftDataToDisplayDateString = convertDateFormat(shiftDataToDisplayDate, 'yyyy-MM-dd');
        Dttbl_shiftTable.ajax.reload();
    });


   

});

