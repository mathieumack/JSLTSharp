using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class FormatDateTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public string OperationName => "formatDate";

        /// <inheritdoc />
        public JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            if (parameters.Count != 1)
                throw new InvalidOperationException($"You must provide only one parameter for function {OperationName}");

            if (token.Type.Equals(JTokenType.Date))
            {
                var date = token.Value<DateTime>();
                return JToken.FromObject(date.ToString(parameters[0]));
            }
            else if(token.Type.Equals(JTokenType.String))
            {
                var date = token.Value<String>();
                if(DateTime.TryParse(date, out DateTime convertedDate))
                    return JToken.FromObject(convertedDate.ToString(parameters[0]));
            }
            
            throw new InvalidOperationException($"Value {token.ToString()} is not a valid date");
        }
    }
}