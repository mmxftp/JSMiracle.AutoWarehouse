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

        private IDataLayer<IMS_TB_Module> dal;

        public NavController(IDataLayer<IMS_TB_Module> repo)
        {
            this.dal = repo;
        }

        public PartialViewResult Menu()
        {
            var rootModuleList = dal.FindWhere(n=>n.ParentID == -1);

            if (rootModuleList == null)
                return PartialView(null);

            var moduleList = dal.FindWhere(n => n.ParentID != -1);

            var data = rootModuleList.OrderBy(n => n.SortID);

            var menuList = new List<IMS_TB_Module>();

            foreach (var m in data)
            {
                var x = from b in moduleList
                        where b.ParentID == m.ModuleID
                        select b;

                menuList.Add(m);
                menuList.AddRange(x);
            }            

            return PartialView(menuList);
        }

    }
}
