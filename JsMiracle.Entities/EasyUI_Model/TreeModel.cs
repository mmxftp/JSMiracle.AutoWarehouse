using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.EasyUI_Model
{
    public class TreeModel
    {
        public int id { get; set; }

        public string text { get; set; }
        public bool @checked { get; set; }

        public List<TreeModel> children { get; set; }

        public Dictionary<string, int> attributes { get; set; }

    }
}
