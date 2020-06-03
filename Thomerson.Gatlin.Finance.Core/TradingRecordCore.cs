using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using Thomerson.Gatlin.Finance.Model;

namespace Thomerson.Gatlin.Finance.Core
{
    public class TradingRecordCore
    {
        public static int Insert(TradingRecord model)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Execute(@"INSERT INTO [dbo].[TradingRecord]
           ([UserId]
           ,[StockCode]
           ,[StockName]
           ,[TradingTpye]
           ,[Price]
           ,[Amount]
           ,[DataDate]
           ,[IsChecked])
     VALUES
           (@UserId
           ,@StockCode
           ,@StockName
           ,@TradingTpye
           ,@Price
           ,@Amount
           ,@DataDate
           ,@IsChecked)", model);
            }
        }

        public static TradingRecord GetLatestBuyRecord(string userId, string stockCode)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.QueryFirst<TradingRecord>("select * from [dbo].[TradingRecord] WHERE 1=1 and IsChecked = 0 and TradingTpye = 0 and  UserId = @userId and StockCode = @stockCode OERDER BY DataDate ", new { userId, stockCode });
            }
        }

        public static void SetChecked(int recordId)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                conn.Execute("UPDAGE [dbo].[TradingRecord] set IsChecked = 1 where Id = @id ", recordId);
            }
        }
    }
}
