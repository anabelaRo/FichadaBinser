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

        public static string ConfirmSaveChanges
        {
            get { return Resource.ConfirmSaveChanges; }
        }

        public static string ValidationCompleteEntryTime
        {
            get { return Resource.ValidationCompleteEntryTime; }
        }

        public static string ValidationCompleteExitTime
        {
            get { return Resource.ValidationCompleteExitTime; }
        }

        public static string ValidationInvalidTime
        {
            get { return Resource.ValidationInvalidTime; }
        }

        public static string ValidationCompleteEndLunchTime
        {
            get { return Resource.ValidationCompleteEndLunchTime; }
        }

        public static string ValidationEntryTimeGreaterThanExitTime
        {
            get { return Resource.ValidationEntryTimeGreaterThanExitTime; }
        }

        public static string ValidationEntryTimeGreaterThanStartLunchTime
        {
            get { return Resource.ValidationEntryTimeGreaterThanStartLunchTime; }
        }

        public static string ValidationStartLunchTimeGreaterThanEndLunchTime
        {
            get { return Resource.ValidationStartLunchTimeGreaterThanEndLunchTime; }
        }

        public static string ValidationStartLunchTimeGreaterThanExitTime
        {
            get { return Resource.ValidationStartLunchTimeGreaterThanExitTime; }
        }

        public static string IncorrectTime
        {
            get { return Resource.IncorrectTime; }
        }

        public static string Ok
        {
            get { return Resource.Ok; }
        }
    }
}
