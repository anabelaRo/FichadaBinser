namespace FichadaBinser.Services
{
    using FichadaBinser.Models;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DayDataService
    {
        public Day GetCurrentDay()
        {
            using (var da = new DataAccess())
            {
                DateTime today = DateTime.Today.ToUniversalTime();

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

        public List<Day> GetCurrentWeekDays()
        {
            var startDate = DateTime.Today.ToUniversalTime().AddDays(-(((DateTime.Today.ToUniversalTime().DayOfWeek - DayOfWeek.Monday) + 7) % 7));
            var endDate = startDate.AddDays(5);

            var numDays = (int)((endDate - startDate).TotalDays);

            List<DateTime> weekDates = Enumerable
                                       .Range(0, numDays)
                                       .Select(x => startDate.AddDays(x))
                                       .ToList();

            var weekDays = new List<Day>();

            using (var da = new DataAccess())
            {
                foreach (DateTime date in weekDates)
                {
                    int dayId = DayHelper.GetDayIdByDate(date);

                    Day day = da.Find<Day>(dayId);

                    if (day != null)
                        weekDays.Add(day);
                }
            }

            return weekDays;
        }
    }
}
