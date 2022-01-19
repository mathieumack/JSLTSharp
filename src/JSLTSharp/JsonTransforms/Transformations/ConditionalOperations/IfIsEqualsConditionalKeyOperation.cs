using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSLTSharp.JsonTransforms.Transformations
{
    public class IfIsEqualsConditionalKeyOperation : IJsonTransformConditionalCustomOperation
    {
        /// <inheritdoc/>
        public virtual string OperationName => "ifisequals";

        /// <inheritdoc/>
        public virtual bool Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            return IsEquals(dataSource, parameters);
        }

        protected bool IsEquals(JToken dataSource, IList<string> parameters)
        {
            var isequal = false;
            if (parameters[0].StartsWith("$"))
            {
                if (parameters.Count != 3)
                    throw new InvalidOperationException($"You must provide only one parameter for function {OperationName}");

                // Token search
                var existsTokenSearch = dataSource.SelectToken(parameters[0]);

                if (existsTokenSearch.Type == JTokenType.Object || existsTokenSearch.Type == JTokenType.Array)
                    return false;

                switch (parameters[2].ToLower())
                {
                    case "boolean":
                        isequal = existsTokenSearch.Type == JTokenType.Boolean &&
                            (existsTokenSearch as JValue).Value<bool>().ToString().Equals(parameters[1], StringComparison.InvariantCultureIgnoreCase);
                        break;
                    case "string":
                        isequal = existsTokenSearch.Type == JTokenType.String &&
                            (existsTokenSearch as JValue).Value<string>().Equals(parameters[1], StringComparison.InvariantCultureIgnoreCase);
                        break;
                    case "null":
                        isequal = existsTokenSearch.Type == JTokenType.Null &&
                            "null".Equals(parameters[1], StringComparison.InvariantCultureIgnoreCase);
                        break;
                }
            }
            else
            {
                if (parameters.Count != 2)
                    throw new InvalidOperationException($"You must provide only one parameter for function {OperationName}");

                // String value :
                isequal = parameters[0].Equals(parameters[1], StringComparison.InvariantCultureIgnoreCase);
            }

            return isequal;
        }
    }
}
