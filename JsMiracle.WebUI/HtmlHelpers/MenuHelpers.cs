﻿using JsMiracle.Entities;
using JsMiracle.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.HtmlHelpers
{
    public static class MenuHelpers
    {
        public static MvcHtmlString MenuLinks(this HtmlHelper html,
          List<IMS_TB_Module> menuList)
        {
            if (menuList == null)
                return new MvcHtmlString("");

            int? lastIdx = -1;
            StringBuilder result = new StringBuilder();
            foreach (var menu in menuList)
            {
                if (menu.ParentID  == -1)
                {
                    if (lastIdx != -1 && lastIdx != menu.ID)
                    {
                        result.Append("</ul></div>");
                    }
                    lastIdx = menu.ID;
                    result.AppendFormat("<div title='{0}'>", menu.ModuleName);
                    result.Append("<ul class='content' >");
                }
                else
                {
                    result.Append("<li>");
                    result.AppendFormat("<a href='{0}' target='ibody'>{1}</a>", menu.URL, menu.ModuleName);
                    result.Append("</li>");
                }
            }
            result.Append("</ul></div>");
            return MvcHtmlString.Create(result.ToString());
        }
    }
}