using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.View
{
    public class PermissionViewModule
    {
        private List<IMS_UP_MK> _modules;

        // 模块权限
        public List<IMS_UP_MK> Modules
        {
            get { return _modules; }
            set
            {
                _modules = value;
                if (ActionDic == null)
                    ActionDic = new Dictionary<string, string>();

                if (_modules != null)
                {
                    foreach (var v in _modules)
                    {
                        if (string.IsNullOrEmpty(v.KZMC) || string.IsNullOrEmpty(v.HDMC))
                            continue;

                        string key = string.Format("{0}{1}", v.KZMC.ToLower(), v.HDMC.ToLower());
                        //ActionDic.Add(key, v);
                        ActionDic.Add(key, key);
                    }
                }
            }
        }

        private List<IMS_UP_MKGN> _functions;

        // 子功能权限
        public List<IMS_UP_MKGN> Functions
        {
            get { return _functions; }
            set
            {
                _functions = value;

                if (ActionDic == null)
                    ActionDic = new Dictionary<string, string>();

                if (_functions != null)
                {
                    foreach (var v in _functions)
                    {
                        if (string.IsNullOrEmpty(v.KZMC) || string.IsNullOrEmpty(v.HDMC))
                            continue;

                        string key = string.Format("{0}{1}", v.KZMC.ToLower(), v.HDMC.ToLower());
                        //ActionDic.Add(key, v);
                        ActionDic.Add(key, key);
                    }
                }
            }
        }


        //public PermissionViewModule(IList<IMS_UP_MK> modules, IList<IMS_UP_MKGN> functions)
        //{
        //    this.Modules = modules;
        //    this.Functions = functions;

        //    ActionDic = new Dictionary<string, object>();
        //    if (modules != null)
        //    {
        //        foreach (var v in modules)
        //        {
        //            if (string.IsNullOrEmpty(v.KZMC) || string.IsNullOrEmpty(v.HDMC))
        //                continue;

        //            string key = string.Format("{0}{1}", v.KZMC.ToLower(), v.HDMC.ToLower());
        //            ActionDic.Add(key, v);
        //        }
        //    }

        //    if (functions != null)
        //    {
        //        foreach (var v in functions)
        //        {
        //            if (string.IsNullOrEmpty(v.KZMC) || string.IsNullOrEmpty(v.HDMC))
        //                continue;

        //            string key = string.Format("{0}{1}", v.KZMC.ToLower(), v.HDMC.ToLower());
        //            ActionDic.Add(key, v);
        //        }
        //    }
        //}

        private Dictionary<string, string> ActionDic;

        /// <summary>
        /// 是否存在指定的权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ExistsPermissions(string controller, string action)
        {
            string key = string.Format("{0}{1}", controller.ToLower(), action.ToLower());
            return ActionDic.ContainsKey(key);
        }
    }
}
