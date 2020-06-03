namespace Thomerson.Gatlin.Finance.Model
{
    public class SystemSetting
    {
        /// <summary>
        /// 佣金比例
        /// 买卖都收费
        /// </summary>
        public static decimal ServiceChargePercent
        {
            get
            {
                return 2.5M / 10000;
            }
        }

        /// <summary>
        /// 过户费比例
        /// 买卖都收费
        /// </summary>
        public static decimal TransferFeePercent
        {
            get
            {
                return 2M / 10000;
            }
        }

        /// <summary>
        /// 印花税比例
        /// 卖出收费，买入不收费
        /// </summary>
        public static decimal StampDutyPercent
        {
            get
            {
                return 1 / 1000;
            }
        }
    }
}
