using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        /// <summary>
        /// 客户端 设置crossDomain: true
        /// </summary>
        /// <returns></returns>
        public JsonpResult JsonpReturn()
        {
            return new JsonpResult(new { Message = "jsonp跨域请求成功！" });
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

    /// <summary>
    ///     jsonp响应结果
    /// </summary>
    public class JsonpResult : JsonResult
    {
        private readonly object _data;

        public JsonpResult()
        {
        }

        public JsonpResult(object data)
        {
            _data = data;
        }

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            if (controllerContext != null)
            {
                var response = controllerContext.HttpContext.Response;
                var request = controllerContext.HttpContext.Request;

                var callbackfunction = request["callback"];
                if (string.IsNullOrEmpty(callbackfunction))
                {
                    throw new Exception("Callback function name must be provided in the request!");
                }
                response.ContentType = "application/x-javascript";
                if (_data != null)
                {
                    var serializer = new JavaScriptSerializer();
                    response.Write(string.Format("{0}({1});", callbackfunction, serializer.Serialize(_data)));
                }
            }
        }
    }

}