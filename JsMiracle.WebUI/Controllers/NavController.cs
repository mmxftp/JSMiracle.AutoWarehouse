using JsMiracle.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        public ActionResult Index()
        {
            
            return View();
        }





        public PartialViewResult Menu()
        {
            var menuList = new List<MenuModel>();



            menuList.Add(new MenuModel()
            {
                ID = 1,
                Name = "用户权限",
                Url = "",
                Parentid = 0
            });

            menuList.Add(new MenuModel()
            {
                ID = 3,
                Name = "Module",
                Url = "/module/index",
                Parentid = 1
            });

            menuList.Add(new MenuModel()
            {
                ID = 3,
                Name = "UserInfo",
                Url = "/user/list",
                Parentid = 1
            });

            menuList.Add(new MenuModel()
            {
                ID= 3,
                Name="extJs6",
                Url= "http://localhost/extjs6",
                Parentid = 1
            });
            menuList.Add(new MenuModel()
            {
                ID = 4,
                Name = "extJs6_Doc",
                Url = "http://localhost/extjs6_Doc",
                Parentid = 1
            });


            menuList.Add(new MenuModel()
            {
                ID = 2,
                Name = "仓库操作",
                Url = "",
                Parentid = 0
            });


            menuList.Add(new MenuModel()
            {
                ID = 5,
                Name = "extJs6",
                Url = "http://localhost/extjs6",
                Parentid = 2
            });
            menuList.Add(new MenuModel()
            {
                ID = 6,
                Name = "extJs6_Doc",
                Url = "http://localhost/extjs6_Doc",
                Parentid = 2
            });



            //int? lastIdx = -1;
            //StringBuilder result = new StringBuilder();
            //foreach (var menu in menuList)
            //{
            //    if (menu.Parentid == 0)
            //    {
            //        if (lastIdx != -1 && lastIdx != menu.ID)
            //        {
            //            result.Append("</ul></div>");
            //        }
            //        lastIdx = menu.ID ;
            //        result.AppendFormat("<div title='{0}'>", menu.Name);
            //        result.Append("<ul class='content' >");
            //    }
            //    else
            //    {
            //        result.Append("<li>");
            //        result.AppendFormat("<a href='{0}' target='ibody'>{1}</a>",menu.Url,menu.Name);
            //        result.Append("</li>");
            //    }
            //}
            //result.Append("</ul></div>");
            //return MvcHtmlString.Create(result.ToString());

            //ViewData["menuHtml"] = MvcHtmlString.Create(result.ToString());
            

            return PartialView(menuList);
        }

    }
}
