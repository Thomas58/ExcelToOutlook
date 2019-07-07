using ExcelToOutlook.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ExcelToOutlook.View.Adorners
{
    class DateChangeAdorner : Adorner
    {
        VisualCollection VisualChildren;
        protected override int VisualChildrenCount { get { return VisualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return VisualChildren[index]; }

        private bool cascade = false;
        public bool Cascade { get { return cascade; } set { cascade = value; } }

        private Border Container;
        private Rect Position;
        private GridData Cell;

        public DateChangeAdorner(UIElement adornedElement, Point position, GridData gridCell, ObservableCollection<GridData> data) : base(adornedElement)
        {
            Position = new Rect(position.X - 8, position.Y - 8, 181, 188);
            Cell = gridCell;

            VisualChildren = new VisualCollection(this);

            Grid Grid = new Grid();
            RowDefinitionCollection rows = Grid.RowDefinitions;
            rows.Add(new RowDefinition() { Height = new GridLength(160, GridUnitType.Pixel) });
            rows.Add(new RowDefinition() { Height = new GridLength(25 , GridUnitType.Pixel) });

            System.Windows.Controls.Calendar DateCalendar = new System.Windows.Controls.Calendar()
            {
                BorderBrush = Brushes.Transparent,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FirstDayOfWeek = DayOfWeek.Monday,
                IsTodayHighlighted = false,
                SelectionMode = CalendarSelectionMode.SingleDate,
                DisplayDate = gridCell.RawDate,
                SelectedDate = gridCell.RawDate
            };

            Grid.SetRow(DateCalendar, 0);

            CheckBox Cascade = new CheckBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Content = "Cascade Date"
            };
            Binding checkBinding = new Binding("Cascade");
            checkBinding.Source = this;
            Cascade.SetBinding(CheckBox.IsCheckedProperty, checkBinding);

            Grid.SetRow(Cascade, 1);

            UIElementCollection GridUIElements = Grid.Children;
            GridUIElements.Add(DateCalendar);
            GridUIElements.Add(Cascade);

            Container = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Background = Brushes.White,
                CornerRadius = new CornerRadius(6)
            };
            Container.Child = Grid;

            VisualChildren.Add(Container);


            DateCalendar.SelectedDatesChanged += new EventHandler<SelectionChangedEventArgs>((sender, e) => 
            {
                var item = (DateTime)e.AddedItems[0];
                Cell.RawDate = item;
                data.RemoveAt(Cell.Row);
                data.Insert(Cell.Row, Cell);
            });
            MouseLeave += new MouseEventHandler((sender, e) =>
            {
                
                AdornerLayer.GetAdornerLayer(AdornedElement).Remove(this);
            });

        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Container.Arrange(Position);
            return finalSize;
        }
    }
}
