using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class ToUpperTransformationOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public virtual string OperationName => "ToUpper";

        /// <inheritdoc />
        public virtual JToken Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            if (objectToApplyTo.Type != JTokenType.String)
                return objectToApplyTo;

            return objectToApplyTo.ToString().ToUpperInvariant();
        }
    }
}
