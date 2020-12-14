using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToM.Controllers
{
    public class fHomeController : Controller
    {
        // GET: fHome
        public ActionResult HomeIndex()
        {
            //if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "fLogin");
            //}
            return View();
        }
    }
}