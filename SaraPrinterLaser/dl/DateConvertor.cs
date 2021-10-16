using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SaraPrinterLaser.dl
{
    class DateConvertor
    {

        public static string DateConverter(bool CommandType)
        {

            DateTime dateTimePicker = DateTime.Now;
            PersianCalendar PerCal = new PersianCalendar();
            string Year, Day, Month, Month2 = "", Hour, Minute, Secound, strDate, strDate2 = "";
            Year = PerCal.GetYear(dateTimePicker).ToString();
            Month = PerCal.GetMonth(dateTimePicker).ToString();
            Day = PerCal.GetDayOfMonth(dateTimePicker).ToString();
            Hour = PerCal.GetHour(dateTimePicker).ToString();
            Minute = PerCal.GetMinute(dateTimePicker).ToString();
            Secound = PerCal.GetSecond(dateTimePicker).ToString();
            if (Day.Length == 1)
            {
                Day = PerCal.GetDayOfMonth(dateTimePicker).ToString().Insert(0, "0");
            }
            if (Month.Length == 1)
            {
                Month = PerCal.GetMonth(dateTimePicker).ToString().Insert(0, "0");
            }
            if (!CommandType)
                strDate = Year + '/' + Month + '/' + Day + " " + Hour + ':' + Minute + ':' + Secound;
            else
            {
                switch (PerCal.GetMonth(dateTimePicker))
                {
                    case 1:
                        Month2 = "فروردین";
                        break;
                    case 2:
                        Month2 = "اردیبهشت";
                        break;
                    case 3:
                        Month2 = "خرداد";
                        break;
                    case 4:
                        Month2 = "تیر";
                        break;
                    case 5:
                        Month2 = "مرداد";
                        break;
                    case 6:
                        Month2 = "شهریور";
                        break;
                    case 7:
                        Month2 = "مهر";
                        break;
                    case 8:
                        Month2 = "آبان";
                        break;
                    case 9:
                        Month2 = "آذر";
                        break;
                    case 10:
                        Month2 = "دی";
                        break;
                    case 11:
                        Month2 = "بهمن";
                        break;
                    case 12:
                        Month2 = "اسفند";
                        break;
                    default:
                        break;
                }
                strDate = Hour + ':' + Minute + ':' + Secound +" " + Year + '/' + Month2 + '/' + Day ;
            }
            return strDate;
        }
    }
}
