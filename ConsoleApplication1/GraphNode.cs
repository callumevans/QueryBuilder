using Common;
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
        /// <summary>
        /// The class of the node being implemented
        /// </summary>
        public Type NodeType { get; }

        /// <summary>
        /// Node input pins
        /// </summary>
        public List<InputPin> NodeInputs { get; set; }

        /// <summary>
        /// Node output pins
        /// </summary>
        public List<OutputPin> NodeOutputs { get; set; }

        /// <summary>
        /// Parent graph manager for the node
        /// </summary>
        public NodeGraphManager GraphManager { get; }

        /// <summary>
        /// Represents a single node on a graph of interconnected nodes
        /// </summary>
        /// <param name="nodeType">The class of the node being implemented</param>
        /// <param name="manager">The parent manager of the node</param>
        public GraphNode(Type nodeType, NodeGraphManager manager)
        {
            GraphManager = manager;

            NodeInputs = new List<InputPin>();
            NodeOutputs = new List<OutputPin>();

            NodeType = nodeType;

            NodeAttributes nodeAttributes = (NodeAttributes)Attribute.GetCustomAttribute(nodeType, typeof(NodeAttributes));

            // Set input pins
            for (int i = 0; i < nodeAttributes.Inputs.Count; i++)
            {
                InputPin input = new InputPin(nodeAttributes.Inputs[i], this);
                NodeInputs.Add(input);
            }

            // Set output pins
            for (int i = 0; i < nodeAttributes.Outputs.Count; i++)
            {
                OutputPin output = new OutputPin(nodeAttributes.Outputs[i], this);
                NodeOutputs.Add(output);
            }
        }

        /// <summary>
        /// Calculate and realise outputs for the node
        /// </summary>
        public void CalculateOutput()
        {
            // Get input values
            IList<IDataTypeContainer> nodeInputList = new List<IDataTypeContainer>();

            for (int i = 0; i < NodeInputs.Count; i++)
            {
                IDataTypeContainer inputValue = (IDataTypeContainer)Activator.CreateInstance(NodeInputs[i].DataType);
                inputValue.SetValue(NodeInputs[i].GetValue.GetDataAsString());

                nodeInputList.Add(inputValue);
            }

            // Process inputs and realise the outputs
            List<IDataTypeContainer> result;
            NodeBase node;

            // If node is executable we need to make sure we pass the query state to it
            if (NodeType.IsSubclassOf(typeof(ExecutableNode)))
            {
                node = (ExecutableNode)Activator.CreateInstance(NodeType, new object[] { GraphManager.QueryState });
            }
            else
            {
                node = (NodeBase)Activator.CreateInstance(NodeType);
            }

            // Calculate and set results
            result = (List<IDataTypeContainer>)node.NodeFunction(nodeInputList);

            for (int i = 0; i < result.Count; i++)
            {
                NodeOutputs[i].OutputValue = result[i];
                NodeOutputs[i].OutputRealised = true;
            }
        }
    }
}
