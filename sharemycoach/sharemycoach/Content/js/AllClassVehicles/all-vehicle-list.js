$(document.body).on("mouseenter", "img.sale-img", function () {
    $(this).attr('src', '../../Content/images/star2.png');
});
$(document.body).on("mouseleave", "img.sale-img", function () {
    $(this).attr('src', '../../Content/images/star1.png');
});

$(document).ready(function ()
{
    var flag_a = false; var flag_b = false; var flag_c = false; var flag_th = false; var flag_tt = false; var flag_ut = false, target_vehicle_sequence_id = 0;
    if (vehicle_list == null)
    {
        document.getElementById('photo_div').innerHTML = 
            '<h4 style="color:red; font-style:italic; text-align:center;">Please check back soon.<br /> There are currently no vehicles configured at this location</h4>';
        return;
    }
    if (sessionStorage.getItem("TargetVehicleSequenceId") === undefined || sessionStorage.getItem("TargetVehicleSequenceId") == null
        || sessionStorage.getItem("TargetVehicleSequenceId") == "undefined")
    {
        target_vehicle_sequence_id = 0;
    }
    else
    {
        target_vehicle_sequence_id = sessionStorage.getItem("TargetVehicleSequenceId");
    }
    if (sessionStorage.getItem("CurrentClassType") === undefined || sessionStorage.getItem("CurrentClassType") == null
        || sessionStorage.getItem("CurrentClassType") == "undefined")
    {
        generateVehicleList(0, 0);
    }
    else
    {
        var current_class_type = sessionStorage.getItem("CurrentClassType");
        var session_current_class_id = sessionStorage.getItem("current_class_id");
        var session_current_price_index = sessionStorage.getItem("current_price_index");
        var current_class_id = 0;
        if (session_current_class_id == null)
        {
            if (current_class_type == "A")
            {
                $("#class_a").children().removeClass('grey_border_div');
                $("#class_a").children().addClass('red_border_div');
                flag_a = true;
                flag_b = false;
                flag_c = false;
                flag_th = false;
                flag_tt = false;
                flag_ut = false;
                current_class_id = 1;
            }
            else if (current_class_type == "B")
            {
                $("#class_b").children().removeClass('grey_border_div');
                $("#class_b").children().addClass('red_border_div');
                flag_b = true;
                flag_a = false;
                flag_c = false;
                flag_th = false;
                flag_tt = false;
                flag_ut = false;
                current_class_id = 2;
            }
            else if (current_class_type == "C")
            {
                $("#class_c").children().removeClass('grey_border_div');
                $("#class_c").children().addClass('red_border_div');
                flag_c = true;
                flag_a = false;
                flag_b = false;
                flag_th = false;
                flag_tt = false;
                flag_ut = false;
                current_class_id = 3;
            }
            else if (current_class_type == "TH")
            {
                $("#class_th").children().removeClass('grey_border_div');
                $("#class_th").children().addClass('red_border_div');
                flag_th = true;
                flag_a = false;
                flag_b = false;
                flag_c = false;
                flag_tt = false;
                flag_ut = false;
                current_class_id = 7;
            }
            else if (current_class_type == "TT")
            {
                $("#class_tt").children().removeClass('grey_border_div');
                $("#class_tt").children().addClass('red_border_div');
                flag_tt = true;
                flag_a = false;
                flag_b = false;
                flag_c = false;
                flag_th = false;
                flag_ut = false;
                current_class_id = 8;
            }
            else if (current_class_type == "UT")
            {
                $("#class_ut").children().removeClass('grey_border_div');
                $("#class_ut").children().addClass('red_border_div');
                flag_ut = true;
                flag_a = false;
                flag_b = false;
                flag_c = false;
                flag_th = false;
                flag_tt = false;
                current_class_id = 9;
            }
        }
        else
        {
            if (session_current_class_id == 1)
            {
                $("#class_a").children().removeClass('grey_border_div');
                $("#class_a").children().addClass('red_border_div');
                flag_a = true;
                flag_b = false;
                flag_c = false;
                flag_th = false;
                flag_tt = false;
                flag_ut = false;
               current_class_id = 1;
            }
            else if (session_current_class_id == 2)
            {
                $("#class_b").children().removeClass('grey_border_div');
                $("#class_b").children().addClass('red_border_div');
                flag_b = true;
                flag_a = false;
                flag_c = false;
                flag_th = false;
                flag_tt = false;
                flag_ut = false;
                current_class_id = 2;
            }
            else if (session_current_class_id == 3)
            {
                $("#class_c").children().removeClass('grey_border_div');
                $("#class_c").children().addClass('red_border_div');
                flag_c = true;
                flag_a = false;
                flag_b = false;
                flag_th = false;
                flag_tt = false;
                flag_ut = false;
                current_class_id = 3;
            }
            else if (session_current_class_id == 7)
            {
                $("#class_th").children().removeClass('grey_border_div');
                $("#class_th").children().addClass('red_border_div');
                flag_th = true;
                flag_a = false;
                flag_b = false;
                flag_c = false;
                flag_tt = false;
                flag_ut = false;
                current_class_id = 7;
            }
            else if (session_current_class_id == 8)
            {
                $("#class_tt").children().removeClass('grey_border_div');
                $("#class_tt").children().addClass('red_border_div');
                flag_tt = true;
                flag_a = false;
                flag_b = false;
                flag_c = false;
                flag_th = false;
                flag_ut = false;
                current_class_id = 8;
            }
            else if (session_current_class_id == 9)
            {
                $("#class_ut").children().removeClass('grey_border_div');
                $("#class_ut").children().addClass('red_border_div');
                flag_ut = true;
                flag_a = false;
                flag_b = false;
                flag_c = false;
                flag_th = false;
                flag_tt = false;
                current_class_id = 9;
            }
        }

        if (session_current_price_index)
        {
            generateVehicleList(current_class_id, session_current_price_index);
        }
        else
        {
            generateVehicleList(current_class_id, 0);
        }
    }
    if (target_vehicle_sequence_id > 0)
    {
        location.hash = target_vehicle_sequence_id;
    }

    $('#class_a').click(function(){
        var sticky = document.getElementById('element');
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        sticky.style.display = 'none';
        is_display_none = true;
        $(this).children().removeClass('grey_border_div');
        $(this).children().addClass('red_border_div');
        $('#class_b').children().removeClass('red_border_div');
        $('#class_b').children().addClass('grey_border_div');
        $('#class_c').children().removeClass('red_border_div');
        $('#class_c').children().addClass('grey_border_div');
        $('#class_th').children().removeClass('red_border_div');
        $('#class_th').children().addClass('grey_border_div');
        $('#class_tt').children().removeClass('red_border_div');
        $('#class_tt').children().addClass('grey_border_div');
        $('#class_ut').children().removeClass('red_border_div');
        $('#class_ut').children().addClass('grey_border_div');
        flag_a = true;
        flag_b = false;
        flag_c = false;
        flag_th = false;
        flag_tt = false;
        flag_ut = false;

        $('.price_id').val(0);
        generateVehicleList(1, 0);
        sessionStorage.setItem("current_class_id", 1);
        sessionStorage.setItem("current_price_index", 0);
    });

    $('#class_b').click(function(){
        var sticky = document.getElementById('element');
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        sticky.style.display = 'none';
        is_display_none = true;
        $(this).children().removeClass('grey_border_div');
        $(this).children().addClass('red_border_div');
        $('#class_a').children().removeClass('red_border_div');
        $('#class_a').children().addClass('grey_border_div');
        $('#class_c').children().removeClass('red_border_div');
        $('#class_c').children().addClass('grey_border_div');
        $('#class_th').children().removeClass('red_border_div');
        $('#class_th').children().addClass('grey_border_div');
        $('#class_tt').children().removeClass('red_border_div');
        $('#class_tt').children().addClass('grey_border_div');
        $('#class_ut').children().removeClass('red_border_div');
        $('#class_ut').children().addClass('grey_border_div');
        flag_b = true;
        flag_a = false;
        flag_c = false;
        flag_th = false;
        flag_tt = false;
        flag_ut = false;

        $('.price_id').val(0);
        generateVehicleList(2, 0);
        sessionStorage.setItem("current_class_id", 2);
        sessionStorage.setItem("current_price_index", 0);
    });

    $('#class_c').click(function(){
        var sticky = document.getElementById('element');
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        sticky.style.display = 'none';
        is_display_none = true;
        $(this).children().removeClass('grey_border_div');
        $(this).children().addClass('red_border_div');
        $('#class_a').children().removeClass('red_border_div');
        $('#class_a').children().addClass('grey_border_div');
        $('#class_b').children().removeClass('red_border_div');
        $('#class_b').children().addClass('grey_border_div');
        $('#class_th').children().removeClass('red_border_div');
        $('#class_th').children().addClass('grey_border_div');
        $('#class_tt').children().removeClass('red_border_div');
        $('#class_tt').children().addClass('grey_border_div');
        $('#class_ut').children().removeClass('red_border_div');
        $('#class_ut').children().addClass('grey_border_div');
        flag_c = true;
        flag_a = false;
        flag_b = false;
        flag_th = false;
        flag_tt = false;
        flag_ut = false;

        $('.price_id').val(0);
        generateVehicleList(3, 0);
        sessionStorage.setItem("current_class_id", 3);
        sessionStorage.setItem("current_price_index", 0);
    });

    $('#class_th').click(function(){
        var sticky = document.getElementById('element');
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        sticky.style.display = 'none';
        is_display_none = true;
        $(this).children().removeClass('grey_border_div');
        $(this).children().addClass('red_border_div');
        $('#class_a').children().removeClass('red_border_div');
        $('#class_a').children().addClass('grey_border_div');
        $('#class_b').children().removeClass('red_border_div');
        $('#class_b').children().addClass('grey_border_div');
        $('#class_c').children().removeClass('red_border_div');
        $('#class_c').children().addClass('grey_border_div');
        $('#class_tt').children().removeClass('red_border_div');
        $('#class_tt').children().addClass('grey_border_div');
        $('#class_ut').children().removeClass('red_border_div');
        $('#class_ut').children().addClass('grey_border_div');
        flag_th = true;
        flag_a = false;
        flag_b = false;
        flag_c = false;
        flag_tt = false;
        flag_ut = false;

        $('.price_id').val(0);
        generateVehicleList(7, 0);
        sessionStorage.setItem("current_class_id", 7);
        sessionStorage.setItem("current_price_index", 0);
    });

    $('#class_tt').click(function(){
        var sticky = document.getElementById('element');
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        sticky.style.display = 'none';
        is_display_none = true;
        $(this).children().removeClass('grey_border_div');
        $(this).children().addClass('red_border_div');
        $('#class_a').children().removeClass('red_border_div');
        $('#class_a').children().addClass('grey_border_div');
        $('#class_b').children().removeClass('red_border_div');
        $('#class_b').children().addClass('grey_border_div');
        $('#class_c').children().removeClass('red_border_div');
        $('#class_c').children().addClass('grey_border_div');
        $('#class_th').children().removeClass('red_border_div');
        $('#class_th').children().addClass('grey_border_div');
        $('#class_ut').children().removeClass('red_border_div');
        $('#class_ut').children().addClass('grey_border_div');
        flag_tt = true;
        flag_a = false;
        flag_b = false;
        flag_c = false;
        flag_th = false;
        flag_ut = false;

        $('.price_id').val(0);
        generateVehicleList(8, 0);
        sessionStorage.setItem("current_class_id", 8);
        sessionStorage.setItem("current_price_index", 0);
    });

    $('#class_ut').click(function(){
        var sticky = document.getElementById('element');
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        sticky.style.display = 'none';
        is_display_none = true;
        $(this).children().removeClass('grey_border_div');
        $(this).children().addClass('red_border_div');
        $('#class_a').children().removeClass('red_border_div');
        $('#class_a').children().addClass('grey_border_div');
        $('#class_b').children().removeClass('red_border_div');
        $('#class_b').children().addClass('grey_border_div');
        $('#class_c').children().removeClass('red_border_div');
        $('#class_c').children().addClass('grey_border_div');
        $('#class_th').children().removeClass('red_border_div');
        $('#class_th').children().addClass('grey_border_div');
        $('#class_tt').children().removeClass('red_border_div');
        $('#class_tt').children().addClass('grey_border_div');
        flag_ut = true;
        flag_a = false;
        flag_b = false;
        flag_c = false;
        flag_th = false;
        flag_tt = false;

        $('.price_id').val(0);
        generateVehicleList(9, 0);
        sessionStorage.setItem("current_class_id", 9);
        sessionStorage.setItem("current_price_index", 0);
    });

    $('#class_total').click(function(){
        var sticky = document.getElementById('element');
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        sticky.style.display = 'none';
        is_display_none = true;
        $('#class_a').children().removeClass('red_border_div');
        $('#class_a').children().addClass('grey_border_div');
        $('#class_b').children().removeClass('red_border_div');
        $('#class_b').children().addClass('grey_border_div');
        $('#class_c').children().removeClass('red_border_div');
        $('#class_c').children().addClass('grey_border_div');
        $('#class_th').children().removeClass('red_border_div');
        $('#class_th').children().addClass('grey_border_div');
        $('#class_tt').children().removeClass('red_border_div');
        $('#class_tt').children().addClass('grey_border_div');
        $('#class_ut').children().removeClass('red_border_div');
        $('#class_ut').children().addClass('grey_border_div');
        flag_a = false;
        flag_b = false;
        flag_c = false;
        flag_th = false;
        flag_tt = false;
        flag_ut = false;

        $('.price_id').val(0);
        generateVehicleList(0, 0);
        sessionStorage.setItem("current_class_id", 0);
        sessionStorage.setItem("current_price_index", 0);
    });

    $('.btn_detail').click(function (e) {
        $('#photo_div').css('display', 'none');
        $('#detail_photo_div').css('display', 'block');
        var sticky = document.getElementById('element');
        sticky.style.display = 'none';
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        is_display_none = true;
        $('.price_id').val(0);

        if (flag_a)
        {
            generateVehicleList(1, 0);
        }
        else if (flag_b)
        {
            generateVehicleList(2, 0);
        }
        else if (flag_c)
        {
            generateVehicleList(3, 0);
        }
        else if (flag_th)
        {
            generateVehicleList(7, 0);
        }
        else if (flag_tt)
        {
            generateVehicleList(8, 0);
        }
        else if (flag_ut)
        {
            generateVehicleList(9, 0);
        }
        else
        {
            generateVehicleList(0, 0);
        }
        flag_detail = true;
    });

    $('.btn_original').click(function(e){
        $('#photo_div').css('display', 'block');
        $('#detail_photo_div').css('display', 'none');
        var sticky = document.getElementById('element');
        sticky.style.display = 'none';
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        is_display_none = true;

        $('.price_id').val(0);
        if (flag_a)
        {
            generateVehicleList(1, 0);
        }
        else if (flag_b)
        {
            generateVehicleList(2, 0);
        }
        else if (flag_c)
        {
            generateVehicleList(3, 0);
        }
        else if (flag_th)
        {
            generateVehicleList(7, 0);
        }
        else if (flag_tt)
        {
            generateVehicleList(8, 0);
        }
        else if (flag_ut)
        {
            generateVehicleList(9, 0);
        }
        else
        {
            generateVehicleList(0, 0);
        }
        flag_detail = false;
    });

    $('.price_id').change(function(e){
        var sticky = document.getElementById('element');
        sticky.style.display = 'none';
        var tab = document.getElementById('sticky-tab');
        tab.style.display = 'none';
        is_display_none = true;

        var price_index = $(this).val();
        sessionStorage.setItem("current_price_index", price_index);
        
        if (flag_a)
        {
            generateVehicleList(1, price_index);
        }
        else if (flag_b)
        {
            generateVehicleList(2, price_index);
        }
        else if (flag_c)
        {
            generateVehicleList(3, price_index);
        }
        else if (flag_th)
        {
            generateVehicleList(7, price_index);
        }
        else if (flag_tt)
        {
            generateVehicleList(8, price_index);
        }
        else if (flag_ut)
        {
            generateVehicleList(9, price_index);
        }
        else
        {
            generateVehicleList(0, price_index);
        }
    });

    function generateVehicleList(index, price_index)
    {
        var d_html = '';
        var html = '';

        var is_location_behind_on_paying = is_location_behind_on_paying;
        if(is_location_behind_on_paying == true)
        {
            var invisible_html = '<br /><br /><h2 style="color:#000000; text-align:center;">This location is currently under maintenance.<br />' +
                'Please check back later.</h2><br /><br /><br /><br /><br /><br />';
            document.getElementById('photo_div').innerHTML = invisible_html;
            document.getElementById('detail_photo_div').innerHTML = invisible_html;
            return;
        }

        if(index == 0)
        {
            if(price_index > 0)
            {
                for (var i = 0; i < vehicle_list.length; i++)
                {
                    if (vehicle_list[i].WebPriceGroup == price_index || (vehicle_list[i].WebPriceGroup > price_index && price_index == 500))
                    {
                        var vehicle_key = vehicle_list[i].VehicleKey;
                        var photo = vehicle_list[i].Photo;
                        var name = vehicle_list[i].Name;
                        var location = vehicle_list[i].Location;
                        var location_name = vehicle_list[i].LocationName;
                        var price = vehicle_list[i].Price;
                        var price_group = vehicle_list[i].WebPriceGroup;
                        var vehicle_class = vehicle_list[i].VehicleClass;
                        var class_type = vehicle_list[i].ClassType;
                        var class_name = vehicle_list[i].ClassName;
                        var web_region_name = vehicle_list[i].WebRegionalName;
                        var children = vehicle_list[i].Children;
                        var adolescents = vehicle_list[i].Adolescents;
                        var adults = vehicle_list[i].Adults;
                        var make = vehicle_list[i].Make;
                        var model = vehicle_list[i].Model;
                        var web_description = vehicle_list[i].WebDescription;
                        var featured_vehicle = vehicle_list[i].FeaturedVehicle;
                        var date_list = vehicle_list[i].DateList;
                        var vehicle_seq = vehicle_list[i].VehicleSequenceId;
                        var web_unique_id = vehicle_list[i].WebUniqueId;
                        var insurance_policy = vehicle_list[i].InsurancePolicy;
                        var is_square_photo = vehicle_list[i].IsSquarePhoto;
                        var showForSale = vehicle_list[i].ShowForSale;
                        var forSaleOn = vehicle_list[i].ForSaleOn;
                        var salePrice = vehicle_list[i].SalePrice;

                        if (target_vehicle_sequence_id == vehicle_seq) {
                            html += generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, _g_website_url, featured_vehicle,
                                children, adolescents, adults, date_list, target_vehicle_sequence_id, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice);
                        }
                        else {
                            html += generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, _g_website_url, featured_vehicle,
                                children, adolescents, adults, date_list, null, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice);
                        }
                        d_html += generateSecondaryPhotoList(web_unique_id, vehicle_key, photo, name, price, make, model, web_description,
                            children, adolescents, adults, featured_vehicle, insurance_policy, date_list);
                    }
                }
            }
            else
            {
                for (var i = 0; i < vehicle_list.length; i++)
                {
                    var vehicle_key = vehicle_list[i].VehicleKey;
                    var photo = vehicle_list[i].Photo;
                    var name = vehicle_list[i].Name;
                    var location = vehicle_list[i].Location;
                    var location_name = vehicle_list[i].LocationName;
                    var price = vehicle_list[i].Price;
                    var price_group = vehicle_list[i].WebPriceGroup;
                    var vehicle_class = vehicle_list[i].VehicleClass;
                    var class_type = vehicle_list[i].ClassType;
                    var class_name = vehicle_list[i].ClassName;
                    var web_region_name = vehicle_list[i].WebRegionalName;
                    var children = vehicle_list[i].Children;
                    var adolescents = vehicle_list[i].Adolescents;
                    var adults = vehicle_list[i].Adults;
                    var make = vehicle_list[i].Make;
                    var model = vehicle_list[i].Model;
                    var web_description = vehicle_list[i].WebDescription;
                    var featured_vehicle = vehicle_list[i].FeaturedVehicle;
                    var date_list = vehicle_list[i].DateList;
                    var vehicle_seq = vehicle_list[i].VehicleSequenceId;
                    var web_unique_id = vehicle_list[i].WebUniqueId;
                    var insurance_policy = vehicle_list[i].InsurancePolicy;
                    var is_square_photo = vehicle_list[i].IsSquarePhoto;
                    var showForSale = vehicle_list[i].ShowForSale;
                    var forSaleOn = vehicle_list[i].ForSaleOn;
                    var salePrice = vehicle_list[i].SalePrice;

                    if (target_vehicle_sequence_id == vehicle_seq) {
                        html += generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, _g_website_url, featured_vehicle,
                            children, adolescents, adults, date_list, target_vehicle_sequence_id, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice);
                    }
                    else {
                        html += generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, _g_website_url, featured_vehicle,
                            children, adolescents, adults, date_list, null, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice);
                    }
                    d_html += generateSecondaryPhotoList(web_unique_id, vehicle_key, photo, name, price, make, model, web_description,
                        children, adolescents, adults, featured_vehicle, insurance_policy, date_list);
                }
            }
        }
        else
        {
            if(price_index > 0)
            {
                for (var i = 0; i < vehicle_list.length; i++)
                {
                    if ((vehicle_list[i].ClassType == index && vehicle_list[i].WebPriceGroup == price_index) ||
                        (vehicle_list[i].ClassType == index && vehicle_list[i].WebPriceGroup > price_index && price_index == 500))
                    {
                        var vehicle_key = vehicle_list[i].VehicleKey;
                        var photo = vehicle_list[i].Photo;
                        var name = vehicle_list[i].Name;
                        var location = vehicle_list[i].Location;
                        var location_name = vehicle_list[i].LocationName;
                        var price = vehicle_list[i].Price;
                        var price_group = vehicle_list[i].WebPriceGroup;
                        var vehicle_class = vehicle_list[i].VehicleClass;
                        var class_type = vehicle_list[i].ClassType;
                        var class_name = vehicle_list[i].ClassName;
                        var web_region_name = vehicle_list[i].WebRegionalName;
                        var children = vehicle_list[i].Children;
                        var adolescents = vehicle_list[i].Adolescents;
                        var adults = vehicle_list[i].Adults;
                        var make = vehicle_list[i].Make;
                        var model = vehicle_list[i].Model;
                        var web_description = vehicle_list[i].WebDescription;
                        var featured_vehicle = vehicle_list[i].FeaturedVehicle;
                        var date_list = vehicle_list[i].DateList;
                        var vehicle_seq = vehicle_list[i].VehicleSequenceId;
                        var web_unique_id = vehicle_list[i].WebUniqueId;
                        var insurance_policy = vehicle_list[i].InsurancePolicy;
                        var is_square_photo = vehicle_list[i].IsSquarePhoto;
                        var showForSale = vehicle_list[i].ShowForSale;
                        var forSaleOn = vehicle_list[i].ForSaleOn;
                        var salePrice = vehicle_list[i].SalePrice;

                        if (target_vehicle_sequence_id == vehicle_seq) {
                            html += generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, _g_website_url, featured_vehicle,
                                children, adolescents, adults, date_list, target_vehicle_sequence_id, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice);
                        }
                        else {
                            html += generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, _g_website_url, featured_vehicle,
                                children, adolescents, adults, date_list, null, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice);
                        }
                        d_html += generateSecondaryPhotoList(web_unique_id, vehicle_key, photo, name, price, make, model, web_description,
                            children, adolescents, adults, featured_vehicle, insurance_policy, date_list);
                    }
                }
            }
            else
            {
                for (var i = 0; i < vehicle_list.length; i++)
                {
                    if(vehicle_list[i].ClassType == index)
                    {
                        var vehicle_key = vehicle_list[i].VehicleKey;
                        var photo = vehicle_list[i].Photo;
                        var name = vehicle_list[i].Name;
                        var location = vehicle_list[i].Location;
                        var location_name = vehicle_list[i].LocationName;
                        var price = vehicle_list[i].Price;
                        var price_group = vehicle_list[i].WebPriceGroup;
                        var vehicle_class = vehicle_list[i].VehicleClass;
                        var class_type = vehicle_list[i].ClassType;
                        var class_name = vehicle_list[i].ClassName;
                        var web_region_name = vehicle_list[i].WebRegionalName;
                        var children = vehicle_list[i].Children;
                        var adolescents = vehicle_list[i].Adolescents;
                        var adults = vehicle_list[i].Adults;
                        var make = vehicle_list[i].Make;
                        var model = vehicle_list[i].Model;
                        var web_description = vehicle_list[i].WebDescription;
                        var featured_vehicle = vehicle_list[i].FeaturedVehicle;
                        var date_list = vehicle_list[i].DateList;
                        var vehicle_seq = vehicle_list[i].VehicleSequenceId;
                        var web_unique_id = vehicle_list[i].WebUniqueId;
                        var insurance_policy = vehicle_list[i].InsurancePolicy;
                        var is_square_photo = vehicle_list[i].IsSquarePhoto;
                        var showForSale = vehicle_list[i].ShowForSale;
                        var forSaleOn = vehicle_list[i].ForSaleOn;
                        var salePrice = vehicle_list[i].SalePrice;

                        if (target_vehicle_sequence_id == vehicle_seq) {
                            html += generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, _g_website_url, featured_vehicle,
                                children, adolescents, adults, date_list, target_vehicle_sequence_id, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice);
                        }
                        else {
                            html += generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, _g_website_url, featured_vehicle,
                                children, adolescents, adults, date_list, null, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice);
                        }
                        d_html += generateSecondaryPhotoList(web_unique_id, vehicle_key, photo, name, price, make, model, web_description,
                            children, adolescents, adults, featured_vehicle, insurance_policy, date_list);
                    }
                }
            }
        }

        if (html)
        {
            document.getElementById('photo_div').innerHTML = '<h2 class="hidden-xs" style="color:#000000;">All vehicles in ' + vehicle_list[0].WebRegionalName + '</h2>' + html;
        }
        else
        {
            document.getElementById('photo_div').innerHTML =
                '<h4 style="color:red; font-style:italic; text-align:center;">Please check back soon.<br /> There are currently no vehicles configured at this location</h4>';
        }
        if (d_html)
        {
            document.getElementById('detail_photo_div').innerHTML = d_html;
        }
        else
        {
            document.getElementById('detail_photo_div').innerHTML =
                '<h4 style="color:red; font-style:italic; text-align:center;">Please check back soon.<br /> There are currently no vehicles configured at this location</h4>';
        }
    }

    function generatePrimaryPhotoList(web_unique_id, vehicle_key, photo, name, price, web_region_name, website_url, featured_vehicle,
        children, adolescents, adults, date_list, is_target, insurance_policy, is_square_photo, showForSale, forSaleOn, salePrice)
    {
        var g_html = '', g_popup_dialog = '', g_tile = '';
        var g_name, g_price, g_web_region_name, g_photo;
        if (name)
        {
            if (insurance_policy)
            {
                if (is_square_photo == true) {
                    g_name = '<h5 class="vehicle-license" style="margin-top:137px;">' + name + '<span>&nbsp;&#10033;</span></h5>';
                }
                else {
                    g_name = '<h5 class="vehicle-license">' + name + '<span>&nbsp;&#10033;</span></h5>';
                }
            }
            else
            {
                if (is_square_photo == true) {
                    g_name = '<h5 class="vehicle-license" style="margin-top:137px;">' + name + '</h5>';
                }
                else {
                    g_name = '<h5 class="vehicle-license">' + name + '</h5>';
                }
            }
        }
        else
        {
            if (is_square_photo == true) {
                g_name = '<h5 class="vehicle-license" style="margin-top:137px;">---</h5>';
            }
            else {
                g_name = '<h5 class="vehicle-license">---</h5>';
            }
        }
        if (price)
        {
            g_price = '<p class="vehicle-price">' + price + '</p>';
        }
        else
        {
            g_price = '<p class="vehicle-price">--</p>';
        }
        if (web_region_name)
        {
            g_web_region_name = '<h6 class="vehicle-location">' + web_region_name + '</h6>';
        }
        else
        {
            g_web_region_name = '<h6 class="vehicle-location">----</h6>';
        }

        if (featured_vehicle)
        {
            if (is_target)
            {
                if (photo == "big")
                {
                    if (showForSale == true && forSaleOn != null)
                    {
                        g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                + '<div class="ribbon" style="bottom:90px;"><div class="featured-txt">Featured</div></div>'
                                + '<img src="../../Content/images/TooLarge.jpg" class="vehicle-img" style="width:194px;">';
                    }
                    else
                    {
                        g_photo = '<div class="ribbon" style="bottom:90px;"><div class="featured-txt">Featured</div></div>'
                                + '<img src="../../Content/images/TooLarge.jpg" class="vehicle-img" style="width:194px;">';
                    }
                }
                else
                {
                    if (is_square_photo == true)
                    {
                        if (showForSale == true && forSaleOn != null)
                        {
                            g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                    + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                    + '<div class="ribbon" style="bottom:93px; right:-21px;"><div class="featured-txt">Featured</div></div>'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="highlight-clip-vehicle-img">';
                        }
                        else
                        {
                            g_photo = '<div class="ribbon" style="bottom:93px; right:-21px;"><div class="featured-txt">Featured</div></div>'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="highlight-clip-vehicle-img">';
                        }
                    }
                    else
                    {
                        if (showForSale == true && forSaleOn != null)
                        {
                            g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                    + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                    + '<div class="ribbon" style="bottom:92px; right:-21px;"><div class="featured-txt">Featured</div></div>'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="vehicle-img" style="width:194px;">';
                        }
                        else
                        {
                            g_photo = '<div class="ribbon" style="bottom:92px; right:-21px;"><div class="featured-txt">Featured</div></div>'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="vehicle-img" style="width:194px;">';
                        }
                    }
                }
            }
            else
            {
                if (photo == "big")
                {
                    if (showForSale == true && forSaleOn != null)
                    {
                        g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                + '<div class="ribbon"><div class="featured-txt">Featured</div></div>'
                                + '<img src="../../Content/images/TooLarge.jpg" class="vehicle-img">';
                    }
                    else
                    {
                        g_photo = '<div class="ribbon"><div class="featured-txt">Featured</div></div>'
                                + '<img src="../../Content/images/TooLarge.jpg" class="vehicle-img">';
                    }
                }
                else
                {
                    if (is_square_photo == true)
                    {
                        if (showForSale == true && forSaleOn != null)
                        {
                            g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                    + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                    + '<div class="ribbon"><div class="featured-txt">Featured</div></div>'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="clip-vehicle-img">';
                        }
                        else
                        {
                            g_photo = '<div class="ribbon"><div class="featured-txt">Featured</div></div>'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="clip-vehicle-img">';
                        }
                    }
                    else
                    {
                        if (showForSale == true && forSaleOn != null)
                        {
                            g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                    + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                    + '<div class="ribbon"><div class="featured-txt">Featured</div></div>'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="vehicle-img">';
                        }
                        else
                        {
                            g_photo = '<div class="ribbon"><div class="featured-txt">Featured</div></div>'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="vehicle-img">';
                        }
                    }
                }
            }
        }
        else
        {
            if (is_target)
            {
                if (photo == "big")
                {
                    if (showForSale == true && forSaleOn != null)
                    {
                        g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                + '<img src="../../Content/images/TooLarge.jpg" class="vehicle-img" style="width:194px;">';
                    }
                    else
                    {
                        g_photo = '<img src="../../Content/images/TooLarge.jpg" class="vehicle-img" style="width:194px;">';
                    }
                }
                else
                {
                    if (is_square_photo == true)
                    {
                        if (showForSale == true && forSaleOn != null)
                        {
                            g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                    + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="highlight-clip-vehicle-img">';
                        }
                        else
                        {
                            g_photo = '<img src="data:image/jpg;base64,' + photo + '" class="highlight-clip-vehicle-img">';
                        }
                    }
                    else
                    {
                        if (showForSale == true && forSaleOn != null)
                        {
                            g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                    + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="vehicle-img" style="width:194px;">';
                        }
                        else
                        {
                            g_photo = '<img src="data:image/jpg;base64,' + photo + '" class="vehicle-img" style="width:194px;">';
                        }
                    }
                }
            }
            else
            {
                if (photo == "big")
                {
                    if (showForSale == true && forSaleOn != null)
                    {
                        g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                + '<img src="../../Content/images/TooLarge.jpg" class="vehicle-img">';
                    }
                    else
                    {
                        g_photo = '<img src="../../Content/images/TooLarge.jpg" class="vehicle-img">';
                    }
                }
                else
                {
                    if (is_square_photo == true)
                    {
                        if (showForSale == true && forSaleOn != null)
                        {
                            g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                    + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="clip-vehicle-img">';
                        }
                        else
                        {
                            g_photo = '<img src="data:image/jpg;base64,' + photo + '" class="clip-vehicle-img">';
                        }
                    }
                    else
                    {
                        if (showForSale == true && forSaleOn != null)
                        {
                            g_photo = '<img src="../../Content/images/star1.png" class="img-responsive sale-img" title="Rental Rate: '
                                    + price + '/day (' + minimum_number_of_time_interval + ' day min)' + String.fromCharCode(13) + 'For Sale: $' + salePrice + '">'
                                    + '<img src="data:image/jpg;base64,' + photo + '" class="vehicle-img">';
                        }
                        else
                        {
                            g_photo = '<img src="data:image/jpg;base64,' + photo + '" class="vehicle-img">';
                        }
                    }
                }
            }
        }

        g_tile = '<a href="' + website_url + 'VehicleDetail/' + web_unique_id + '" class="tile-img">' + g_photo + g_name + g_web_region_name + g_price + '</a>';

        g_popup_dialog = generatePopUpDialog(children, adolescents, adults, date_list, vehicle_key, web_unique_id);

        if (is_target)
        {
            g_html = '<div class="col-lg-3 col-md-3 col-sm-4 col-xs-12 vehicle-info" id="' + is_target + '"><div class="primary-vehicle-div" id="target-vehicle-div-tooltip">'
                + g_popup_dialog + g_tile + '</div><br /></div>';
        }
        else
        {
            g_html = '<div class="col-lg-3 col-md-3 col-sm-4 col-xs-12 vehicle-info"><div class="primary-vehicle-div">' + g_popup_dialog + g_tile + '</div><br /></div>';
        }

        return g_html;
    }

    function generateSecondaryPhotoList(web_unique_id, vehicle_key, photo, name, price, make, model, web_description,
        children, adolescents, adults, featured_vehicle, insurance_policy, date_list)
    {
        

        var g_d_html = '', g_d_popup_dialog = '';
        var g_d_name, g_d_price, g_d_make, g_d_model, g_d_sleeps, g_d_sleep_icons, g_d_featured_vehicle, g_d_photo, g_d_web_description;

        if (name)
        {
            if (insurance_policy)
            {
                g_d_name = '<div class="row daily-div"><div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"><h4>' + name +
                    '<span style="color:black;">&nbsp;&#10033;</span></h4></div></div>';
            }
            else
            {
                g_d_name = '<div class="row daily-div"><div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"><h4>' + name + '</h4></div></div>';
            }
        }
        else
        {
            g_d_name = '<div class="row daily-div"><div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"><h4>---</h4></div></div>';
        }

        if (make)
        {
            g_d_make = '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4"><h5>Make: ' + make + '</h5></div>';
        }
        else
        {
            g_d_make = '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-4"><h5>Make: - </h5></div>';
        }
        if (model)
        {
            g_d_model = '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-5"><h5>Model: ' + model + '</h5></div>';
        }
        else
        {
            g_d_model = '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-5"><h5>Model: - </h5></div>';
        }
        var sleeps = children + adolescents + adults;
        g_d_sleeps = '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3"><h5>Sleep:&nbsp;' + sleeps + '</h5></div>';

        if (adolescents + adults > 7)
        {
            var icons = '';
            for (var i = 0; i < 7; i++)
            {
                icons += '<i class="fa fa-male fa-lg" aria-hidden="true"></i>';
            }
            g_d_sleep_icons = '<div class="col-lg-3 col-md-3 col-sm-3 hidden-xs" style="padding-top:12px;">' + icons + '...</div>';
        }
        else
        {
            var l_icons = '';
            var s_icons = '';
            var others = '';
            for (var l = 0; l < adolescents + adults; l++)
            {
                l_icons += '<i class="fa fa-male fa-lg" aria-hidden="true"></i>';
            }
            if (sleeps > 7)
            {
                for (var s = 0; s < 7 - adolescents - adults; s++)
                {
                    s_icons += '<i class="fa fa-male fa-male" aria-hidden="true"></i>';
                }
                s_icons += '<span style="color:black;">...</span>';
            }
            else
            {
                for (var s = 0; s < children; s++)
                {
                    s_icons += '<i class="fa fa-male fa-male" aria-hidden="true"></i>';
                }
            }
            g_d_sleep_icons = '<div class="col-lg-3 col-md-3 col-sm-3 hidden-xs" style="padding-top:12px;">' + l_icons + s_icons + '</div>';
        }
        var group2 = '<div class="row daily-div">' + g_d_make + g_d_model + g_d_sleeps + g_d_sleep_icons + '</div>';

        if (price)
        {
            g_d_price = '<div class="row daily-div"><div class="col-lg-4 col-md-4 col-sm-4 col-xs-6"><h5>Daily:&nbsp;'
                     + price + '</h5></div><div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">'
                     + '<button type="button" class="estimate_data btn btn-success btn-quote hidden-xs" style="margin-right:10px;" onclick="getEstimateOfDetail(this);" value="'
                     + vehicle_key + '">Get Estimate</button><button type="button" class="detail_data btn btn-danger btn-details" onclick="getMorePhotos(this);" value="'
                     + web_unique_id + '">More Photos</button></div></div>';
        }
        else
        {
            g_d_price = '<div class="row daily-div"><div class="col-lg-4 col-md-4 col-sm-4 col-xs-6"><h5>Daily:&nbsp;---</h5></div><div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">' +
                     '<button type="button" class="estimate_data btn btn-success btn-quote hidden-xs" style="margin-right:10px;" onclick="getEstimateOfDetail(this);" value="'
                     + vehicle_key + '">Get Estimate</button><button type="button" class="detail_data btn btn-danger btn-details" onclick="getMorePhotos(this);" value="'
                     + web_unique_id + '">More Photos</button></div></div>';
        }

        g_d_popup_dialog = generatePopUpDialog(children, adolescents, adults, date_list, vehicle_key, web_unique_id);

        if (featured_vehicle)
        {
            if (photo == -1) {
                g_d_photo = '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><div class="secondary-vehicle-div">' + g_d_popup_dialog
                        + '<div class="list-ribbon-detail"><div class="featured-txt">Featured</div></div>'
                        + '<img src="../../Content/images/TooLarge.jpg" class="img-responsive vehicle-list-img"></div><br /></div>';
            }
            else {
                g_d_photo = '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><div class="secondary-vehicle-div">' + g_d_popup_dialog
                        + '<div class="list-ribbon-detail"><div class="featured-txt">Featured</div></div>'
                        + '<img src="data:image/jpg;base64,' + photo + '" class="img-responsive vehicle-list-img"></div><br /></div>';
            }
        }
        else
        {
            if (photo == -1) {
                g_d_photo = '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><div class="secondary-vehicle-div">' + g_d_popup_dialog
                        + '<img src="../../Content/images/TooLarge.jpg" class="img-responsive vehicle-list-img"></div><br /></div>';
            }
            else {
                g_d_photo = '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><div class="secondary-vehicle-div">' + g_d_popup_dialog
                        + '<img src="data:image/jpg;base64,' + photo + '" class="img-responsive vehicle-list-img"></div><br /></div>';
            }
        }

        var web_description_text = web_description.replace(/<[^>]+>/g, '').replace(/&nbsp;/g, '');
        if (web_description_text)
        {
            g_d_web_description = web_description_text;
        }
        else
        {
            g_d_web_description = "No description";
        }

        var g_d_panel = '<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8"><div class="listview-div" title="' + g_d_web_description + '">' +
            g_d_name + group2 + '<br />' + g_d_price + '<br /></div></div>';

        g_d_html = '<div class="row" style="padding-bottom:20px;">' + g_d_photo + g_d_panel + '</div>';

        return g_d_html;
    }

    function generatePopUpDialog(children, adolescents, adults, date_list, vehicle_key, web_unique_id)
    {
        var l_icons = '', s_icons = '', g_sleeps = '', g_date_list = '';
        var sleeps = children + adolescents + adults;
        for (var l = 0; l < adolescents + adults; l++)
        {
            l_icons += '<i class="fa fa-male fa-lg" aria-hidden="true"></i>';
        }
        for (var s = 0; s < children; s++)
        {
            s_icons += '<i class="fa fa-male fa-male" aria-hidden="true"></i>';
        }
        g_sleeps = '<h6 style="color:black; margin-bottom:-5px; margin-top:-5px;">Sleep:&nbsp;' + sleeps + '&nbsp;&nbsp;&nbsp;' + l_icons + s_icons + '</h6>';

        if (date_list.length > 0)
        {
            g_date_list = '<input type="text" name="datefilter" class="booked_daterangepicker" value="' + vehicle_key + '" /><input type="hidden" class="booked_dates" value="'
                            + date_list + '" />';
        }
        else
        {
            g_date_list = '<input type="text" name="datefilter" class="no_booked_daterangepicker" value="' + vehicle_key + '" />'
        }

        var dialog = '<div class="row popup-dialog popup-invisible"><div class="col-md-12" style="text-align:left;"><h6 style="color:black; margin-bottom:-5px; margin-top:-5px;">'
                    + g_sleeps + '<h4 style="color:black; margin-bottom:0px; text-align:center;">Enter your dates</h4>'
                    + g_date_list + '<br /><br /><div style="text-align:center;">'
                    + '<button type="button" class="estimate_data btn btn-success" style="width:80px; margin-right:5px; font-size:12px; padding-left:5px;" onclick="getEstimate(this);" value="'
                    + vehicle_key + '">Get Estimate</button>'
                    + '<button type="button" class="detail_data btn btn-danger" style="width:80px; font-size:12px; padding-left:5px;" onclick="getMorePhotos(this);" value="'
                    + web_unique_id + '">More Photos</button></div></div></div>';
        return dialog;
    }
});