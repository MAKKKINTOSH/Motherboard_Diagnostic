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
            InitializeInstrumentPanel();
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
        private Instruments getSelectedInstrument()
        {
            StackPanel instruments = InstrumentsPanel;
            foreach (var instr in instruments.Children.OfType<RadioButton>())
            {
                if (instr.IsChecked ?? false)
                {
                    return DiagnosticHandbook.InstrumentsDictionary.FirstOrDefault(x => x.Value == instr.Name).Key;
                }
            }
            return Instruments.Ohmmeter;
        }

        private void LaunchPCButton(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)e.Source;
            switch (bt.Content)
            {
                case "Запустить ПК":
                    if (Diagnostic.Faults.Count != 0)
                    {
                        EventPanel.AddEvent("ПК не запускается, устраните неисправности", EventType.Warning);
                    }
                    else
                    {
                        EventPanel.AddEvent("ПК запущен", EventType.Victory);
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

        private void diagnosticSouthBridge(object sender, RoutedEventArgs e)
        {
            Motherboard.SouthBridge.MakeDiagnostic(getSelectedInstrument());
        }

        private void diagnosticBIOS(object sender, RoutedEventArgs e)
        {
            Motherboard.BIOS.MakeDiagnostic(getSelectedInstrument());
        }

        private void repairButton(object sender, RoutedEventArgs e)
        {
            if (Diagnostic.IsRunning)
            {
                new RepairWindow().Show();
            }
            else if (Diagnostic.PCIsLaunch)
            {
                EventPanel.AddEvent("Ремонт включенного компьютера невозможен", EventType.Warning);
            }
            else{
                EventPanel.AddEvent("Продиагностируйте неисправность");
            }
        }
        private void InitializeInstrumentPanel()
        {
            Thickness margin = new(50, 0, 0, 0);
            InstrumentsPanel.Children.Clear();
            foreach (var instr in DiagnosticHandbook.InstrumentsDictionary.Values)
            {
                RadioButton btn = new();
                btn.Name = instr;
                btn.Content = DiagnosticHandbook.RusInstrumentsNames[instr];
                btn.Margin = margin;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.GroupName = "Instruments";
                InstrumentsPanel.Children.Add(btn);
            }
            ((RadioButton)InstrumentsPanel.Children[0]).IsChecked = true;
        }
    }
}
