using FichadaBinser.Models;
using FichadaBinser.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FichadaBinser.Interfaces;

namespace FichadaBinser.ViewModels
{
    public class SemanaViewModel : BaseViewModel, ITimerViewModel
    {
        #region Attributes

        private string totalTimeMondayString;
        private string totalTimeTuesdayString;
        private string totalTimeWednesdayString;
        private string totalTimeThursdayString;
        private string totalTimeFridayString;
        private string totalTimeSaturdayString;
        private string totalTimeSundayString;
        private string totalTime;

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

        public string TotalTimeSaturdayString
        {
            get { return totalTimeSaturdayString; }
            set { SetValue(ref totalTimeSaturdayString, value); }
        }

        public string TotalTimeSundayString
        {
            get { return totalTimeSundayString; }
            set { SetValue(ref totalTimeSundayString, value); }
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

        }

        #endregion

        #region Methods

        public void DoTimerAction()
        {
            List<Day> weekDays = MainViewModel.GetInstance().WeekDays;

            this.LoadWeekDays(weekDays);
            this.LoadTotalTime(weekDays);
        }

        private void LoadWeekDays(List<Day> weekDays)
        {
            Day monday = weekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Monday).FirstOrDefault();
            Day tuesday = weekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Tuesday).FirstOrDefault();
            Day wednesday = weekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Wednesday).FirstOrDefault();
            Day thursday = weekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Thursday).FirstOrDefault();
            Day friday = weekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Friday).FirstOrDefault();
            Day saturday = weekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Saturday).FirstOrDefault();
            Day sunday = weekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Sunday).FirstOrDefault();

            this.TotalTimeMondayString = monday != null ? monday.TotalTimeString : "-";
            this.TotalTimeTuesdayString = tuesday != null ? tuesday.TotalTimeString : "-";
            this.TotalTimeWednesdayString = wednesday != null ? wednesday.TotalTimeString : "-";
            this.TotalTimeThursdayString = thursday != null ? thursday.TotalTimeString : "-";
            this.TotalTimeFridayString = friday != null ? friday.TotalTimeString : "-";
            this.TotalTimeSaturdayString = saturday != null ? saturday.TotalTimeString : "-";
            this.TotalTimeSundayString = sunday != null ? sunday.TotalTimeString : "-";
        }

        private void LoadTotalTime(List<Day> weekDays)
        {
            int totalSeconds = weekDays.Sum(x => x.TotalTime);

            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);

            this.TotalTime = time.ToString(@"hh\:mm\:ss");
        }

        #endregion
    }
}
