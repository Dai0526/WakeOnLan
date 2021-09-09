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

        private void savePCInfo()
        {
            ComputerInfo info = mainWindowViewModel.SelectedPCInfo;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(ConfigDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Warning: Please Select an Item to Delete", "Warning", MessageBoxButton.OK);
                return;
            }

            MessageBoxResult res = MessageBox.Show("Delete action cannot be reverted, continue delete? ", "Warning", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                string targetId = ((ComputerInfo)ConfigDataGrid.SelectedItem).id;
                bool isRemoved = mainWindowViewModel.PCInfoMap.Remove(targetId);

                if(string.Compare(targetId, mainWindowViewModel.SelectedPCInfo.id) == 0){
                    mainWindowViewModel.SelectedPCInfo = null;
                }

                UpdateGUI();
            }

        }

        private void CheckAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var pc in mainWindowViewModel.PCInfoMap)
            {
                Task.Run(() => CheckSinglePC(pc.Value));
            }
        }

        private void WakeAllButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WakeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => CheckSinglePC(mainWindowViewModel.SelectedPCInfo));
        }

        private void CheckSinglePC(ComputerInfo info) 
        {
            bool isOnline = m_wake.PingTarget(info);
            if (isOnline)
            {
                info.status = PCStatus.ONLINE;
            }
            else
            {
                info.status = PCStatus.OFFLINE;
            }

            info.UpdateLastCheck();
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            mainWindowViewModel.UpdateSelectPCDisplay();
            mainWindowViewModel.PCInfoMap = m_config.m_pcInfo;
        }
    }
}
