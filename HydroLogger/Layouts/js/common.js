(function (HydroLogger)
{
    HydroLogger.Common = {
        Post: function (functionName, data)
        {
            $.ajax({
                type: "POST",
                url: "/Pages/Services.aspx/" + functionName,
                data: data,     //'{ data: "test" }'
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response)
                {
                    return response.d;
                }
            });
        }
    }
})(window.HydroLogger = window.HydroLogger || {})