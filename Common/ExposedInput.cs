using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExposedInput : Attribute
    {
        public LabelDisplay LabelDisplay { get; private set; }
        public string Label { get; private set; }

        public ExposedInput()
        {
            this.LabelDisplay = LabelDisplay.Hidden;
            this.Label = "";
        }

        public ExposedInput(LabelDisplay labelDisplay, string label = "")
        {
            if (labelDisplay == LabelDisplay.Field)
            {
                this.LabelDisplay = LabelDisplay.Field;
            }
            else if (labelDisplay == LabelDisplay.Custom)
            {
                this.LabelDisplay = LabelDisplay.Custom;
                this.Label = label;
            }
            else
            {
                this.LabelDisplay = LabelDisplay.Hidden;
                this.Label = "";
            }
        }
    }
}
