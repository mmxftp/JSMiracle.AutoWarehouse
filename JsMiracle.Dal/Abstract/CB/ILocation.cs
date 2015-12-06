using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.CB
{
    /// <summary>
    /// 处理位置的接口
    /// </summary>
    public interface ILocation:IDataLayer<IMS_CB_WZ>
    {
        int InitLocation(int x, int y, int z);

        DataTable GetLocationState(int p, int maxL);
    }
}
