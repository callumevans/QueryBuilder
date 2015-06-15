using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualQueryApplication.Model
{
    public class MainWindowModel
    {
        public List<Type> LoadedNodes { get; set; }

        public MainWindowModel()
        {
            LoadedNodes = new List<Type>();
        }
    }
}
