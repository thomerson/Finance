using System;
using Thomerson.Gatlin.Finance.Model;

namespace Thomerson.Gatlin.Finance.model
{
    /// <summary>
    /// 股票交易日数据记录
    /// </summary>
    public class StockRecord: Stock
    {
        public int? Id { get; set; }
        
        /// <summary>
        /// 交易日
        /// </summary>
        public string DataDate { get; set; }
        /// <summary>
        /// 开盘价
        /// </summary>
        public decimal OpeningPrice { get; set; }
        /// <summary>
        /// 收盘价
        /// </summary>
        public decimal ClosingPrice { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal HighestPrice { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal LowestPrice { get; set; }
        /// <summary>
        /// 前一天收盘价
        /// </summary>
        public decimal PreClosingPrice { get; set; }
        /// <summary>
        /// 涨跌额
        /// </summary>
        public decimal BalancePrice { get; set; }
        /// <summary>
        /// 涨跌幅
        /// </summary>
        public decimal BalanceRange { get; set; }
        /// <summary>
        /// 换手率
        /// </summary>
        public decimal TurnoverRate { get; set; }
        /// <summary>
        /// 成交量
        /// </summary>
        public int Volume { get; set; }
        /// <summary>
        /// 成交金额
        /// </summary>
        public Int64 TurnoverAmount { get; set; }
        /// <summary>
        /// 总市值
        /// </summary>
        public decimal TotalMarketCapitalisation { get; set; }
        /// <summary>
        /// 流通市值
        /// </summary>
        public decimal FreeFloatMarketCapitalisation { get; set; }

        public DateType DateType { get; set; }
    }
}
