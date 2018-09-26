using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taskApi.Enums;

namespace taskApi.Help
{
    public static class classExtension
    {
        public static int DayNumberWeek(this DateTime dt)
        {
            switch (dt.DayOfWeek)
            {

                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    return 7;
            }
        }
        /// <summary>
        /// Czy dzien jest sobota lub niedziela( DayType.UnWork/DayType.Workv )
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DayType IsWork(this DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
            {
                return DayType.UnWork;
            }
            else
                return DayType.Work;
        }
    }
}