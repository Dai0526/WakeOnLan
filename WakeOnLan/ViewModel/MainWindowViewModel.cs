using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        // Bind Records
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
    }
}
