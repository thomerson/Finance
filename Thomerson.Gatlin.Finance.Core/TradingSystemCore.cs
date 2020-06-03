using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Thomerson.Gatlin.Finance.Model;

namespace Thomerson.Gatlin.Finance.Core
{
    /// <summary>
    /// 规则：
    /// 1.T+1
    /// 2.涨停不可买 跌停不可卖
    /// 3.分红日等价转化股份
    /// </summary>
    public class TradingSystemCore
    {

        public static void Exec(string stockCode, int tradingSystemId)
        {
            var list = new StockRecordCore().GetStockByCode(stockCode);
            var stockName = list.First().Name;

            for (int i = 1; i < list.Count; i++)
            {
                var date = list[i].DataDate;
                Console.WriteLine($"交易日{date}");
                var buy = false;
                var sell = false;

                foreach (var item in UserAppContext.CurrentUser.StockAmount)
                {
                    item.Value.AvailableAmount = item.Value.HoldAmount;
                }

                #region 开盘买入
                if (list[i - 1].ClosingPrice > list[i - 1].OpeningPrice) //昨日收阳
                {
                    if (list[i].OpeningPrice > list[i - 1].ClosingPrice) //今日开阳
                    {
                        //开盘涨幅
                        var increasePercent = Math.Round(list[i].OpeningPrice / list[i - 1].ClosingPrice, 4) - 1;
                        if (increasePercent > 0.05M)
                        {
                            //开阳大涨5%
                            continue;
                        }

                        var trade = new StockTrade()
                        {
                            StockCode = stockCode,
                            StockName = stockName,
                        };
                        //1.5~3.5 best  //涨1% 形成跳涨
                        if (increasePercent > 0.015M)
                        {
                            trade.Price = list[i].OpeningPrice;
                            buy = true;
                        }
                        else
                        {
                            //今日最高涨幅 
                            var highestIncreasePercent = Math.Round(list[i].HighestPrice / list[i - 1].OpeningPrice, 4) - 1;
                            if (highestIncreasePercent > 0.015M)
                            {
                                trade.Price = Math.Round(0.015M * list[i - 1].OpeningPrice, 2);
                                buy = true;
                            }
                        }

                        if (buy)
                        {
                            UserAppContext.BuyStock(trade);
                        }
                    }
                    else
                    {
                        //今日开阴
                        var decreasePercent = 1 - Math.Round(list[i].OpeningPrice / list[i - 1].ClosingPrice, 4);
                        if (!UserAppContext.CurrentUser.StockAmount.ContainsKey(stockCode) || UserAppContext.CurrentUser.StockAmount[stockCode].AvailableHandAmount < 1)
                        {
                            continue;
                        }

                        var trade = new StockTrade()
                        {
                            StockCode = stockCode,
                            StockName = stockName
                        };

                        //大跌1.5%
                        if (decreasePercent > 0.015M)
                        {
                            trade.Price = list[i].OpeningPrice;
                            sell = true;
                        }
                        else
                        {


                            //今日最大跌幅 
                            var lowestIncreasePercent = 1 - Math.Round(list[i].LowestPrice / list[i - 1].OpeningPrice, 4);
                            if (lowestIncreasePercent > 0.015M)
                            {
                                trade.Price = Math.Round((1 - 0.015M) * list[i - 1].OpeningPrice, 2);
                                sell = true;
                            }
                        }

                        if (sell)
                        {
                            UserAppContext.SellStock(trade);
                        }

                    }
                }
                else//昨日收阴
                {
                    if (list[i].OpeningPrice < list[i - 1].ClosingPrice) //开盘价小于昨日收盘价
                    {
                        var decreasePercent = 1 - Math.Round(list[i].OpeningPrice / list[i - 1].ClosingPrice, 4);
                        if (!UserAppContext.CurrentUser.StockAmount.ContainsKey(stockCode) || UserAppContext.CurrentUser.StockAmount[stockCode].AvailableHandAmount < 1)
                        {
                            continue;
                        }

                        var trade = new StockTrade()
                        {
                            StockCode = stockCode,
                            StockName = stockName
                        };

                        //大跌1.5%
                        if (decreasePercent > 0.015M)
                        {
                            trade.Price = list[i].OpeningPrice;
                        }
                        else
                        {
                            //今日最大跌幅 
                            var lowestIncreasePercent = 1 - Math.Round(list[i].LowestPrice / list[i - 1].OpeningPrice, 4);
                            if (lowestIncreasePercent > 0.015M)
                            {
                                trade.Price = Math.Round((1 - 0.015M) * list[i - 1].OpeningPrice, 2);
                                sell = true;
                            }
                        }

                        if (sell)
                        {
                            UserAppContext.SellStock(trade);
                        }
                    }
                }
                #endregion


                Console.WriteLine("******************************");
            }
        }
    }
}
