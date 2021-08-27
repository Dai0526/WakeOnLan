using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WakeOnLan.Utility;
using WakeOnLan.ViewModel;

namespace WakeOnLan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly SolidColorBrush COLOR_OFFLINE = new SolidColorBrush(Colors.Red);
        private readonly SolidColorBrush COLOR_DEFAULT = new SolidColorBrush(Colors.White);
        private readonly SolidColorBrush COLOR_ONLINE = new SolidColorBrush(Colors.Green);

        static MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        static WakeOnLanCore m_wake = Singleton<WakeOnLanCore>.Instance;
        static Configuration m_config = Singleton<Configuration>.Instance;

        private string m_configPath = @".\WoL.xml";

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainWindowViewModel;
            LoadConfig(m_configPath);
        }

        private void LoadConfig(string path)
        {
            m_configPath = path;
            m_config.ReadConfigXml(m_configPath);
            mainWindowViewModel.PCInfoMap = m_config.m_pcInfo;
        }

        private void ConfigDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfigDataGrid.SelectedItem == null)
            {
                return;
            }

            string selectedId = ((ComputerInfo)ConfigDataGrid.SelectedItem).id;
            mainWindowViewModel.SetSelectedPC(selectedId);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}
