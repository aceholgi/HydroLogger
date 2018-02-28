<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="HydroLogger.Pages.Statistics" %>

<asp:Content ID="contentCss" ContentPlaceHolderID="contentCss" runat="server">
    <link href="../Layouts/css/statistics.css" type="text/css" rel="stylesheet" />
    <link href="../Layouts/css/libs/daterangepicker.min.css" type="text/css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="contentScripts" ContentPlaceHolderID="contentScripts" runat="server">
    <script src="../Layouts/js/libs/moment.min.js" defer></script>
    <script src="../Layouts/js/libs/jquery.daterangepicker.min.js" defer></script>
    <script src="../Layouts/js/libs/chart.min.js" defer></script>
    <script src="../Layouts/js/statistics.js" defer></script>

    <script>
        if (document.readyState === "complete")
            HydroLogger.Statistics.Init();
        else
            window.addEventListener("DOMContentLoaded", function () { HydroLogger.Statistics.Init() }, false);
    </script>
</asp:Content>

<asp:Content ID="contentBody" ContentPlaceHolderID="contentBody" runat="server">
    <div class="filter">
        <div class="property-container">
            <span class="label">Position:</span>
            <select id="positionDropDown"></select>
        </div>
        <div class="property-container">
            <span class="label">Datum:</span>
            <span id="dateRangePicker">
                <input id="dateRangePickerFrom" class="date-range-picker" />
                <span>Bis:</span>
                <input id="dateRangePickerTo" class="date-range-picker" />
            </span>
        </div>
        <div class="property-container">
            <input class="button-filter"type="button" value="Filtern" onclick="HydroLogger.Statistics.GetChartData()" />
        </div>
    </div>
    <br />
    <div id="allChartContainer"></div>
</asp:Content>
