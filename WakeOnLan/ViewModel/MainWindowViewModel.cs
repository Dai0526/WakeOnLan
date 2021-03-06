using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public void UpdatePCInfoMap()
        {
            // make a snapshot
            ComputerInfo[] copy = new ComputerInfo[records.Count];
            records.CopyTo(copy, 0);

            Dictionary<string, ComputerInfo> temp = new Dictionary<string, ComputerInfo>();

            pcInfoMap = copy.Select(x=>new KeyValuePair<string, ComputerInfo>(x.id, x)).ToDictionary(y => y.Key, y => y.Value);
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
                UpdateSelectPCDisplay();
            }
        }

        public void SetSelectedPC(string id)
        {
            ComputerInfo temp = null;
            PCInfoMap.TryGetValue(id, out temp);

            if(temp != null)
            {
                SelectedPCInfo = PCInfoMap[id];
            }else
            {
                SelectedPCInfo = null;
            }
        }

        public void UpdateSelectPCDisplay()
        {
            if(SelectedPCInfo != null)
            {
                SelectedId = SelectedPCInfo.id;
                SelectedIP = SelectedPCInfo.GetIPString();
                SelectedMac = SelectedPCInfo.GetMACString();
                SelectedStatus = SelectedPCInfo.GetStatusString();
                SelectedlastChecked = SelectedPCInfo.GetLastCheckString();
                SelectedDescription = SelectedPCInfo.description;
            }else
            {
                SelectedId = "NA";
                SelectedIP = "NA";
                SelectedMac = "NA";
                SelectedStatus = PCStatus.UNKNOW.ToString();
                SelectedlastChecked = "NA";
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
                SelectedPCInfo.IP = SelectedPCInfo.ParseIp(value);
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
                SelectedPCInfo.macAddress = SelectedPCInfo.ParseMacAddress(value);
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
                SelectedPCInfo.id = value;
            }
        }

        private string selectedStatus = "UNKNOW";
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
                SelectedPCInfo.description = value;
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
