using hasbiHAliCia.Veri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace hasbiHAliCia.Controllers.Araclar
{
    public class ZiyaretActionFilter : ActionFilterAttribute
    {
        public static Ziyaret ziyaret;

        public override void OnActionExecuting(ActionExecutingContext seciliBaglam)
        {
            ziyaret = new Ziyaret(seciliBaglam.HttpContext);
            Controller denetleyici = seciliBaglam.Controller as Controller;
            denetleyici.ViewBag.Ziyaretci = ziyaret.BilgilerTekSatir();
            ziyaret.Kaydet();
            
        }
    }
}