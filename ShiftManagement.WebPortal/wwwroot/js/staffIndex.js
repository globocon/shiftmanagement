$(function () {

    let shiftDataToDisplayDate = new Date();
    let shiftDataToDisplayDateString = convertDateFormat(shiftDataToDisplayDate, 'yyyy-MM-dd');
    let Dttbl_staffTable = $('#staffShiftTable').DataTable({        
        paging: false,
        ordering: false,
        fixedHeader: false,
        info: false,
        scrollX: true,
        autoWidth: false,
        searching: false,
        ajax: {
            url: '/StaffIndex?handler=EmployeeShiftData',
            data: function (d) {
                d.shiftDate = shiftDataToDisplayDateString;
            },
            type: 'GET',
            dataType: 'json',
            dataSrc:'shiftData'
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
                var hdr = 'Shift (' + rj.startDate + ' ~ ' + rj.endDate +')'
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
        Dttbl_staffTable.ajax.reload();
    });

    $('#btn_previous_table_data').on('click', function (e) {
        shiftDataToDisplayDate = new Date(shiftDataToDisplayDate);
        shiftDataToDisplayDate.setDate(shiftDataToDisplayDate.getDate() - 7);
        shiftDataToDisplayDateString = convertDateFormat(shiftDataToDisplayDate, 'yyyy-MM-dd');
        Dttbl_staffTable.ajax.reload();
    });

    $('#btn_next_table_data').on('click', function (e) {
        shiftDataToDisplayDate = new Date(shiftDataToDisplayDate);
        shiftDataToDisplayDate.setDate(shiftDataToDisplayDate.getDate() + 7);
        shiftDataToDisplayDateString = convertDateFormat(shiftDataToDisplayDate, 'yyyy-MM-dd');
        Dttbl_staffTable.ajax.reload();
    });

});