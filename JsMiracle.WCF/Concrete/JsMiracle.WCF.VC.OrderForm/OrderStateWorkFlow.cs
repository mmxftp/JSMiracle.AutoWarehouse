using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.VC.OrderForm
{
    /// <summary>
    /// 订单状态工作流
    /// </summary>
    public class OrderStateWorkFlow
    {

        #region DJT
        /// <summary>
        /// 下一步骤可使用的订单头状态
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public static List<EnumOrderFormState> CanUseNextStepOrderFormStateList(EnumOrderFormState currentState)
        {
            List<EnumOrderFormState> allNextStateList = new List<EnumOrderFormState>();

            switch (currentState)
            {
                case EnumOrderFormState.VHSTS_New:
                case EnumOrderFormState.VHSTS_Submit:
                    allNextStateList.Add(EnumOrderFormState.VHSTS_Ready);
                    allNextStateList.Add(EnumOrderFormState.VHSTS_Submit);
                    break;

                case EnumOrderFormState.VHSTS_Ready:
                    allNextStateList.Add(EnumOrderFormState.VHSTS_Closed);
                    allNextStateList.Add(EnumOrderFormState.VHSTS_Completed);
                    allNextStateList.Add(EnumOrderFormState.VHSTS_Cancel);
                    break;

                case EnumOrderFormState.VHSTS_Cancel:
                case EnumOrderFormState.VHSTS_Completed:
                    allNextStateList.Add(EnumOrderFormState.VHSTS_Closed);
                    break;
                case EnumOrderFormState.VHSTS_Closed:
                    // 没有下一步
                    break;

                default:
                    throw new Exception("未知状态" + currentState.ToString());
            }

            return allNextStateList;
        }


        /// <summary>
        /// 是否包含订单头下一步状态
        /// </summary>
        /// <param name="currentState">当前状态</param>
        /// <param name="nextState">下一状态</param>
        /// <returns></returns>
        public static bool ContainNextOrderFormState(EnumOrderFormState currentState, EnumOrderFormState nextState)
        {
            List<EnumOrderFormState> allNextStateList = CanUseNextStepOrderFormStateList(currentState);

            // 输入的下一步是否在可操作的步骤内
            return (allNextStateList.Exists(n => n == nextState));
        }


        /// <summary>
        /// 更新订单可操作的属性
        /// </summary>
        /// <param name="entity">需更新属性的实例</param>
        public static void SetOrderFormProp(V_IMS_VC_DJT entity)
        {
            if (entity == null)
                return;

            var currentState = FunctionHelp.GetEnum<EnumOrderFormState>(entity.DJZT);
            var allNextStateList = CanUseNextStepOrderFormStateList(currentState);
            entity.CanCancel = false;
            entity.CanAudit = false;
            entity.CanClose = false;
            entity.CanComplete = false;
            entity.CanModify = false;

            foreach (var state in allNextStateList)
            {
                switch (state)
                {
                    case EnumOrderFormState.VHSTS_New:
                    case EnumOrderFormState.VHSTS_Submit:
                        entity.CanModify = true;
                        break;

                    case EnumOrderFormState.VHSTS_Ready:
                        entity.CanModify = true;
                        entity.CanAudit = true;
                        break;

                    case EnumOrderFormState.VHSTS_Cancel:
                        entity.CanCancel = true;
                        break;

                    case EnumOrderFormState.VHSTS_Completed:
                        entity.CanComplete = true;
                        break;

                    case EnumOrderFormState.VHSTS_Closed:
                        entity.CanClose = true;                        
                        break;

                    default:
                        throw new JsMiracleException(string.Format("未处理的状态:{0}", state.ToString()));
                }
            }

        }

        /// <summary>
        /// 根据单据头的状态得到可用于更新单据行的状态
        /// </summary>
        /// <param name="currentState">当前状态</param>
        /// <returns></returns>
        public static EnumOrderDataState GetOrderDataStateByOrderFormState(EnumOrderFormState currentState)
        {
            // 默认为无需状态更新的值 
            EnumOrderDataState state = EnumOrderDataState.None;

            switch (currentState)
            {
                case EnumOrderFormState.VHSTS_New:
                    state = EnumOrderDataState.VLSTS_New;
                    break;

                case EnumOrderFormState.VHSTS_Submit:
                case EnumOrderFormState.VHSTS_Ready:
                    state = EnumOrderDataState.VLSTS_Ready;
                    break;

                case EnumOrderFormState.VHSTS_Cancel:
                case EnumOrderFormState.VHSTS_Completed:
                case EnumOrderFormState.VHSTS_Closed:
                    // 无需状态更新，保持原状态
                    break;

                default:
                    throw new Exception("未知状态" + currentState.ToString());
            }

            return state;
        }
        #endregion


        #region DJH
        /// <summary>
        /// 更新订单可操作的属性
        /// </summary>
        /// <param name="entity">需更新属性的实例</param>
        public static void SetOrderDataProp(V_IMS_VC_DJH entity)
        {
            if (entity == null)
                return;

            var currentState = FunctionHelp.GetEnum<EnumOrderDataState>(entity.ZT);
            var allNextStateList = CanUseNextStepOrderDataStateList(currentState);
            entity.CanCancel= false;
            entity.CanZP = false;
            entity.CanRelease = false;
            entity.CanComplete = false;
            entity.CanModify = false;

            foreach (var state in allNextStateList)
            {
                switch (state)
                {
                    case EnumOrderDataState.VLSTS_New:
                    case EnumOrderDataState.VLSTS_Ready:
                        entity.CanModify = true;
                        break;

                    case EnumOrderDataState.VLSTS_Released :
                        entity.CanRelease = true;
                        break;

                    case EnumOrderDataState.VLSTS_Cancel :
                        entity.CanCancel = true;
                        entity.CanZP = true;
                        break;

                    case EnumOrderDataState.VLSTS_Completed:
                        entity.CanComplete = true;
                        entity.CanZP = true;
                        break;
                  
                    default:
                        throw new JsMiracleException(string.Format("未处理的状态:{0}", state.ToString()));
                }
            }
        }

        /// <summary>
        /// 下一步骤可使用的订单行状态
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public static List<EnumOrderDataState> CanUseNextStepOrderDataStateList(EnumOrderDataState currentState)
        {
            List<EnumOrderDataState> allNextStateList = new List<EnumOrderDataState>();

            switch (currentState)
            {
                case EnumOrderDataState.VLSTS_New:
                    allNextStateList.Add(EnumOrderDataState.VLSTS_Ready);
                    break;

                case EnumOrderDataState.VLSTS_Ready:
                    allNextStateList.Add(EnumOrderDataState.VLSTS_Released);
                    break;

                case EnumOrderDataState.VLSTS_Released:
                    allNextStateList.Add(EnumOrderDataState.VLSTS_Completed);
                    allNextStateList.Add(EnumOrderDataState.VLSTS_Cancel);
                    break;

                case EnumOrderDataState.VLSTS_Cancel:
                case EnumOrderDataState.VLSTS_Completed:
                    // 没有后续状态
                    break;

                default:
                    throw new Exception("未知状态" + currentState.ToString());
            }

            return allNextStateList;
        }

        /// <summary>
        /// 是否包含订单行下一步状态
        /// </summary>
        /// <param name="currentState">当前状态</param>
        /// <param name="nextState">下一状态</param>
        /// <returns></returns>
        public static bool ContainNextOrderDataState(
            EnumOrderDataState currentState, EnumOrderDataState nextState)
        {
            List<EnumOrderDataState> allNextStateList = CanUseNextStepOrderDataStateList(currentState);

            // 输入的下一步是否在可操作的步骤内
            return (allNextStateList.Exists(n => n == nextState));
        }

        #endregion
    }
}
