using System;

namespace Thomerson.Gatlin.Finance.Model
{
    public class TradingSystem
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 策略说明
        /// </summary>
        public string Remark { get; set; }

    }
}
