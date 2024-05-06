using System;
using System.Collections.Generic;
using System.Linq;
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
            initRepairs();
        }
        public void initRepairs()
        {
            foreach (var item in DiagnosticHandbook.Solutions)
            {
                Button button = new();
                button.Click += makeRepair;
                button.Content = item.description;
                repairPanel.Children.Add(button);
            }
        }
        private void makeRepair(object sender, RoutedEventArgs e)
        {
            int solumtionId = -1;
            foreach (var item in DiagnosticHandbook.Solutions)
            {
                if (((Button)e.Source).Content.ToString() == item.description)
                {
                    solumtionId = item.id;
                    break;
                }
            }
            foreach (var item in DiagnosticHandbook.Faults)
            {
                if (item.id == solumtionId)
                {
                    Diagnostic.Faults.Remove(item);
                    EventPanel.AddEvent("Поздравляем, неисправность исправлена", "good");
                    this.Hide();
                    return;
                }
            }
            EventPanel.AddEvent("Неправильно", "warning");
            return;
        }
    }
}
