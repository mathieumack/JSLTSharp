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
        public JToken Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            if (objectToApplyTo is null || objectToApplyTo.Type == JTokenType.Null)
                return objectToApplyTo;
            else if(objectToApplyTo.Type != JTokenType.Array)
                throw new InvalidOperationException($"Value must not be an array");

            var values = objectToApplyTo as JArray;

            return new JArray(values.Select(e => e.Value<object>()).Distinct());
        }
    }
}
