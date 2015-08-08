using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualQueryApplication.ViewModels
{
    public class GeneratedQueryViewViewModel : ViewModelBase
    {
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private string query = "";
    }
}
