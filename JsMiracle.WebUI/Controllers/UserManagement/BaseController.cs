using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Controllers.FilterAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers.UserManagement
{
    [AuthorizeFilter]
    [ExceptionFilter]
    public class BaseController : Controller
    {

    }
}