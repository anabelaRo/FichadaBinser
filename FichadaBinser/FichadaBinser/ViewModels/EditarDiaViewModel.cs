using FichadaBinser.Helpers;
using FichadaBinser.Interfaces;
using FichadaBinser.Models;
using FichadaBinser.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FichadaBinser.ViewModels
{
    public class EditarDiaViewModel : BaseViewModel
    {
        #region Attributes

        private string dateShowing;

        private TimeSpan entryTimeSpan;
        private TimeSpan startLunchTimeSpan;
        private TimeSpan endLunchTimeSpan;
        private TimeSpan exitTimeSpan;

        private Day DayShowing;

        #endregion

        #region Properties

        public string DateShowing
        {
            get { return dateShowing; }
            set { SetValue(ref dateShowing, value); }
        }

        public TimeSpan EntryTimeSpan
        {
            get { return entryTimeSpan; }
            set { SetValue(ref entryTimeSpan, value); }
        }

        public TimeSpan StartLunchTimeSpan
        {
            get { return startLunchTimeSpan; }
            set { SetValue(ref startLunchTimeSpan, value); }
        }

        public TimeSpan EndLunchTimeSpan
        {
            get { return endLunchTimeSpan; }
            set { SetValue(ref endLunchTimeSpan, value); }
        }

        public TimeSpan ExitTimeSpan
        {
            get { return exitTimeSpan; }
            set { SetValue(ref exitTimeSpan, value); }
        }

        #endregion

        #region Constructor

        public EditarDiaViewModel(Day day)
        {
            this.DayShowing = day;

            this.LoadDateShowing();
            this.LoadDayTimes();
        }

        #endregion

        #region Commands

        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(Cancel);
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        public async void Cancel()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

            MainViewModel.GetInstance().EditarDia = null;
        }

        public async void Save()
        {
            if (!this.ValidateTimes())
                return;

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ConfirmSaveChanges,
                Languages.Cancel,
                null,
                Languages.Yes,
                Languages.No);

            if (source == Languages.Yes)
            {
                this.DayShowing.EntryTime = this.GetDateTime(this.EntryTimeSpan);
                this.DayShowing.StartLunchTime = this.GetDateTime(this.StartLunchTimeSpan);
                this.DayShowing.EndLunchTime = this.GetDateTime(this.EndLunchTimeSpan);
                this.DayShowing.ExitTime = this.GetDateTime(this.ExitTimeSpan);

                MainViewModel.GetInstance().SaveToDataBase(this.DayShowing);

                await Application.Current.MainPage.Navigation.PopAsync();

                MainViewModel.GetInstance().EditarDia = null;
            }
        }

        #endregion

        #region Methods

        private void LoadDateShowing()
        {
            CultureInfo myCulture = new CultureInfo("es-ES");

            string strFecha = string.Format(
                    "{0} {1} de {2}",
                    StringHelper.FirstUpper(myCulture.DateTimeFormat.GetDayName(this.DayShowing.Date.DayOfWeek)),
                    this.DayShowing.Date.Day.ToString(),
                    StringHelper.FirstUpper(myCulture.DateTimeFormat.GetMonthName(this.DayShowing.Date.Month)));

            this.DateShowing = strFecha;
        }

        private void LoadDayTimes()
        {
            this.EntryTimeSpan = this.GetTimeSpan(this.DayShowing.EntryTime);
            this.StartLunchTimeSpan = this.GetTimeSpan(this.DayShowing.StartLunchTime);
            this.EndLunchTimeSpan = this.GetTimeSpan(this.DayShowing.EndLunchTime);
            this.ExitTimeSpan = this.GetTimeSpan(this.DayShowing.ExitTime);
        }

        private TimeSpan GetTimeSpan(DateTime? time)
        {
            if (time != null)
            {
                return new TimeSpan(time.Value.ToLocalTime().Hour,
                    time.Value.ToLocalTime().Minute,
                    time.Value.ToLocalTime().Second);
            }

            return new TimeSpan();
        }

        private DateTime? GetDateTime(TimeSpan timeSpan)
        {
            if (timeSpan.Hours == 0
                && timeSpan.Minutes == 0
                && timeSpan.Seconds == 0)
            {
                return null;
            }

            return new DateTime(
                this.DayShowing.Date.Year,
                this.DayShowing.Date.Month,
                this.DayShowing.Date.Day,
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds);
        }

        #endregion

        #region Validations


        private bool ValidateTimes()
        {
            DateTime? entryTime = this.GetDateTime(this.EntryTimeSpan);
            DateTime? startLunchTime = this.GetDateTime(this.StartLunchTimeSpan);
            DateTime? endLunchTime = this.GetDateTime(this.EndLunchTimeSpan);
            DateTime? exitTime = this.GetDateTime(this.ExitTimeSpan);

            bool isCurrentDate = (this.DayShowing.Date.ToLocalTime() == DateTime.Today.Date.ToLocalTime());

            if (entryTime == null)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.IncorrectTime,
                    Languages.ValidationCompleteEntryTime,
                    Languages.Ok);

                return false;
            }

            if (!isCurrentDate)
            {
                if (exitTime == null)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.IncorrectTime,
                        Languages.ValidationCompleteExitTime,
                        Languages.Ok);

                    return false;
                }

                if (startLunchTime != null && endLunchTime == null)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.IncorrectTime,
                        Languages.ValidationCompleteEndLunchTime,
                        Languages.Ok);

                    return false;
                }
            }

            if (entryTime != null && exitTime != null)
            {
                if (entryTime > exitTime)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.IncorrectTime,
                        Languages.ValidationEntryTimeGreaterThanExitTime,
                        Languages.Ok);

                    return false;
                }
            }

            if (entryTime != null && startLunchTime != null)
            {
                if (entryTime > startLunchTime)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.IncorrectTime,
                        Languages.ValidationEntryTimeGreaterThanStartLunchTime,
                        Languages.Ok);

                    return false;
                }
            }

            if (startLunchTime != null && endLunchTime != null)
            {
                if (startLunchTime > endLunchTime)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.IncorrectTime,
                        Languages.ValidationStartLunchTimeGreaterThanEndLunchTime,
                        Languages.Ok);

                    return false;
                }
            }

            if (endLunchTime != null && exitTime != null)
            {
                if (endLunchTime > exitTime)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.IncorrectTime,
                        Languages.ValidationStartLunchTimeGreaterThanExitTime,
                        Languages.Ok);

                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
