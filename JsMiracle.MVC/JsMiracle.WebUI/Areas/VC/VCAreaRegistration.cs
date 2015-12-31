using System.Web.Mvc;

namespace JsMiracle.WebUI.Areas.VC
{
    public class VCAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "VC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "VC_default",
                "VC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
