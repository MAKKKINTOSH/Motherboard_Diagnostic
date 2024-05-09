using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Net.Http.Headers;
using System.Windows.Media.Imaging;

namespace Motherboard_Diagnostic
{
    static class EventPanel
    {
        private static readonly StackPanel Panel = ObjectsManager.FindChild<StackPanel>(Application.Current.MainWindow, "MessagePanel");
        private static void AddEvent(Event new_event, EventType eventType = EventType.Normal)
        {
            var color = eventType switch
            {
                EventType.Warning => Colors.IndianRed,
                EventType.Good => Colors.LightGreen,
                EventType.VeryGood => Colors.Gold,
                _ => Colors.White,
            };
            new_event.SetColor(color);
            Panel.Children.Insert(0, new_event);
            if (Panel.Children.Count > Config.maxEvents)
            {
                Panel.Children.RemoveAt(Panel.Children.Count - 1);
            }
        }
        public static void RemoveAllEvents()
        {
            Panel.Children.Clear();
        }
        public static void AddMessageEvent(string text, EventType eventType = EventType.Normal)
        {
            TextEvent message = new(text);
            AddEvent(message, eventType);
        }
        public static void AddChartEvent(string chartFilename, EventType eventType = EventType.Normal)
        {
            ChartEvent message = new(chartFilename);
            AddEvent(message, eventType);
        }
    }
    class Event : Label
    {
        private static Thickness BorderThicknes = new(0, 0, 1, 1);
        public Event()
        {
            this.FontSize = 14;
            this.BorderThickness = BorderThicknes;
            this.BorderBrush = Brushes.Black;
        }
        public void SetColor(Color color)
        {
            this.Background = new SolidColorBrush(color);
        }
    }
    class TextEvent : Event
    {
        public TextEvent(string eventMessage) : base()
        {
            TextBlock textBlock = new();
            textBlock.Text = eventMessage;
            textBlock.TextWrapping = TextWrapping.Wrap;
            this.Content = textBlock;
        }
    }
    class ChartEvent : Event
    {
        public ChartEvent(string filename) : base()
        {
            Image image = new Image();
            image.Source = new BitmapImage(
                new System.Uri(filename)
            );
            this.Content = image;
        }
    }
    enum EventType
    {
        Warning,
        Good,
        VeryGood, 
        Normal
    }
}
