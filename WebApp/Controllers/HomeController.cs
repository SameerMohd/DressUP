using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var DMdl = new DressUpModel();
            return View(DMdl);
        }

        [HttpPost]
        public ActionResult DressUp(DressUpModel Mdl)
        {
            try
            {
                DressUPLogic DBL = new DressUPLogic();
                String Comd = Mdl.Commands.Replace(" ", "").Trim(',');
                Mdl.Result = DBL.getResponses(Mdl.Temp, Comd);
                return View("Index", Mdl);

            }
            catch (Exception)
            {
                return View("InternalError");
            }
        }


    }
}