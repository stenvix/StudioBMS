// Write your Javascript code.

window.chartColors = {
    red: 'rgb(255, 99, 132)',
    orange: 'rgb(255, 159, 64)',
    yellow: 'rgb(255, 205, 86)',
    green: 'rgb(75, 192, 192)',
    blue: 'rgb(54, 162, 235)',
    purple: 'rgb(153, 102, 255)',
    grey: 'rgb(201, 203, 207)'
};

function UpdateDatetimePickers() {
    $(".datetimepicker").each(function (i, e) {
        var datepicker = $(e);
        var locale = $(e).data("locale");
        var mindate = $(e).data("mindate");
        var format = $(e).data("format");

        var momentMinDate = null;
        if (mindate) {
            momentMinDate = moment(mindate);
        }

        var config = {};
        config.format = format;
        config.locale = locale;
        if (moment.isMoment(momentMinDate))
            config.minDate = momentMinDate;

        datepicker.datetimepicker(config);

        //if (datepicker.is("#journal-picker")) {
        //    datepicker.on("dp.change",
        //        function(date, oldDate) {
        //            window.location.replace("/journals/"+date);
        //        });
        //}
    });
}

$(document).ready(function () {

    UpdateDatetimePickers();

    var items = $(".chosen");
    items.each(function (i, e) {
        var selected_options = $(e).data("selmax");

        if (!selected_options) {
            selected_options = 0;
        }

        $(e).chosen({
            inherit_select_classes: true,
            disable_search_threshold: 10,
            display_disabled_options: false,
            display_selected_options: false,
            max_selected_options: selected_options,
            no_results_text: "NOTFOUND",
            placeholder_text_multiple: "SEVERALPOSITION",
            placeholder_text_single: "CHOSEDATA"
        });
    });
})