<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="HydroLogger._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Temperaturübewachung</title>

    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet" />
    <link href="Layouts/css/cd.css" type="text/css" rel="stylesheet" />
    <link href="Layouts/css/chartist.css" rel="stylesheet" />
    <link href="Images/favicon.png" rel="icon" />
</head>

<body>
    <div class="menu-bar">
        <div class="menu-item"><span>Übersicht</span></div>
        <div class="menu-item"><span>Statistiken</span></div>
        <div class="menu-item"><span>Einstellungen</span></div>
    </div>

    <div class="dualChartContainer">
        <span class="chartName">Wohnzimmer (°C)</span>
        <div id="chart_temp_wohnzimmer" class="tempChart ct-chart ct-double-octave"></div>
        <span class="chartName">Wohnzimmer (%)</span>
        <div id="chart_humid_wohnzimmer" class="humidChart ct-chart ct-double-octave"></div>
    </div>
</body>
</html>

<script src="Layouts/js/chartist.js"></script>
<script type="text/javascript">

    var data_temp_wohnzimmer = {
        //labels: [],
        //series: [
        //    [ ]
        //]
    };

    var data_humid_wohnzimmer = {
        //labels: [],
        //series: [
        //    [ ]
        //]
    };

    var options = {
        //high: 100,
        //low: 0,
        showPoint: false,
        axisX: {
            showGrid: false
        }
    };
    new Chartist.Line('#chart_temp_wohnzimmer', data_temp_wohnzimmer, options);
    new Chartist.Line('#chart_humid_wohnzimmer', data_humid_wohnzimmer, options);
</script>
