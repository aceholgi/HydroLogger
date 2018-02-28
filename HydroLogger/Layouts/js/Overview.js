(function (HydroLogger)
{
    HydroLogger.Overview = {
        Init: function ()
        {
            let data = {};
            data["Positions"] = "";
            data["FromDate"] = moment().add(-1, 'days').toISOString();
            data["ToDate"] = moment().toISOString();

            HydroLogger.Common.Post('GetChartData', "{data:'" + JSON.stringify(data) + "'}", function (result) { HydroLogger.Common.CreateCharts(JSON.parse(result), document.getElementById('allChartContainer'), 'hour'); });
        }
    }
})(window.HydroLogger = window.HydroLogger || {})