using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Library.Mobile.Authentication;

namespace Library.Mobile
{
    public partial class MainPage : ContentPage
    {
        public static GoogleAuthenticator Auth;

        public MainPage()
        {
            InitializeComponent();
        }

        public void RunGoogleOAuthRegistation(object sender, EventArgs e)
        {
            Auth = new GoogleAuthenticator(
                clientId: "968459113173-n17ni6ee8umhhmsmjbhke3c3fl8esfna.apps.googleusercontent.com",
                scope: "https://www.googleapis.com/auth/youtube.readonly",
                redirectUrl: "com.companyname.library:/oauth2redirect");


            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(Auth.GetAuthenticator());
        }
    }
}
