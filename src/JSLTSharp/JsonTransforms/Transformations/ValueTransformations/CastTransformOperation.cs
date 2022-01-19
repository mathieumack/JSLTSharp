using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.Transformations
{
    public class CastTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public string OperationName => "cast";

        /// <inheritdoc />
        public JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            if (token.Type == JTokenType.Object || token.Type == JTokenType.Array)
                throw new InvalidOperationException($"Value must not be a JOject or a JArray");
            if (parameters.Count != 1)
                throw new InvalidOperationException($"You must provide only one parameter for function {OperationName}");

            var value = (token as JValue).Value;
            object result = null;
            if (value != null)
                result = GenericTypes.Cast(value.ToString(), parameters[0]);
            else
                result = GenericTypes.Cast(null, parameters[0]);

            if (result is null)
                return JValue.CreateNull();
            else
                return JValue.FromObject(result);
        }
    }
}
