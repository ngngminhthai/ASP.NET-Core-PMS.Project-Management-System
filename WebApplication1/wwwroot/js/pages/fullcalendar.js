document.addEventListener('DOMContentLoaded', function() {
    var calendarEl = document.getElementById('calendar');
    console.log(tasks);

    var tasks2 = [{
        title: 'Conference',
        start: '2022-03-03',
        end: '2022-03-05'
    }, {
            title: 'Conference2',
            start: '2022-03-03',
            end: '2022-03-05'
        }
    ]
    var tasks3 = tasks
    console.log(tasks2);

    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },
        initialDate: '2023-03-03',
        navLinks: true, // can click day/week names to navigate views
        businessHours: true, // display business hours
        editable: true,
        selectable: true,
        events: tasks
    });

    calendar.render();
});