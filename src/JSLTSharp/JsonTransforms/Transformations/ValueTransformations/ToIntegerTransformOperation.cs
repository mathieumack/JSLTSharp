using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSLTSharp.JsonTransforms.Transformations
{
    public class ToIntegerTransformOperation : ExistsTransformOperation
    {
        /// <inheritdoc />
        public override string OperationName => "tointeger";

        /// <inheritdoc />
        public override JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            switch(token.Type)
            {
                case JTokenType.String:
                    var stringValue =  token.Value<string>();
                    if (int.TryParse(stringValue, out int convertedString))
                        return JValue.FromObject(convertedString);
                    else
                        return JValue.CreateNull();
                case JTokenType.Date:
                    return JValue.FromObject(token.Value<DateTime>().Ticks);
                case JTokenType.Integer:
                    return JValue.FromObject(token.Value<int>());
                case JTokenType.Float:
                    return JValue.FromObject((int)token.Value<float>());
                case JTokenType.Boolean:
                    var boolValue = token.Value<bool>();
                    return JValue.FromObject(boolValue ? 1 : 0);
                default:
                    return JValue.CreateNull();
            }
        }
    }
}
