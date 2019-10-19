using FichadaBinser.Helpers;
using FichadaBinser.Interfaces;
using FichadaBinser.Models;
using FichadaBinser.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

        #region Commands

        public ICommand TotalTimeMondayCommand
        {
            get
            {
                return new RelayCommand(TotalTimeMonday);
            }
        }

        public ICommand TotalTimeTuesdayCommand
        {
            get
            {
                return new RelayCommand(TotalTimeTuesday);
            }
        }

        public ICommand TotalTimeWednesdayCommand
        {
            get
            {
                return new RelayCommand(TotalTimeWednesday);
            }
        }

        public ICommand TotalTimeThursdayCommand
        {
            get
            {
                return new RelayCommand(TotalTimeThursday);
            }
        }

        public ICommand TotalTimeFridayCommand
        {
            get
            {
                return new RelayCommand(TotalTimeFriday);
            }
        }

        public ICommand TotalTimeSaturdayCommand
        {
            get
            {
                return new RelayCommand(TotalTimeSaturday);
            }
        }

        public ICommand TotalTimeSundayCommand
        {
            get
            {
                return new RelayCommand(TotalTimeSunday);
            }
        }

        private async void TotalTimeMonday()
        {
            Day day = MainViewModel.GetInstance().WeekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Monday).FirstOrDefault();

            await this.EditDayTimes(day);
        }

        private async void TotalTimeTuesday()
        {
            Day day = MainViewModel.GetInstance().WeekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Tuesday).FirstOrDefault();

            await this.EditDayTimes(day);
        }

        private async void TotalTimeWednesday()
        {
            Day day = MainViewModel.GetInstance().WeekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Wednesday).FirstOrDefault();

            await this.EditDayTimes(day);
        }

        private async void TotalTimeThursday()
        {
            Day day = MainViewModel.GetInstance().WeekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Thursday).FirstOrDefault();

            await this.EditDayTimes(day);
        }

        private async void TotalTimeFriday()
        {
            Day day = MainViewModel.GetInstance().WeekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Friday).FirstOrDefault();

            await this.EditDayTimes(day);
        }

        private async void TotalTimeSaturday()
        {
            Day day = MainViewModel.GetInstance().WeekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Saturday).FirstOrDefault();

            await this.EditDayTimes(day);
        }

        private async void TotalTimeSunday()
        {
            Day day = MainViewModel.GetInstance().WeekDays.Where(x => x.Date.DayOfWeek == DayOfWeek.Sunday).FirstOrDefault();

            await this.EditDayTimes(day);
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
            this.TotalTimeMondayString = this.GetDayTotalTimeForLabel(weekDays, DayOfWeek.Monday);
            this.TotalTimeTuesdayString = this.GetDayTotalTimeForLabel(weekDays, DayOfWeek.Tuesday);
            this.TotalTimeWednesdayString = this.GetDayTotalTimeForLabel(weekDays, DayOfWeek.Wednesday);
            this.TotalTimeThursdayString = this.GetDayTotalTimeForLabel(weekDays, DayOfWeek.Thursday);
            this.TotalTimeFridayString = this.GetDayTotalTimeForLabel(weekDays, DayOfWeek.Friday);
            this.TotalTimeSaturdayString = this.GetDayTotalTimeForLabel(weekDays, DayOfWeek.Saturday);
            this.TotalTimeSundayString = this.GetDayTotalTimeForLabel(weekDays, DayOfWeek.Sunday);
        }

        private void LoadTotalTime(List<Day> weekDays)
        {
            int totalSeconds = weekDays.Sum(x => x.TotalTime);

            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);

            this.TotalTime = string.Format(
                    "{0:00}:{1:00}:{2:00}",
                    (int)time.TotalHours,
                    time.Minutes,
                    time.Seconds
                );
        }

        private string GetDayTotalTimeForLabel(List<Day> weekDays, DayOfWeek dayOfWeek)
        {
            Day day = weekDays.Where(x => x.Date.DayOfWeek == dayOfWeek).FirstOrDefault();

            if (day != null && day.TotalTime != 0)
                return day.TotalTimeString;
            else
                return "-";
        }

        private async Task EditDayTimes(Day day)
        {
            MainViewModel.GetInstance().EditarDia = new EditarDiaViewModel(day);

            await Application.Current.MainPage.Navigation.PushAsync(new EditarDiaPage());
        }

        #endregion
    }
}
