using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSLTSharp.JsonTransforms.EmbededFunctions
{
    public class ToBooleanTransformOperation : ExistsTransformOperation
    {
        /// <inheritdoc />
        public override string OperationName => "toboolean";

        /// <inheritdoc />
        public override JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            var fieldValue = false;
            var defaultValue = false;

            if (token.Type == JTokenType.Boolean)
                return token;

            if (token.Type == JTokenType.String && bool.TryParse(token.Value<string>().ToLower().Trim(), out fieldValue))
                return JValue.FromObject(fieldValue);
                
            if (parameters.Count == 1 && bool.TryParse(parameters[0].ToLower().Trim(), out defaultValue))
                return JValue.FromObject(defaultValue);
                
            return JValue.CreateNull();
        }
    }
}
