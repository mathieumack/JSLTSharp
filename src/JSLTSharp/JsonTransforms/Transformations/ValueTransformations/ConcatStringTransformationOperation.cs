using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSLTSharp.JsonTransforms.Transformations.ValueTransformations
{
    public class ConcatStringTransformationOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public virtual string OperationName => "ConcatString";

        /// <inheritdoc />
        public virtual JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            var result = "";

            if (token.Type != JTokenType.Null && token.Type != JTokenType.None)
                result += token.ToString();

            foreach (string parameter in parameters) 
            {
                if (parameter.StartsWith("$"))
                {
                    try
                    {
                        var foundToken = dataSource.SelectToken(parameter);

                        if (foundToken.Type != JTokenType.Null && foundToken.Type != JTokenType.None)
                            result += foundToken.ToString();
                    }
                    catch
                    { 
                        //Nothing to do, token does not exist
                    }
                } 
                else
                {
                    result += parameter;
                }
            }

            return result;
        }
    }
}
