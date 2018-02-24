<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HydroLogger.Pages.Settings" %>

<asp:Content ID="contentCss" ContentPlaceHolderID="contentCss" runat="server">
    <link rel="stylesheet" href="../Layouts/css/settings.css" />
</asp:Content>
<asp:Content ID="contentScripts" ContentPlaceHolderID="contentScripts" runat="server">
    <script src="../Layouts/js/settings.js" type="text/javascript" defer></script>

    <script>
        if (document.readyState === "complete")
            HydroLogger.Settings.Init();
        else
            window.addEventListener("DOMContentLoaded", function () { HydroLogger.Settings.Init() }, false);
    </script>
</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="contentBody" runat="server">

    <table border="1" class="idpos-table">
        <thead>
            <tr>
                <th>Transmitter ID</th>
                <th>Position</th>
            </tr>
        </thead>
        <tbody id="IdPositionTableBody">
        </tbody>
    </table>
    <input type="button" value="Speichern" onclick="HydroLogger.Settings.Save();" />
    <input type="button" value="Reihe hinzufügen" onclick="HydroLogger.Settings.AddRow();" />
</asp:Content>
