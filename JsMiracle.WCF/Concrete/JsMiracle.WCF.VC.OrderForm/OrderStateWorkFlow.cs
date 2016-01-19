using JsMiracle.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.VC.OrderForm
{
    public class OrderStateWorkFlow
    {
        public static EnumOrderFormState GetNextOrderFormState(EnumOrderFormState? currentState)
        {
            EnumOrderFormState nextState = EnumOrderFormState.VHSTS_New;

            if (currentState == null)
                return nextState;

            switch (currentState)
            {
                case EnumOrderFormState.VHSTS_New:
                    nextState = EnumOrderFormState.VHSTS_Ready;
                    break;

                case  EnumOrderFormState.VHSTS_Submit:
                    nextState = EnumOrderFormState.VHSTS_Submit;
                    break;

                case EnumOrderFormState.VHSTS_Cancel:
                case EnumOrderFormState.VHSTS_Completed:
                case EnumOrderFormState.VHSTS_Closed:
                    nextState = EnumOrderFormState.VHSTS_Closed;
                    break;

                case EnumOrderFormState.VHSTS_Ready:
                    nextState = EnumOrderFormState.VHSTS_Completed;
                    break;

                default:
                    throw new Exception("未知状态" + currentState.ToString());
            }

            return nextState;

        }

        public static EnumOrderDataState GetNextOrderDataState(EnumOrderDataState? currentState)
        {
            EnumOrderDataState nextState = EnumOrderDataState.VLSTS_New;

            if (currentState == null)
                return nextState;


            switch (currentState)
            {
                case EnumOrderDataState.VLSTS_New:
                case EnumOrderDataState.VLSTS_Ready:
                    nextState = EnumOrderDataState.VLSTS_Released;
                    break;

                case EnumOrderDataState.VLSTS_Released:
                    nextState = EnumOrderDataState.VLSTS_Completed;
                    break;

                case EnumOrderDataState.VLSTS_Cancel:                   
                case EnumOrderDataState.VLSTS_Completed:
                    // 没有后续状态
                    break;

                default:
                    throw new Exception("未知状态" + currentState.ToString());
            }

            return nextState;
        }

    }
}
