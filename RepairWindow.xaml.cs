using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            initRepairs();
        }
        public void initRepairs()
        {
            foreach (var item in Diagnostic.Solutions)
            {
                Button button = new();
                TextBlock textBlock = new();
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Text = item.Description;

                button.Click += makeRepair;
                button.Content = textBlock;
                repairPanel.Children.Add(button);
            }
        }
        private void makeRepair(object sender, RoutedEventArgs e)
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
                    EventPanel.AddEvent("Поздравляем, неисправность исправлена", "good");
                    this.Hide();
                    return;
                }
            }
            EventPanel.AddEvent("Неправильно", "warning");
            this.Hide();
            return;
        }
    }
}
