using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace sharemycoach
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Old-Contact-Us",
                url: "Contact-Us.aspx",
                defaults: new { controller = "Contact", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-NewsLetter",
                url: "Newsletter.aspx",
                defaults: new { controller = "NewsLetter", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-Resources",
                url: "Page/Resources.aspx",
                defaults: new { controller = "Resources", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-RVOwners",
                url: "Page/RV-Owners.aspx",
                defaults: new { controller = "RVOwners", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-SMCFAQ",
                url: "Page/SMCFAQ.aspx",
                defaults: new { controller = "Faq", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-Testimonials",
                url: "Testimonials.aspx",
                defaults: new { controller = "Testimonials", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-Vehice-Rentals",
                url: "Vehicle-Rentals.aspx",
                defaults: new { controller = "VehicleRentals", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-Vehicle-Rental",
                url: "Vehicle-Rental/{id}",
                defaults: new { controller = "VehicleRental", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-Vehicle-Quote",
                url: "Vehicle-Quote/{id}",
                defaults: new { controller = "VehicleQuote", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-Page",
                url: "Page/{id}",
                defaults: new { controller = "Page", action = "Index" }
            );

            routes.MapRoute(
                name: "Old-Vehicle-Details",
                url: "Vehicle-Details.aspx",
                defaults: new { controller = "VehicleDetails", action = "Index" }
            );

            routes.MapRoute(
               name: "VehicleDetail",
               url: "VehicleDetail/{id}",
               defaults: new { controller = "VehicleDetail", action = "Index" }
            );

            routes.MapRoute(
                name: "CheckOut",
                url: "CheckOut/{id}",
                defaults: new { controller = "CheckOut", action = "Index" }
            );

            routes.MapRoute(
                name: "Destinations",
                url: "Destination/{id}",
                defaults: new { controller = "Destination", action = "Index" }
            );

            routes.MapRoute(
               name: "Events",
               url: "Event/{id}",
               defaults: new { controller = "Event", action = "Index" }
            );

            routes.MapRoute(
                name: "FromWebCityStaticPage",
                url: "{WebCityStaticPage}/AllClassVehicles/Location/{name}",
                defaults: new { controller = "AllClassVehicles", action = "Index", name = UrlParameter.Optional }
            );
            
            routes.MapRoute(
               name: "Insurance-Company",
               url: "Insurance-Company.aspx",
               defaults: new { controller = "Insurance", action = "Specific" }
            );

            routes.MapRoute(
               name: "Optional-Equipment",
               url: "Optional-Equipment.aspx",
               defaults: new { controller = "OptionalEquipment", action = "Specific" }
            );

            routes.MapRoute(
               name: "AddYourFleet",
               url: "AddYourFleet.aspx",
               defaults: new { controller = "AddYourFleet", action = "Specific" }
            );

            routes.MapRoute(
               name: "Default-AddYourFleet",
               url: "AddYourFleet",
               defaults: new { controller = "AddYourFleet", action = "Specific" }
            );

            routes.MapRoute(
               name: "Vehicles-For-Sale",
               url: "Vehicles-For-Sale.aspx",
               defaults: new { controller = "VehiclesForSale", action = "Specific" }
            );

            routes.MapRoute(
                name: "Vehicle-Sale",
                url:"Vehicle-Sale/{id}",
                defaults: new { controller = "VehicleDetailForSale", action = "Index" }
            );

            routes.MapRoute(
               name: "General-Info",
               url: "General-Info.aspx",
               defaults: new { controller = "GeneralInfo", action = "Specific" }
            );

            routes.MapRoute(
               name: "Campgrounds",
               url: "Campgrounds.aspx",
               defaults: new { controller = "Campground", action = "Specific" }
            );

            routes.MapRoute(
               name: "Private-RV-Video-Training",
               url: "Private-RV-Video-Training.aspx",
               defaults: new { controller = "RVTrainingVideos", action = "Index" }
            );

            routes.MapRoute(
                name: "FAQ",
                url: "FAQ.aspx",
                defaults: new { controller = "Faq", action = "Specific" }
            );

            routes.MapRoute(
             name: "EventFlyers",
             url: "EventFlyer/{id}",
             defaults: new { controller = "EventFlyer", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "webmail",
             url: "webmail",
             defaults: new { controller = "WebMail", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
