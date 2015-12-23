using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework.Serialized
{
    public interface ISerialized
    {
        T Deserialize<T>(string xml) where T : class;

        string Serialize<T>(T obj) where T : class;
    }
}
