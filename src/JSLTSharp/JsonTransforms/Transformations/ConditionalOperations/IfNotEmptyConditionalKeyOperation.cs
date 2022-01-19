using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSLTSharp.JsonTransforms.Transformations
{
    public class IfNotEmptyConditionalKeyOperation : IJsonTransformConditionalCustomOperation
    {
        public string OperationName => "ifnotempty";

        public bool Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            if (parameters.Count != 1)
                throw new InvalidOperationException($"You must provide only one parameter for function {OperationName}");

            return Exists(dataSource, objectToApplyTo, parameters);
        }

        protected bool Exists(JToken dataSource, JToken token, IList<string> parameters)
        {
            if (parameters[0].StartsWith("$"))
            {
                var existsTokenSearch = dataSource.SelectToken(parameters[0]);
                if (existsTokenSearch is null)
                {
                    return false;
                }
                if (existsTokenSearch.Type == JTokenType.Object || existsTokenSearch.Type == JTokenType.Array)
                    return existsTokenSearch.Children().Any();
                else
                    return existsTokenSearch.Type != JTokenType.Null;
            }
            else
                return !string.IsNullOrWhiteSpace(parameters[0]);
        }
    }
}
