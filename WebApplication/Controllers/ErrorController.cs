using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public partial class ErrorController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
        public virtual ActionResult DisableJavaScript()
        {
            Response.StatusCode = 404;
            return View();
        }
        public virtual ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}