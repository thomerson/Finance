using System.Collections.Generic;

namespace Thomerson.Gatlin.Finance.Model
{
    /// <summary>
    /// 账户历史流水记录
    /// </summary>
    public class AccountHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// 当前现金金额
        /// </summary>
        public decimal CurrentAvailableMoney { get; set; }
        /// <summary>
        /// 当前持有股票
        /// </summary>
        public List<StockTrade> CurrentHoldStocks { get; set; }

    }
}
