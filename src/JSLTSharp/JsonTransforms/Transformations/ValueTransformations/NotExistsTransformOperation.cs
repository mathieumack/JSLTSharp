using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JSLTSharp.JsonTransforms.Transformations
{
    public class NotExistsTransformOperation : ExistsTransformOperation
    {
        /// <inheritdoc />
        public override string OperationName => "notexists";

        /// <inheritdoc />
        public override JToken Apply(JToken dataSource, JToken token, IList<string> parameters)
        {
            return JValue.FromObject(!Exists(dataSource, token, parameters));
        }
    }
}