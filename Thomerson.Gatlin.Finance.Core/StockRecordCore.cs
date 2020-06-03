using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thomerson.Gatlin.Finance.model;

namespace Thomerson.Gatlin.Finance.Core
{
    public class StockRecordCore
    {
        public List<StockRecord> GetStockByCode(string code)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Query<StockRecord>("select * from Stock WHERE 1=1 and Code = @code OERDER BY DataDate ", code)?.ToList();
            }
        }

        public int Insert(StockRecord stock)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Execute(@"INSERT INTO [dbo].[Stocks]
           ([Code]
           ,[Name]
           ,[DataDate]
           ,[OpeningPrice]
           ,[ClosingPrice]
           ,[HighestPrice]
           ,[LowestPrice]
           ,[PreClosingPrice]
           ,[BalancePrice]
           ,[BalanceRange]
           ,[TurnoverRate]
           ,[Volume]
           ,[TurnoverAmount]
           ,[TotalMarketCapitalisation]
           ,[FreeFloatMarketCapitalisation])
            VALUES(
            @Code
           ,@Name
           ,@DataDate
           ,@OpeningPrice
           ,@ClosingPrice
           ,@HighestPrice
           ,@LowestPrice
           ,@PreClosingPrice
           ,@BalancePrice
           ,@BalanceRange
           ,@TurnoverRate
           ,@Volume
           ,@TurnoverAmount
           ,@TotalMarketCapitalisation
           ,@FreeFloatMarketCapitalisation)", stock);
            }
        }


    }
}
