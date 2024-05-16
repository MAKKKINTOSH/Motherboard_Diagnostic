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
            bool isRepair = false;
            bool isBadlyBroken = false;
            if (Diagnostic.HasFault(DiagnosticHandbook.Faults.Find(x => x.Id == 0)))
            {
                isBadlyBroken = true;
            }
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
                    EventPanel.AddMessageEvent("Поздравляем, неисправность исправлена", EventType.Good);
                    isRepair = true;
                    break;
                }
            }
            if (!isBadlyBroken && solution.Description == DiagnosticHandbook.Faults.Find(x => x.Id == 0).Solution.Description)
            {
                EventPanel.AddMessageEvent("Плату можно было починить, а ты купил новую...", EventType.VeryBad);
                Diagnostic.Faults.Clear();
                isRepair = true;
            }
            else if (isBadlyBroken && isRepair)
            {
                Diagnostic.Faults.Clear();
                EventPanel.AddMessageEvent("Действительно, единственным вариантом тут была только замена платы, ты хорошо справился!", EventType.VeryGood);
            }
            else if (!isRepair)
            {
                EventPanel.AddMessageEvent("Неправильно", EventType.Warning);
            }
            this.Hide();
        }
    }
}
