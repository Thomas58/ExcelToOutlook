using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ExcelToOutlook.View.Adorners
{
    class ColumnAdorner : Adorner
    {
        public ColumnAdorner(UIElement adornedElement) : base(adornedElement)
        {

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRoundedRectangle(new SolidColorBrush(Colors.Transparent), new Pen(new SolidColorBrush(Colors.Navy), 3.0), new Rect(this.AdornedElement.DesiredSize), 5.0, 5.0);
        }
    }
}
