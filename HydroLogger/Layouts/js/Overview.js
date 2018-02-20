function CreateCharts(data)
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
            CreateChart(data[i]['Name'], dates, temperatures, humiditys);
    }
}

function CreateChart(name, dates, temperatures, humiditys)
{
    let labels = beautifyDates(dates, 10);

    let dataTemperatures = {
        labels: labels,
        series: [
            temperatures
        ]
    };

    let dataHumiditys = {
        labels: labels,
        series: [
            humiditys
        ]
    };

    let options = {
        //high: 100,
        //low: 0,
        //showPoint: false,
        axisX: {
            showGrid: false
        }
    };

    let container = document.getElementById('chartContainer');

    let dualChartContainer = document.createElement('div');
    dualChartContainer.classList = 'dual-chart-container';

    //  Temperature
    let temperatureChart = document.createElement('div');
    temperatureChart.id = 'chartTemperature' + name;
    temperatureChart.classList = 'temperature-chart ct-chart ct-double-octave';

    let temperatureDescription = document.createElement('span');
    temperatureDescription.classList = 'chart-name';
    temperatureDescription.innerText = name + " (°C)";

    let temperatureContainer = document.createElement('div');
    temperatureContainer.classList = 'chart-container';
    temperatureContainer.appendChild(temperatureDescription);
    temperatureContainer.appendChild(temperatureChart);

    //  Humidity
    let humidityChart = document.createElement('div');
    humidityChart.id = 'chartHumidity' + name;
    humidityChart.classList = 'humidity-chart ct-chart ct-double-octave';

    let humidityDescription = document.createElement('span');
    humidityDescription.classList = 'chart-name';
    humidityDescription.innerText = name + ' (%)';

    let humidityContainer = document.createElement('div');
    humidityContainer.classList = 'chart-container';
    humidityContainer.appendChild(humidityDescription);
    humidityContainer.appendChild(humidityChart);

    //  Appending
    dualChartContainer.appendChild(temperatureContainer);
    dualChartContainer.appendChild(humidityContainer);

    container.appendChild(dualChartContainer);

    new Chartist.Line('#chartTemperature' + name, dataTemperatures, options)
    new Chartist.Line('#chartHumidity' + name, dataHumiditys, options)
}

function beautifyDates(dates, spacing)
{
    let newDates = [];

    for (let i = 0; i < dates.length; i++)
    {
        if (i % spacing == 0)
        {
            let d = new Date(dates[i]);
            let hours = d.getHours();
            let minutes = d.getMinutes();

            if (hours == 0)
                hours = '00';
            if (minutes == 0)
                minutes = '00';

            newDates.push(hours + ':' + minutes);
        }
        else
            newDates.push('');
    }
    return newDates;
}