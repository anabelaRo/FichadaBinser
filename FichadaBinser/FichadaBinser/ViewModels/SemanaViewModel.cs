using FichadaBinser.Enums;
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

        private string mondayTextColor;
        private string tuesdayTextColor;
        private string wednesdayTextColor;
        private string thursdayTextColor;
        private string fridayTextColor;
        private string saturdayTextColor;
        private string sundayTextColor;
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

        public string MondayTextColor
        {
            get { return mondayTextColor; }
            set { SetValue(ref mondayTextColor, value); }
        }

        public string TuesdayTextColor
        {
            get { return tuesdayTextColor; }
            set { SetValue(ref tuesdayTextColor, value); }
        }

        public string WednesdayTextColor
        {
            get { return wednesdayTextColor; }
            set { SetValue(ref wednesdayTextColor, value); }
        }

        public string ThursdayTextColor
        {
            get { return thursdayTextColor; }
            set { SetValue(ref thursdayTextColor, value); }
        }

        public string FridayTextColor
        {
            get { return fridayTextColor; }
            set { SetValue(ref fridayTextColor, value); }
        }

        public string SaturdayTextColor
        {
            get { return saturdayTextColor; }
            set { SetValue(ref saturdayTextColor, value); }
        }

        public string SundayTextColor
        {
            get { return sundayTextColor; }
            set { SetValue(ref sundayTextColor, value); }
        }

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
            this.MondayTextColor = Convert.ToChar(EnumTextColor.Gray).ToString();
            this.TuesdayTextColor = Convert.ToChar(EnumTextColor.Gray).ToString();
            this.WednesdayTextColor = Convert.ToChar(EnumTextColor.Gray).ToString();
            this.ThursdayTextColor = Convert.ToChar(EnumTextColor.Gray).ToString();
            this.FridayTextColor = Convert.ToChar(EnumTextColor.Gray).ToString();
            this.SaturdayTextColor = Convert.ToChar(EnumTextColor.Gray).ToString();
            this.sundayTextColor = Convert.ToChar(EnumTextColor.Gray).ToString();
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

        public void DoTimerAction(bool refreshView = false)
        {
            List<Day> weekDays = MainViewModel.GetInstance().WeekDays;

            this.LoadWeekDays(weekDays);
            this.LoadTotalTime(weekDays);
            this.SetTextColor(weekDays);
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

        private void SetTextColor(List<Day> weekDays)
        {
            this.MondayTextColor = this.SetTextColor(weekDays, DayOfWeek.Monday);
            this.TuesdayTextColor = this.SetTextColor(weekDays, DayOfWeek.Tuesday);
            this.WednesdayTextColor = this.SetTextColor(weekDays, DayOfWeek.Wednesday);
            this.ThursdayTextColor = this.SetTextColor(weekDays, DayOfWeek.Thursday);
            this.FridayTextColor = this.SetTextColor(weekDays, DayOfWeek.Friday);
            this.SaturdayTextColor = this.SetTextColor(weekDays, DayOfWeek.Saturday);
            this.SundayTextColor = this.SetTextColor(weekDays, DayOfWeek.Sunday);
        }

        private string SetTextColor(List<Day> weekDays, DayOfWeek dayOfWeek)
        {
            Day day = weekDays.Where(x => x.Date.DayOfWeek == dayOfWeek).FirstOrDefault();

            if (day != null)
            {
                if (day.EntryTime != null && day.ExitTime == null && day.Date.ToLocalTime() != DateTime.Today.ToLocalTime())
                    return Convert.ToChar(EnumTextColor.Red).ToString();

                if (day.StartLunchTime != null && day.EndLunchTime == null && day.Date.ToLocalTime() != DateTime.Today.ToLocalTime())
                    return Convert.ToChar(EnumTextColor.Red).ToString();
            }

            return Convert.ToChar(EnumTextColor.Gray).ToString();
        }

        #endregion
    }
}
