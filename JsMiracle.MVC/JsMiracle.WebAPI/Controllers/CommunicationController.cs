using JsMiracle.Framework.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebAPI.Controllers
{
    public class CommunicationController : Controller
    {
        ILog log;

        public CommunicationController(ILog repoLog)
        {
            log = repoLog;           
        }

        //
        // GET: /Communication/

        public ActionResult Index()
        {
            return View();
        }


    }
}
