using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagement.Data.Helpers
{
    public static class DateHelper
    {

        public static DateTime yesterday
        {
            get
            {
                DateTime baseDate = DateTime.Today;
                var DayBefore = baseDate.AddDays(-1);
                return DayBefore;
            }
        }
        public static DateTime thisWeekStart
        {
            get
            {
                DateTime baseDate = DateTime.Today;
                var thisWeekStartDate = baseDate.AddDays(-(int)baseDate.DayOfWeek);
                return thisWeekStartDate;
            }
        }
        public static DateTime thisWeekEnd
        {
            get
            {
                var thisWeekEndDate = thisWeekStart.AddDays(7).AddSeconds(-1);
                return thisWeekEndDate;
            }
        }
        public static DateTime lastWeekStart
        {
            get
            {
                var lastWeekStartDate = thisWeekStart.AddDays(-7);
                return lastWeekStartDate;
            }
        }
        public static DateTime lastWeekEnd
        {
            get
            {
                var lastWeekEndDate = thisWeekStart.AddSeconds(-1);
                return lastWeekEndDate;
            }
        }
        public static DateTime thisMonthStart
        {
            get
            {
                DateTime baseDate = DateTime.Today;
                var thisMonthStartDate = baseDate.AddDays(1 - baseDate.Day);
                return thisMonthStartDate;
            }
        }
        public static DateTime thisMonthEnd
        {
            get
            {
                var thisMonthEndDate = thisMonthStart.AddMonths(1).AddSeconds(-1);
                return thisMonthEndDate;
            }
        }
        public static DateTime lastMonthStart
        {
            get
            {
                var lastMonthStartDate = thisMonthStart.AddMonths(-1);
                return lastMonthStartDate;
            }
        }
        public static DateTime lastMonthEnd
        {
            get
            {
                var lastMonthEndDate = thisMonthStart.AddSeconds(-1).Date;
                return lastMonthEndDate;
            }
        }





        public static DateTime yesterdayOfDate(DateTime dt)
        {
            var DayBefore = dt.AddDays(-1);
            return DayBefore;
        }
        public static DateTime thisWeekStartOfDate(DateTime dt)
        {
            var thisWeekStartDate = dt.AddDays(-(int)dt.DayOfWeek);
            return thisWeekStartDate;
        }
        public static DateTime thisWeekEndOfDate(DateTime dt)
        {
            var thisWeekEndDate = thisWeekStartOfDate(dt).AddDays(7).AddSeconds(-1);
            return thisWeekEndDate;
        }
        public static DateTime lastWeekStartOfDate(DateTime dt)
        {
            var lastWeekStartDate = thisWeekStartOfDate(dt).AddDays(-7);
            return lastWeekStartDate;

        }
        public static DateTime lastWeekEndOfDate(DateTime dt)
        {
            var lastWeekEndDate = thisWeekStartOfDate(dt).AddSeconds(-1);
            return lastWeekEndDate;
        }
        public static DateTime thisMonthStartOfDate(DateTime dt)
        {
            var thisMonthStartDate = dt.AddDays(1 - dt.Day);
            return thisMonthStartDate;
        }
        public static DateTime thisMonthEndOfDate(DateTime dt)
        {
            var thisMonthEndDate = thisMonthStartOfDate(dt).AddMonths(1).AddSeconds(-1);
            return thisMonthEndDate;
        }
        public static DateTime lastMonthStartOfDate(DateTime dt)
        {
            var lastMonthStartDate = thisMonthStartOfDate(dt).AddMonths(-1);
            return lastMonthStartDate;
        }
        public static DateTime lastMonthEndOfDate(DateTime dt)
        {
            var lastMonthEndDate = thisMonthStartOfDate(dt).AddSeconds(-1).Date;
            return lastMonthEndDate;
        }
    }
}
