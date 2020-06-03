using System;
using Thomerson.Gatlin.Finance.Core;
using Thomerson.Gatlin.Finance.Model;

namespace Thomerson.Gatlin.Finance.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var user = new UserInfo()
            {
                UserId = "thomerson",
                StockMoney = 0,
                AvailableMoney = 50000
            };
            UserAppContext.CurrentUser = user;

            Test();
        }

        private static void Test()
        {
            var stockCode = "603259";
            var tradingSystemId = 1;
            TradingSystemCore.Exec(stockCode, tradingSystemId);
        }
    }
}
