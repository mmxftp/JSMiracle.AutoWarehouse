using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.Entities
{
    public interface IModelBase
    {
        long ID { get; set; }
    }

    #region UP

    public partial class IMS_UP_JSYH : IModelBase { }

    public partial class IMS_UP_JS : IModelBase { }

    public partial class IMS_UP_MKGN : IModelBase { }

    public partial class IMS_UP_JSMK : IModelBase { }
    #endregion

    #region CB



    #endregion

    #region CM
    public partial class IMS_CM_DM : IModelBase { }

    public partial class IMS_CM_DMLX : IModelBase { }

    #endregion
}
