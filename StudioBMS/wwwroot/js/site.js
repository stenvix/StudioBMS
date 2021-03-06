﻿// Write your Javascript code.

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
        var minDate = $(e).data("min-date");
        var maxDate = $(e).data("max-date");
        var format = $(e).data("format");

        var config = {};
        config.format = format;
        config.locale = locale;

        datepicker.datetimepicker(config);

        if (minDate) {
            $(e).data("DateTimePicker").minDate(moment(minDate));
        }
        if (maxDate) {
            $(e).data("DateTimePicker").maxDate(moment(maxDate));
        }

        var dayOfWeek = $("#dayofweek");
        dayOfWeek.on("change", function () {
            var selected = dayOfWeek.find(":selected");
            var minDate = selected.data("min-date");
            var maxDate = selected.data("max-date");
            console.log(minDate);
            console.log(maxDate);
            if (minDate && maxDate) {
                $('#startTime').data("DateTimePicker").minDate(minDate);
                $('#endTime').data("DateTimePicker").minDate(minDate);
                $('#startTime').data("DateTimePicker").maxDate(maxDate);
                $('#endTime').data("DateTimePicker").maxDate(maxDate);
            }
        });
    });
}

function InitOrderForm() {
    var workers = "#workers";
    var services = "#services";
    $("#workshops").change(function () {
        var workshopId = $(this).find(":selected").val();
        if (workshopId !== "None") {
            $.post('/workers/json', { "workshopId": workshopId }).success(function (data) {
                $(workers).empty();
                $(services).empty();
                $(data).each(function (i, e) {
                    $(workers).append($("<option>").attr("value", e.id).text(e.fullName + " ("+ e.role.localizedName+")"));
                });
                $(workers).trigger("chosen:updated");
                $(workers).val("");
                $(workers).trigger("chosen:updated");
                $(services).trigger("chosen:updated");
            });
        } else {
            $(workers).empty();
            $(services).empty();
            $(workers).trigger("chosen:updated");
            $(services).trigger("chosen:updated");
        }

    });

    $(workers).change(function () {
        var workerId = $(this).find(":selected").val();
        $.post('/services/json', { "workerId": workerId })
            .success(function (data) {
                $(services).empty();
                $(data).each(function (i, e) {
                    $(services).append($("<option>").val(e.id).text(e.title));
                });
                $(services).trigger("chosen:updated");
            });
        $.post('/workers/time', { "workerId": workerId }).success(function (data) {
            //$('#orderTime')
            console.log(data);
            var disabledTimespans = [];
            $(data.disabledTimespans).each(function (i, e) {
                disabledTimespans.push([moment(e.start), moment(e.end)]);
            });
            var picker = $("#orderTime").data("DateTimePicker");
            picker.daysOfWeekDisabled(data.disabledDays);
            picker.disabledTimeIntervals(disabledTimespans);
        });
    });
}
if ($.validator) {
    $.validator.setDefaults({ ignore: ".ignore" });
}
$(document).ready(function () {

    UpdateDatetimePickers();
    InitOrderForm();

    var items = $(".chosen");
    items.each(function (i, e) {
        var selected_options = $(e).data("selmax");

        if (!selected_options) {
            selected_options = 0;
        }

        var select = $(e).find(":selected").val();

        $(e).chosen({
            allow_single_deselect: true,
            inherit_select_classes: true,
            disable_search_threshold: 10,
            display_disabled_options: false,
            display_selected_options: false,
            max_selected_options: selected_options,
            no_results_text: "Не знайдено",
            placeholder_text_multiple: "Виберіть декілька позицій",
            placeholder_text_single: "Виберіть дані"
        });

        if (select === "None") {
            $(e).val("");
            $(e).trigger("chosen:updated");
        }
    });
})