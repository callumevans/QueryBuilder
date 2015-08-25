using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.Basic_SQL
{
    [NodeName("WHERE")]
    [NodeCategory("Basic SQL")]
    [ExecutionOutDescription(0, "Out")]
    public class WHERE : ExecutableNode
    {
        [ExposedInput(LabelDisplay.Custom, "Field")]
        public DataTypes.String fields;

        [ExposedInput(LabelDisplay.Custom, "Operation")]
        public DataTypes.String operation;

        [ExposedInput(LabelDisplay.Custom, "Value")]
        public DataTypes.String value;

        public WHERE(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            return 0;
        }

        public override void NodeFunction()
        {
            List<Tuple<string, string, string>> whereFields = new List<Tuple<string, string, string>>();

            if (State.VariableBag.ContainsKey("WhereFields") == false)
            {
                whereFields.Add(new Tuple<string, string, string>(fields.value, operation.value, value.value));
                State.VariableBag.Add("WhereFields", whereFields);
            }
            else
            {
                object existingList;
                State.VariableBag.TryGetValue("WhereFields", out existingList);

                List<Tuple<string, string, string>> castList = (List<Tuple<string, string, string>>)existingList;
                castList.Add(new Tuple<string, string, string>(fields.value, operation.value, value.value));

                State.VariableBag["WhereFields"] = castList;
            }
        }
    }
}
