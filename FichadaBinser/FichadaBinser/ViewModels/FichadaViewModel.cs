using FichadaBinser.Helpers;
using FichadaBinser.Interfaces;
using FichadaBinser.Models;
using FichadaBinser.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FichadaBinser.ViewModels
{
    public class FichadaViewModel : BaseViewModel, ITimerViewModel
    {
        #region Services

        DayDataService dayDataService;

        #endregion

        #region Attributes

        private string currentDate;
        private string currentTime;
        private string entryTime;
        private string startLunchTime;
        private string endLunchTime;
        private string exitTime;
        private string totalTime;

        private bool isEnabledRegisterEntry;
        private bool isEnabledRegisterStartLunch;
        private bool isEnabledRegisterEndLunch;
        private bool isEnabledRegisterExit;

        private bool isVisibleCancelEntry;
        private bool isVisibleCancelStartLunch;
        private bool isVisibleCancelEndLunch;
        private bool isVisibleCancelExit;

        private Day CurrentDay
        {
            get
            {
                return MainViewModel.GetInstance().CurrentDay;
            }
        }

        #endregion

        #region Properties

        public string CurrentDate
        {
            get { return currentDate; }
            set { SetValue(ref currentDate, value); }
        }

        public string CurrentTime
        {
            get { return currentTime; }
            set { SetValue(ref currentTime, value); }
        }

        public string EntryTime
        {
            get { return entryTime; }
            set { SetValue(ref entryTime, value); }
        }

        public string StartLunchTime
        {
            get { return startLunchTime; }
            set { SetValue(ref startLunchTime, value); }
        }

        public string EndLunchTime
        {
            get { return endLunchTime; }
            set { SetValue(ref endLunchTime, value); }
        }

        public string ExitTime
        {
            get { return exitTime; }
            set { SetValue(ref exitTime, value); }
        }

        public string TotalTime
        {
            get { return totalTime; }
            set { SetValue(ref totalTime, value); }
        }

        public bool IsEnabledRegisterEntry
        {
            get { return isEnabledRegisterEntry; }
            set { SetValue(ref isEnabledRegisterEntry, value); }
        }

        public bool IsEnabledRegisterStartLunch
        {
            get { return isEnabledRegisterStartLunch; }
            set { SetValue(ref isEnabledRegisterStartLunch, value); }
        }

        public bool IsEnabledRegisterEndLunch
        {
            get { return isEnabledRegisterEndLunch; }
            set { SetValue(ref isEnabledRegisterEndLunch, value); }
        }

        public bool IsEnabledRegisterExit
        {
            get { return isEnabledRegisterExit; }
            set { SetValue(ref isEnabledRegisterExit, value); }
        }

        public bool IsVisibleCancelEntry
        {
            get { return isVisibleCancelEntry; }
            set { SetValue(ref isVisibleCancelEntry, value); }
        }

        public bool IsVisibleCancelStartLunch
        {
            get { return isVisibleCancelStartLunch; }
            set { SetValue(ref isVisibleCancelStartLunch, value); }
        }

        public bool IsVisibleCancelEndLunch
        {
            get { return isVisibleCancelEndLunch; }
            set { SetValue(ref isVisibleCancelEndLunch, value); }
        }

        public bool IsVisibleCancelExit
        {
            get { return isVisibleCancelExit; }
            set { SetValue(ref isVisibleCancelExit, value); }
        }

        #endregion

        #region Constructor

        public FichadaViewModel()
        {
            this.dayDataService = new DayDataService();

            //this.currentDay = dayDataService.GetCurrentDay();

            this.LoadCurrentDate();
            this.LoadCurrentTime();
            this.SetButtonsEnabled();
            this.RefreshView();
        }

        #endregion

        #region Methods

        public void DoTimerAction()
        {
            this.LoadCurrentTime();
            this.LoadTotalTime();
        }

        private void LoadCurrentDate()
        {
            CultureInfo myCulture = new CultureInfo("es-ES");

            string strFecha = string.Format(
                    "{0} {1} de {2}",
                    StringHelper.FirstUpper(myCulture.DateTimeFormat.GetDayName(this.CurrentDay.Date.DayOfWeek)),
                    this.CurrentDay.Date.Day.ToString(),
                    StringHelper.FirstUpper(myCulture.DateTimeFormat.GetMonthName(this.CurrentDay.Date.Month)));

            this.CurrentDate = strFecha;
        }

        private void LoadTotalTime()
        {
            this.TotalTime = this.CurrentDay.TotalTimeString;
        }

        private void LoadCurrentTime()
        {
            this.CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        private void SetButtonsEnabled()
        {
            this.IsEnabledRegisterEntry = this.CurrentDay.EntryTime == null;
            this.IsEnabledRegisterStartLunch = this.CurrentDay.StartLunchTime == null && this.CurrentDay.EntryTime != null && this.CurrentDay.ExitTime == null;
            this.IsEnabledRegisterEndLunch = this.CurrentDay.EndLunchTime == null && this.CurrentDay.StartLunchTime != null && this.CurrentDay.ExitTime == null;
            this.IsEnabledRegisterExit = this.CurrentDay.ExitTime == null && this.CurrentDay.EntryTime != null && ((this.CurrentDay.StartLunchTime == null && this.CurrentDay.EndLunchTime == null) || (this.CurrentDay.StartLunchTime != null && this.CurrentDay.EndLunchTime != null));

            this.IsVisibleCancelEntry = this.CanCancelEntry();
            this.IsVisibleCancelStartLunch = this.CanCancelStartLunch();
            this.IsVisibleCancelEndLunch = this.CanCancelEndLunch();
            this.IsVisibleCancelExit = this.CanCancelExit();
        }

        private bool CanCancelEntry()
        {
            if (this.CurrentDay.EntryTime != null
                && this.CurrentDay.StartLunchTime == null
                && this.CurrentDay.EndLunchTime == null
                && this.CurrentDay.ExitTime == null)
            {
                if (this.CurrentDay.Date.ToLocalTime() == DateTime.Today.ToLocalTime())
                    return true;
            }

            return false;
        }

        private bool CanCancelStartLunch()
        {
            if (this.CurrentDay.EntryTime != null
                && this.CurrentDay.StartLunchTime != null
                && this.CurrentDay.EndLunchTime == null
                && this.CurrentDay.ExitTime == null)
            {
                if (this.CurrentDay.Date.ToLocalTime() == DateTime.Today.ToLocalTime())
                    return true;
            }

            return false;
        }

        private bool CanCancelEndLunch()
        {
            if (this.CurrentDay.EntryTime != null
                && this.CurrentDay.StartLunchTime != null
                && this.CurrentDay.EndLunchTime != null
                && this.CurrentDay.ExitTime == null)
            {
                if (this.CurrentDay.Date.ToLocalTime() == DateTime.Today.ToLocalTime())
                    return true;
            }

            return false;
        }

        private bool CanCancelExit()
        {
            if (this.CurrentDay.EntryTime != null
                && this.CurrentDay.ExitTime != null)
            {
                if (this.CurrentDay.Date.ToLocalTime() == DateTime.Today.ToLocalTime())
                    return true;
            }

            return false;
        }

        #endregion

        #region Commands

        public ICommand RegisterEntryCommand
        {
            get
            {
                return new RelayCommand(RegisterEntry);
            }
        }

        public ICommand RegisterStartLunchCommand
        {
            get
            {
                return new RelayCommand(RegisterStartLunch);
            }
        }

        public ICommand RegisterEndLunchCommand
        {
            get
            {
                return new RelayCommand(RegisterEndLunch);
            }
        }

        public ICommand RegisterExitCommand
        {
            get
            {
                return new RelayCommand(RegisterExit);
            }
        }

        public ICommand CancelEntryCommand
        {
            get
            {
                return new RelayCommand(CancelEntry);
            }
        }

        public ICommand CancelStartLunchCommand
        {
            get
            {
                return new RelayCommand(CancelStartLunch);
            }
        }

        public ICommand CancelEndLunchCommand
        {
            get
            {
                return new RelayCommand(CancelEndLunch);
            }
        }

        public ICommand CancelExitCommand
        {
            get
            {
                return new RelayCommand(CancelExit);
            }
        }

        public async void RegisterEntry()
        {
            this.CurrentDay.EntryTime = DateTime.Now.ToLocalTime();

            this.dayDataService.Update(this.CurrentDay);

            this.SetButtonsEnabled();
            this.RefreshView();
        }

        public async void RegisterStartLunch()
        {
            this.CurrentDay.StartLunchTime = DateTime.Now.ToLocalTime();

            this.dayDataService.Update(this.CurrentDay);

            this.SetButtonsEnabled();
            this.RefreshView();
        }

        public async void RegisterEndLunch()
        {
            this.CurrentDay.EndLunchTime = DateTime.Now.ToLocalTime();

            this.dayDataService.Update(this.CurrentDay);

            this.SetButtonsEnabled();
            this.RefreshView();
        }

        public async void RegisterExit()
        {
            this.CurrentDay.ExitTime = DateTime.Now.ToLocalTime();

            this.dayDataService.Update(this.CurrentDay);

            this.SetButtonsEnabled();
            this.RefreshView();
        }

        private async void CancelEntry()
        {
            var response = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ConfirmCancelEntry,
                Languages.Cancel,
                null,
                Languages.Yes,
                Languages.No);

            if (response == Languages.Yes)
            {
                this.CurrentDay.EntryTime = null;

                this.dayDataService.Update(this.CurrentDay);

                this.SetButtonsEnabled();
                this.RefreshView();
            }
        }

        private async void CancelStartLunch()
        {
            var response = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ConfirmCancelStartLunch,
                Languages.Cancel,
                null,
                Languages.Yes,
                Languages.No);

            if (response == Languages.Yes)
            {
                this.CurrentDay.StartLunchTime = null;

                this.dayDataService.Update(this.CurrentDay);

                this.SetButtonsEnabled();
                this.RefreshView();
            }
        }

        private async void CancelEndLunch()
        {
            var response = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ConfirmCancelEndLunch,
                Languages.Cancel,
                null,
                Languages.Yes,
                Languages.No);

            if (response == Languages.Yes)
            {
                this.CurrentDay.EndLunchTime = null;

                this.dayDataService.Update(this.CurrentDay);

                this.SetButtonsEnabled();
                this.RefreshView();
            }
        }

        private async void CancelExit()
        {
            var response = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ConfirmCancelExit,
                Languages.Cancel,
                null,
                Languages.Yes,
                Languages.No);

            if (response == Languages.Yes)
            {
                this.CurrentDay.ExitTime = null;

                this.dayDataService.Update(this.CurrentDay);

                this.SetButtonsEnabled();
                this.RefreshView();
            }
        }

        private void RefreshView()
        {
            this.EntryTime = this.CurrentDay.EntryTimeString;
            this.StartLunchTime = this.CurrentDay.StartLunchTimeString;
            this.EndLunchTime = this.CurrentDay.EndLunchTimeString;
            this.ExitTime = this.CurrentDay.ExitTimeString;
            this.TotalTime = this.CurrentDay.TotalTimeString;
        }

        #endregion
    }
}
