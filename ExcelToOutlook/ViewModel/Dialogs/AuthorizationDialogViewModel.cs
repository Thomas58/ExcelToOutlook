using GalaSoft.MvvmLight.CommandWpf;
using Oauth;
using Oauth.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExcelToOutlook.ViewModel.Dialogs
{
    class AuthorizationDialogViewModel : ViewModel
    {
        public Process Process = new Process();
        private Oauth2Manager Manager;

        public Uri AuthorizationLink { get; set; }
        private string authorizationCode { get; set; } = "";
        public string AuthorizationCode { get { return authorizationCode; } set { authorizationCode = value; OnPropertyChanged(); } }

        public RelayCommand OpenLinkCommand => new RelayCommand(OpenLink);
        public RelayCommand CopyLinkCommand => new RelayCommand(CopyLink);
        public RelayCommand<Window> AuthorizeCommand => new RelayCommand<Window>(Authorize);

        private void CopyLink()
        {
            Clipboard.SetText(AuthorizationLink.ToString());
        }

        public void Authorize(Window window)
        {
            try
            {
                Manager.EnterAuthorizationCode(AuthorizationCode);
                Process.Dispose();
                window.Close();
            }
            catch (Exception e)
            {
                if (e.Data.Contains("StatusCode") && e.Data["StatusCode"].Equals(HttpStatusCode.BadRequest))
                {
                    MessageBox.Show("Incorrect Authorization Code!", "Authorization", MessageBoxButton.OK, MessageBoxImage.None);
                }
                else throw e;
            }
        }

        private void OpenLink()
        {
            try
            {
                Process = Process.Start(AuthorizationLink.ToString());
            }
            catch (SecurityException)
            {
                MessageBox.Show("Error while opening web-link, please copy and paste the link to your web-browser.", "Authorization", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public Oauth2Manager GetOathManager()
        {
            return Manager;
        }

        public AuthorizationDialogViewModel()
        {
            this.Manager = new Oauth2Manager(new Outlook());
            AuthorizationLink = Manager.AuthorizationWebLink;
        }

    }
}
