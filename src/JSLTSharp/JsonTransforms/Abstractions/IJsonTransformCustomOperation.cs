using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JSLTSharp.JsonTransforms.Abstractions
{
    public interface IJsonTransformCustomOperation
    {
        /// <summary>
        /// Name of the operation
        /// </summary>
        string OperationName { get; }

        /// <summary>
        /// Root object to apply patch operation
        /// </summary>
        /// <param name="dataSource">currentDataSource, can be used to execute search on it</param>
        /// <param name="objectToApplyTo">object to patch</param>
        /// <param name="parameters">parameters for the operation</param>
        JToken Apply(JToken dataSource, JToken objectToApplyTo, IList<string> parameters);
    }
}
