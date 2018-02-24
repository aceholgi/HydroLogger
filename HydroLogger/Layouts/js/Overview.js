(function (HydroLogger)
{
    HydroLogger.Overview = {
        Init: function (data)
        {
            HydroLogger.Overview.CreateCharts(data);
        },
        CreateCharts: function (data)
        {
            for (let i = 0; i < data.length; i++)
            {
                let humiditys = [];
                let temperatures = [];
                let dates = [];

                for (let j = 0; j < data[i]['HumitureItems'].length; j++)
                {
                    dates.push(data[i]['HumitureItems'][j]['Date']);
                    temperatures.push(data[i]['HumitureItems'][j]['Temperature']);
                    humiditys.push(data[i]['HumitureItems'][j]['Humidity']);
                }
                //dont render empty charts
                if (humiditys.length > 0)
                    HydroLogger.Overview.CreateChart(data[i]['Name'], dates, temperatures, humiditys);
            }
        },
        CreateChart: function (name, dates, temperatures, humiditys)
        {
            let canvasHeight = 80;

            let container = document.getElementById('allChartContainer');

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
                            unit: 'hour',
                            unitStepSize: 1,
                            //http://momentjs.com/docs/#/displaying/format/
                            displayFormats: {
                                'hour': 'HH:mm', // Sept 4, 5PM
                                'day': 'MMM Do', // Sep 4 2015
                                'week': 'll', // Week 46, or maybe "[W]WW - YYYY" ?
                                'month': 'MMM YYYY', // Sept 2015
                                'year': 'YYYY', // 2015
                            }
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            labelString: 'Temperatur'
                        },

                    }]
                },
            };

            let chartTemperature = new Chart(ctxTemperature, {
                type: 'line',
                data: {
                    datasets: [{
                        label: name + ' Temperatur',
                        data: HydroLogger.Overview.PrepareData(temperatures, dates),
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
                        data: HydroLogger.Overview.PrepareData(humiditys, dates),
                        backgroundColor: 'blue',
                        borderColor: 'blue',
                        lineTension: 0, 
                        fill: false,
                    }]
                },
                options: options
            });

            /*
blue
:
"rgb(54, 162, 235)"
green
:
"rgb(75, 192, 192)"
grey
:
"rgb(201, 203, 207)"
orange
:
"rgb(255, 159, 64)"
purple
:
"rgb(153, 102, 255)"
red
:
"rgb(255, 99, 132)"
yellow
:
"rgb(255, 205, 86)"
            */

        },
        PrepareData: function (values, dates)
        {
            let ret = [];

            for (let i = 0; i < values.length; i++)
            {
                let obj = {};
                obj['t'] = moment(parseInt(dates[i].substring(6, dates[i].length - 2))); //new Date(parseInt(dates[i].substring(6, dates[i].length - 2)));
                obj['y'] = values[i];

                ret.push(obj)
            }

            return ret;
        }
    }
})(window.HydroLogger = window.HydroLogger || {})