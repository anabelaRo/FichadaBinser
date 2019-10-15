using System;

namespace FichadaBinser.Helpers
{
    public static class DayHelper
    {
        public static int GetDayIdByDate(DateTime date)
        {
            try
            {
                string strId = string.Format("{0:0000}{1:00}{2:00}", date.Year, date.Month, date.Day);

                return Convert.ToInt32(strId);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static DateTime GetDateByDayId(int dayId)
        {
            try
            {
                int year = Convert.ToInt32(dayId.ToString().Substring(0, 4));
                int month = Convert.ToInt32(dayId.ToString().Substring(4, 2));
                int day = Convert.ToInt32(dayId.ToString().Substring(6, 2));

                return new DateTime(year, month, day);
            }
            catch (Exception ex)
            {
                return new DateTime(1900, 1, 1);
            }
        }
    }
}
