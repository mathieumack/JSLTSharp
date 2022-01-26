using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class ToBooleanTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public string OperationName => "toboolean";

        /// <inheritdoc />
        public JToken Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            if (objectToApplyTo.Type == JTokenType.Boolean)
                return objectToApplyTo;
            else if (bool.TryParse(objectToApplyTo.ToString().ToLower().Trim(), out bool fieldValue))
                return JValue.FromObject(fieldValue);

            if (parameters.Count == 1 && bool.TryParse(parameters[0].ToLower().Trim(), out bool defaultValue))
                return JValue.FromObject(defaultValue);
                
            return JValue.CreateNull();
        }
    }
}
