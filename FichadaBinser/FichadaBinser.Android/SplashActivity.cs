using Android.App;
using Android.OS;

namespace FichadaBinser.Droid
{
    [Activity(Theme = "@style/Theme.Splash", //Indicates the theme to use for this activity
                 MainLauncher = true, //Set it as boot activity
                 NoHistory = true)] //Doesn't place it in back stack
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Threading.Thread.Sleep(1800);
            this.StartActivity(typeof(MainActivity));
        }
    }
}