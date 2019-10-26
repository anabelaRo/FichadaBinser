using FichadaBinser.Helpers;
using FichadaBinser.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Globalization;
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
            DayShowing = day;

            LoadDateShowing();
            LoadDayTimes();
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

        public ICommand DeleteEntryTimeCommand
        {
            get
            {
                return new RelayCommand(DeleteEntryTime);
            }
        }

        public ICommand DeleteStartLunchTimeCommand
        {
            get
            {
                return new RelayCommand(DeleteStartLunchTime);
            }
        }

        public ICommand DeleteEndLunchTimeCommand
        {
            get
            {
                return new RelayCommand(DeleteEndLunchTime);
            }
        }

        public ICommand DeleteExitTimeCommand
        {
            get
            {
                return new RelayCommand(DeleteExitTime);
            }
        }

        public async void Cancel()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

            MainViewModel.GetInstance().EditarDia = null;
        }

        public async void Save()
        {
            if (!ValidateTimes())
                return;

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ConfirmSaveChanges,
                Languages.Cancel,
                null,
                Languages.Yes,
                Languages.No);

            if (source == Languages.Yes)
            {
                DayShowing.EntryTime = GetDateTime(EntryTimeSpan);
                DayShowing.StartLunchTime = GetDateTime(StartLunchTimeSpan);
                DayShowing.EndLunchTime = GetDateTime(EndLunchTimeSpan);
                DayShowing.ExitTime = GetDateTime(ExitTimeSpan);

                MainViewModel.GetInstance().SaveToDataBase(DayShowing);

                await Application.Current.MainPage.Navigation.PopAsync();

                MainViewModel.GetInstance().EditarDia = null;
            }
        }

        private void DeleteEntryTime()
        {
            EntryTimeSpan = new TimeSpan();
        }

        private void DeleteStartLunchTime()
        {
            StartLunchTimeSpan = new TimeSpan();
        }

        private void DeleteEndLunchTime()
        {
            EndLunchTimeSpan = new TimeSpan();
        }

        private void DeleteExitTime()
        {
            ExitTimeSpan = new TimeSpan();
        }

        #endregion

        #region Methods

        private void LoadDateShowing()
        {
            CultureInfo myCulture = new CultureInfo("es-ES");

            string strFecha = string.Format(
                    "{0} {1} de {2}",
                    StringHelper.FirstUpper(myCulture.DateTimeFormat.GetDayName(DayShowing.Date.DayOfWeek)),
                    DayShowing.Date.Day.ToString(),
                    StringHelper.FirstUpper(myCulture.DateTimeFormat.GetMonthName(DayShowing.Date.Month)));

            DateShowing = strFecha;
        }

        private void LoadDayTimes()
        {
            EntryTimeSpan = GetTimeSpan(DayShowing.EntryTime);
            StartLunchTimeSpan = GetTimeSpan(DayShowing.StartLunchTime);
            EndLunchTimeSpan = GetTimeSpan(DayShowing.EndLunchTime);
            ExitTimeSpan = GetTimeSpan(DayShowing.ExitTime);
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
                DayShowing.Date.Year,
                DayShowing.Date.Month,
                DayShowing.Date.Day,
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds);
        }

        #endregion

        #region Validations

        private bool ValidateTimes()
        {
            DateTime? entryTime = GetDateTime(EntryTimeSpan);
            DateTime? startLunchTime = GetDateTime(StartLunchTimeSpan);
            DateTime? endLunchTime = GetDateTime(EndLunchTimeSpan);
            DateTime? exitTime = GetDateTime(ExitTimeSpan);

            bool isCurrentDate = (DayShowing.Date.ToLocalTime() == DateTime.Today.Date.ToLocalTime());

            if (entryTime == null)
            {
                if (startLunchTime != null || endLunchTime != null || exitTime != null)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.IncorrectTime,
                        Languages.ValidationCompleteEntryTime,
                        Languages.Ok);

                    return false;
                }
            }

            if (!isCurrentDate)
            {
                if (entryTime != null && exitTime == null)
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
