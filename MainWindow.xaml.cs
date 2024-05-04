using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Motherboard_Diagnostic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Diagnosic diagnostic = new();
        StackPanel messagePanel;
        public MainWindow()
        {
            InitializeComponent();
            messagePanel = ObjectsManager.FindChild<StackPanel>(this, "MessagePanel");
            startDiagnosic();
        }
        public void startDiagnosic()
        {
            this.diagnostic.generateFaults(Config.faultsQuantity);
        }

        private void LaunchPCButton(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)e.Source;
            switch (bt.Content)
            {
                case "Запустить ПК":
                    bt.Background = Brushes.IndianRed;
                    bt.Content = "Выключить";
                    diagnostic.IsRunning = true;
                    break;

                case "Выключить":
                    bt.Background = Brushes.LightGreen;
                    bt.Content = "Запустить ПК";
                    diagnostic.IsRunning = false;
                    break;
            }
        }
    }
}
