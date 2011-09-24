/// <reference path="jquery-1.6.4-vsdoc.js" />
/// <reference path="jquery-ui-1.8.16.js" />
/// <reference path="jquery.tmpl.js" />

$(document).ready(function () {

    $(":input[data-datepicker]").datepicker();

    $(":input[data-autocomplete]").each(function () {
        $(this).autocomplete({ source: $(this).attr("data-autocomplete") });
    });

    $("#searchForm").each(function () {
        $(this).submit(function () {
            $.getJSON(
                $(this).attr("action"),
                $(this).serialize(), function (data) {
                    var result = $("#searchTemplate").tmpl(data);
                    $("#searchResults").empty().append(result);
                });

            return false;
        });
    });
})