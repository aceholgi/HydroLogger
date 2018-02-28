(function (HydroLogger)
{
    HydroLogger.Common = {
        Post: function (functionName, data, callback)
        {
            let result = '';

            $.ajax({
                type: "POST",
                url: "/Pages/Services.aspx/" + functionName,
                data: data,//"{data:" + JSON.stringify(data) + "}",//JSON.stringify(data),     //'{ data: "test" }'
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: true,
                success: function (response)
                {
                    if (callback)
                        callback(response.d);
                    result = response.d;
                }
            });
            return result;
        },
        CreateCharts: function (dataArray, container, unit)
        {
            Array.prototype.forEach.call(dataArray, function (data, index)
            {
                let humiditys = [];
                let temperatures = [];
                let dates = [];

                for (let j = 0; j < data['HumitureItems'].length; j++)
                {
                    dates.push(data['HumitureItems'][j]['Date']);
                    temperatures.push(data['HumitureItems'][j]['Temperature']);
                    humiditys.push(data['HumitureItems'][j]['Humidity']);
                }
                //dont render empty charts
                if (humiditys.length > 0)
                    HydroLogger.Common.CreateChart(container,data['Name'], dates, HydroLogger.Common.BuildDataObjects(temperatures, dates), HydroLogger.Common.BuildDataObjects(humiditys, dates), unit);
            });
        },
        CreateChart: function (container, name, dates, temperatures, humiditys, unit)
        {
            let canvasHeight = 80;

            //Temperature
            let temperatureContainer = document.createElement('div');
            temperatureContainer.classList = 'chart-container';

            temperatureCanvas = document.createElement('canvas');
            temperatureCanvas.id = 'chart-temperature' + name;
            temperatureCanvas.height = canvasHeight;

            temperatureContainer.appendChild(temperatureCanvas);
            container.appendChild(temperatureContainer);

            //Humidity
            let humidityContainer = document.createElement('div');
            humidityContainer.classList = 'chart-container';

            humidityCanvas = document.createElement('canvas');
            humidityCanvas.id = 'chart-humidity' + name;
            humidityCanvas.height = canvasHeight;

            humidityContainer.appendChild(humidityCanvas);
            container.appendChild(humidityContainer);

            let ctxTemperature = document.getElementById('chart-temperature' + name).getContext('2d');
            let ctxHumidity = document.getElementById('chart-humidity' + name).getContext('2d');

            let options = {
                scales: {
                    maintainAspectRatio: false,
                    xAxes: [{
                        type: 'time',
                        distribution: 'linear',
                        time: {
                            unit: unit,
                            unitStepSize: 1,
                            //http://momentjs.com/docs/#/displaying/format/
                            displayFormats: {
                                'hour': 'HH:mm', // Sept 4, 5PM
                                'day': 'DD.MM', // Sep 4 2015
                                'week': 'll', //46
                                'month': 'MM YYYY',
                            }
                        }
                    }]
                },
            };

            let chartTemperature = new Chart(ctxTemperature, {
                type: 'line',
                data: {
                    datasets: [{
                        label: name + ' Temperatur',
                        data: temperatures,
                        backgroundColor: 'green',
                        borderColor: 'green',
                        lineTension: 0,
                        fill: false
                    }]
                },
                options: options
            });

            let chartHumidity = new Chart(ctxHumidity, {
                type: 'line',
                data: {
                    datasets: [{
                        label: name + ' Luftfeuchtigkeit',
                        data: humiditys,
                        backgroundColor: 'blue',
                        borderColor: 'blue',
                        lineTension: 0,
                        fill: false,
                    }]
                },
                options: options
            });
        },
        BuildDataObjects: function (values, dates)
        {
            let ret = [];

            Array.prototype.forEach.call(values, function (data, index)
            {
                let obj = {};
                obj['t'] = moment(dates[index]);
                obj['y'] = values[index];

                ret.push(obj)
            });

            return ret;
        }
    }
})(window.HydroLogger = window.HydroLogger || {})