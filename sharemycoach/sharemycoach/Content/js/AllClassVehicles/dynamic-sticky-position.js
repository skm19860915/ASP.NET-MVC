var flag_detail = false, galleryHeight = 0;
var d_galleryHeight = 0;
var is_display_none = false;

$(function(){
    var e = document.getElementById('element');
    galleryHeight = getGalleryHeight();
    var responsiveHeight = getStatusDiv();
    var currentWindowHeight = $(window).height();

    initialMethod();
    $(window).resize(function(){
        galleryHeight = getGalleryHeight();
        d_galleryHeight = $("#detail_photo_div").outerHeight();
        responsiveHeight = getStatusDiv();
        var documentWidth = $(document).width();
        if(documentWidth >= 1200){
            e.style.marginLeft = '325px';
        }
        else if(documentWidth >= 992 && documentWidth < 1200){
            e.style.marginLeft = '240px';
        }
        else if(documentWidth >= 768 && documentWidth < 992){
            e.style.marginLeft = '130px';
        }
        else if(documentWidth < 768){
            e.style.marginLeft = '-800px';
        }
    })

    function getStatusDiv()
    {
        var totalHeight = $("#count_div").outerHeight()+ $("#state_div").outerHeight()+ $("#price_div").outerHeight()+ $("#class_div").outerHeight() + 123;
        return totalHeight;
    }

    function initialMethod() 
    {
        $("#element").css('top', currentWindowHeight - 433 +'px');
        $("#element").css('position', 'fixed');
        $("#sticky-tab").css('top', currentWindowHeight - 432 +'px');
        $("#sticky-tab").css('position', 'fixed');
    }
});
function getGalleryHeight() 
{
    var height = $("#photo_div").outerHeight();
    return height;
}