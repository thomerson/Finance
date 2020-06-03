using System;

namespace Thomerson.Gatlin.Finance.Model
{
    /// <summary>
    /// 交易
    /// </summary>
    public class StockTrade
    {
        public string StockCode { get; set; }
        public string StockName { get; set; }
        /// <summary>
        /// 交易股数
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 交易单价
        /// </summary>
        public decimal Price { get; set; }
        ///// <summary>
        ///// 交易手数
        ///// 一次交易买+1
        ///// 一次交易卖-1
        ///// </summary>
        //public int HandAmount { get; set; }

    }
}
