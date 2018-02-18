<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="HydroLogger._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Temperaturübewachung</title>

    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet" />
    <link href="Layouts/css/cd.css" type="text/css" rel="stylesheet" />    
    <link href="Layouts/css/chartist.css" rel="stylesheet"  />
    <link href="Images/favicon.png" rel="icon" />
</head>

<body>
    <div class="menu-bar">
        <div class="menu-item"><span>Übersicht</span></div>
        <div class="menu-item"><span>Statistiken</span></div>
        <div class="menu-item"><span>Einstellungen</span></div>
    </div>

    <div id="chart_Wohnzimmer" class="ct-chart ct-double-octave">
        <span class="chartName">Wohnzimmer</span>
    </div>

</body>
</html>

<script src="Layouts/js/chartist.js"></script>
<script type="text/javascript">

    var data = {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri'],
        series: [
            [20, 21, 19, 20, 25],   //temp
            [55, 60, 35, 30, 45]    //humid
        ]
    };

    var options = {
        high: 100,
        low: 0,
        axisX: {
            showGrid: false
        }
    };
    new Chartist.Line('#chart_Wohnzimmer', data, options);
</script>
