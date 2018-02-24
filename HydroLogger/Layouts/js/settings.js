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
            HydroLogger.Settings.RemoveEmptyRows();
            HydroLogger.Settings.HighlightEmptyFields();
        },
        RemoveEmptyRows: function ()
        {
            let table = HydroLogger.Settings.GetTable();

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
        HighlightEmptyFields: function ()
        {
            let table = HydroLogger.Settings.GetTable();
            let inputs = table.getElementsByTagName("input");

            for (let i = 0; i < inputs.length; i++)
                if (inputs[i].value == '')
                    inputs[i].classList = 'error';
        }
    }
})(window.HydroLogger = window.HydroLogger || {})