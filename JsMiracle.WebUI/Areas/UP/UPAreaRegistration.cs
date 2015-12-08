using System.Web.Mvc;

namespace JsMiracle.WebUI.Areas.UP
{
    public class UPAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UP_default",
                "UP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
