using GalaSoft.MvvmLight.CommandWpf;
using Oauth;
using Oauth.Providers;
using ExcelToOutlook.Commands;
using System.Collections.ObjectModel;
using ExcelToOutlook.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using ExcelToOutlook.View.Dialogs;
using System.Windows;
using Excel;
using System.IO;
using System.Data;
using ExcelToOutlook.ViewModel.Dialogs;
using System.Windows.Documents;
using ExcelToOutlook.View.Adorners;
using System;
using System.Linq;
using System.Drawing;

namespace ExcelToOutlook.ViewModel
{
    class MainWindowViewModel : ViewModel
    {
        #region Fields
        private ExcelManager ExcelManager;
        private ExcelCommands ExcelCommands = new ExcelCommands();

        private ObservableCollection<GridData> data = new ObservableCollection<GridData>();
        public ObservableCollection<GridData> Data { get { return data; } set { data = value; OnPropertyChanged(); } }

        private ObservableCollection<string> subjects = new ObservableCollection<string>();
        private ObservableCollection<Event> SubjectEvents = new ObservableCollection<Event>();
        public ObservableCollection<string> Subjects { get { return subjects; } set { subjects = value; OnPropertyChanged(); } }
        private int selectedIndex = 0;
        public int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; OnPropertyChanged(); OnPropertyChanged("CurrentSubject"); } }
        public Event CurrentSubject { get { return SubjectEvents.Count > 0 ? SubjectEvents[selectedIndex] : new Event(); } }
        private int GridIndex = 0;
        public int MatchSubject { get { return GridIndex; } set { GridIndex = value; if (value >= 0) { selectedIndex = subjects.IndexOf(data[value].Subject); OnPropertyChanged("SelectedIndex"); OnPropertyChanged("CurrentSubject"); } } }
        
        private int calendarColorIndex = 0;
        public int CalendarColorIndex { get { return calendarColorIndex; } set { calendarColorIndex = value; } }
        private string calendarName = "";
        public string CalendarName { get { return calendarName; } set { calendarName = value; } }
        #endregion

        #region Commands
        public RelayCommand OpenFileCommand => new RelayCommand(ConstructTable);
        public RelayCommand SendCommand => new RelayCommand(SendEntries);
        public RelayCommand<DataGridCellInfo> EditDateCellCommand => new RelayCommand<DataGridCellInfo>(EditDate, (e) => { return data.Count > 0; });

        public MainWindowViewModel()
        {
            //Test Data
            data = new ObservableCollection<GridData>()
            {
                new GridData() { Row = 0, Subject = "Alpha", RawDate = new DateTime(2017, 1, 1) },
                new GridData() { Row = 1, Subject = "Beta", RawDate = new DateTime(2017, 1, 2) },
                new GridData() { Row = 2, Subject = "Charlie", RawDate = new DateTime(2017, 1, 3) },
                new GridData() { Row = 3, Subject = "Beta", RawDate = new DateTime(2017, 1, 4) },
                new GridData() { Row = 4, Subject = "Beta", RawDate = new DateTime(2017, 1, 5) },
                new GridData() { Row = 5, Subject = "Alpha", RawDate = new DateTime(2017, 1, 6) }
            };
            subjects = new ObservableCollection<string>()
            {
                "Alpha",
                "Beta",
                "Charlie"
            };
            SubjectEvents = new ObservableCollection<Event>()
            {
                new Event() { Subject = "Alpha", Body = "Body Alpha", Start = new DateTime(2016, 1, 1, 8, 0, 0), End = new DateTime(2016, 1, 1, 16, 0, 0), IsAllDay = false },
                new Event() { Subject = "Beta", Body = "Body Beta", Start = new DateTime(2016, 1, 2, 16, 0, 0), End = new DateTime(2016, 1, 2, 0, 0, 0), IsAllDay = false },
                new Event() { Subject = "Charlie", Body = "Body Charlie", Start = new DateTime(2016, 1, 3, 0, 0, 0), End = new DateTime(2016, 1, 3, 0, 0, 0), IsAllDay = true }
            };
        }

        public void EditDate(DataGridCellInfo info)
        {
            try
            {
                AdornerLayer Adorners = AdornerLayer.GetAdornerLayer(((UIElement)Application.Current.MainWindow.Content));
                Adorners.Add(
                    new DateChangeAdorner(
                        ((UIElement)Application.Current.MainWindow.Content),
                        System.Windows.Input.Mouse.GetPosition(Application.Current.MainWindow),
                        (GridData)info.Item,
                        Data));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        public void AuthorizationDialog()
        {
            AuthorizationDialog Dialog = new AuthorizationDialog() { Owner = Application.Current.MainWindow };
            Dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Dialog.ShowDialog();
        }

        public void SendEntries()
        {
            List<Event> Events = new List<Event>();
            foreach (GridData data in Data)
            {
                var index = Subjects.IndexOf(data.Subject);
                Event NextEvent = SubjectEvents[index].Clone();
                NextEvent.Start = new System.DateTime(data.RawDate.Year, data.RawDate.Month, data.RawDate.Day, NextEvent.Start.Hour, NextEvent.Start.Minute, NextEvent.Start.Second);
                NextEvent.End = new System.DateTime(data.RawDate.Year, data.RawDate.Month, data.RawDate.Day, NextEvent.End.Hour, NextEvent.End.Minute, NextEvent.End.Second);
                Events.Add(NextEvent);
            }

            ProgressDialog Dialog = new ProgressDialog(Events) { Owner = Application.Current.MainWindow };
            Dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Dialog.ShowDialog();
        }

        public void ConstructTable()
        {
            FileInfo File = ExcelCommands.OpenFile();
            if (File == null)
                return;

            ExcelManager = new ExcelManager(File);
            // Create DataTable from Excel Doc
            DataTable table = ExcelManager.WorksheetToDataTable();
            int dateIndex = ExcelManager.FindDateColumn();
            ExcelDialog ExcelDialog = new ExcelDialog(table, dateIndex) { Owner = Application.Current.MainWindow };
            ExcelDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bool? result = ExcelDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var dialog = (ExcelDialogViewModel)ExcelDialog.DataContext;
                var dateColumn = dialog.DateColumnIndex;
                var scheduleColumn = dialog.ScheduleColumnIndex;

                Data.Clear();
                subjects.Clear();
                
                List<DateTime> newDates = ExcelManager.ExtractColumnToDates(dateColumn);
                List<string> newSubjects = ExcelManager.ExtractColumnToString(scheduleColumn);

                for (int i = 0; i < newSubjects.Count; i++)
                {
                    if (!subjects.Contains(newSubjects[i]))
                    {
                        subjects.Add(newSubjects[i]);
                        SubjectEvents.Add(new Event() { Subject = newSubjects[i], Start = newDates[i] });
                    }
                    Data.Add(new GridData() { Row = i, Subject = newSubjects[i], RawDate = newDates[i] });
                }

                SelectedIndex = 0;
            }
        }
        #endregion
    }
}