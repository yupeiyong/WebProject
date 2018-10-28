using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 客户端 设置crossDomain: true
        /// </summary>
        /// <returns></returns>
        public JsonResult CrossDomain()
        {
            return Json(new{Message="您跨域请求成功！"},JsonRequestBehavior.AllowGet);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //设置支持跨域的响应头
            Response.AddHeader("Access-Control-Allow-Methods", "OPTIONS,POST,GET");
            Response.AddHeader("Access-Control-Allow-Headers", "x-requested-with,content-type");
            Response.AddHeader("Access-Control-Allow-Credentials", "true");
            Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:39891");
            base.OnActionExecuted(filterContext);
        }
    }
}