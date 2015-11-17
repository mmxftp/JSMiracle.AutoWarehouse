using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework
{
    public class JsMiracleException:Exception
    {
        public JsMiracleException(string message) : base(message) { }

        public JsMiracleException(string title, string message) : base(message) { }

        public string Title { get; set; }

    }
}
