// let category_options = {
//     series: [44, 55, 41, 17],
//     labels: ['Cloths', 'Devices', 'Bags', 'Watches'],
//     chart: {
//         type: 'donut',
//     },
//     colors: ['#6ab04c', '#2980b9', '#f39c12', '#d35400']
// }

// let category_chart = new ApexCharts(document.querySelector("#category-chart"), category_options)
// category_chart.render()


let customer_options = {
    series: [{
        name: "Complete",
        data: [40, 70, 20, 90, 36, 80, 30, 91, 60]
    }, {
        name: "Doing",
        data: [10, 30, 50, 20, 76, 40, 20, 51, 10]
    }],
    colors: ['#3C21F7', '#FFCA1F'],
    chart: {
        height: 300,
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


}

let customer_chart = new ApexCharts(document.querySelector("#customer-chart"), customer_options)
customer_chart.render()