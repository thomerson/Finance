using System;
using System.Collections.Generic;
using System.Text;

namespace Thomerson.Gatlin.Finance.Model
{
    public class AccountRecord : Stock
    {
        /// <summary>
        /// 交易单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 交易股数
        /// </summary>
        public int HandAmount { get; set; }
        /// <summary>
        /// 交易总额
        /// </summary>
        public decimal TotalPrice { get { return Price * HandAmount; } }
    }
}
