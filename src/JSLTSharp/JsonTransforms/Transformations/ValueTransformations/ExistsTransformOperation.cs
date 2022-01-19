using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JSLTSharp.JsonTransforms.Transformations
{
    public class ExistsTransformOperation : IJsonTransformCustomOperation
    {
        /// <inheritdoc />
        public virtual string OperationName => "exists";

        /// <inheritdoc />
        public virtual JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            if (parameters.Count != 1)
                throw new InvalidOperationException($"You must provide only one parameter for function {OperationName}");
            
            return JValue.FromObject(Exists(dataSource, token, parameters));
        }

        protected bool Exists(JToken dataSource, JToken token, IList<string> parameters)
        {
            var existsTokenSearch = dataSource.SelectTokens(parameters[0]);
            return existsTokenSearch != null && existsTokenSearch.Any();
        }
    }
}