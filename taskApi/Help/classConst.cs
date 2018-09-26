using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace taskApi.Help
{
    public class classConst
    {
        public static int Year
        {
            get
            {
                return DateTime.Now.Year;
            }
        }
        /// <summary>
        /// Format daty yyyyMMdd
        /// </summary>
        public static string formatdaty = "yyyyMMdd";
        /// <summary>
        /// Format daty yyyy-MM-dd
        /// </summary>
        public static string format_daty = "yyyy-MM-dd";
        /// <summary>
        /// Maksymalna ilość dni(web.config->iloscmaxurlop)
        /// </summary>
        public static int iloscmaxurlop
        {
            get
            {
                string dane = ConfigurationSettings.AppSettings["iloscmaxurlop"];
                return int.Parse(dane);
            }
        }

        public static string UserName
        {
            get
            {

                return HttpContext.Current.User.Identity.Name.ToUpper();
            }
        }

        public static string[] monthName = { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
        /// <summary>
        /// Nazwa dni(wersja 2-3 literowa)
        /// </summary>
        public static string[] dayNameShort = { "Po", "Wto", "Śr", "Czw", "Pi", "So", "Nd" };
        /// <summary>
        /// Nazwa dni(wersja pełna)
        /// </summary>
        public static string[] dayNameLong = { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
        /// <summary>
        /// Ilość dni w kolejnych miesiącach
        /// </summary>
        public static int[] monthDayYear
        {
            get
            {
                int[] wynik = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

                int days = DateTime.DaysInMonth(Year, 2);
                wynik[1] = days;
                return wynik;
            }
        }
    }
}