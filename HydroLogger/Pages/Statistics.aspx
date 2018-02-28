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
    <select id="positionDropDown"></select>
    <input id="dateRangePicker" class="date-range-picker" />
    <input type="button" value="Request" onclick="HydroLogger.Statistics.GetChartData()" />
    <br />
    <div id="allChartContainer"></div>
</asp:Content>
