using System;

namespace Thomerson.Gatlin.Finance.Model
{
    /// <summary>
    /// 交易记录
    /// </summary>
    public class TradingRecord
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// 买/卖
        /// </summary>
        public TradingTpye TradingTpye { get; set; }
        /// <summary>
        /// 股票
        /// </summary>
        public string StockCode { get; set; }
        /// <summary>
        /// 股票
        /// </summary>
        public string StockName { get; set; }
        /// <summary>
        /// 成交价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 成交量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 成交金额
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                return Price * Amount * 100;
            }
        }
        /// <summary>
        /// 成交日期
        /// </summary>
        public string DataDate { get; set; }


        #region 手续费
        /// <summary>
        /// 手续费
        /// </summary>
        public decimal ServiceCharge { get; set; }
        /// <summary>
        /// 印花税
        /// </summary>
        public decimal StampDuty { get; set; }
        /// <summary>
        /// 过户费
        /// </summary>
        public decimal TransferFee { get; set; }

        /// <summary>
        /// 总手续费
        /// </summary>
        public decimal TotalServiceCharge
        {
            get
            {
                return this.ServiceCharge + this.StampDuty + this.TransferFee;
            }
        }

        /// <summary>
        /// 计算手续费
        /// </summary>
        public void CalculateServiceCharge()
        {
            this.ServiceCharge = Math.Round(TotalPrice * SystemSetting.ServiceChargePercent, 2);
            this.StampDuty = this.TradingTpye == TradingTpye.Buy ? 0M :
                Math.Round(TotalPrice * SystemSetting.StampDutyPercent, 2);
            this.TransferFee = Math.Round(TotalPrice * SystemSetting.TransferFeePercent, 2);
        }

        /// <summary>
        /// 是否核对
        /// 买<->卖一次为已核对
        /// </summary>
        public bool IsChecked { get; set; }

        public DateTime CreateStamp { get; set; }

        #endregion
    }
}
