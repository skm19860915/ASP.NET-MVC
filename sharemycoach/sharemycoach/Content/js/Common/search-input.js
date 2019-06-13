$(document).ready(function () {
    

    // on the webpage
    $("#btn-vehicle-search-on-web").click(function (e) {
        var key = $('#input-vehicle-search-on-web').val();
        var default_search_status = sessionStorage.getItem("default_search");
        getVehicleInformation(key, true, default_search_status);
    });
    $("#input-vehicle-search-on-web").keyup(function (e) {
        if (e.which == 13) {
            var key = $('#input-vehicle-search-on-web').val();
            var default_search_status = sessionStorage.getItem("default_search");
            getVehicleInformation(key, true, default_search_status);
        }
    });

    // on the phone
    $("#btn-vehicle-search-on-mobile").click(function (e) {
        var key = $('#input-vehicle-search-on-mobile').val();
        getVehicleInformation(key, false, 0);
    });
    $("#input-vehicle-search-on-mobile").keyup(function (e) {
        if (e.which == 13) {
            var key = $('#input-vehicle-search-on-mobile').val();
            getVehicleInformation(key, false, 0);
        }
    });
});

function getVehicleInformation(key, isWeb, status)
{
    if (key)
    {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: _g_webapi_url + "search/" + key + "?token=AIzaSyAJSErr4vFLsOoylqQYkfQLKM26lnsHLXY",
            success: function (data)
            {
                var jsonResult = jQuery.parseJSON(JSON.stringify(data));
                if (jsonResult)
                {
                    if (status == 1) {
                        location.href = _g_website_url + "CheckOut/" + jsonResult;
                    }
                    else {
                        location.href = _g_website_url + "VehicleDetail/" + jsonResult;
                    }
                }
                else
                {
                    if (isWeb)
                    {
                        $("#search-error-status").removeClass("invisible-search-error");
                        $("#search-error-status").addClass("visible-search-error");
                    }
                    else
                    {
                        document.getElementById("quick-search-status").innerHTML = "Not Found";
                    }
                }
            },
            error: function (error)
            {
                var jsonResult = jQuery.parseJSON(error.responseText);
                console.log("error" + error.responseText);
            }
        });
    }
    else
    {
        if (isWeb)
        {
            $("#search-error-status").removeClass("invisible-search-error");
            $("#search-error-status").addClass("visible-search-error");
        }
        else
        {
            document.getElementById("quick-search-status").innerHTML = "Not Found";
        }
    }
}
