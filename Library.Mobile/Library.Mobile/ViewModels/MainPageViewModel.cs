using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Auth;
using System.Linq;
using System.Diagnostics;

namespace Library.Mobile
{
    public class MainPageViewModel : ViewModel
    {
        //public ICommand SomeCommand { get; private set; }


        public MainPageViewModel()
        {
            //SomeCommand = new Command(()=> { someMethod(); });
        }

        //private void someMethod() { }
    }
}
