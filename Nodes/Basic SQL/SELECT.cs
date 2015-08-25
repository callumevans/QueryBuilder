using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.Basic_SQL
{
    [NodeName("SELECT")]
    [NodeCategory("Basic SQL")]
    [ExecutionOutDescription(0, "Out")]
    public class SELECT : ExecutableNode
    {
        [ExposedInput(LabelDisplay.Custom, "Fields")]
        public DataTypes.String fields;

        [ExposedInput(LabelDisplay.Custom, "From")]
        public DataTypes.String from;

        public SELECT(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            return 0;
        }

        public override void NodeFunction()
        {
            List<string> selectFields = new List<string>();
            string fieldsString = fields.value;

            string[] values = fieldsString.Split(',');
            selectFields.AddRange(values);

            if (State.VariableBag.ContainsKey("SelectFields") == false)
            {
                State.VariableBag.Add("SelectFields", selectFields);
            }
            else
            {
                object existingList;
                State.VariableBag.TryGetValue("SelectFields", out existingList);

                List<string> castList = (List<string>)existingList;
                castList.AddRange(selectFields);

                State.VariableBag["SelectFields"] = castList;
            }

            if (State.VariableBag.ContainsKey("FromTable") == false)
                State.VariableBag.Add("FromTable", from.value);
            else
                State.VariableBag["FromTable"] = from.value;
        }
    }
}
