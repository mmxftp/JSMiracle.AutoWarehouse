using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
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

        private IModule dal;

        public NavController(IModule repo)
        {
            this.dal = repo;
        }

        public PartialViewResult Menu()
        {
            var rootModuleList = dal.GetRootModule();
                
            if (rootModuleList == null)
                return PartialView(null);

            var data = rootModuleList.OrderBy(n => n.SortID);

            var menuList = new List<IMS_TB_Module>();

            foreach (var m in data)
            {
                var x = dal.GetChildModuleList(m.ModuleID);

                menuList.Add(m);
                menuList.AddRange(x);
            }            

            return PartialView(menuList);
        }

    }
}
