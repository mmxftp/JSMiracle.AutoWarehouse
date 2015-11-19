using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities
{
    partial class IMS_UP_Modu : IModelBase
    {
        public string GetUrl()
        {
            if (!string.IsNullOrEmpty(this.Controller_Name)
                && !string.IsNullOrEmpty(this.Action_Name))
            {
                return string.Format("/{0}/{1}", this.Controller_Name, this.Action_Name);
            }

            return this.URL;
        }
    }
}
