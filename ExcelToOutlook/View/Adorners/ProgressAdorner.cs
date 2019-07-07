using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ExcelToOutlook.View.Adorners
{
    class ProgressAdorner : Adorner, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        VisualCollection VisualChildren;
        protected override int VisualChildrenCount { get { return VisualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return VisualChildren[index]; }

        Grid Grid = new Grid();
        private bool cancelButtonEnabled = false;
        public bool CancelButtonEnabled { get { return cancelButtonEnabled; } set { cancelButtonEnabled = value; OnPropertyChanged("CancelButtonEnabled"); } }

        public ProgressAdorner(UIElement adornedElement) : base(adornedElement)
        {
            VisualChildren = new VisualCollection(this);
            
            RowDefinitionCollection rows = Grid.RowDefinitions;
            rows.Add(new RowDefinition() { Height = new GridLength(1 , GridUnitType.Star ) });
            rows.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
            rows.Add(new RowDefinition() { Height = new GridLength(25, GridUnitType.Pixel) });
            rows.Add(new RowDefinition() { Height = new GridLength(25, GridUnitType.Pixel) });
            rows.Add(new RowDefinition() { Height = new GridLength(1 , GridUnitType.Star ) });
            
            Ellipse Ellipse = new Ellipse()
            {
                Name = "Icon",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 50,
                Width = 50,
                Stroke = Brushes.Black,
                Fill = Brushes.RoyalBlue
            };
            Grid.SetRow(Ellipse, 1);

            TriggerCollection Triggers = Ellipse.Triggers;
            EventTrigger Event = new EventTrigger(Ellipse.LoadedEvent);
            BeginStoryboard BeginStoryboard = new BeginStoryboard();
            Event.Actions.Add(BeginStoryboard);
            Triggers.Add(Event);

            Storyboard Storyboard = new Storyboard();
            BeginStoryboard.Storyboard = Storyboard;
            DoubleAnimation Animation1 = new DoubleAnimation()
            {
                From = 20.0,
                To = 50.0,
                Duration = new Duration(new TimeSpan(TimeSpan.TicksPerSecond)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            DoubleAnimation Animation2 = new DoubleAnimation()
            {
                From = 20.0,
                To = 50.0,
                Duration = new Duration(new TimeSpan(TimeSpan.TicksPerSecond)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            Storyboard.SetTarget(Animation1, Ellipse);
            Storyboard.SetTarget(Animation2, Ellipse);
            Storyboard.SetTargetProperty(Animation1, new PropertyPath(Ellipse.HeightProperty));
            Storyboard.SetTargetProperty(Animation2, new PropertyPath(Ellipse.WidthProperty));
            TimelineCollection storyboard = Storyboard.Children;
            storyboard.Add(Animation1);
            storyboard.Add(Animation2);

            ProgressBar ProgressBar = new ProgressBar()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 20,
                Width = 200
            };
            Grid.SetRow(ProgressBar, 2);

            Button Button = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 100,
                Height = 25,
                FontSize = 12,
                Content = "Cancel"
            };
            Button.Click += new RoutedEventHandler(CancelTask);
            Binding Binding = new Binding("CancelButtonEnabled");
            Binding.Source = this;
            Button.SetBinding(Button.IsEnabledProperty, Binding);

            Loaded += new RoutedEventHandler((sender, e) =>
            {
                Timer ButtonTimer = new Timer(3000);
                ButtonTimer.Elapsed += new ElapsedEventHandler((sender2, e2) => { CancelButtonEnabled = true; });
                ButtonTimer.AutoReset = false;
                ButtonTimer.Start();
            });
            Grid.SetRow(Button, 3);
            
            UIElementCollection GridUIElements = Grid.Children;
            GridUIElements.Add(Ellipse);
            GridUIElements.Add(ProgressBar);
            GridUIElements.Add(Button);
            Brush Brush = Brushes.LightGray.Clone();
            Brush.Opacity = 0.5;
            Grid.Background = Brush;

            VisualChildren.Add(Grid);
        }

        public void CancelTask(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(AdornedElement).Remove(this);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var element = (FrameworkElement)AdornedElement;
            Grid.Arrange(new Rect(0,0, element.ActualWidth, element.ActualHeight));
            
            return finalSize;
        }
    }
}
