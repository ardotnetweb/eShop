using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebApplication.BaseClassWebApplication
{
    public static class ConvertDateChart
    {
        public static string ConvertToPerssionDay(DateTime date)
        {
            string Day, Month, Year, NameDay, Date = "";
            PersianCalendar perssionCalender = new PersianCalendar();
            if (date != null)
            {
                NameDay = string.Empty;
                Year = perssionCalender.GetYear(date).ToString();
                Month = perssionCalender.GetMonth(date).ToString();
                Day = perssionCalender.GetDayOfMonth(date).ToString();

                switch (date.ToString("dddd"))
                {
                    case "Sunday":
                        NameDay = "یکشنبه";
                        break;
                    case "Monday":
                        NameDay = "دوشنبه";
                        break;
                    case "Tuesday":
                        NameDay = "سه شنبه";
                        break;
                    case "Wednesday":
                        NameDay = "چهار شنبه";
                        break;
                    case "Thursday":
                        NameDay = "پنج شنبه";
                        break;
                    case "Friday":
                        NameDay = "جمعه";
                        break;
                    case "Saturday":
                        NameDay = "شنبه";
                        break;
                }

                switch (Month)
                {
                    case "1": Month = "فروردین";
                        break;
                    case "2": Month = "اردیبهشت";
                        break;
                    case "3": Month = "خرداد";
                        break;
                    case "4": Month = "تیر";
                        break;
                    case "5": Month = "مرداد";
                        break;
                    case "6": Month = "شهریور";
                        break;
                    case "7": Month = "مهر";
                        break;
                    case "8": Month = "آبان";
                        break;
                    case "9": Month = "آذر";
                        break;
                    case "10": Month = "دی";
                        break;
                    case "11": Month = "بهمن";
                        break;
                    case "12": Month = "اسفند";
                        break;
                    default:
                        break;
                }
                //Date = Month + " / " + Day;
                // Date = Day + " / " + Month + " / " + Year;
                // Date = Month;
                 Date = string.Format("{0}/{1}-{2}/{3}", NameDay, Day, Month, Year);
                //Date = string.Format("{0}-{1}", Day, Month);
            }
            else
            {

            }
            return Date;
        }
    }
}