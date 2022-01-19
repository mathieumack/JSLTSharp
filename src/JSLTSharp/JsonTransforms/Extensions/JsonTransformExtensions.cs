using JSLTSharp.JsonTransforms.Transformations;
using JSLTSharp.JsonTransforms.Transformations.ValueTransformations;
using Microsoft.Extensions.DependencyInjection;

namespace JSLTSharp.JsonTransforms.Extensions
{
    public static class JsonTransformExtensions
    {
        /// <summary>
        /// Register custom functions for JsonTransform engine in service collection
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void RegisterJsonCustomTransformFunctions(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, CastTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ExistsTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, FormatDateTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, NotExistsTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ToBooleanTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ToIntegerTransformOperation>();

            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ConcatStringTransformationOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, DistinctArrayTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ToUpperTransformationOperation>();

            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, IfIsNotEqualsConditionalKeyOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, IfIsEqualsConditionalKeyOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, ExistsConditionalKeyOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, NotExistsTransformConditionalOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, IfNotEmptyConditionalKeyOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, NotNullConditionalTransformOperation>();
        }
    }
}
