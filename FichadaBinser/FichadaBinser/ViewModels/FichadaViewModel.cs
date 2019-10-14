using FichadaBinser.Helpers;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FichadaBinser.ViewModels
{
    public class FichadaViewModel : BaseViewModel
    {
        #region Attributes

        private string currentDate;
        private string currentTime;
        private string entryTime;
        private string startLunchTime;
        private string endLunchTime;
        private string exitTime;

        private bool isEnabledRegisterEntry;
        private bool isEnabledRegisterStartLunch;
        private bool isEnabledRegisterEndLunch;
        private bool isEnabledRegisterExit;

        private bool isVisibleCancelEntry;
        private bool isVisibleCancelStartLunch;
        private bool isVisibleCancelEndLunch;
        private bool isVisibleCancelExit;

        private DateTime? internalEntryTime;
        private DateTime? internalStartLunchTime;
        private DateTime? internalEndLunchTime;
        private DateTime? internalExitTime;

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
            this.LoadCurrentDate();
            this.LoadCurrentTime();
            this.InitializeTimer();
            this.SetButtonsEnabled();
        }

        #endregion

        #region Methods

        private void LoadCurrentDate()
        {
            DateTime fecha = DateTime.Now;

            CultureInfo myCulture = new CultureInfo("es-ES");

            string strFecha = string.Format(
                    "{0} {1} de {2}",
                    StringHelper.FirstUpper(myCulture.DateTimeFormat.GetDayName(fecha.DayOfWeek)),
                    fecha.Day.ToString(),
                    StringHelper.FirstUpper(myCulture.DateTimeFormat.GetMonthName(fecha.Month)));

            this.CurrentDate = strFecha;
        }

        private void LoadCurrentTime()
        {
            this.CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        private void InitializeTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => this.LoadCurrentTime());
                return true;
            });
        }

        private void SetButtonsEnabled()
        {
            this.IsEnabledRegisterEntry = this.internalEntryTime == null;
            this.IsEnabledRegisterStartLunch = this.internalStartLunchTime == null && this.internalEntryTime != null && this.internalExitTime == null;
            this.IsEnabledRegisterEndLunch = this.internalEndLunchTime == null && this.internalStartLunchTime != null && this.internalExitTime == null;
            this.IsEnabledRegisterExit = this.internalExitTime == null && this.internalEntryTime != null && ((this.internalStartLunchTime == null && this.internalEndLunchTime == null) || (this.internalStartLunchTime != null && this.internalEndLunchTime != null));

            this.IsVisibleCancelEntry = this.CanCancelEntry();
            this.IsVisibleCancelStartLunch = this.CanCancelStartLunch();
            this.IsVisibleCancelEndLunch = this.CanCancelEndLunch();
            this.IsVisibleCancelExit = this.CanCancelExit();
        }

        private bool CanCancelEntry()
        {
            if (this.internalEntryTime != null
                && this.internalStartLunchTime == null
                && this.internalEndLunchTime == null
                && this.internalExitTime == null)
            {
                return true;
            }

            return false;
        }

        private bool CanCancelStartLunch()
        {
            if (this.internalEntryTime != null
                && this.internalStartLunchTime != null
                && this.internalEndLunchTime == null
                && this.internalExitTime == null)
            {
                return true;
            }

            return false;
        }

        private bool CanCancelEndLunch()
        {
            if (this.internalEntryTime != null
                && this.internalStartLunchTime != null
                && this.internalEndLunchTime != null
                && this.internalExitTime == null)
            {
                return true;
            }

            return false;
        }

        private bool CanCancelExit()
        {
            if (this.internalEntryTime != null
                && this.internalExitTime != null)
            {
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
            this.internalEntryTime = DateTime.Now;
            this.EntryTime = this.internalEntryTime.Value.ToString("HH:mm:ss");

            this.SetButtonsEnabled();
        }

        public async void RegisterStartLunch()
        {
            this.internalStartLunchTime = DateTime.Now;
            this.StartLunchTime = this.internalStartLunchTime.Value.ToString("HH:mm:ss");

            this.SetButtonsEnabled();
        }

        public async void RegisterEndLunch()
        {
            this.internalEndLunchTime = DateTime.Now;
            this.EndLunchTime = this.internalEndLunchTime.Value.ToString("HH:mm:ss");

            this.SetButtonsEnabled();
        }

        public async void RegisterExit()
        {
            this.internalExitTime = DateTime.Now;
            this.ExitTime = this.internalExitTime.Value.ToString("HH:mm:ss");

            this.SetButtonsEnabled();
        }

        private void CancelEntry()
        {
            this.internalEntryTime = null;
            this.EntryTime = string.Empty;

            this.SetButtonsEnabled();
        }

        private void CancelStartLunch()
        {
            this.internalStartLunchTime = null;
            this.StartLunchTime = string.Empty;

            this.SetButtonsEnabled();
        }

        private void CancelEndLunch()
        {
            this.internalEndLunchTime = null;
            this.EndLunchTime = string.Empty;

            this.SetButtonsEnabled();
        }

        private void CancelExit()
        {
            this.internalExitTime = null;
            this.ExitTime = string.Empty;

            this.SetButtonsEnabled();
        }

        #endregion
    }
}
