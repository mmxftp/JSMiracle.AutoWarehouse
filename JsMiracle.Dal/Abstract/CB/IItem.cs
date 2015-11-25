using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.CB
{
    public interface IItem : IDataLayer<IMS_CB_WL>
    {
        IMS_CB_WL GetEntity(string wlbh);
    }
}
