using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class ToIntegerTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public string OperationName => "tointeger";

        /// <inheritdoc />
        public JToken Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            switch(objectToApplyTo.Type)
            {
                case JTokenType.String:
                    var stringValue =  objectToApplyTo.Value<string>();
                    if (int.TryParse(stringValue, out int convertedString))
                        return JValue.FromObject(convertedString);
                    else
                        return JValue.CreateNull();
                case JTokenType.Date:
                    return JValue.FromObject(objectToApplyTo.Value<DateTime>().Ticks);
                case JTokenType.Integer:
                    return JValue.FromObject(objectToApplyTo.Value<int>());
                case JTokenType.Float:
                    return JValue.FromObject((int)objectToApplyTo.Value<float>());
                case JTokenType.Boolean:
                    var boolValue = objectToApplyTo.Value<bool>();
                    return JValue.FromObject(boolValue ? 1 : 0);
                default:
                    return JValue.CreateNull();
            }
        }
    }
}
