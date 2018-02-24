(function (HydroLogger)
{
    HydroLogger.Settings = {

        Init: function (data)
        {
            HydroLogger.Settings.AddRow();
        },
        GetTable: function ()
        {
            return document.getElementById("IdPositionTableBody");
        },
        AddRow: function ()
        {
            let table = HydroLogger.Settings.GetTable();

            let row = document.createElement("tr");

            let cellId = document.createElement("td");
            let inputId = document.createElement("input");
            inputId.type = "number";
            cellId.appendChild(inputId);

            let cellPosition = document.createElement("td");
            let inputPosition = document.createElement("input");
            cellPosition.appendChild(inputPosition);

            row.appendChild(cellId);
            row.appendChild(cellPosition);

            table.appendChild(row);
        },
        Save: function ()
        {
            let table = HydroLogger.Settings.GetTable();

            HydroLogger.Settings.RemoveEmptyRows(table);

            if (!HydroLogger.Settings.HighlightEmptyFields(table))
                return; //if fields are empty or whitewspace

            let rows = table.getElementsByTagName("tr");
            let firstRowInputs = rows[0].getElementsByTagName("input");
            let data = [];

            for (let i = 0; i < rows.length; i++)
            {
                let idPositionData = {};

                let inputs = rows[i].getElementsByTagName("input");
                idPositionData["Id"] = inputs[0].value;
                idPositionData["Position"] = inputs[1].value;

                data.push(idPositionData);
            }

            let b = "{data:'" + JSON.stringify(data) + "'}";

            console.log(data)
            console.log(b)

            HydroLogger.Common.Post('SaveUploaderPosition', b)
        },
        RemoveEmptyRows: function (table)
        {
            let rows = table.getElementsByTagName("tr");

            let rowsToRemove = [];

            for (let i = 0; i < rows.length; i++)
            {
                let inputs = rows[i].getElementsByTagName("input");
                let remainingRows = table.getElementsByTagName("tr");

                if (inputs[0].value == '' && inputs[1].value == '')
                    rowsToRemove.push(rows[i])
            }

            if (rows.length == rowsToRemove.length)
                rowsToRemove.pop();

            for (let i = 0; i < rowsToRemove.length; i++)
                table.removeChild(rowsToRemove[i]);
        },
        HighlightEmptyFields: function (table)
        {
            let inputs = table.getElementsByTagName("input");
            let isValid = true;

            for (let i = 0; i < inputs.length; i++)
                if (inputs[i].value == '' || !(/\S/.test(inputs[i].value)))
                {
                    inputs[i].classList = 'error';
                    isValid = false;
                }
                else
                    inputs[i].classList = '';

            return isValid;
        }
    }
})(window.HydroLogger = window.HydroLogger || {})