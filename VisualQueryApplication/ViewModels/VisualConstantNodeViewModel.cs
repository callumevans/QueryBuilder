using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualQueryApplication.Model;

namespace VisualQueryApplication.ViewModels
{
    public class VisualConstantNodeViewModel : VisualGraphComponentViewModel
    {
        public PinModel OutputPin
        {
            get { return outputPin; }
            set
            {
                outputPin = value;
                OnPropertyChanged(nameof(OutputPin));
            }
        }

        private PinModel outputPin;

        public VisualConstantNodeViewModel(Type type)
        {
            OutputPin = new PinModel("Output", type);
        }

        public override void RemoveConnections()
        {
            throw new NotImplementedException();
        }
    }
}
