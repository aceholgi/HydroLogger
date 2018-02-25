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
    <div class="settings">
<%--        <div class="slot-key">
            <span>Key: </span>
            <input type="text" id="input-key" /><input type="button" value="Authenticate" />
        </div>--%>

        <div class="slot-id-position">
            <table border="1">
                <thead>
                    <tr>
                        <th>Transmitter ID</th>
                        <th>Position</th>
                    </tr>
                </thead>
                <tbody id="IdPositionTableBody">
                </tbody>
            </table>
            <input type="button" value="+" class="button button-add" onclick="HydroLogger.Settings.AddRow();" />
            <input type="button" value="Speichern" class="button button-save" onclick="HydroLogger.Settings.Save();" />
        </div>
    </div>
</asp:Content>
