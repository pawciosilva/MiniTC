using MiniTC.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LeftPanel = new PanelTCViewModel();
            RightPanel = new PanelTCViewModel();
        }

        public PanelTCViewModel LeftPanel { get; }
        public PanelTCViewModel RightPanel { get; }

        private ICommand _kopiuj = null;

        public ICommand Kopiuj
        {
            get
            {
                if (_kopiuj == null)
                {
                    _kopiuj = new RelayCommand(
                      arg => { Copy(); },
                      arg => (LeftPanel.path != null && RightPanel.path != null));
                }
                return _kopiuj;
            }
        }

        private void Copy()
        {
            try
            {
                string file = LeftPanel.File.file;
                string filePath = Path.GetFileName(file);
                string destination = RightPanel.path + '\\' + filePath;
                LeftPanel.File = null;

                File.Copy(file, destination, true);
            }
            catch { }

            LeftPanel.Reload(LeftPanel.path);
            RightPanel.Reload(RightPanel.path);
        }
    }
}
