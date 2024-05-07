using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Net.Http.Headers;

namespace Motherboard_Diagnostic
{
    static class EventPanel
    {
        private static readonly StackPanel Panel = ObjectsManager.FindChild<StackPanel>(Application.Current.MainWindow, "MessagePanel");
        public static void AddEvent(string text, string eventType = "normal")
        {
            var color = eventType switch
            {
                "warning" => Colors.IndianRed,
                "good" => Colors.LightGreen,
                _ => Colors.White,
            };
            Event message = new(text, color);
            Panel.Children.Insert(0, message);
            if (Panel.Children.Count > Config.maxEvents)
            {
                Panel.Children.RemoveAt(0);
            }
        }
        public static void RemoveAllEvents()
        {
            Panel.Children.Clear();
        }
    }
    class Event : Label
    {
        private static Thickness BorderThicknes = new(0, 0, 1, 1);
        public Event(string Event, Color color)
        {
            TextBlock textBlock = new();
            textBlock.Text = Event;
            textBlock.TextWrapping = TextWrapping.Wrap;
            this.Content = textBlock;
            this.FontSize = 14;
            this.BorderThickness = BorderThicknes;
            this.BorderBrush = Brushes.Black;
            this.Background = new SolidColorBrush(color);
        }
    }
}
