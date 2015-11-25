using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities
{
    partial class IMS_UP_MK : IModelBase
    {
        public string GetUrl()
        {
            if (!string.IsNullOrEmpty(this.KZMC)
                && !string.IsNullOrEmpty(this.HDMC))
            {
                return string.Format("/{0}/{1}", this.KZMC, this.HDMC);
            }

            return this.URL;
        }
    }
}
