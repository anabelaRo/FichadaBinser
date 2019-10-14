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
        private string lunchStartTime;
        private string lunchEndTime;
        private string exitTime;

        private bool isEnabledRegisterEntry;
        private bool isEnabledRegisterStartLunch;
        private bool isEnabledRegisterEndLunch;
        private bool isEnabledRegisterExit;

        private DateTime? internalEntryTime;
        private DateTime? internalLunchStartTime;
        private DateTime? internalLunchEndTime;
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

        public string LunchStartTime
        {
            get { return lunchStartTime; }
            set { SetValue(ref lunchStartTime, value); }
        }

        public string LunchEndTime
        {
            get { return lunchEndTime; }
            set { SetValue(ref lunchEndTime, value); }
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
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(() => this.LoadCurrentTime());
                return true;
            });
        }

        private void SetButtonsEnabled()
        {
            this.IsEnabledRegisterEntry = this.internalEntryTime == null;
            this.IsEnabledRegisterStartLunch = this.internalLunchStartTime == null;
            this.IsEnabledRegisterEndLunch = this.internalLunchEndTime == null;
            this.IsEnabledRegisterExit = this.internalExitTime == null;
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

        public async void RegisterEntry()
        {
            this.internalEntryTime = DateTime.Now;

            this.SetButtonsEnabled();
        }

        public async void RegisterStartLunch()
        {
            this.internalLunchStartTime = DateTime.Now;

            this.SetButtonsEnabled();
        }

        public async void RegisterEndLunch()
        {
            this.internalLunchEndTime = DateTime.Now;

            this.SetButtonsEnabled();
        }

        public async void RegisterExit()
        {
            this.internalExitTime = DateTime.Now;

            this.SetButtonsEnabled();
        }

        #endregion
    }
}
