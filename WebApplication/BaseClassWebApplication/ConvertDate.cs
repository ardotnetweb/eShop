using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace WebApplication.BaseClassWebApplication
{
    public static class ConvertDate
    {
        public static string PerssionDate(DateTime dateInput)
        {
            PersianCalendar pdate = new PersianCalendar();
            return String.Format("{0}/{1}/{2}", pdate.GetYear(dateInput), pdate.GetMonth(dateInput),
                                          pdate.GetDayOfMonth(dateInput));
        }
    }
}