using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SurokConnector
{
    public class HelperSurok
    {
        public delegate bool TApplyPoints(int amount, int transaction_id, ref string error_message);
        public delegate bool TEnrollPoints(int amount, [MarshalAs(UnmanagedType.LPWStr)] string phone, ref string error_message);
        public delegate bool TApplyDiscount(int amount, ref string error_message);


        [DllImport("C:\\PROJECT\\SUROK\\Win32\\Debug\\SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern void ShowLoyalty_IIKO(TApplyPoints apply_comp, TEnrollPoints enroll_points, int shop_id, DateTime business_date, string json_order);

        [DllImport("C:\\PROJECT\\SUROK\\Win32\\Debug\\SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern void ShowLoyaltyWitchCardCode_IIKO(TApplyPoints apply_points, TEnrollPoints enroll_points, TApplyDiscount apply_discount, string card_code, int shop_id, DateTime business_date, string order_json);

        [DllImport("C:\\PROJECT\\SUROK\\Win32\\Debug\\SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool CloseCheckWitchWriteOffPoints_IIKO(int transaction_id);

        [DllImport("C:\\PROJECT\\SUROK\\Win32\\Debug\\SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool CloseCheckWitchEnrollPoints_IIKO(int shop_id, DateTime business_date, string phone, string order_json);


        public void ShowLoyalty(TApplyPoints apply_comp, TEnrollPoints enroll_points, int shop_id, DateTime business_date, Order order)
        {
            ShowLoyalty_IIKO(apply_comp, enroll_points, shop_id, business_date, order.GetJsonStr());
        }

        public void ShowLoyalty(TApplyPoints apply_points, TEnrollPoints enroll_points, TApplyDiscount apply_discount, string card_code, int shop_id, DateTime business_date, Order order)
        {
            ShowLoyaltyWitchCardCode_IIKO(apply_points, enroll_points, apply_discount, card_code, shop_id, business_date, order.GetJsonStr());
        }

        public bool CloseCheck(int transaction_id)
        {
            return CloseCheckWitchWriteOffPoints_IIKO(transaction_id);
        }

        public bool CloseCheck(int shop_id, DateTime business_date, string phone, Order order)
        {
            return CloseCheckWitchEnrollPoints_IIKO(shop_id, business_date, phone, order.GetJsonStr());
        }

    }
}
