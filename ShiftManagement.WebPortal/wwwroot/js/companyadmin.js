
$(function () {

    new PerfectScrollbar('.customers-list');

    const addClientBtn = document.getElementById('add-client-btn');

    // Get a reference to the client profile modal
    const clientProfileModal = new bootstrap.Modal(document.getElementById('client-profile-modal'));
   
    addClientBtn.addEventListener('click', function () {
        //$('#mdl_client_name').text("Save New Client")
        clientProfileModal.show();

    });

    const addStaffBtn = document.getElementById('add-staff-btn'); 
    const staffProfileModal = new bootstrap.Modal(document.getElementById('employee-profile-modal'));
    addStaffBtn.addEventListener('click', function () {       
        staffProfileModal.show();

    });
    $(document).on('click', '.deleteclient', function () {
        const btn = $(this);
        const clientId = btn.data('clientid');
        const clientName = btn.data('clientname');
        $('#inpClientidDeleteConfirmModal').val(clientId);
        $('#inpClientNameDeleteConfirmModal').val(clientName);
        $('#msgDeleteConfirmModal').html('Are you sure to delete client "' + clientName + '" ?');
        $('#DeleteConfirmModal').modal('show');
    });
    $('#btnDeleteConfirmModal').on('click', function () {
        var clientToDeleteId = $('#inpClientidDeleteConfirmModal').val();
        var clientToDeleteName = $('#inpClientNameDeleteConfirmModal').val();
        $('#DeleteConfirmModal').modal('hide');
        deleteClient(clientToDeleteId, clientToDeleteName);
    });
    // alert('client ' + clientName + ' marked for deletion !!!');


    $("#DeleteConfirmModal").on("hidden.bs.modal", function () {
        $('#inpClientidDeleteConfirmModal').val('');
        $('#inpClientNameDeleteConfirmModal').val('');
    });

    function deleteClient(clientToDeleteId, clientToDeleteName) {
        $.ajax({
            url: '/CAdminIndex?handler=DeleteClientDetails',
            data: { id: clientToDeleteId, name: clientToDeleteName },
            type: 'POST',
            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
        }).done(function (data) {
            if (data.success) {
                $('#add-new-client-modal').modal('hide');
                alert_success_with_refresh_window(data.message);
            } else {
                alert_error(data.message);
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
        var csname = $('#hinp_client_profile_modal_clientname').val();
        var csid = $('#hinp_client_profile_modal_clientid').val();
        if (csname == '') {

            csname = "Add New Client";
            $('#mdl_client_name').text(csname)
        }
        else {
            csname = "View / Edit Settings for : " + csname;
            $('#mdl_client_name').text(csname)
        }
        $('#div_client_profile_settings').load('/CAdminIndex?handler=ClientProfileSettings&clientId=' + csid, function () {
            // This function will be executed after the content is loaded
            

            //enable or disable salutation combobox
            var originalSalutationOptions = document.getElementById('Salutation').innerHTML;
            document.getElementById('useSalutationCheckbox').addEventListener('change', function () {
                var salutationSelect = document.getElementById('Salutation');
                salutationSelect.disabled = !this.checked;
                if (!this.checked) {
                  
                        // Clear existing options
                    Salutation.innerHTML = '';

                        // Create and add new option
                        var option = document.createElement('option');
                    option.text = 'Select Salutation.';
                    option.value = 'Select Salutation.';
                    Salutation.add(option);
                   
                }
                
                else {
                    Salutation.innerHTML = originalSalutationOptions;
                    Salutation.value = "Mr.";
                }

            });    

            // Code to update the display name as per name text boxes
      
                // Function to update display name based on input values
                function updateDisplayName() {
                    var firstName = $('#FirstName').val();
                    var middleName = $('#SecondName').val();
                    var lastName = $('#LastName').val();

                    // Concatenate first, middle, and last names with spaces
                    var displayName = firstName + (middleName ? ' ' + middleName : '') + (lastName ? ' ' + lastName : '');

                    // Update display name textbox
                    $('#DisplayName').val(displayName);
                }

                // Call updateDisplayName function when any of the name inputs change
            $('#FirstName, #SecondName, #LastName').on('input', function () {
                    updateDisplayName();
                });

                // Allow the user to edit the display name directly
                $('#DisplayName').on('input', function () {
                  
                });
            

            $('.btncancel').on('click', function (e) {
                $('#client-profile-modal').modal('hide');
            });
            $('.btnsave').on('click', function (e) {
                var data = {
                    'id': csid,
                    'Salutation': $('#Salutation').val(),
                    'FirstName':$('#FirstName').val(),
                    'SecondName':$('#SecondName').val(),
                    'LastName': $('#LastName').val(),
                    'DisplayName': $('#DisplayName').val(),
                    'Gender': $('#Gender').val(),
                    'DateOfBirth': $('#DateOfBirth').val(),
                    'address': $('#Address').val(),
                    'UnitOrApartmentNo': $('#UnitOrApartmentNo').val(),
                    'Mobile': $('#Mobile').val(),
                    'phone': $('#Phone').val(),
                    'email': $('#Email').val(),                                       
                    'MaritalStatus': $('#MaritalStatus').val(),
                    'Nationality': $('#Nationality').val(),
                    'Languages': $('#Languages').val(),
                    'ClientStatus': $('#ClientStatus').prop('checked'),            
                                        
                };
                $.ajax({
                    url: '/CAdminIndex?handler=SaveUpdateClientDetails',
                    data: { record: data },
                    type: 'POST',
                    headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
                }).done(function (data) {
                    if (data.success) {
                        
                        $('#client-profile-modal').modal('hide');
                        alert_success_with_refresh_window(data.message);
                        
                    }
                    else {
                        alert(data.message);
                    }
                }).fail(function () {
                    console.log('error');
                });
            });
        });
    });
   
    $('#employee-profile-modal').on('shown.bs.modal', function (event) {
        $('#div_employee_settings').html('');

        const button = $(event.relatedTarget);
        const empId = button.data('id');// $(this).data('name')
        const empName = button.data('name');
       
        if (empId != 0 && empId != null || empId == undefined) {
            $('#mdl_employee_name').text(empName)
            if (button.attr('id') == "btnDeleteStaff") {
                $('#employee-profile-modal').modal('hide');
                $('#inpClientidDeleteConfirmModal').val(empId);
                $('#msgDeleteConfirmModal').html('Are you sure to delete this employee ?');
                $('#DeleteConfirmModal').modal('show');
            }
           // else if (button.attr('id') === "btnEditStaff" || button.attr('id') === "btnViewStaff" || button.attr('id') === "btnSaveStaff") {
                //if (button.attr('id') === "btnSaveStaff") 
                //{
                //    const btn = $(this);
                //    const clientId = btn.attr('data-clientid');
                //    const clientName = btn.attr('data-clientname');
                //    $('#hinp_client_profile_modal_clientid').val('');
                //    $('#hinp_client_profile_modal_clientname').val('');
                //    $('#client-profile-modal').modal('show');
                //}

                $('#div_employee_profile_settings').load('/CAdminIndex?handler=EmployeeProfileSettings&EmployeeId=' + empId, function () {

                    var originalSalutationStaffOptions = document.getElementById('SalutationStaff').innerHTML;
                    document.getElementById('useSalutationStaff').addEventListener('change', function () {
                        var SalutationStaffSelect = document.getElementById('SalutationStaff');
                        SalutationStaffSelect.disabled = !this.checked;
                        if (!this.checked) {

                            // Clear existing options
                            SalutationStaff.innerHTML = '';

                            // Create and add new option
                            var option = document.createElement('option');
                            option.text = 'Select Salutation.';
                            option.value = 'Select Salutation.';
                            SalutationStaff.add(option);

                        }

                        else {
                            SalutationStaff.innerHTML = originalSalutationOptions;
                            SalutationStaff.value = "Mr.";
                        }

                    });
                    $('.btncancelEmployee').on('click', function (e) {
                        $('#employee-profile-modal').modal('hide');
                    });
                    $('.btnsave_employee').on('click', function (e) {
                        var data = {
                            'id': empId,
                            'Name': $('#Name').val(),
                            'Email': $('#Email').val(),
                            'Mobile': $('#Mobile').val(),
                            'Phone': $('#Phone').val(),
                            'Gender': $('#Gender').val(),
                            'DOB': $('#DOB').val(),
                            'EmployementTypeId': $('#EmployementTypeId').val(),
                            'Address': $('#Address').val(),
                            'DOJ': $('#DOJ').val(),
                        };
                        $.ajax({
                            url: '/CAdminIndex?handler=SaveUpdateEmployeeDetails',
                            data: { record: data },
                            type: 'POST',
                            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
                        }).done(function (data) {
                            if (data.success) {

                                $('#employee-profile-modal').modal('hide');
                                alert_success_with_refresh_window(data.message);
                            }
                            else {
                                alert(data.message);
                            }
                        }).fail(function () {
                            console.log('error');
                        });
                    });

                });
         

            $('#btnDeleteConfirmModal').on('click', function () {

                var employeeToDeleteId = $('#inpClientidDeleteConfirmModal').val();
                var employeeToDeleteName = $('#inpClientNameDeleteConfirmModal').val();
                $('#DeleteConfirmModal').modal('hide');
                deleteClient(employeeToDeleteId, employeeToDeleteName);
            });

            $("#DeleteConfirmModal").on("hidden.bs.modal", function () {
                $('#inpClientidDeleteConfirmModal').val('');
                $('#inpClientNameDeleteConfirmModal').val('');
            });

            function deleteClient(employeeToDeleteId, employeeToDeleteName) {
                $.ajax({
                    url: '/CAdminIndex?handler=DeleteEmployeeDetails',
                    data: { id: employeeToDeleteId, name: employeeToDeleteName },
                    type: 'POST',
                    headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
                }).done(function (data) {
                    if (data.success) {
                       
                        alert_success_with_refresh_window(data.message);
                    }
                    else {
                        alert(data.message);
                    }
                }).fail(function () {
                    console.log('error');
                });
            }
        }
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
            { data: 'id', visible: false },
            {
                data: 'name', title: 'Name',
                "render": function (data, type, row) {
                    var rtn = '<div class="d-flex align-items-center">';
                    rtn += '    <div class="">';
                    var staffimg = '/Images/Employees/NoImage.png';
                    if (row.imageExtn != null) {
                        staffimg = '/Images/Employees/' + row.id + row.imageExtn + '?t=' + Date.now(); // Timestamp added to overcome browser cache issue
                    }
                    rtn += '<img src="' + staffimg + '" class="rounded-circle" width="46" height="46" alt="StaffImage_' + row.id + '" asp-append-version="true" />';
                    rtn += '    </div>';
                    rtn += '    <div class="ms-2">';
                    rtn += '        <h6 class="mb-1 font-14">' + data + '</h6>';
                    rtn += '        <p class="mb-0 font-13 text-secondary">Employee Id # ' + row.id + '</p>';
                    rtn += '    </div>';
                    rtn += '</div>';
                    return rtn;
                }
            },
            { data: 'formattedDOB', title: 'Date Of Birth' },
            { data: 'phone', title: 'Phone' },
            { data: 'gender', title: 'Gender' },
            {
                data: 'employementTypeId', title: 'Employement Type',
                "render": function (data, type, row) {
                   // console.log(data);
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
            {
                targets: -1,
                orderable: false,
                width: '4%',
                data: 'id',
                "render": function (data, type, row) {

                    var rtn = '<div class="d-flex order-actions text-center">' +
                        '<button type="button" class="btn btn-outline-primary mr-2" id="btnViewStaff" data-bs-toggle="modal" data-bs-target="#employee-profile-modal" data-id="' + data + '" data-name="' + row.name + '"><i class="fa fa-eye mr-2"></i></button>' +
                        '<button type="button" class="btn btn-outline-primary mr-2" id="btnEditStaff" data-bs-toggle="modal" data-bs-target="#employee-profile-modal" data-id="' + data + '" "><i class="fa fa-pencil mr-2"></i></button>' +
                        '<button type="button" class="btn btn-sm btn-outline-primary px-2 mx-2" data-id="' + data + '"  id="btnDeleteStaff" data-bs-toggle="modal" data-bs-target="#employee-profile-modal" ><i class="fa fa-trash mr-2"></i></button>' +

                        '</div>';
                    return rtn;
                }

            }
        ],
    });
    //function viewStaff(id) {
    //    //var id = element.getAttribute('data-id'); // Get the id from the data attribute of the clicked element

    //    // Ajax request to retrieve staff information
    //    $.ajax({
    //        url: '/CAdminIndex?handler=EmployeeProfileSettings',
    //        data: { id: id },
    //        type: 'GET',
    //        dataType: 'json'
    //    }).done(function (data) {
    //        // Redirect to the URL received in the response data
    //        window.location.href = data;
    //    }).fail(function (jqXHR, textStatus, errorThrown) {
    //        // Handle any errors that occur during the Ajax request
    //        console.error("Error fetching staff information:", textStatus, errorThrown);
    //    });
    //}

    //var viewStaff = function (id) {
    ////   // alert("hi");
    ////    const btn = $(this);
    ////    const employeeId = btn.attr('data-employeeid');
    ////    const employeeName = btn.attr('data-employeename');
    ////    $('#hinp_employee_profile_modal_employeeid').val(employeeId);
    ////    $('#hinp_employee_profile_modal_employeename').val(employeeName);
    ////    $('#employee-profile-modal').modal('show');
    //    $.ajax({
    //        url: '/CAdminIndex?handler=EmployeeProfileSettings',
    //        data: { id: parseInt(id) },
    //        type: 'GET',
    //        dataType: 'json'
    //    }).done(function (data) {
    //        window.location.href = data;
    //    });
    //}



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