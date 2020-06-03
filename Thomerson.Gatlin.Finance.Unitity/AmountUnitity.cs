using System;

namespace Thomerson.Gatlin.Finance.Unitity
{
    public class AmountUnitity
    {
        /// <summary>
        /// 买入手数
        /// </summary>
        /// <returns></returns>
        public static int BuyAmount(decimal price)
        {
            if (price >= 120)
            {
                return 0;
            }

            if (price >= 70)
            {
                return 1;
            }
            if (price >= 40)
            {
                return 2;
            }
            if (price >= 30)
            {
                return (int)Math.Floor(100 / price);
            }
            return 0;
        }
    }
}
