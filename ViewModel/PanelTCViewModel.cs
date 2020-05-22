using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MiniTC.ViewModel.BaseClass;
using MiniTC.Model;
using MiniTC.ViewModel;

namespace MiniTC.ViewModel
{
    class PanelTCViewModel : ViewModelBase
    {
        public PanelTCViewModel()
        {
            GetDrives(_drives = new List<Model.Drive>()) ;
        }

        public ObservableCollection<Model.File> _files;

        public ObservableCollection<Model.File> Files
        {
            get { return _files; }
            set { _files = value; onPropertyChanged(""); }
        }
       
        private List<Model.Drive> _drives;

        public List<Model.Drive> Drives
        {
            get { return _drives; }
            set { _drives = value; onPropertyChanged(nameof(_drives)); }
        }

        public Model.Drive selectedDrive { get; set; }

        public Model.File File { get; set; }

        private string _path;

        public string path
        {
            get { return _path; }
            set { _path = value; onPropertyChanged(nameof(Path)); }
        }

        public void GetDrives(List<Model.Drive> drivesList)
        {
            var drives = Directory.GetLogicalDrives();

            foreach (var item in drives)
            {
                drivesList.Add(new Model.Drive{driveName = item});
            }
        }

        public void Reload(string pathName)
        { 
            Files = new ObservableCollection<Model.File>();
            path = pathName;

            if (path.Length > 3)
               Files.Add(new Model.File(Directory.GetParent(path).FullName, ".."));
            
            string[] directories = Directory.GetDirectories(pathName);
            string[] files = Directory.GetFiles(pathName);

            foreach (var item in directories)
                Files.Add(new Model.File(item, "<D>" + item.Substring(item.LastIndexOf('\\') + 1)));
            
            foreach (var item in files)
                Files.Add(new Model.File(item, item.Substring(item.LastIndexOf('\\') + 1)));
        }

        private ICommand _updateList = null;

        public ICommand UpdateList
        {
            get
            {
                if (_updateList == null)
                {
                    _updateList = new RelayCommand(
                    arg => { Reload(File.file); },
                    arg => (File != null && (File.fileName.Contains("<D>") || File.fileName == "..")));
                }
                return _updateList;
            }
        }

        private ICommand _updateCB = null;

        public ICommand UpdateCB
        {
            get
            {
                if (_updateCB == null)
                {
                    _updateCB = new RelayCommand(
                    arg => { Reload(selectedDrive.driveName); },
                    arg => (selectedDrive != null));
                }
                return _updateCB;
            }
        }
    }
}
