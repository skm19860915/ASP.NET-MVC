using sharemycoach.ViewModels;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class VehicleDetailController : BaseController
    {
        private VehicleDetailViewModel detailInfo;

        public ActionResult Index(string id)
        {
            detailInfo = _wc.GetDetail(id, false, _token);

            if (detailInfo == null)
            {
                detailInfo = _wc.GetDetail(id, true, _token);
                if(detailInfo == null)
                    return RedirectToAction("Index", "Error");
            }

            GetDetailInfo(detailInfo);
            return View();
        }

        public void GetDetailInfo(VehicleDetailViewModel info)
        {
            ViewBag.Title = info.NameOnWeb + " - " + info.DBAName + " - RV Rentals | " + Properties.Resources.SHARE_MY_COACH;
            TempData["Detail"] = info;
            TempData.Keep();
            ViewBag.Info = info;
        }
    }
}