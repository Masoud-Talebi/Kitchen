using System.Globalization;

namespace Kitchen.api.Application.Tools
{
    public static class Extention
    {
        public static string ToPersian(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            try
            {
                return persianCalendar.GetHour(date) + " : " + persianCalendar.GetMinute(date)+"   "+"   "+ persianCalendar.GetYear(date) + "/" + persianCalendar.GetMonth(date) + "/" +
                       persianCalendar.GetDayOfMonth(date);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
