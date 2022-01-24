using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class DistinctArrayTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public string OperationName => "distinct";

        /// <inheritdoc />
        public JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
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
