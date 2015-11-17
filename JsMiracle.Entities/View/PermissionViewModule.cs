using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.View
{
    public class PermissionViewModule
    {
        // 模块权限
        public IList<IMS_TB_Module> Modules { get; private set; }

        // 子功能权限
        public IList<IMS_TB_ModuleFunction> Functions { get;private set; }

        public PermissionViewModule(IList<IMS_TB_Module> modules, IList<IMS_TB_ModuleFunction> functions)
        {
            this.Modules = modules;
            this.Functions = functions;

            ActionDic = new Dictionary<string,object>();
            if ( modules != null)
            {
                foreach ( var v in modules)
                {
                    if (string.IsNullOrEmpty(v.Controller_Name) || string.IsNullOrEmpty(v.Action_Name))
                        continue;

                    string key = string.Format("{0}{1}", v.Controller_Name.ToLower(), v.Action_Name.ToLower());
                    ActionDic.Add(key, v);
                }
            }

            if ( functions != null)
            {
                foreach (var v in functions)
                {
                    if (string.IsNullOrEmpty(v.Controller_Name) || string.IsNullOrEmpty(v.Action_Name))
                        continue;

                    string key = string.Format("{0}{1}", v.Controller_Name.ToLower(), v.Action_Name.ToLower());
                    ActionDic.Add(key, v);
                }
            }
        }

        private Dictionary<string, object> ActionDic;

        /// <summary>
        /// 是否存在指定的权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ExistsPermissions(string controller, string action)
        {
            string key = string.Format( "{0}{1}", controller.ToLower(), action.ToLower());
            return ActionDic.ContainsKey(key);
        }
    }
}
