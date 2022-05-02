using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library.Mobile.Authentication;
using Android.Content.PM;

namespace Library.Mobile.Droid
{
    [Activity(Label = "LoginRedirectActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTask)]
    [IntentFilter(
        actions: new[] { Intent.ActionView },
        Categories = new[] 
        {
            Intent.CategoryDefault, 
            Intent.CategoryBrowsable 
        },
        DataSchemes = new[]
        {
            "com.companyname.library",
        },
        DataPaths = new[]
        {
            ":/oauth2redirect",
        })]
    public class LoginGoogleRedirectActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Android.Net.Uri uriAndroid = Intent.Data;
            var uriNetfx = new Uri(uriAndroid.ToString());
            MainPage.Auth?.OnPageLoading(uriNetfx);

            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            Finish();
            return;
        }
    }
}