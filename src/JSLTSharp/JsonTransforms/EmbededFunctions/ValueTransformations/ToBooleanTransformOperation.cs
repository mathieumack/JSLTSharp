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
        public JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            var fieldValue = false;
            var defaultValue = false;

            if (token.Type == JTokenType.Boolean)
                return token;
            else if (bool.TryParse(token.ToString().ToLower().Trim(), out fieldValue))
                return JValue.FromObject(fieldValue);

            if (parameters.Count == 1 && bool.TryParse(parameters[0].ToLower().Trim(), out defaultValue))
                return JValue.FromObject(defaultValue);
                
            return JValue.CreateNull();
        }
    }
}
