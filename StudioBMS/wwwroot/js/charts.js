
function getData() {
    var category = $("#category").val();
    var ids = $("#filter").val();
    var periodStart = $("#start").val();
    var periodEnd = $("#end").val();
    if (category === "customers") {
        $.post('/statistics/customers', { Ids: ids, PeriodStart: periodStart, PeriodEnd: periodEnd })
            .success(function (data) {
                $(data).each(function (i, e) {
                    $(e.barStatistics).each(function (ind, elm) {
                        orderBarChar(elm.label, elm.orderItems);
                    });
                });
            });
    }
}

$(document).ready(function () {
    getData();
});

function orderBarChar(label, barData) {
    console.log(barData);
    console.log(barData[0].active);
    var doneLabel = $("#orderBarChar").data("done");
    var activeLabel = $("#orderBarChar").data("active");
    var declinedLabel = $("#orderBarChar").data("declined");
    var ctx = document.getElementById("orderBarChar").getContext('2d');

    var labels = [];
    var data = [];

    $("#table").bootstrapTable("load", barData);


    $(barData).each(function(i, e) {
        labels.push(e.label);
        data.push(e);
    });

    var char = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: doneLabel,
                    backgroundColor: "#3e95cd",
                    data: [barData[0].done]
                }, {
                    label: activeLabel,
                    backgroundColor: "#8e5ea2",
                    data: [barData[0].active]
                }, {
                    label: declinedLabel,
                    backgroundColor: "#8e5ea2",
                    data: [barData[0].declined]
                }
            ]
        },
        options: {
            title: {
                display: true,
                text: 'Статистика замовлень'
            }
        }
    });

    //var chart = new Chart(ctx, {
    //    type: 'bar',
    //    data: {
    //        labels: ["Stepanov", "Blue", "Yellow",/* "Green", "Purple", "Orange"*/],
    //        datasets: [{
    //            label: 'Active',
    //            data: [barData[0].active,2,3],
    //            backgroundColor: [
    //                'rgba(255, 99, 132, 0.2)',
    //                //'rgba(54, 162, 235, 0.2)',
    //                //'rgba(255, 206, 86, 0.2)',
    //                //'rgba(75, 192, 192, 0.2)',
    //                //'rgba(153, 102, 255, 0.2)',
    //                //'rgba(255, 159, 64, 0.2)'
    //            ],
    //            borderColor: [
    //                'rgba(255,99,132,1)',
    //                //'rgba(54, 162, 235, 1)',
    //                //'rgba(255, 206, 86, 1)',
    //                //'rgba(75, 192, 192, 1)',
    //                //'rgba(153, 102, 255, 1)',
    //                //'rgba(255, 159, 64, 1)'
    //            ],
    //            borderWidth: 1
    //        },
    //            {
    //                label: "Declined",
    //                data: [barData[0].declined, 4, 3],
    //                backgroundColor: ['rgba(255, 206, 86, 0.2)'],
    //                borderColor: ['rgba(255, 206, 86, 1)'],
    //                borderWidth: 1
    //            },
    //            {
    //                label: "Done",
    //                data: [barData[0].done, 7, 2],
    //                backgroundColor: ['rgba(255, 206, 86, 1)'],
    //                borderColor: ['rgba(255, 159, 64, 0.2)'],
    //                borderWidth: 1
    //            }
    //        ]
    //    },
    //    options: {
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    beginAtZero: true}
    //            }]
    //        }
    //    }
    //});
}

//var ctx = document.getElementById("myChart").getContext('2d');
//makeChart(ctx);
//ctx = document.getElementById("myChart1").getContext('2d');
//makePieChart(ctx);
//ctx = document.getElementById("myChart2").getContext('2d');
//makeLineChart(ctx);
//ctx = document.getElementById("myChart3").getContext('2d');
//makeChart(ctx);
function makeChart(ctx) {
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
            datasets: [{
                label: '# of Votes',
                data: [12, 19, 3, 5, 2, 3],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
function randomScalingFactor() {
    return Math.round(Math.random() * 100);
};

function makePieChart(ctx) {

    var config = {
        type: 'pie',
        data: {
            datasets: [{
                data: [
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                ],
                backgroundColor: [
                    window.chartColors.red,
                    window.chartColors.orange,
                    window.chartColors.yellow,
                    window.chartColors.green,
                    window.chartColors.blue,
                ],
                label: 'Dataset 1'
            }],
            labels: [
                "Red",
                "Orange",
                "Yellow",
                "Green",
                "Blue"
            ]
        },
        options: {
            responsive: true
        }
    };

    var skills = new Chart(ctx, config);
}

function makeLineChart(ctx) {
    var MONTHS = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var config = {
        type: 'line',
        data: {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [{
                label: "My First dataset",
                backgroundColor: window.chartColors.red,
                borderColor: window.chartColors.red,
                data: [
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor()
                ],
                fill: false,
            }, {
                label: "My Second dataset",
                fill: false,
                backgroundColor: window.chartColors.blue,
                borderColor: window.chartColors.blue,
                data: [
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor()
                ],
            }]
        },
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Chart.js Line Chart'
            },
            tooltips: {
                mode: 'index',
                intersect: false,
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Month'
                    }
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Value'
                    }
                }]
            }
        }
    };

    var line = new Chart(ctx, config);
}