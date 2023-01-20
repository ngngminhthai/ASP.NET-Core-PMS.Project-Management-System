let line_chart = {
    series: [{
        name: "Complete",
        data: [40, 70, 20, 90, 36, 80, 30, 91, 60]
    }, {
        name: "Doing",
        data: [10, 30, 50, 20, 76, 40, 20, 51, 10]
    }],
    colors: ['#3C21F7', '#FFCA1F'],
    chart: {
        height: 350,
        type: 'line',
    },
    dataLabels: {
        enabled: false
    },
    stroke: {
        curve: 'smooth'
    },
    xaxis: {
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
    },
};

var chart1 = new ApexCharts(document.querySelector("#line-chart"), line_chart);
chart1.render();

let area_chart = {
    series: [{
        name: 'series1',
        data: [31, 40, 28, 51, 42, 109, 100]
    }, {
        name: 'series2',
        data: [11, 32, 45, 32, 34, 52, 41]
    }],
    chart: {
        height: 350,
        type: 'area'
    },
    dataLabels: {
        enabled: false
    },
    stroke: {
        curve: 'smooth'
    },
    xaxis: {
        type: 'datetime',
        categories: ["2018-09-19T00:00:00.000Z", "2018-09-19T01:30:00.000Z", "2018-09-19T02:30:00.000Z", "2018-09-19T03:30:00.000Z", "2018-09-19T04:30:00.000Z", "2018-09-19T05:30:00.000Z", "2018-09-19T06:30:00.000Z"]
    },
    tooltip: {
        x: {
            format: 'dd/MM/yy HH:mm'
        },
    },
};

let chart2 = new ApexCharts(document.querySelector("#area-chart"), area_chart);
chart2.render();

let column_chart = {
    series: [{
        name: 'Net Profit',
        data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
    }, {
        name: 'Revenue',
        data: [76, 85, 101, 98, 87, 105, 91, 114, 94]
    }, {
        name: 'Free Cash Flow',
        data: [35, 41, 36, 26, 45, 48, 52, 53, 41]
    }],
    chart: {
        type: 'bar',
        height: 350
    },
    plotOptions: {
        bar: {
            horizontal: false,
            columnWidth: '55%',
            endingShape: 'rounded'
        },
    },
    dataLabels: {
        enabled: false
    },
    stroke: {
        show: true,
        width: 2,
        colors: ['transparent']
    },
    xaxis: {
        categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
    },
    yaxis: {
        title: {
            text: '$ (thousands)'
        }
    },
    fill: {
        opacity: 1
    },
    tooltip: {
        y: {
            formatter: function(val) {
                return "$ " + val + " thousands"
            }
        }
    }
};

let chart3 = new ApexCharts(document.querySelector("#column-chart"), column_chart);
chart3.render();

let pie_chart = {
    series: [44, 55, 13, 43, 22],
    chart: {
        width: 480,
        type: 'pie',
    },
    labels: ['Team A', 'Team B', 'Team C', 'Team D', 'Team E'],
    responsive: [{
        breakpoint: 480,
        options: {
            chart: {
                width: 200
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

let chart4 = new ApexCharts(document.querySelector("#pie-chart"), pie_chart);
chart4.render();


let radar_chart = {
    series: [{
        name: 'Series 1',
        data: [80, 50, 30, 40, 100, 20],
    }],
    chart: {
        height: 450,
        type: 'radar',
    },
    xaxis: {
        categories: ['January', 'February', 'March', 'April', 'May', 'June']
    }
};

let chart5 = new ApexCharts(document.querySelector("#radar-chart"), radar_chart);
chart5.render();


let polar_chart = {
    series: [14, 23, 21, 17, 15, 10, 12, 17, 21],
    chart: {
        height: 450,
        type: 'polarArea',
    },
    stroke: {
        colors: ['#fff']
    },
    fill: {
        opacity: 0.8
    },
    responsive: [{
        breakpoint: 480,
        options: {
            chart: {
                width: 200
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

let chart6 = new ApexCharts(document.querySelector("#polar-chart"), polar_chart);
chart6.render();