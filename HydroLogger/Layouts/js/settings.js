(function (HydroLogger)
{
    HydroLogger.Settings = {

        Init: function (data)
        {
            HydroLogger.Settings.Load();
            HydroLogger.Settings.AddRow();
        },
        GetTable: function ()
        {
            return document.getElementById('IdPositionTableBody');
        },
        AddRow: function (valueId, valuePosition)
        {
            let table = HydroLogger.Settings.GetTable();

            let row = document.createElement('tr');

            let cellId = document.createElement('td');
            let inputId = document.createElement('input');
            inputId.type = 'number';
            if (valueId)
                inputId.value = valueId;
            cellId.appendChild(inputId);

            let cellPosition = document.createElement('td');
            let inputPosition = document.createElement('input');
            if (valuePosition)
                inputPosition.value = valuePosition;
            cellPosition.appendChild(inputPosition);

            row.appendChild(cellId);
            row.appendChild(cellPosition);

            table.appendChild(row);
        },
        Save: function ()
        {
            let table = HydroLogger.Settings.GetTable();

            HydroLogger.Settings.RemoveEmptyRows(table);

            if (!HydroLogger.Settings.HighlightInvalidFileds(table))
                return; //if fields are empty or whitewspace

            let rows = table.getElementsByTagName('tr');
            let firstRowInputs = rows[0].getElementsByTagName('input');
            let data = [];

            for (let i = 0; i < rows.length; i++)
            {
                let idPositionData = {};

                let inputs = rows[i].getElementsByTagName('input');
                idPositionData['UploaderId'] = inputs[0].value;
                idPositionData['Position'] = inputs[1].value;

                data.push(idPositionData);
            }

            HydroLogger.Common.Post('SaveUploaderConfig', "{data:'" + JSON.stringify(data) + "'}", new function () { alert('Erfolgreich gespeichert!') })
        },
        RemoveEmptyRows: function (table)
        {
            let rows = table.getElementsByTagName('tr');

            let rowsToRemove = [];

            for (let i = 0; i < rows.length; i++)
            {
                let inputs = rows[i].getElementsByTagName('input');
                let remainingRows = table.getElementsByTagName('tr');

                if (inputs[0].value == '' && inputs[1].value == '')
                    rowsToRemove.push(rows[i])
            }

            if (rows.length == rowsToRemove.length)
                rowsToRemove.pop();

            for (let i = 0; i < rowsToRemove.length; i++)
                table.removeChild(rowsToRemove[i]);
        },
        HighlightInvalidFileds: function (table)
        {
            let inputs = table.getElementsByTagName('input');    //HTML Collection to array
            let inputValues = [];
            
            let isValid = true;

            for (let i = 0; i < inputs.length; i++)
                inputValues.push(inputs[i].value);

            for (let i = 0; i < inputs.length; i++)
            {
                let isInputValid = true;
                
                if (inputs[i].value == '' || !(/\S/.test(inputs[i].value)))
                    isInputValid = false;

                if (inputValues.indexOf(inputs[i].value) != inputValues.lastIndexOf(inputs[i].value)) //identische Felder
                    isInputValid = false;



                if (isInputValid)
                {
                    inputs[i].classList = '';
                }
                else
                {
                    inputs[i].classList = 'error';
                    isValid = false;
                }
            }
            return isValid;
        },
        Load: function ()
        {
            let json = HydroLogger.Common.Post('LoadUploaderConfig');

            let configData = JSON.parse(json);
            HydroLogger.Settings.Fill(configData);
        },
        Fill: function (configData)
        {
            for (let i = 0; i < configData.length; i++)
                HydroLogger.Settings.AddRow(configData[i]['UploaderId'], configData[i]['Position']);
        }
    }
})(window.HydroLogger = window.HydroLogger || {})