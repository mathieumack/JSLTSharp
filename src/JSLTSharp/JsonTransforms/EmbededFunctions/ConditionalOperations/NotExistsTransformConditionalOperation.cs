using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.EmbededFunctions
{
    public class NotExistsTransformConditionalOperation : ExistsConditionalKeyOperation
    {
        /// <inheritdoc />
        public override string OperationName => "notexists";

        /// <inheritdoc />
        public override bool Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            if (parameters.Count != 1)
                throw new InvalidOperationException($"You must provide one select as parameter for function {OperationName}");

            return !Exists(dataSource, parameters[0]);
        }
    }
}