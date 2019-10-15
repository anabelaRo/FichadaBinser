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
        public SemanaViewModel Semana { get; set; }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            instance = this;

            this.Fichada = new FichadaViewModel();
            this.Semana = new SemanaViewModel();
        }

        #endregion
    }
}
