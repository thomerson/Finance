using System;
using Thomerson.Gatlin.Finance.Model;
using Thomerson.Gatlin.Finance.Unitity;

namespace Thomerson.Gatlin.Finance.Core
{
    public class UserAppContext
    {
        public static UserInfo CurrentUser;

        public static bool BuyStock(StockTrade trade)
        {
            if (trade.Price > CurrentUser.AvailableMoney)
            {
                return false;
            }

            if (CurrentUser.StockAmount.ContainsKey(trade.StockCode) && CurrentUser.StockAmount[trade.StockCode].HoldHandAmount > 3)
            {
                return false;
            }

            //生成交易记录
            var stockRecord = new TradingRecord()
            {
                StockCode = trade.StockCode,
                StockName = trade.StockName,
                UserId = CurrentUser.UserId,
                TradingTpye = TradingTpye.Buy,
                Price = trade.Price,
                Amount = AmountUnitity.BuyAmount(trade.Price),
                DataDate = DateTime.Now.ToString("yyyy-MM-dd"),
                IsChecked = false
            };

            stockRecord.CalculateServiceCharge();


            Console.WriteLine($"买入股票{trade.StockName}{stockRecord.Amount}股，单价{trade.Price},手续费共{stockRecord.TotalServiceCharge}");
            CurrentUser.AvailableMoney = CurrentUser.AvailableMoney - stockRecord.TotalPrice - stockRecord.TotalServiceCharge; //减去手续费

            TradingRecordCore.Insert(stockRecord);

            if (CurrentUser.StockAmount.ContainsKey(trade.StockCode))
            {
                CurrentUser.StockAmount[trade.StockCode].HoldAmount += stockRecord.Amount;
            }
            else
            {
                CurrentUser.StockAmount.Add(trade.StockCode, new StockAmount()
                {
                    HoldAmount = stockRecord.Amount,
                    AvailableAmount = 0,
                });
            }
            return true;
        }

        public static bool SellStock(StockTrade trade)
        {
            if (!CurrentUser.StockAmount.ContainsKey(trade.StockCode) || CurrentUser.StockAmount[trade.StockCode].AvailableAmount > 0)
            {
                return false;
            }

            var firstAvailableTrade = TradingRecordCore.GetLatestBuyRecord(CurrentUser.UserId, trade.StockCode);
            if (firstAvailableTrade == null || firstAvailableTrade.Amount <= 0)
            {
                return false;
            }

            //生成交易记录
            var stockRecord = new TradingRecord()
            {
                StockCode = trade.StockCode,
                StockName = trade.StockName,
                UserId = CurrentUser.UserId,
                TradingTpye = TradingTpye.Sell,
                Price = trade.Price,
                Amount = firstAvailableTrade.Amount,
                DataDate = DateTime.Now.ToString("yyyy-MM-dd"),
                IsChecked = true
            };

            stockRecord.CalculateServiceCharge();

            Console.WriteLine($"卖出股票{trade.StockName}{stockRecord.Amount}股，单价{trade.Price},手续费共{stockRecord.TotalServiceCharge}");
            CurrentUser.AvailableMoney = CurrentUser.AvailableMoney + stockRecord.TotalPrice - stockRecord.TotalServiceCharge; //减去手续费

            CurrentUser.StockAmount[trade.StockCode].HoldAmount -= stockRecord.Amount;
            CurrentUser.StockAmount[trade.StockCode].AvailableAmount -= stockRecord.Amount;

            TradingRecordCore.Insert(stockRecord);
            TradingRecordCore.SetChecked(firstAvailableTrade.Id);



            return true;
        }
    }
}
