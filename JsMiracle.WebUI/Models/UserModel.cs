using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace JsMiracle.WebUI.Models
{
    public class UserModel
    {
        //public PagedList<IMS_TB_UserInfo> Infos { get; set; }

        /// <summary>
        /// easyui datagrid 要用的字段不可修改名称
        /// </summary>
        public int total { get; set; }

        public IList<IMS_TB_UserInfo> rows { get; set; }
    }
}