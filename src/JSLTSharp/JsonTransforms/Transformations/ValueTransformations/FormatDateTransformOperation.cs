using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.Transformations
{
    public class FormatDateTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public string OperationName => "formatDate";

        /// <inheritdoc />
        public JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            if (token.Type != JTokenType.Date)
                throw new InvalidOperationException($"Value must not be a date");
            if (parameters.Count != 1)
                throw new InvalidOperationException($"You must provide only one parameter for function {OperationName}");

            var date = token.Value<DateTime>();
            return JToken.FromObject(date.ToString(parameters[0]));
        }
    }
}