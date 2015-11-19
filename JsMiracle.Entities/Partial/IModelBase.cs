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

    public partial class IMS_UP_RoUr : IModelBase { }

    public partial class IMS_UP_Role : IModelBase { }

    public partial class IMS_UP_MoFn : IModelBase { }

    public partial class IMS_UP_RoMo : IModelBase { }

    public partial class IMS_BS_Item : IModelBase { }

}
