using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.Transformations
{
    public class NotExistsTransformConditionalOperation : ExistsConditionalKeyOperation
    {
        /// <inheritdoc />
        public override string OperationName => "notexists";

        /// <inheritdoc />
        public override bool Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            return !Exists(dataSource, token, parameters);
        }
    }
}