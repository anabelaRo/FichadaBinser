namespace FichadaBinser.Services
{
    using FichadaBinser.Models;
    using Helpers;
    using System;

    public class DayDataService
    {
        public Day GetCurrentDay()
        {
            using (var da = new DataAccess())
            {
                int dayId = DayHelper.GetDayIdByDate(DateTime.Today);

                Day current = da.Find<Day>(dayId);

                if (current == null)
                {
                    current = new Day(DateTime.Today);

                    da.Insert<Day>(current);
                }

                return current;
            }
        }

        public void Update(Day day)
        {
            using (var da = new DataAccess())
            {
                da.Update<Day>(day);
            }
        }
    }
}
