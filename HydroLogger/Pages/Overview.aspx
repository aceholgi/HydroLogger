<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master.Master" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="HydroLogger.Pages.Overview" %>

<asp:Content ID="contentCss" ContentPlaceHolderID="contentCss" runat="server">
    <link href="../Layouts/css/chartist.css" type="text/css" rel="stylesheet" />
    <link href="../Layouts/css/overview.css" type="text/css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="contentScripts" ContentPlaceHolderID="contentScripts" runat="server">
    <script src="../Layouts/js/libs/chartist.min.js" defer></script>
    <%--<script src="../Layouts/js/libs/chartist-plugin-axistitle.min.js" defer></script>
    <script src="../Layouts/js/libs/chartist-plugin-threshold.min.js" defer></script>--%>
    <script src="../Layouts/js/overview.js" defer></script>

    <script type="text/javascript">
        var jsonData = <%=JsonData%>;

        if (document.readyState === "complete")
            HydroLogger.Overview.Init(jsonData);
        else
            window.addEventListener("DOMContentLoaded", function () { HydroLogger.Overview.Init(jsonData) }, false);

    </script>
</asp:Content>

<asp:Content ID="contentBody" ContentPlaceHolderID="contentBody" runat="server">
    <div id="chartContainer"></div>
</asp:Content>
