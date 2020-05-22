using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    class File
    {
        private string _fileName;
        public string fileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private string _file;
        public string file
        {
            get { return _file; }
            set { _file = value; }
        }
        public File(string file, string fileName)
        {
            this.file = file;
            this.fileName = fileName;
        }
    }
}
