using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WakeOnLan.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            
        }

        private Configuration config;
         public Configuration WoLConfig
        {
            get { return config; }
            set
            {
                config = value;
                OnPropertyChanged(nameof(WoLConfig));
            }
        }


        // Bind Records - map is the main data control, it affects records
        private Dictionary<string, ComputerInfo> pcInfoMap;
        public Dictionary<string, ComputerInfo> PCInfoMap
        {
            get { return pcInfoMap; }
            set
            {
                pcInfoMap = value;
                OnPropertyChanged(nameof(PCInfoMap));
                SetRecords();
            }
        }

        private void SetRecords()
        {
            Records = new ObservableCollection<ComputerInfo>(PCInfoMap.Values);
        }

        // records is in charge of the GUI display
        private ObservableCollection<ComputerInfo> records;
        public ObservableCollection<ComputerInfo> Records
        {
            get { return records; }
            set
            {
                records = value;
                OnPropertyChanged(nameof(Records));
            }
        }

        // Bind Select PC info
        #region SELECTED PC GRID
        private ComputerInfo selectedPC = null;
        public ComputerInfo SelectedPCInfo
        {
            get { return selectedPC; }
            set
            {
                selectedPC = value;
                OnPropertyChanged(nameof(SelectedPCInfo));
            }
        }

        public void SetSelectedPC(string id)
        {
            ComputerInfo temp = null;
            PCInfoMap.TryGetValue(id, out temp);

            if(temp != null)
            {
                SelectedPCInfo = PCInfoMap[id];

                SelectedId = SelectedPCInfo.id;
                SelectedIP = SelectedPCInfo.GetIPString();
                SelectedMac = SelectedPCInfo.GetMACString();
                SelectedStatus = SelectedPCInfo.GetStatusString();
                SelectedlastChecked = SelectedPCInfo.GetLastCheckString();
                SelectedDescription = SelectedPCInfo.description;
            }else
            {
                SelectedPCInfo = null;

                SelectedId = "NA";
                SelectedIP = "NA";
                SelectedMac = "NA";
                SelectedStatus = "NA";
                SelectedlastChecked = PCStatus.UNKNOW.ToString();
                SelectedDescription = string.Empty;
            }
        }

        private string selectedIP = "NA";
        public string SelectedIP
        {
            get { return selectedIP; }
            set
            {
                selectedIP = value;
                OnPropertyChanged(nameof(SelectedIP));
            }
        }

        private string selectedMac = "NA";
        public string SelectedMac
        {
            get { return selectedMac; }
            set
            {
                selectedMac = value;
                OnPropertyChanged(nameof(SelectedMac));
            }
        }

        private string selectedId = "NA";
        public string SelectedId
        {
            get { return selectedId; }
            set
            {
                selectedId = value;
                OnPropertyChanged(nameof(SelectedId));
            }
        }

        private string selectedStatus = "NA";
        public string SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }
        private string selectedDescription = string.Empty;
        public string SelectedDescription
        {
            get { return selectedDescription; }
            set
            {
                selectedDescription = value;
                OnPropertyChanged(nameof(SelectedDescription));
            }
        }
        private string selectedlastChecked = "NA";
        public string SelectedlastChecked
        {
            get { return selectedlastChecked; }
            set
            {
                selectedlastChecked = value;
                OnPropertyChanged(nameof(SelectedlastChecked));
            }
        }
        #endregion

    }
}
