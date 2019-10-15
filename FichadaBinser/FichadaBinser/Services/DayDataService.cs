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
                DateTime today = DateTime.Today;

                int dayId = DayHelper.GetDayIdByDate(today);

                Day current = da.Find<Day>(dayId);

                if (current == null)
                {
                    current = new Day(today);

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
