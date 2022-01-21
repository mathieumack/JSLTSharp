using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class DistinctArrayTransformOperation : ExistsTransformOperation
    {
        /// <inheritdoc />
        public override string OperationName => "distinct";

        /// <inheritdoc />
        public override JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            if (token is null || token.Type == JTokenType.Null)
                return token;
            else if(token.Type != JTokenType.Array)
                throw new InvalidOperationException($"Value must not be an array");

            var values = token as JArray;

            return new JArray(values.Select(e => e.Value<object>()).Distinct());
        }
    }
}
