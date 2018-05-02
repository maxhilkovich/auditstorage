$(function() {

    $(".datepicker").datepicker({
        format: "mm/dd/yyyy",
        message: "Wrong format",
        startView: 1,
        todayBtn: "linked",
        daysOfWeekHighlighted: "0,6",
        autoclose: true,
        todayHighlight: true,
        weekStart: 1
    });

});
