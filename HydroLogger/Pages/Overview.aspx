<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master.Master" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="HydroLogger.Pages.Overview" %>

<asp:Content ID="contentCss" ContentPlaceHolderID="contentCss" runat="server">
    <link href="../Layouts/css/overview.css" type="text/css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="contentScripts" ContentPlaceHolderID="contentScripts" runat="server">
    <script src="../Layouts/js/libs/moment.min.js" defer></script>
    <script src="../Layouts/js/libs/chart.min.js" defer></script>
    <script src="../Layouts/js/overview.js" defer></script>

    <script type="text/javascript">
        if (document.readyState === "complete")
            HydroLogger.Overview.Init();
        else
            window.addEventListener("DOMContentLoaded", function () { HydroLogger.Overview.Init();}, false);
    </script>
</asp:Content>

<asp:Content ID="contentBody" ContentPlaceHolderID="contentBody" runat="server">
    <div id="allChartContainer"></div>
</asp:Content>
