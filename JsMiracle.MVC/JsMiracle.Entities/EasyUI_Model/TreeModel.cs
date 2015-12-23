using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.EasyUI_Model
{
    public class TreeModel
    {
        public TreeModel()
        {
            this.attributes = new TreeAttr();
            this.children = new List<TreeModel>();
        }

        public int id { get; set; }

        public string text { get; set; }
        public bool @checked { get; set; }

        public List<TreeModel> children { get; set; }

        //public Dictionary<string, int> attributes { get; set; }
        public TreeAttr attributes { get; set; }

    }

    public class TreeAttr
    {
        public int parentid { get; set; }
        public int moduleid { get; set; }
        public int functionid { get; set; }
    }
}
