using JSLTSharp.JsonTransforms.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JSLTSharp.JsonTransforms.EmbededFunctions.ConditionalOperations
{
    public class ExistsConditionalKeyOperation : IJsonTransformConditionalCustomOperation
    {
        /// <inheritdoc />
        public virtual string OperationName => "exists";

        /// <inheritdoc />
        public virtual bool Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters)
        {
            if (parameters.Count != 1)
                throw new InvalidOperationException($"You must provide one select as parameter for function {OperationName}");

            return Exists(dataSource, parameters[0]);
        }

        /// <summary>
        /// Check if the <paramref name="jsonPath"/> returns a result
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="jsonPath"></param>
        /// <returns></returns>
        protected bool Exists(JToken dataSource, string jsonPath)
        {
            var existsTokenSearch = dataSource.SelectTokens(jsonPath);
            return existsTokenSearch != null && existsTokenSearch.Any();
        }
    }
}