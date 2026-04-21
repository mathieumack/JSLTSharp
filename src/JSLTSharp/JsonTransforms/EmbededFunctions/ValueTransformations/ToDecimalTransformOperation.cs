using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class ToDecimalTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public string OperationName => "ToDecimal";

        /// <inheritdoc />
        public JToken Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            switch (objectToApplyTo.Type)
            {
                case JTokenType.String:
                    var stringValue = objectToApplyTo.Value<string>();
                    if (decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal convertedString))
                        return JValue.FromObject(convertedString);
                    else
                        return JValue.CreateNull();
                case JTokenType.Integer:
                    return JValue.FromObject((decimal)objectToApplyTo.Value<long>());
                case JTokenType.Float:
                    return JValue.FromObject((decimal)objectToApplyTo.Value<double>());
                case JTokenType.Boolean:
                    var boolValue = objectToApplyTo.Value<bool>();
                    return JValue.FromObject(boolValue ? 1m : 0m);
                default:
                    return JValue.CreateNull();
            }
        }
    }
}
