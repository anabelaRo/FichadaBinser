using System;
using System.Collections.Generic;
using System.Text;

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

        #region ViewModels

        public FichadaViewModel Fichada { get; set; }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            instance = this;

            this.Fichada = new FichadaViewModel();
        }

        #endregion
    }
}
