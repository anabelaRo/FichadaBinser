using FichadaBinser.Interfaces;
using FichadaBinser.Models;
using FichadaBinser.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FichadaBinser.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();

            return instance;
        }

        #endregion

        #region Services

        DayDataService dayDataService;

        #endregion

        #region Properties

        public Day CurrentDay;
        public List<Day> WeekDays;

        private bool IsDirty;

        #endregion

        #region ViewModels

        public FichadaViewModel Fichada { get; set; }
        public SemanaViewModel Semana { get; set; }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            instance = this;

            this.dayDataService = new DayDataService();

            this.CurrentDay = dayDataService.GetCurrentDay();
            this.WeekDays = this.dayDataService.GetCurrentWeekDays();

            this.Fichada = new FichadaViewModel();
            this.Semana = new SemanaViewModel();

            this.InitializeTimer();
        }

        #endregion

        #region Methods

        private void InitializeTimer()
        {
            var viewModels = new ITimerViewModel[] { this.Fichada, this.Semana };

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => this.DoTimerAction(viewModels));
                return true;
            });
        }

        private void DoTimerAction(IEnumerable<ITimerViewModel> viewModels)
        {
            if (this.IsDirty)
            {
                this.WeekDays = this.dayDataService.GetCurrentWeekDays();

                this.IsDirty = false;
            }

            foreach (ITimerViewModel timerViewModel in viewModels)
            {
                timerViewModel.DoTimerAction();
            }
        }

        public void UpdateDay(Day day)
        {
            this.dayDataService.Update(day);

            this.IsDirty = true;
        }

        #endregion
    }
}
