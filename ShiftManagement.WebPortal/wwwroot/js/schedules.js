'use strict';

/*eslint-disable*/

var ScheduleList = [];

var SCHEDULE_CATEGORY = [
    'milestone',
    'task'
];

function ScheduleInfo() {
    this.id = null;
    this.calendarId = null;

    this.title = null;
    this.body = null;
    this.location = null;
    this.isAllday = false;
    this.start = null;
    this.end = null;
    this.category = '';
    this.dueDateClass = '';

    this.color = null;
    this.bgColor = null;
    this.dragBgColor = null;
    this.borderColor = null;
    this.customStyle = '';

    this.isFocused = false;
    this.isPending = false;
    this.isVisible = true;
    this.isReadOnly = false;
    this.isPrivate = false;
    this.goingDuration = 0;
    this.comingDuration = 0;
    this.recurrenceRule = '';
    this.state = '';

    this.raw = {
        memo: '',
        hasToOrCc: false,
        hasRecurrenceRule: false,
        location: null,
        creator: {
            name: '',
            avatar: '',
            company: '',
            email: '',
            phone: ''
        }
    };
}

function generateTime(schedule, startTime, endTime) {
    var startDate = moment(startTime)
    var endDate = moment(endTime);
    var diffDate = endDate.diff(startDate, 'days');
        
    schedule.isAllday = true;
    schedule.category = 'allday';

    schedule.start = startDate.toDate();
    schedule.end = endDate.toDate();
        
}

//function generateTime_Old(schedule, renderStart, renderEnd) {
//    var startDate = moment(renderStart.getTime())
//    var endDate = moment(renderEnd.getTime());
//    var diffDate = endDate.diff(startDate, 'days');

//    console.log('startDate:' + startDate);
//    console.log('endDate:' + endDate);
//    console.log('diffDate:' + diffDate);

//    schedule.isAllday = true; // chance.bool({likelihood: 30});
//    if (schedule.isAllday) {
//        schedule.category = 'allday';
//    } else if (chance.bool({likelihood: 30})) {
//        schedule.category = SCHEDULE_CATEGORY[chance.integer({min: 0, max: 1})];
//        if (schedule.category === SCHEDULE_CATEGORY[1]) {
//            schedule.dueDateClass = 'morning';
//        }
//    } else {
//        schedule.category = 'time';
//    }

//    startDate.add(chance.integer({min: 0, max: diffDate}), 'days');
//    startDate.hours(chance.integer({min: 0, max: 23}))
//    startDate.minutes(chance.bool() ? 0 : 30);
//    schedule.start = startDate.toDate();

//    endDate = moment(startDate);
//    if (schedule.isAllday) {
//        endDate.add(chance.integer({min: 0, max: 3}), 'days');
//    }

//    schedule.end = endDate
//        .add(chance.integer({min: 1, max: 4}), 'hour')
//        .toDate();

//    if (!schedule.isAllday && chance.bool({likelihood: 20})) {
//        schedule.goingDuration = chance.integer({min: 30, max: 120});
//        schedule.comingDuration = chance.integer({min: 30, max: 120});;

//        if (chance.bool({likelihood: 50})) {
//            schedule.end = schedule.start;
//        }
//    }

//    console.log('schedule.start:' + schedule.start);
//    console.log('schedule.end:' + schedule.end);
//}

function generateEmployeeSchedule(calendar, renderStart, renderEnd) {    
    
    var sdate = moment(renderStart.getTime()).format('YYYY-MM-DD'); 
    var edate = moment(renderEnd.getTime()).format('YYYY-MM-DD');  

    return new Promise(function (resolve, reject) {
        // Make AJAX call
        $.ajax({
            url: '/CAdminIndex?handler=SingleEmployeeShiftData',
            type: 'GET',
            data: {
                empId: calendar.empid,
                startDate: sdate,
                endDate: edate
            },
            dataSrc: '',
            success: function (data) {
                // Resolve the promise when AJAX call succeeds
               // console.log('call resolved:');
                resolve(data);
            },
            error: function (xhr, status, error) {
                // Reject the promise if AJAX call fails
                //console.log('call error:')
                reject(error);
            }
        });
    });
}

// Define a function that returns a Promise
function processCalendar(calendar, renderStart, renderEnd) {
    return new Promise(function (resolve, reject) {      
        generateEmployeeSchedule(calendar, renderStart, renderEnd).then(function (data) {
            // Code to execute after the AJAX call finishes successfully
            //console.log('AJAX call finished successfully with data:', data);
            if (data != null) {
                data.forEach(function (item) {
                    var schedule = new ScheduleInfo();
                    schedule.id = item.id;
                    schedule.calendarId = calendar.id;

                    schedule.title = item.clients.name;
                    schedule.body = '';
                    schedule.isReadOnly = false;
                    //generateTime(schedule, renderStart, renderEnd);

                    generateTime(schedule, item.startTime, item.endTime);

                    //schedule.start = item.startTime;
                    //schedule.end = item.endTime;

                    schedule.isPrivate = true;
                    schedule.location = 'no address';
                    schedule.attendees = [];
                    schedule.recurrenceRule = 'repeated events';
                    schedule.state = 'Busy';
                    schedule.color = calendar.color;
                    schedule.bgColor = calendar.bgColor;
                    schedule.dragBgColor = calendar.dragBgColor;
                    schedule.borderColor = calendar.borderColor;

                    if (schedule.category === 'milestone') {
                        schedule.color = schedule.bgColor;
                        schedule.bgColor = 'transparent';
                        schedule.dragBgColor = 'transparent';
                        schedule.borderColor = 'transparent';
                    }

                    //schedule.raw.memo = chance.sentence();
                    schedule.raw.creator.name = item.clients.name;
                    //schedule.raw.creator.avatar = chance.avatar();
                    //schedule.raw.creator.company = chance.company();
                    //schedule.raw.creator.email = chance.email();
                    //schedule.raw.creator.phone = chance.phone();

                    //if (chance.bool({ likelihood: 20 })) {
                    //    var travelTime = chance.minute();
                    //    schedule.goingDuration = travelTime;
                    //    schedule.comingDuration = travelTime;
                    //}

                    ScheduleList.push(schedule);
                });

            }
            resolve();
        }).catch(function (error) {
            // Code to handle errors if the AJAX call fails
           // console.error('AJAX call failed:', error);
            reject();
        });
    });
}

// Create an async function to iterate over the CalendarList
async function generateSchedule(viewName, renderStart, renderEnd) {
    ScheduleList = [];
    for (const calendar of CalendarList) {
        // Wait for the processing of the current calendar to complete
        await processCalendar(calendar, renderStart, renderEnd);
    }
}
