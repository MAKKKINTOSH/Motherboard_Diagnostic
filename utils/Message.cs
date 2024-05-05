using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Motherboard_Diagnostic
{
    static class EventPanel
    {
        private static StackPanel Panel = ObjectsManager.FindChild<StackPanel>(Application.Current.MainWindow, "MessagePanel");
        public static void AddEvent(string text)
        {
            Event message = new Event(text);
            Panel.Children.Add(message);
            if (Panel.Children.Count > 50)
            {
                Panel.Children.RemoveAt(0);
            }
        }
    }
    class Event : Label
    {
        private static StackPanel EventPanel = ObjectsManager.FindChild<StackPanel>(Application.Current.MainWindow, "EventPanel");
        private static Thickness BorderThicknes = new Thickness(0, 0, 1, 1);
        public Event(string Event)
        {
            this.Content = Event;
            this.FontSize = 14;
            this.BorderThickness = BorderThicknes;
            this.BorderBrush = Brushes.Black;
            this.Background = new SolidColorBrush(Colors.White);
        }
    }
}
