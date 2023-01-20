(function($) {
    /* "use strict" */


    var dzChartlist = function() {
        let draw = Chart.controllers.line.__super__.draw; //draw shadow
        var screenWidth = $(window).width();



        $('.canvas-container').each(function(index, el) {

                var width = $(this).width();
                var height = width + width * .25;
                var data = [];
                var colors = [];
                var labels = [];
                $(el).find('.chart-data > *').each(function() {
                    data.push(parseInt($(this).attr('data-percent')));
                    colors.push($(this).attr('data-color'));
                    labels.push($(this).attr('data-label'));
                })
                console.log(data);
                $('#chartjs-4').attr('width', width).attr('height', height);
                new Chart(document.getElementById("chartjs-4"), {
                    "type": "doughnut",
                    responsive: true,
                    options: {
                        legend: {
                            display: false,
                        }

                    },
                    maintainAspectRatio: false,
                    "data": {
                        "labels": labels,
                        "datasets": [{
                            "data": data,
                            "backgroundColor": colors
                        }]
                    }
                });
            })
            //______calendar
        $('.custom-calendar').pignoseCalendar({
            disabledDates: [
                '2021-01-20'
            ],
            format: 'YYY-MM-DD'
        });
        /* Function ============ */
        return {
            init: function() {},


            load: function() {
                donutChart1();
                donutChart2();
                lineChart();
                chartBar2();
                chartBar3();
                chartBar4();
                chartBar5();
            },

            resize: function() {

            }
        }

    }();

    jQuery(document).ready(function() {});



    jQuery(window).on('resize', function() {


    });

})(jQuery);