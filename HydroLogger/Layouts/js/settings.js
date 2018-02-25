(function (HydroLogger)
{
    HydroLogger.Settings = {

        Init: function (data)
        {
            HydroLogger.Settings.Load();            
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

            Array.prototype.forEach.call(rows, function (row, index)
            {
                let idPositionData = {};

                let inputs = row.getElementsByTagName('input');
                idPositionData['UploaderId'] = inputs[0].value;
                idPositionData['Position'] = inputs[1].value;

                data.push(idPositionData);
            });

            HydroLogger.Common.Post('SaveUploaderConfig', "{data:'" + JSON.stringify(data) + "'}", new function () { alert('Erfolgreich gespeichert!') })
        },
        RemoveEmptyRows: function (table)
        {
            let rows = table.getElementsByTagName('tr');

            let rowsToRemove = [];

            Array.prototype.forEach.call(rows, function (row, index)
            {
                let inputs = row.getElementsByTagName('input');
                let remainingRows = table.getElementsByTagName('tr');

                if (inputs[0].value == '' && inputs[1].value == '')
                    rowsToRemove.push(row)
            });

            if (rows.length == rowsToRemove.length)
                rowsToRemove.pop();

            Array.prototype.forEach.call(rowsToRemove, function (row, index)
            {
                table.removeChild(row);
            });
        },
        HighlightInvalidFileds: function (table)
        {
            let inputs = table.getElementsByTagName('input');    //HTML Collection to array
            let inputValues = [];

            let isValid = true;

            for (let i = 0; i < inputs.length; i++)
                inputValues.push(inputs[i].value);

            Array.prototype.forEach.call(inputs, function (input, index)
            {
                let isInputValid = true;

                if (input.value == '' || !(/\S/.test(input.value)))
                    isInputValid = false;

                if (inputValues.indexOf(input.value) != inputValues.lastIndexOf(input.value)) //identische Felder
                    isInputValid = false;

                if (isInputValid)
                {
                    input.classList = '';
                }
                else
                {
                    input.classList = 'error';
                    isValid = false;
                }
            });

            return isValid;
        },
        Load: function ()
        {
            HydroLogger.Common.Post('LoadUploaderConfig', null, function (result) { HydroLogger.Settings.Fill(JSON.parse(result)); });
        },
        Fill: function (configData)
        {
            Array.prototype.forEach.call(configData, function (data, index)
            {
                HydroLogger.Settings.AddRow(data['UploaderId'], data['Position']);
            });
            HydroLogger.Settings.AddRow();
        }
    }
})(window.HydroLogger = window.HydroLogger || {})