using DataTypes;
using Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GraphNode
    {
        public Type NodeType { get; }

        public List<InputPin> NodeInputs { get; set; }
        public List<OutputPin> NodeOutputs { get; set; }

        public GraphNode(Type nodeType)
        {
            NodeInputs = new List<InputPin>();
            NodeOutputs = new List<OutputPin>();

            NodeType = nodeType;

            NodeAttributes nodeAttributes = (NodeAttributes)Attribute.GetCustomAttribute(nodeType, typeof(NodeAttributes));

            // Get input pins
            for (int i = 0; i < nodeAttributes.Inputs.Count; i++)
            {
                InputPin input = new InputPin(nodeAttributes.Inputs[i], this);
                NodeInputs.Add(input);
            }

            // Get output pins
            for (int i = 0; i < nodeAttributes.Outputs.Count; i++)
            {
                OutputPin output = new OutputPin(nodeAttributes.Outputs[i], this);
                NodeOutputs.Add(output);
            }
        }

        public void CalculateOutput()
        {
            VisualNodeBase classInstance = (VisualNodeBase)Activator.CreateInstance(NodeType);

            IList<IDataTypeContainer> nodeInputList = new List<IDataTypeContainer>();

            for (int i = 0; i < NodeInputs.Count; i++)
            {
                IDataTypeContainer inputValue = (IDataTypeContainer)Activator.CreateInstance(NodeInputs[i].DataType);
                inputValue.SetValue(NodeInputs[i].GetValue.GetDataAsString());

                nodeInputList.Add(inputValue);
            }

            List<IDataTypeContainer> result = (List<IDataTypeContainer>)classInstance.NodeFunction(nodeInputList);
            
            for (int i = 0; i < result.Count; i++)
            {
                NodeOutputs[i].OutputValue = result[i];
            }
        }
    }
}
