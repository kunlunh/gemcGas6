using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gemcGas.Common
{
    public class CommonFunctions
    {
        public static IEnumerable<string> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Year * 12 + toDate.Month - fromDate.Year * 12 + fromDate.Month - 1)
                .Select(d => fromDate.AddMonths(d).ToString("yyyyMM"));
        }
    }
}
