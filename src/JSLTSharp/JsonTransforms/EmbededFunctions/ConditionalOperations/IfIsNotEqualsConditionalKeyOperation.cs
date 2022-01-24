using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ConditionalOperations
{
    public class IfIsNotEqualsConditionalKeyOperation : IfIsEqualsConditionalKeyOperation
    {
        /// <inheritdoc/>
        public override string OperationName => "ifisnotequals";

        /// <inheritdoc/>
        public override bool Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            return !IsEquals(dataSource, parameters);
        }
    }
}
