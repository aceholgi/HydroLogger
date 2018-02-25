﻿(function (HydroLogger)
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
        }
    }
})(window.HydroLogger = window.HydroLogger || {})