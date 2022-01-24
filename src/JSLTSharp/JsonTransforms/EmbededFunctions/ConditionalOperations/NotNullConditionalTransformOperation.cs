using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ConditionalOperations
{
    public class NotNullConditionalTransformOperation : IJsonTransformConditionalCustomOperation
    {
        /// <inheritdoc />
        public virtual string OperationName => "notnull";

        /// <inheritdoc />
        public virtual bool Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            if (parameters.Count != 1)
                throw new InvalidOperationException($"You must provide only one parameter for function {OperationName}");
            
            return Exists(dataSource, token, parameters);
        }

        protected bool Exists(JToken dataSource, JToken token, IList<string> parameters)
        {
            var existsTokenSearch = dataSource.SelectTokens(parameters[0]);
            return existsTokenSearch != null && existsTokenSearch.Any() && existsTokenSearch.First().Type != JTokenType.Null;
        }
    }
}