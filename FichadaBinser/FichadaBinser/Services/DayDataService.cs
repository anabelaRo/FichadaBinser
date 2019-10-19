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

                    this.Insert(current);
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

        public void Insert(Day day)
        {
            if (day.DayId == null)
                day.DayId = DayHelper.GetDayIdByDate(day.Date);

            using (var da = new DataAccess())
            {
                da.Insert<Day>(day);
            }
        }

        public List<Day> GetCurrentWeekDays()
        {
            var startDate = DateTime.Today.ToUniversalTime().AddDays(-(((DateTime.Today.ToUniversalTime().DayOfWeek - DayOfWeek.Monday) + 7) % 7));
            var endDate = startDate.AddDays(7);

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
                    else
                        weekDays.Add(new Day(date));
                }
            }

            return weekDays;
        }
    }
}
