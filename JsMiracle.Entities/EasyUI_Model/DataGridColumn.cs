using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.EasyUI_Model
{
    public class DataGridColumn
    {

        public string field { get; set; }

        public string title { get; set; }

        public string width { get; set; }

        public string align { get; set; }


        readonly Dictionary<string, string> functionJavascriptDic;

        public DataGridColumn()
        {
            functionJavascriptDic = new Dictionary<string, string>();
        }

        [JsonIgnore]
        public string styler
        {
            get { return functionJavascriptDic["styler"]; }
            set { functionJavascriptDic["styler"] = string.Concat("styler:", value); }
        }

        [JsonIgnore]
        public string formatter
        {
            get { return functionJavascriptDic["formatter"]; }
            set { functionJavascriptDic["formatter"] = string.Concat("formatter:", value); }
        }


        public override string ToString()
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(this);

            if (functionJavascriptDic.Count == 0)
                return str;

            StringBuilder sb = new StringBuilder();
            sb.Append(str.TrimEnd('}'));

            foreach (var kv in functionJavascriptDic)
            {
                sb.Append("," + kv.Value);
            }

            return string.Concat(sb.ToString(), "}");
        }

    }


}
