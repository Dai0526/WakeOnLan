
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            // save current changes then load new item
            mainWindowViewModel.UpdatePCInfoMap();
            string selectedId = ((ComputerInfo)ConfigDataGrid.SelectedItem).id;
            mainWindowViewModel.SetSelectedPC(selectedId);

            UpdateGUI();
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

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => CheckSinglePC(mainWindowViewModel.SelectedPCInfo));
        }

        private void WakeAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var pc in mainWindowViewModel.PCInfoMap)
            {
                Task.Run(() => CheckSinglePC(pc.Value));
            }
        }

        private void WakeButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => WakeSinglePC(mainWindowViewModel.SelectedPCInfo));
        }

        private void WakeSinglePC(ComputerInfo info)
        {
            m_wake.Wake(info);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SavePCInfo();
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


        private void MenuItem_Load(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Title = "Please select Configuration XML file.";
            ofd.InitialDirectory = @Directory.GetCurrentDirectory();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            m_configPath = ofd.FileName;
            LoadConfig(m_configPath);
            UpdateGUI();
        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            SavePCInfo();
        }

        private void SavePCInfo()
        {
            try
            {
                mainWindowViewModel.UpdatePCInfoMap();
                m_config.m_pcInfo = mainWindowViewModel.PCInfoMap;
                m_config.WriteConfigXml(m_configPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to Save xml File " + ex.Data, "Error", MessageBoxButton.OK);
            }

            MessageBox.Show("Success!", "Success", MessageBoxButton.OK);
        }

        private void UpdateGUI()
        {
            mainWindowViewModel.UpdateSelectPCDisplay();
            mainWindowViewModel.PCInfoMap = m_config.m_pcInfo;
        }

    }
}
