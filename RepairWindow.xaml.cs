using System.Windows;
using System.Windows.Controls;


namespace Motherboard_Diagnostic
{
    /// <summary>
    /// Логика взаимодействия для RepairWindow.xaml
    /// </summary>
    public partial class RepairWindow : Window
    {
        public RepairWindow()
        {
            InitializeComponent();
            this.Name = "RepairWindow";
            InitRepairs();
        }
        public void InitRepairs()
        {
            foreach (var item in Diagnostic.Solutions)
            {
                Button button = new();
                TextBlock textBlock = new();
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Text = item.Description;

                button.Click += MakeRepair;
                button.Content = textBlock;
                repairPanel.Children.Add(button);
            }
        }
        private void MakeRepair(object sender, RoutedEventArgs e)
        {
            Solution solution = null;
            foreach (var item in Diagnostic.Solutions)
            {
                Button button = (Button)e.Source;
                if (((TextBlock)button.Content).Text == item.Description)
                {
                    solution = item;
                    break;
                }
            }
            foreach (var item in Diagnostic.Faults)
            {
                if (item.Solution == solution)
                {
                    Diagnostic.Faults.Remove(item);
                    Diagnostic.Solutions.Remove(solution);
                    EventPanel.AddEvent("Поздравляем, неисправность исправлена", EventType.Good);
                    this.Hide();
                    return;
                }
            }
            EventPanel.AddEvent("Неправильно", EventType.Warning);
            this.Hide();
            return;
        }
    }
}
