using ExcelToOutlook.Model;
using ExcelToOutlook.Tools;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;

namespace ExcelToOutlook.ViewModel.Dialogs
{
    class ProgressDialogViewModel : ViewModel
    {
        List<Event> Events;
        public Event CurrentEvent { get; set; } = new Event() { Subject = "Subject", Start = DateTime.Now, End = DateTime.Now };
        bool hasConnected = false;
        public bool HasConnected { get { return hasConnected; } set { hasConnected = value; } }
        private bool canceled = false;

        public RelayCommand CancelCommand => new RelayCommand(() => { canceled = true; });

        public ProgressDialogViewModel(List<Event> events)
        {
            this.Events = events;
            if (0 < Events.Count)
                this.CurrentEvent = Events[0];
            else
                this.CurrentEvent = new Event() { Subject = "Subject", Start = DateTime.Now, End = DateTime.Now };
        }

        private void ConnectToOutlookAsync()
        {
            // Connect to Outlook and create events.
            // Create ouath2manager with authorizationdialog.
            // Start a backgroundworker to create events.
            // Remember to check for 'Cancel' when possible.
        }
    }
}
