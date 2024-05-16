
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
        private Instruments GetSelectedInstrument()
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
                        EventPanel.AddMessageEvent("ПК не запускается, устраните неисправности", EventType.Warning);
                        if (
                            Diagnostic.HasFault(DiagnosticHandbook.Faults.Find((x) => x.Id == 5)) ||
                            Diagnostic.HasFault(DiagnosticHandbook.Faults.Find((x) => x.Id == 8))
                            )
                        {
                            EventPanel.AddMessageEvent("Изображения нет", EventType.Warning);
                        }
                        else
                        {
                            EventPanel.AddMessageEvent("Изображение есть", EventType.VeryGood);
                        }
                    }
                    else
                    {
                        EventPanel.AddMessageEvent("ПК запущен, Изображение есть", EventType.VeryGood);
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
                    EventPanel.AddMessageEvent("ПК выключен");
                    break;
            }
        }
        private void DiagnosticPower(object sender, RoutedEventArgs e)
        {
            Motherboard.Power.MakeDiagnostic(GetSelectedInstrument(), ((Button)sender).Content.ToString());
        }

        private void DiagnosticUSB(object sender, RoutedEventArgs e)
        {
            Motherboard.USB.MakeDiagnostic(GetSelectedInstrument(), ((Button)sender).Content.ToString());
        }

        private void DiagnosticRAMSlot(object sender, RoutedEventArgs e)
        {
            Motherboard.RAMSlot.MakeDiagnostic(GetSelectedInstrument(), ((Button)sender).Content.ToString());
        }


        private void DiagnosticBIOS(object sender, RoutedEventArgs e)
        {
            Motherboard.BIOS.MakeDiagnostic(GetSelectedInstrument());
        }
        private void DiagnosticBiosbattery(object sender, RoutedEventArgs e)
        {
            Motherboard.Biosbattery.MakeDiagnostic(GetSelectedInstrument());
        }

        private void DiagnosticPCIEInterface(object sender, RoutedEventArgs e)
        {
            Motherboard.PCInterface.MakeDiagnostic(GetSelectedInstrument());
        }

        private void DiagnosticCapasitor(object sender, RoutedEventArgs e)
        {
            Motherboard.Capacitor.MakeDiagnostic(GetSelectedInstrument(), ((Button)sender).Content.ToString());
        }

        private void RepairButton(object sender, RoutedEventArgs e)
        {
            if (Diagnostic.IsRunning)
            {
                new RepairWindow().Show();
            }
            else if (Diagnostic.PCIsLaunch)
            {
                EventPanel.AddMessageEvent("Ремонт включенного компьютера невозможен", EventType.Warning);
            }
            else{
                EventPanel.AddMessageEvent("Продиагностируйте неисправность");
            }
        }
        private void InitializeInstrumentPanel()
        {
            Thickness margin = new(25, 0, 25, 0);
            InstrumentsPanel.Children.Clear();
            foreach (var instr in DiagnosticHandbook.InstrumentsDictionary.Values)
            {
                RadioButton btn = new()
                {
                    Name = instr,
                    Content = DiagnosticHandbook.RusInstrumentsNames[instr],
                    Margin = margin,
                    VerticalAlignment = VerticalAlignment.Center,
                    GroupName = "Instruments"
                };
                InstrumentsPanel.Children.Add(btn);
            }
            ((RadioButton)InstrumentsPanel.Children[0]).IsChecked = true;
        }

    }
}
