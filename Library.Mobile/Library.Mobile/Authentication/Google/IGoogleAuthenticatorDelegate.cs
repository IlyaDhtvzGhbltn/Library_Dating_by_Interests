using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Mobile.Authentication
{
    public interface IGoogleAuthenticatorDelegate
    {
        void OnAuthenticationCompleted(GoogleOAuthToken token);
        void OnAuthenticationFailed(string message, Exception exception);
        void OnAuthenticationCanceled();
    }
}
