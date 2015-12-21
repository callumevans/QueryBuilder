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
