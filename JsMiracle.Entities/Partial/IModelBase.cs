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

    public partial class IMS_TB_RoleUser : IModelBase { }

    public partial class IMS_TB_RoleInfo : IModelBase { }

    public partial class IMS_TB_ModuleFunction : IModelBase { }

    public partial class IMS_TB_Permission : IModelBase { }

}
