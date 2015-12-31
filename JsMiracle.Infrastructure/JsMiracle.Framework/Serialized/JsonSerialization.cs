using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework.Serialized
{
    public class JsonSerialization
    {

        public static T Deserialize<T>(string json) 
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static string Serialize<T>(T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
