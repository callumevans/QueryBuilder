# QueryBuilder
QueryBuilder is an extensible, visual graph construction system written for WPF.
QueryBuilder was built as a part of a university project, and attempts to provide the foundation for developers
that would like to integrate a visual graph editor into their own projects.

<img src="https://cloud.githubusercontent.com/assets/2576186/11944230/778a8e94-a83c-11e5-8eb3-fcc85dbf83d0.PNG" style="width: 30%; height: 30%" />

##Installation

Setting up QueryBuilder should (hopefully) be quite straightforward. Simply clone the repository into Visual Studio and then build it.
'VisualQueryApplication' is the front-end editor, and demonstrates how developers might go about implementing the system in
one of their applications.

<img src="https://cloud.githubusercontent.com/assets/2576186/11944396/f7c7a0a0-a83d-11e5-957a-1e710ba6e05c.png" style="width: 30%; height: 30%" />

The 'NodePlugins' folder can be found in the same directory as VisualQueryApplication.exe (try running the executable if it isn't there).

To add nodes to the system, libraries need to be placed into the NodePlugins folder. The 'Nodes' project distributed with this repository is a library of sample nodes to help get you started. To makes these nodes available in the visual editor, find the compiled 'Nodes.dll' library in the Nodes project binaries folder and add it to the NodePlugins folder in the VisualEditor project.

<img src="https://cloud.githubusercontent.com/assets/2576186/11944750/3f21bc22-a840-11e5-8eff-199b928dfdaf.PNG" style="width: 20%; height: 20%" />

## Building a Graph

### Adding Nodes and Creating Connections

To add a node to the graph, double click on an entry from the list on the left (this will only populate when you've added some libraries 
to NodePlugins). Pins are the coloured circles that come with the nodes, and they represent either a data connection or a control-flow connection.
Currently, all input data pins must be connected for the graph to compile.

You can create a connection from an output pin (those on the right) to an input pin (those on the left). Do this by clicking on the 
output pin you want to take data from, and then clicking on the input pin you want to provide that data to.

<img src="https://cloud.githubusercontent.com/assets/2576186/11944998/b74e834a-a842-11e5-9699-be8e946df580.PNG" />

You can input values directly by clicking from an input pin and onto the grey canvas area. A small box with only an output pin should
automatically be inserted. These boxes have text fields and will convert their values into a data connection.

<img src="https://cloud.githubusercontent.com/assets/2576186/11945028/1b10f5d4-a843-11e5-9ab7-0b672114be20.PNG" />

If we build the graph now, nothing will happen. We need a print node to extract data from our graph. Print nodes are executable 
(impure) nodes. These are nodes that can affect the control-flow and internal state of our graph. The Branch node is a good example of this, but the Print node just takes an input value and prints it to the output log.

<img src="https://cloud.githubusercontent.com/assets/2576186/11945361/fedb3682-a846-11e5-9c6d-a422539bf4d5.PNG" />

When you have a complete graph, press Build, and then 'View Query' to see the output!

### Data Types

There are three data types defined in the system:
 - Numeric (green)
 - String (pink)
 - Boolean (red)
 
These are defined in the DataTypes project. These can be changed without too much hassle.

## Custom Nodes

Custom nodes can be added by extending the NodeBase and ExecutableNode classes. NodeBase defines a pure node (one with no execution pins) and ExecutableNode defines an impure node. Input and Output pins are denoted by the ExposedInput and ExposedOutput attributes respectively. NodeFunction is the function executed when the graph activates the node.

```C#
    [NodeName("Add")]
    [NodeCategory("Maths")]
    public class Add : NodeBase
    {
        [ExposedInput]
        public DataTypes.Numeric A;

        [ExposedInput]
        public DataTypes.Numeric B;

        [ExposedOutput]
        public DataTypes.Numeric output;

        public override void NodeFunction()
        {
            output = new DataTypes.Numeric(A.value + B.value);
        }
    }
```

Executable nodes are defined in a similar way. Their execution paths are declared with ExecutionOutDescription. GetExecutionPath tells the graph which output path to follow. A QueryState object is passed into the constructor--this represents the 'state' of the entire graph and you can add any variables you like to it. This can be used for communicating between nodes and building more complex functionality.

```C#
    [NodeName("Branch")]
    [NodeCategory("Flow")]
    [ExecutionOutDescription(0, "True")]
    [ExecutionOutDescription(1, "False")]
    public class Branch : ExecutableNode
    {
        [ExposedInput(LabelDisplay.Hidden)]
        public DataTypes.Boolean condition;

        public Branch(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            if (condition.value)
                return 0;
            else
                return 1;
        }

        public override void NodeFunction()
        {
        }
    }
```
