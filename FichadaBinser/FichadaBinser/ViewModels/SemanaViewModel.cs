using FichadaBinser.Models;
using FichadaBinser.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FichadaBinser.ViewModels
{
    public class SemanaViewModel : BaseViewModel
    {
        #region Services

        DayDataService dayDataService;

        #endregion

        #region Attributes

        private string totalTimeMondayString;
        private string totalTimeTuesdayString;
        private string totalTimeWednesdayString;
        private string totalTimeThursdayString;
        private string totalTimeFridayString;
        private string totalTime;

        private Day dayMonday;
        private Day dayTuesday;
        private Day dayWednesday;
        private Day dayThursday;
        private Day dayFriday;

        #endregion

        #region Properties

        public string TotalTimeMondayString
        {
            get { return totalTimeMondayString; }
            set { SetValue(ref totalTimeMondayString, value); }
        }

        public string TotalTimeTuesdayString
        {
            get { return totalTimeTuesdayString; }
            set { SetValue(ref totalTimeTuesdayString, value); }
        }

        public string TotalTimeWednesdayString
        {
            get { return totalTimeWednesdayString; }
            set { SetValue(ref totalTimeWednesdayString, value); }
        }

        public string TotalTimeThursdayString
        {
            get { return totalTimeThursdayString; }
            set { SetValue(ref totalTimeThursdayString, value); }
        }

        public string TotalTimeFridayString
        {
            get { return totalTimeFridayString; }
            set { SetValue(ref totalTimeFridayString, value); }
        }

        public string TotalTime
        {
            get { return totalTime; }
            set { SetValue(ref totalTime, value); }
        }


        #endregion

        #region Constructor

        public SemanaViewModel()
        {
            this.dayDataService = new DayDataService();

            this.LoadWeekDays();
        }

        #endregion

        #region Methods

        private void LoadWeekDays()
        {
            List<Day> days = this.dayDataService.GetCurrentWeekDays();

            Day monday = days.Where(x => x.Date.DayOfWeek == DayOfWeek.Monday).FirstOrDefault();
            Day tuesday = days.Where(x => x.Date.DayOfWeek == DayOfWeek.Tuesday).FirstOrDefault();
            Day wednesday = days.Where(x => x.Date.DayOfWeek == DayOfWeek.Wednesday).FirstOrDefault();
            Day thursday = days.Where(x => x.Date.DayOfWeek == DayOfWeek.Thursday).FirstOrDefault();
            Day friday = days.Where(x => x.Date.DayOfWeek == DayOfWeek.Friday).FirstOrDefault();

            this.TotalTimeMondayString = monday != null ? monday.TotalTimeString : string.Empty;
            this.TotalTimeTuesdayString = tuesday != null ? tuesday.TotalTimeString : string.Empty;
            this.TotalTimeWednesdayString = wednesday != null ? wednesday.TotalTimeString : string.Empty;
            this.TotalTimeThursdayString = thursday != null ? thursday.TotalTimeString : string.Empty;
            this.TotalTimeFridayString = friday != null ? friday.TotalTimeString : string.Empty;

            int totalSeconds = days.Sum(x => x.TotalTime);

            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);

            this.TotalTime = time.ToString(@"hh\:mm\:ss");
        }

        #endregion
    }
}
