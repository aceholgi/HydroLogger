(function (HydroLogger)
{
    HydroLogger.Statistics = {
        dateRange: {},

        Init: function ()
        {
            //Position DropDown
            HydroLogger.Common.Post('GetAllPositions', null, function (result) { HydroLogger.Statistics.FillPositionDropDown(JSON.parse(result)); });
            HydroLogger.Statistics.AddDateRangePicker();
        },
        FillPositionDropDown: function (jsonData)
        {
            let dropDown = document.getElementById("positionDropDown");

            Array.prototype.forEach.call(jsonData, function (data, index)
            {
                let option = document.createElement("option");
                option.value = data;
                option.text = data;

                dropDown.add(option);
            })
        },
        AddDateRangePicker: function ()
        {
            $('#dateRangePicker').dateRangePicker({
                autoClose: true,
                showShortcuts: true,
                format: 'DD.MM.YYYY',  //more formats at http://momentjs.com/docs/#/displaying/format/
                time: {
                    enabled: false
                },
                shortcuts:
                {
                    'prev-days': [3, 5, 7],
                    'prev': ['week', 'month', 'year'],
                    'next-days': null,
                    'next': null
                }
            })
                .bind('datepicker-change', function (event, obj)
                {
                    HydroLogger.Statistics.dateRange = obj;

                    /* This event will be triggered when second date is selected */
                    //console.log('change', obj);
                    // obj will be something like this:
                    // {
                    // 		date1: (Date object of the earlier date),
                    // 		date2: (Date object of the later date),
                    //	 	value: "2013-06-05 to 2013-06-07"
                    // }
                })
        },
        GetChartData: function ()
        {
            let data = {};
            data["Positions"] = document.getElementById("positionDropDown").value;
            data["FromDate"] = moment(HydroLogger.Statistics.dateRange.date1).toISOString();
            data["ToDate"] = moment(HydroLogger.Statistics.dateRange.date2).toISOString();

            HydroLogger.Common.Post('GetChartData', "{data:'" + JSON.stringify(data) + "'}", function (result) { HydroLogger.Common.CreateCharts(JSON.parse(result), document.getElementById('allChartContainer'), 'day'); });
        }
    }
})(window.HydroLogger = window.HydroLogger || {})