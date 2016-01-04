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
                if (_ActionDic == null)
                    _ActionDic = new Dictionary<string, object>();

                if (_modules != null)
                {
                    foreach (var v in _modules)
                    {
                        if (string.IsNullOrEmpty(v.KZMC) || string.IsNullOrEmpty(v.HDMC))
                            continue;

                        string key = string.Format("{0}{1}", v.KZMC.ToLower(), v.HDMC.ToLower());
                        //ActionDic.Add(key, v);
                        _ActionDic.Add(key, v);
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

                if (_ActionDic == null)
                    _ActionDic = new Dictionary<string, object>();

                if (_FunDic == null)
                    _FunDic = new Dictionary<int, List<IMS_UP_MKGN>>();

                if (_functions != null)
                {
                    foreach (var v in _functions)
                    {
                        if (string.IsNullOrEmpty(v.KZMC) || string.IsNullOrEmpty(v.HDMC))
                            continue;

                        string key = string.Format("{0}{1}", v.KZMC.ToLower(), v.HDMC.ToLower());
                        _ActionDic.Add(key, v);

                        List<IMS_UP_MKGN> dataList;

                        if (!_FunDic.TryGetValue(v.MKID, out dataList))
                        {
                            dataList = new List<IMS_UP_MKGN>();
                            _FunDic.Add(v.MKID, dataList);
                        }

                        dataList.Add(v);
                    }
                }
            }
        }

        /// <summary>
        /// 所有权限的集合
        /// </summary>
        private Dictionary<string, object> _ActionDic;

        /// <summary>
        /// 所有功能的集合
        /// </summary>
        private Dictionary<int, List<IMS_UP_MKGN>> _FunDic;


        /// <summary>
        /// 是否存在指定的权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ExistsPermissions(string area, string controller, string action)
        {
            string key = GetKey(area, controller, action);

            return _ActionDic.ContainsKey(key);
        }


        private string GetKey(string area, string controller, string action)
        {
            string key;

            if (!string.IsNullOrEmpty(area))
            {
                key =
                    string.Format("{0}/{1}{2}", area.ToLower(), controller.ToLower(), action.ToLower());
            }
            else
            {
                key =
                    string.Format("{0}{1}", controller.ToLower(), action.ToLower());
            }

            return key;
        }


        /// <summary>
        /// 得到模块对应的所有功能
        /// </summary>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public List<IMS_UP_MKGN> GetFunctionList(string area, string controller, string action)
        {
            string key = GetKey(area, controller, action);

            if (!_ActionDic.ContainsKey(key))
                return null;

            var module = _ActionDic[key] as IMS_UP_MK;

            if (module == null || _functions == null)
                return null;

            //return _functions.FindAll(n => n.MKID == module.MKID);

            if ( _FunDic.ContainsKey(module.MKID))
                return _FunDic[module.MKID];

            return null;
        }
    }
}
