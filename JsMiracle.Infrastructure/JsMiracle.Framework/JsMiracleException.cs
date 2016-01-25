using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework
{
    [Serializable]
    public class JsMiracleException : Exception
    {
        public JsMiracleException(string message) : base(message) { }
        public JsMiracleException(string message, Exception innerExp) : base(message, innerExp) { }

        public JsMiracleException(string title, string message) : base(message) { this.Title = title; }

        public string Title { get; set; }

    }
}
