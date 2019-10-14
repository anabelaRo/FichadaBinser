namespace FichadaBinser.Helpers
{
    using FichadaBinser.Interfaces;
    using FichadaBinser.Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string ConfirmCancelEntry
        {
            get { return Resource.ConfirmCancelEntry; }
        }

        public static string ConfirmCancelStartLunch
        {
            get { return Resource.ConfirmCancelStartLunch; }
        }

        public static string ConfirmCancelEndLunch
        {
            get { return Resource.ConfirmCancelEndLunch; }
        }

        public static string ConfirmCancelExit
        {
            get { return Resource.ConfirmCancelExit; }
        }

        public static string Cancel
        {
            get { return Resource.Cancel; }
        }

        public static string Yes
        {
            get { return Resource.Yes; }
        }

        public static string No
        {
            get { return Resource.No; }
        }
    }
}
