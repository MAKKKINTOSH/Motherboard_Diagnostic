using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Motherboard_Diagnostic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartDiagnosic();
        }
        private void StartDiagnosic()
        {
            Diagnostic.Init();
            Motherboard.Init();
        }
        private void RestartDiagnostic(object sender, RoutedEventArgs e)
        {
            Window repairWindow = ObjectsManager.FindChild<Window>(this, "RepairWindow");
            if (repairWindow != null)
            {
                repairWindow.Hide();
            }
            EventPanel.RemoveAllEvents();
            Button bt = ObjectsManager.FindChild<Button>(this, "StartPCButton");
            bt.Background = Brushes.LightGreen;
            bt.Content = "Запустить ПК";
            StartDiagnosic();
        }
        private string getSelectedInstrument()
        {
            StackPanel instruments = InstrumentsPanel;
            foreach (var instr in instruments.Children.OfType<RadioButton>())
            {
                if (instr.IsChecked ?? false)
                {
                    return instr.Name;
                }
            }
            return "Инструмент не выбран";
        }

        private void LaunchPCButton(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)e.Source;
            switch (bt.Content)
            {
                case "Запустить ПК":
                    if (Diagnostic.Faults.Count != 0)
                    {
                        EventPanel.AddEvent("ПК не запускается, устраните неисправности", "warning");
                    }
                    else
                    {
                        EventPanel.AddEvent("ПК запущен", "good");
                    }
                    bt.Background = Brushes.IndianRed;
                    bt.Content = "Выключить";
                    Diagnostic.IsRunning = false;
                    Diagnostic.PCIsLaunch = true;
                    break;

                

                case "Выключить":
                    bt.Background = Brushes.LightGreen;
                    bt.Content = "Запустить ПК";
                    Diagnostic.IsRunning = true;
                    Diagnostic.PCIsLaunch = false;
                    EventPanel.AddEvent("ПК выключен");
                    break;
            }
        }
        private void diagnosticPower(object sender, RoutedEventArgs e)
        {
            Motherboard.Power.MakeDiagnostic(getSelectedInstrument());
        }
        private void repairButton(object sender, RoutedEventArgs e)
        {
            if (Diagnostic.IsRunning)
            {
                new RepairWindow().Show();
            }
            else if (Diagnostic.PCIsLaunch)
            {
                EventPanel.AddEvent("Ремонт включенного компьютера невозможен", "warning");
            }
            else{
                EventPanel.AddEvent("Продиагностируйте неисправность");
            }
        }
    }
}
