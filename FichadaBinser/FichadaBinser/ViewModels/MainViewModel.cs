﻿using FichadaBinser.Helpers;
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
        private bool IsCurrentDayDirty;

        #endregion

        #region ViewModels

        public FichadaViewModel Fichada { get; set; }
        public SemanaViewModel Semana { get; set; }
        public EditarDiaViewModel EditarDia { get; set; }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            instance = this;

            dayDataService = new DayDataService();

            CurrentDay = dayDataService.GetCurrentDay();
            WeekDays = dayDataService.GetCurrentWeekDays();

            Fichada = new FichadaViewModel();
            Semana = new SemanaViewModel();

            InitializeTimer();
        }

        #endregion

        #region Methods

        private void InitializeTimer()
        {
            var viewModels = new ITimerViewModel[] { Fichada, Semana };

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => DoTimerAction(viewModels));
                return true;
            });
        }

        private void DoTimerAction(IEnumerable<ITimerViewModel> viewModels)
        {
            bool refreshView = false;

            if (IsDirty)
            {
                WeekDays = dayDataService.GetCurrentWeekDays();

                IsDirty = false;
            }

            if (IsCurrentDayDirty)
            {
                refreshView = true;

                CurrentDay = dayDataService.GetCurrentDay();

                IsCurrentDayDirty = false;
            }

            foreach (ITimerViewModel timerViewModel in viewModels)
            {
                timerViewModel.DoTimerAction(refreshView);
            }
        }

        public void SaveToDataBase(Day day)
        {
            if (day.DayId != null)
                dayDataService.Update(day);
            else
                dayDataService.Insert(day);

            if (day.DayId == DayHelper.GetDayIdByDate(DateTime.Today))
                IsCurrentDayDirty = true;

            IsDirty = true;
        }

        #endregion
    }
}
