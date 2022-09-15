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

        /// <summary>
        /// Вызывается когда решили применить баллы к чеку
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="transaction_id"></param>
        /// <param name="error_message"></param>
        /// <returns></returns>
        public delegate bool TApplyPoints(int amount, int transaction_id, [MarshalAs(UnmanagedType.LPWStr)] string order_guid, ref string error_message);

        /// <summary>
        /// Вызывается когда решили накопить баллы 
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="phone"></param>
        /// <param name="error_message"></param>
        /// <returns></returns>
        public delegate bool TEnrollPoints(int amount, [MarshalAs(UnmanagedType.LPWStr)] string phone, [MarshalAs(UnmanagedType.LPWStr)] string order_guid, ref string error_message);

        /// <summary>
        /// Вызывается когда накладывается скидка 15%
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="error_message"></param>
        /// <returns></returns>
        public delegate bool TApplyDiscount(int amount, [MarshalAs(UnmanagedType.LPWStr)] string order_guid, ref string error_message);


        [DllImport("SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern void ShowLoyalty_IIKO(TApplyPoints apply_comp, TEnrollPoints enroll_points, int shop_id, DateTime business_date, string json_order);

        [DllImport("SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern void ShowLoyaltyWitchCardCode_IIKO(TApplyPoints apply_points, TEnrollPoints enroll_points, TApplyDiscount apply_discount, string card_code, int shop_id, DateTime business_date, string order_json);

        [DllImport("SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool CloseCheckWitchWriteOffPoints_IIKO(int transaction_id);

        [DllImport("SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool CloseCheckWitchEnrollPoints_IIKO(int shop_id, DateTime business_date, string phone, string order_json);

        [DllImport("SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool CloseCheckWitchApplyDiscount_IIKO(int shop_id, DateTime business_date, string card_code, string order_json);
        
        [DllImport("SUROK.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern void CancelTransaction_IIKO(int transaction_id);


        /// <summary>
        /// Вызываем по кнопке
        /// </summary>
        /// <param name="apply_comp"></param>
        /// <param name="enroll_points"></param>
        /// <param name="shop_id"></param>
        /// <param name="business_date"></param>
        /// <param name="order"></param>
        public void ShowLoyalty(TApplyPoints apply_comp, TEnrollPoints enroll_points, int shop_id, DateTime business_date, Order order)
        {
            ShowLoyalty_IIKO(apply_comp, enroll_points, shop_id, business_date, order.GetJsonStr());
        }

        /// <summary>
        /// Вызываем при прокатке карты
        /// </summary>
        /// <param name="apply_points"></param>
        /// <param name="enroll_points"></param>
        /// <param name="apply_discount"></param>
        /// <param name="card_code"></param>
        /// <param name="shop_id"></param>
        /// <param name="business_date"></param>
        /// <param name="order"></param>
        public void ShowLoyalty(TApplyPoints apply_points, TEnrollPoints enroll_points, TApplyDiscount apply_discount, string card_code, int shop_id, DateTime business_date, Order order)
        {
            ShowLoyaltyWitchCardCode_IIKO(apply_points, enroll_points, apply_discount, card_code, shop_id, business_date, order.GetJsonStr());
        }

        /// <summary>
        /// Вызываем при закрытии чека при условии что к чеку применили баллы (скидку)
        /// </summary>
        /// <param name="transaction_id"></param>
        /// <returns></returns>
        public bool CloseCheck(int transaction_id)
        {
            return CloseCheckWitchWriteOffPoints_IIKO(transaction_id);
        }

        /// <summary>
        /// Вызываем при закрытии чека при условии что нужно накопить быллы
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="business_date"></param>
        /// <param name="phone"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool CloseCheck(int shop_id, DateTime business_date, string phone, Order order)
        {
            return CloseCheckWitchEnrollPoints_IIKO(shop_id, business_date, phone, order.GetJsonStr());
        }

        /// <summary>
        /// Вызываем при закрытии чека при условии что на чек наложена скидка 
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="business_date"></param>
        /// <param name="phone"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool CloseCheckWitchDiscount(int shop_id, DateTime business_date, string card_code, Order order)
        {
            return CloseCheckWitchApplyDiscount_IIKO(shop_id, business_date, card_code, order.GetJsonStr());            
        }

        /// <summary>
        /// Вызываем когда удалили баллы (скидку) с чека
        /// </summary>
        /// <param name="transaction_id"></param>
        public void CancelTransaction(int transaction_id)
        {
            CancelTransaction_IIKO(transaction_id);
        }

    }
}
