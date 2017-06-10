
function getData() {
    var category = $("#category").val();
    var ids = $("#filter").val();
    var periodStart = $("#start").val();
    var periodEnd = $("#end").val();
    $.post('/statistics/' + category, { Ids: ids, PeriodStart: periodStart, PeriodEnd: periodEnd })
        .success(function (data) {
            $(data).each(function (i, e) {
                console.log(e);
                orderBarChar(e.barStatistics);
                paymentBarChar(e.barStatistics);
                makePieChart(e.pieStatistic);
                makeLineChart(e.avarageBills);
            });
        });

}

$(document).ready(function () {
    getData();
});


var colors = ['#61D2D6', '#EA3556', '#ED146F', '#EDDE45', '#F98903', '#D31996', '#19DD89', '#4C3232', '#693E52', '#BADF96', '#7EB19F', '#2E4750'];
var colorNum = 0;
function getRandomColor() {
    if (colorNum > colors.length - 1)
        colorNum = 0;
    var color = colors[colorNum];
    colorNum++;
    return color;
}

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}


function getDataOrders(barStatistics, labels, dataDone, dataActive, dataDecline) {
    $(barStatistics).each(function (i, e) {
        labels.push(e.label);
        dataDone.push(e.orderItems.done);
        dataActive.push(e.orderItems.active);
        dataDecline.push(e.orderItems.declined);
    });
}

function getDataPayments(barStatistics, labels, dataBalance, dataPrice) {
    $(barStatistics).each(function (i, e) {
        labels.push(e.label);
        dataBalance.push(e.paymentItems.balanceAmount);
        dataPrice.push(e.paymentItems.priceAmount);
    });
}

function getBarData(barData, field) {
    var data = [];
    $(barData).each(function (i, e) {
        data.push(e[field]);
    });
    return data;
}

function getLineDates(lineData) {
    var dates = [];
    $(lineData).each(function (i, e) {
        dates.push(moment(e.date).format('YYYY-MM-DD'));
    });
    return dates;
}

function getContext(container) {
    var can = $("#" + container);
    var data = can.data();
    var canClass = can.attr('class');
    var parent = $('#' + container).parent('div');
    parent.empty();
    var canvas = $("<canvas>").attr('id', container).data(data).attr('class', canClass);
    parent.append(canvas);
    return document.getElementById(container).getContext('2d');
}

function orderBarChar(barStatistics) {
    var titleLabel = $('#orderBarChar').data('title');
    var doneLabel = $("#orderBarChar").data("done");
    var activeLabel = $("#orderBarChar").data("active");
    var declinedLabel = $("#orderBarChar").data("declined");

    var ctx = getContext('orderBarChar');
    var labels = [];
    var dataDone = [];
    var dataActive = [];
    var dataDecline = [];
    getDataOrders(barStatistics, labels,dataDone, dataActive, dataDecline);

    var tableData = [];
    $(barStatistics).each(function (i, e) {
        tableData.push(e.orderItems);
    });

    $("#orderTable").bootstrapTable("load", tableData);

    var char = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: doneLabel,
                    backgroundColor: getRandomColor(),
                    data: dataDone
                }, {
                    label: activeLabel,
                    backgroundColor: getRandomColor(),
                    data: dataActive
                }, {
                    label: declinedLabel,
                    backgroundColor: getRandomColor(),
                    data: dataDecline
                }
            ]
        },
        options: {
            title: {
                display: true,
                text: titleLabel
            },
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

function paymentBarChar(barStatistics) {
    var titleLabel = $('#paymentBarChar').data('title');
    var balanceLabel = $('#paymentBarChar').data('balance');
    var priceLabel = $('#paymentBarChar').data('price');

    var tableData = [];
    $(barStatistics).each(function(i, e) {
        tableData.push(e.paymentItems);
    });

    $("#paymentTable").bootstrapTable("load", tableData);

    var ctx = getContext('paymentBarChar');
    var labels = [];
    var dataBalance = [];
    var dataPrice = [];
    getDataPayments(barStatistics, labels, dataBalance, dataPrice);

    var char = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: balanceLabel,
                    backgroundColor: getRandomColor(),
                    data: dataBalance
                },
                {
                    label: priceLabel,
                    backgroundColor: getRandomColor(),
                    data: dataPrice
                }
            ]
        },
        options: {
            title: {
                display: true,
                text: titleLabel
            },
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

function makePieChart(pieData) {
    var doneLabel = $("#orderBarChar").data("done");
    var activeLabel = $("#orderBarChar").data("active");
    var declinedLabel = $("#orderBarChar").data("declined");
    var ctx = getContext('pieChart');
    ctx.height = 255;
    ctx.width = 255;
    var config = {
        type: 'pie',
        data: {
            datasets: [{
                data: [pieData.done, pieData.active, pieData.declined],
                backgroundColor: [getRandomColor(), getRandomColor(), getRandomColor()],
                label: 'Dataset 1'
            }],
            labels: [doneLabel, activeLabel, declinedLabel]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    };

    var skills = new Chart(ctx, config);
}

function makeLineChart(lineData) {
    var dateLabel = $('#lineChart').data('date');
    var valueLabel = $('#lineChart').data('value');
    var balanceLabel = $('#lineChart').data('avarage-balance');
    var priceLabel = $('#lineChart').data('avarage-price');

    var ctx = getContext('lineChart');

    var config = {
        type: 'line',
        data: {
            labels: getLineDates(lineData, "date"),
            datasets: [{
                label: balanceLabel,
                backgroundColor: window.chartColors.red,
                borderColor: window.chartColors.red,
                data: getBarData(lineData, "balance"),
                fill: false
            }, {
                label: priceLabel,
                fill: false,
                backgroundColor: window.chartColors.blue,
                borderColor: window.chartColors.blue,
                data: getBarData(lineData, "price")
            }]
        },
        options: {
            responsive: true,
            tooltips: {
                mode: 'index',
                intersect: false
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
                        labelString: dateLabel
                    }
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: valueLabel
                    }
                }]
            }
        }
    };

    var line = new Chart(ctx, config);
}