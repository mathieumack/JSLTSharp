using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations
{
    public class ConcatStringTransformationOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public virtual string OperationName => "ConcatString";

        /// <inheritdoc />
        public virtual JToken Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            var result = new StringBuilder();

            if (objectToApplyTo.Type != JTokenType.Null && objectToApplyTo.Type != JTokenType.None)
                result.Append(objectToApplyTo.ToString());

            foreach (string parameter in parameters) 
            {
                if (parameter.StartsWith("$"))
                {
                    try
                    {
                        var foundToken = dataSource.SelectToken(parameter);

                        if (foundToken.Type != JTokenType.Null && foundToken.Type != JTokenType.None)
                            result.Append(foundToken.ToString());
                    }
                    catch
                    { 
                        //Nothing to do, token does not exist
                    }
                } 
                else
                {
                    result.Append(parameter);
                }
            }

            return result.ToString();
        }
    }
}
