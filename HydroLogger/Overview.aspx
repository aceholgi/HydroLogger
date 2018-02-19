<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="HydroLogger.Overview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Temperaturübewachung</title>

    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet" />
    <link href="Layouts/css/overview.css" type="text/css" rel="stylesheet" />
    <link href="Layouts/css/chartist.css" rel="stylesheet" />
    <link href="Images/favicon.png" rel="icon" />
</head>

<body>
    <div class="menu-bar">
        <div class="menu-item"><span>Übersicht</span></div>
        <div class="menu-item"><span>Statistiken</span></div>
        <div class="menu-item"><span>Einstellungen</span></div>
    </div>

    <div id="chartContainer"></div>

</body>
</html>

<script src="Layouts/js/chartist.js"></script>
<script src="Layouts/js/overview.js"></script>

<script type="text/javascript">
    var jsonData = <%=JsonData%>;
    CreateCharts(jsonData);
</script>
