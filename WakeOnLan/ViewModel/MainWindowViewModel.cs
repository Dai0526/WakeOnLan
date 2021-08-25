using System;
using System.Collections.Generic;
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


        // Bind Record
        private Dictionary<string, ComputerInfo> records;
        public Dictionary<string, ComputerInfo> Records
        {
            get { return records; }
            set
            {
                records = value;
                OnPropertyChanged(nameof(Records));
            }
        }
    }
}
