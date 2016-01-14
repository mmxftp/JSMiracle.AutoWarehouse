using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.View
{
    /// <summary>
    /// 查询实体
    /// (此实体与javascript对象对应，如需修改请一同修改
    /// Scripts\CustomScripts\knockout.QueryBuilder.js 中内容)
    /// </summary>
    public class QueryBuilderModule
    {
        public string fieldText { get; set; }


        public string fieldValue { get; set; }

        public string splitChar { get; set; }
    }
}
