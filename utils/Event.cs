using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Net.Http.Headers;

namespace Motherboard_Diagnostic
{
    static class EventPanel
    {
        private static readonly StackPanel Panel = ObjectsManager.FindChild<StackPanel>(Application.Current.MainWindow, "MessagePanel");
        public static void AddEvent(string text, EventType eventType = EventType.Normal)
        {
            var color = eventType switch
            {
                EventType.Warning => Colors.IndianRed,
                EventType.Good => Colors.LightGreen,
                EventType.Victory => Colors.Gold,
                _ => Colors.White,
            };
            Event message = new(text, color);
            Panel.Children.Insert(0, message);
            if (Panel.Children.Count > Config.maxEvents)
            {
                Panel.Children.RemoveAt(Panel.Children.Count - 1);
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
    enum EventType
    {
        Warning,
        Good,
        Victory, 
        Normal
    }
}
