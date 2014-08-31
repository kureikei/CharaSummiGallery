using CoreTweet;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
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
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ViewBag.Status = "ツイートの取得に失敗したよ";
                return View();
            }
        }

        public ActionResult Latest(string ids)
        {
            return RedirectToAction("Index", new { ids = "500502509308231681,500494860948762626,500556010411405313,499187649664401409,499706162061643776,499116220830863361,499878140290678784,499205208308609024,499502799307018240,499491770409832448,500126486129434624,499171426352365568,499861121184972800,500170091804114944,499127169117741056,500095043261067265,499113815011635201,500251309274984448,499769955076694019,499197403417698305,499361768544555008,500248335165304832,499410019872305152,499346797819461632,499142822373507073,499130610422517760,500140819853357056,499219470095618048,501253305956175872,501962105260482560,501840751303475200,502057898260717568,502114760523923456,502678546251124736,503394545266999297,503135485833646080,503736171642372096,504874915070095360,504924941121093632" });
        }
    }
}