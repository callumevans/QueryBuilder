using Common;
using DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Graph
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
        /// Returns the given execution path for executable nodes
        /// </summary>
        public int ExecutionPath { get; private set; } = -1;

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

            // Extract information about the node
            FieldInfo[] fields = NodeType.GetFields();

            // Set input pins
            foreach (FieldInfo field in fields)
            {
                Attribute[] attributes = field.GetCustomAttributes().ToArray();

                foreach (Attribute attribute in attributes)
                {
                    if (attribute.GetType() == typeof(ExposedInput))
                    {
                        InputPin input = new InputPin(field.FieldType, this);
                        NodeInputs.Add(input);
                    }
                }
            }

            // Set output pins
            foreach (FieldInfo field in fields)
            {
                Attribute[] attributes = field.GetCustomAttributes().ToArray();

                foreach (Attribute attribute in attributes)
                {
                    if (attribute.GetType() == typeof(ExposedOutput))
                    {
                        OutputPin output = new OutputPin(field.FieldType, this);
                        NodeOutputs.Add(output);
                    }
                }
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

            // Forward input values to the instantiated node
            for (int i = 0; i < NodeInputs.Count; i++)
            {
                int fieldCounter = 0;

                foreach (FieldInfo field in node.GetType().GetFields())
                {
                    foreach (Attribute attribute in field.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(ExposedInput))
                        {
                            if (fieldCounter++ == i)
                                field.SetValue(node, NodeInputs[i].GetValue);
                        }
                    }
                }
            }

            // Ready to calculate the output
            node.NodeFunction();

            result = new List<IDataTypeContainer>();

            // Extract the newly calculated output from the node
            for (int i = 0; i < NodeOutputs.Count; i++)
            {
                int fieldCounter = 0;

                foreach (FieldInfo field in node.GetType().GetFields())
                {
                    foreach (Attribute attribute in field.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(ExposedOutput))
                        {
                            if (fieldCounter++ == i)
                                result.Add(field.GetValue(node) as IDataTypeContainer);
                        }
                    }
                }
            }

            // And set the outputs in the graph model
            for (int i = 0; i < result.Count; i++)
            {
                NodeOutputs[i].OutputValue = result[i];
                NodeOutputs[i].OutputRealised = true;
            }

            if (node is ExecutableNode)
            {
                ExecutionPath = (node as ExecutableNode).GetExecutionPath();
            }
        }
    }
}
