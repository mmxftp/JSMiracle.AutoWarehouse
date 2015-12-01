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
    /// <summary>
    /// 角色用户关系
    /// </summary>
    partial class IMS_UP_JSYH : IModelBase { }

    /// <summary>
    /// 角色信息
    /// </summary>
    partial class IMS_UP_JS : IModelBase { }

    /// <summary>
    /// 模块功能关系
    /// </summary>
    partial class IMS_UP_MKGN : IModelBase { }

    /// <summary>
    /// 角色模块关系
    /// </summary>
    partial class IMS_UP_JSMK : IModelBase { }

    /// <summary>
    /// 模块配置
    /// </summary>
    partial class IMS_UP_MK : IModelBase { }

    /// <summary>
    /// 用户信息
    /// </summary>
    partial class IMS_UP_YH : IModelBase { }

    #endregion

    #region CB

    /// <summary>
    /// 位置
    /// </summary>
    partial class IMS_CB_WZ : IModelBase { }

    /// <summary>
    /// 物料信息
    /// </summary>
    partial class IMS_CB_WL : IModelBase { }

    /// <summary>
    /// 容器类型
    /// </summary>
    partial class IMS_CB_RQLX : IModelBase { }

    /// <summary>
    /// 位置类型
    /// </summary>
    partial class IMS_CB_WZLX : IModelBase { }

    /// <summary>
    /// 容器
    /// </summary>
    partial class IMS_CB_RQ : IModelBase { }
    #endregion

    #region CM
    /// <summary>
    /// 代码
    /// </summary>
    partial class IMS_CM_DM : IModelBase { }

    /// <summary>
    /// 代码类型
    /// </summary>
    partial class IMS_CM_DMLX : IModelBase { }

    #endregion
}
