using GalaSoft.MvvmLight.CommandWpf;
using System.Data;
using System.Diagnostics;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;

namespace ExcelToOutlook.ViewModel.Dialogs
{
    class ExcelDialogViewModel : ViewModel
    {
        private string label = "Please select the date column";
        public string Label
        {
            get
            {
                return label;
            }

            set
            {
                label = value;
                OnPropertyChanged();
            }
        }

        private DataGrid DataGrid;
        private DataTable table;
        private DataTable Table { get { return table; } set { table = value; OnPropertyChanged("Data"); } }
        public DataView Data { get { return Table.AsDataView(); } }

        public RelayCommand OkayCommand => new RelayCommand(() => { }, () => { return DateColumnIndex > 0 && ScheduleColumnIndex > 0; });
        public RelayCommand<SelectedCellsChangedEventArgs> SelectColumnCommand => new RelayCommand<SelectedCellsChangedEventArgs>(SelectColumn);

        public RelayCommand DateCommand => new RelayCommand(() => { Label = "Please select the date column"; Mode = 0; });
        public RelayCommand ScheduleCommand => new RelayCommand(() => { Label = "Please select your schedule column"; Mode = 1; }, () => { return DateColumnIndex >= 0; });

        private ObservableCollection<Boolean> isSelected = new ObservableCollection<Boolean>();
        public ObservableCollection<Boolean> IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Brush> columnColor = new ObservableCollection<Brush>();
        public ObservableCollection<Brush> ColumnColor
        {
            get
            {
                return columnColor;
            }

            set
            {
                columnColor = value;
                OnPropertyChanged();
            }
        }

        public int Mode = 0;
        public int DateColumnIndex = -1;
        public int ScheduleColumnIndex = -1;
        public Brush DateColumnColor = Brushes.AliceBlue;
        public Brush ScheduleColumnColor = Brushes.IndianRed;

        private void SelectColumn(SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count < 1 || e.AddedCells[0].Column.DisplayIndex == DateColumnIndex || e.AddedCells[0].Column.DisplayIndex == ScheduleColumnIndex)
                return;
            if (DateColumnIndex < 0 || Mode == 0)
            {
                if (DateColumnIndex >= 0)
                    ColumnColor[DateColumnIndex] = Brushes.White;
                else
                {
                    Mode = 1;
                    Label = "Please select your schedule column";
                }
                ColumnColor[e.AddedCells[0].Column.DisplayIndex] = DateColumnColor;
                DateColumnIndex = e.AddedCells[0].Column.DisplayIndex;
            }
            else if (ScheduleColumnIndex >= 0 || Mode != 0)
            {
                if (ScheduleColumnIndex >= 0)
                    ColumnColor[ScheduleColumnIndex] = Brushes.White;
                ColumnColor[e.AddedCells[0].Column.DisplayIndex] = ScheduleColumnColor;
                ScheduleColumnIndex = e.AddedCells[0].Column.DisplayIndex;
            }
        }

        public ExcelDialogViewModel(DataGrid grid, DataTable data, int dateIndex)
        {
            DataGrid = grid;
            table = data;
            DateColumnIndex = dateIndex;
        }

        public void CheckDateColumn()
        {
            if (DateColumnIndex >= 0)
            {
                Mode = 1;
                Label = "Please select your schedule column";
                columnColor[DateColumnIndex] = DateColumnColor;
            }
        }
    }
}
