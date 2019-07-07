using ExcelToOutlook.Model;
using System.Collections.Generic;
using System.Linq;

namespace ExcelToOutlook.Model
{
    public enum Color
    {
        LightBlue = 0, LightGreen = 1, LightOrange = 2,
        LightGray = 3, LightYellow = 4, LightTeal = 5,
        LightPink = 6, LightBrown = 7, LightRed = 8,
        MaxColor = 9, Auto = -1
    }

    public static class ColorExt
    {
        public static System.Windows.Media.Color ToMedia(this Color color)
        {
            System.Windows.Media.Color NewColor;
            if (MediaColor.TryGetValue(color, out NewColor))
                return NewColor;
            else
                return System.Windows.Media.Colors.White;
        }

        public static Color FromMedia(System.Windows.Media.Color color)
        {
            Color NewColor;
            if (MediaToColor.TryGetValue(color, out NewColor))
                return NewColor;
            else
                return Color.Auto;
        }

        public static Color FromString(string color)
        {
            Color NewColor;
            if (StringToColor.TryGetValue(color, out NewColor))
                return NewColor;
            else
                return Color.Auto;
        }

        public static string ToString(this Color color)
        {
            string NewColor;
            if (StringColor.TryGetValue(color, out NewColor))
                return NewColor;
            else
                return "White";
        }

        #region Color Dictionaries
        private static Dictionary<Color, System.Windows.Media.Color> MediaColor = new Dictionary<Color, System.Windows.Media.Color>()
        {
            { Model.Color.LightBlue, System.Windows.Media.Colors.LightBlue },
            { Model.Color.LightBrown, System.Windows.Media.Colors.Brown },
            { Model.Color.LightGray, System.Windows.Media.Colors.LightGray },
            { Model.Color.LightGreen, System.Windows.Media.Colors.LightGreen },
            { Model.Color.LightOrange, System.Windows.Media.Colors.Orange },
            { Model.Color.LightPink, System.Windows.Media.Colors.LightPink },
            { Model.Color.LightRed, System.Windows.Media.Colors.Red },
            { Model.Color.LightTeal, System.Windows.Media.Colors.Teal },
            { Model.Color.MaxColor, System.Windows.Media.Colors.Black },
            { Model.Color.Auto, System.Windows.Media.Colors.White }
        };

        private static Dictionary<System.Windows.Media.Color, Color> MediaToColor = new Dictionary<System.Windows.Media.Color, Color>()
        {
            { System.Windows.Media.Colors.LightBlue, Model.Color.LightBlue },
            { System.Windows.Media.Colors.Brown, Model.Color.LightBrown },
            { System.Windows.Media.Colors.LightGray, Model.Color.LightGray },
            { System.Windows.Media.Colors.LightGreen, Model.Color.LightGreen },
            { System.Windows.Media.Colors.Orange, Model.Color.LightOrange },
            { System.Windows.Media.Colors.LightPink, Model.Color.LightPink },
            { System.Windows.Media.Colors.Red, Model.Color.LightRed },
            { System.Windows.Media.Colors.Teal, Model.Color.LightTeal },
            { System.Windows.Media.Colors.Black, Model.Color.MaxColor },
            { System.Windows.Media.Colors.White, Model.Color.Auto }
        };

        private static Dictionary<string, Color> StringToColor = new Dictionary<string, Color>()
        {
            { "LightBlue", Model.Color.LightBlue },
            { "Brown", Model.Color.LightBrown },
            { "LightGray", Model.Color.LightGray },
            { "LightGreen", Model.Color.LightGreen },
            { "Orange", Model.Color.LightOrange },
            { "LightPink", Model.Color.LightPink },
            { "Red", Model.Color.LightRed },
            { "Teal", Model.Color.LightTeal },
            { "Black", Model.Color.MaxColor },
            { "White", Model.Color.Auto }
        };

        private static Dictionary<Color, string> StringColor = new Dictionary<Color, string>()
        {
            { Model.Color.LightBlue, "LightBlue" },
            { Model.Color.LightBrown, "Brown" },
            { Model.Color.LightGray, "LightGray" },
            { Model.Color.LightGreen, "LightGreen" },
            { Model.Color.LightOrange, "Orange" },
            { Model.Color.LightPink, "LightPink" },
            { Model.Color.LightRed, "Red" },
            { Model.Color.LightTeal, "Teal" },
            { Model.Color.MaxColor, "Black" },
            { Model.Color.Auto, "White" }
        };
        #endregion
    }

    public class Calendar
    {
        public string Name { get; set; } = "Calendar";
        public Color Color { get; set; } = Color.Auto;
        public List<Event> Events { get; set; } = new List<Event>();
}
}
