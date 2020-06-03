using System.Collections.Generic;

namespace Thomerson.Gatlin.Finance.Model
{
    public class UserInfo
    {
        public UserInfo()
        {
            this.StockAmount = new Dictionary<string, StockAmount>();
        }

        public string UserId { get; set; }
        ///// <summary>
        ///// 持有股票金额
        ///// </summary>
        //public decimal StockMoney
        //{
        //    get
        //    {
        //        return this.UserStocks.Sum(s => s.Price * s.HoldAmount) ;
        //    }
        //}

        /// <summary>
        /// 可用购买金额
        /// </summary>
        public decimal AvailableMoney { get; set; }
        /// <summary>
        /// 当前持有
        /// </summary>
        public Dictionary<string, StockAmount> StockAmount { get; set; }

    }
}
