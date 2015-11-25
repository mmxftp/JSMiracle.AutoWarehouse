using System.Web.Mvc;

namespace JsMiracle.WebUI.Areas.CB
{
    public class CBAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CB";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CB_default",
                "CB/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
