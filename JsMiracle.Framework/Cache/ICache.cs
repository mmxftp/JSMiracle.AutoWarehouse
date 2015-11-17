using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework.Cache
{
    /// <summary>
    /// 用于缓存数据
    /// </summary>
    public interface ICache
    {             
        object GetApplicationCache(string key);

        void SetApplicationCache(string key, object obj);

        void RemoveApplicationCache(string key);

        object GetSessionCache(string key);

        void SetSessionCache(string key, object obj);

        void RemoveSessionCache(string key);
    }
}
