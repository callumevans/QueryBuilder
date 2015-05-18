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

        public List<GraphNodePin> NodeInputs { get; set; }
        public List<GraphNodePin> NodeOutputs { get; set; }

        public GraphNode(Type nodeType)
        {
            NodeInputs = new List<GraphNodePin>();
            NodeOutputs = new List<GraphNodePin>();

            NodeType = nodeType;

            NodeAttributes nodeAttributes = (NodeAttributes)Attribute.GetCustomAttribute(nodeType, typeof(NodeAttributes));

            // Get input pins
            for (int i = 0; i < nodeAttributes.Inputs.Count; i++)
            {
                GraphNodePin input = new GraphNodePin(nodeAttributes.Inputs[i], this, PinType.INPUT);
                NodeInputs.Add(input);
            }

            // Get output pins
            for (int i = 0; i < nodeAttributes.Outputs.Count; i++)
            {
                GraphNodePin output = new GraphNodePin(nodeAttributes.Outputs[i], this, PinType.OUTPUT);
                NodeOutputs.Add(output);
            }

            // Create connection for each output
            foreach (GraphNodePin output in NodeOutputs)
            {
                output.Connection = new GraphNodeConnection(output);
            }
        }

        public void CalculateOutput()
        {
            VisualNodeBase classInstance = (VisualNodeBase)Activator.CreateInstance(NodeType);

            IList<IDataTypeContainer> nodeInputList = new List<IDataTypeContainer>();

            for (int i = 0; i < NodeInputs.Count; i++)
            {
                IDataTypeContainer inputInstance = (IDataTypeContainer)Activator.CreateInstance(NodeInputs[i].Type);
                inputInstance.SetValue(NodeInputs[i].Connection.Value.GetDataAsString());

                nodeInputList.Add(inputInstance);
            }

            List<IDataTypeContainer> result = (List<IDataTypeContainer>)classInstance.NodeFunction(nodeInputList);
            
            for (int i = 0; i < result.Count; i++)
            {
                NodeOutputs[i].Connection.Value = result[i];
            }
        }
    }
}
