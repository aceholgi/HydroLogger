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
                seperator: ' bis ',
                startOfWeek: 'monday',
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
                },
                getValue: function ()
                {
                    if ($('#dateRangePickerFrom').val() && $('#dateRangePickerTo').val())
                        return $('#dateRangePickerFrom').val() + ' to ' + $('#dateRangePickerTo').val();
                    else
                        return '';
                },
                setValue: function (s, s1, s2)
                {
                    $('#dateRangePickerFrom').val(s1);
                    $('#dateRangePickerTo').val(s2);
                }
            })
                .bind('datepicker-change', function (event, obj)
                {
                    HydroLogger.Statistics.dateRange = obj;
                });

            //set defaults
            document.getElementById("dateRangePickerFrom").value = "dd.mm.yyyy";
            document.getElementById("dateRangePickerTo").value = "dd.mm.yyyy";
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