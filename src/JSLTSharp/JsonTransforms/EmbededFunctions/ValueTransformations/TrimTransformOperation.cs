using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class TrimTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public virtual string OperationName => "Trim";

        /// <inheritdoc />
        public virtual JToken Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            if (objectToApplyTo.Type != JTokenType.String)
                return objectToApplyTo;

            return objectToApplyTo.ToString().Trim();
        }
    }
}
