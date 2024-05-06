using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Net.Http.Headers;

namespace Motherboard_Diagnostic
{
    static class EventPanel
    {
        private static StackPanel Panel = ObjectsManager.FindChild<StackPanel>(Application.Current.MainWindow, "MessagePanel");
        public static void AddEvent(string text, string eventType = "normal")
        {
            Color color;
            switch (eventType)
            {
                case "warning":
                    color = Colors.Red;
                    break;
                case "good":
                    color = Colors.Green;
                    break;
                case "normal":
                default:
                    color = Colors.White;
                    break;
            }
            Event message = new Event(text, color);
            Panel.Children.Add(message);
            if (Panel.Children.Count > Config.maxEvents)
            {
                Panel.Children.RemoveAt(0);
            }
        }
    }
    class Event : Label
    {
        private static StackPanel EventPanel = ObjectsManager.FindChild<StackPanel>(Application.Current.MainWindow, "EventPanel");
        private static Thickness BorderThicknes = new Thickness(0, 0, 1, 1);
        public Event(string Event, Color color)
        {
            this.Content = Event;
            this.FontSize = 14;
            this.BorderThickness = BorderThicknes;
            this.BorderBrush = Brushes.Black;
            this.Background = new SolidColorBrush(color);
        }
    }
}
