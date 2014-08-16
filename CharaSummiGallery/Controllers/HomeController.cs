using CoreTweet;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CharaSummiGallery.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 900, VaryByParam = "ids")]
        // GET: Default
        public ActionResult Index(string ids)
        {
            if (string.IsNullOrEmpty(ids) || !Regex.IsMatch(ids, @"^[\d,]+$"))
            {
                return View();
            }

            try
            {
                var tokens = Tokens.Create(
                    WebConfigurationManager.AppSettings["ConsumerKey"],
                    WebConfigurationManager.AppSettings["ConsumerSecret"],
                    WebConfigurationManager.AppSettings["AccessToken"],
                    WebConfigurationManager.AppSettings["AccessSecret"]);
                return View(tokens.Statuses.Lookup(id => ids));
            }
            catch (Exception)
            {
                ViewBag.Status = "ツイートの取得に失敗したよ";
                return View();
            }
        }
    }
}