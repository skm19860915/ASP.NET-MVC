using sharemycoach.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class GridController : BaseController
    {
        public ActionResult Index(long id)
        {
            var allGridInfos = _wc.GetAllGridInfos(id, _token);
            if(allGridInfos == null || allGridInfos.Count() < 1)
                return RedirectToAction("Index", "Error");

            return View(allGridInfos);
        }
    }
}