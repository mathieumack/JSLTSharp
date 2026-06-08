using JSLTSharp.JsonTransforms.Abstractions;
using JSLTSharp.JsonTransforms.EmbededFunctions.ConditionalOperations;
using JSLTSharp.JsonTransforms.EmbededFunctions.ValueTransformations;
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
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, FormatDateTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ToBooleanTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ToIntegerTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ToDecimalTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ConcatStringTransformationOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, DistinctArrayTransformOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ToUpperTransformationOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, ToLowerTransformationOperation>();
            serviceCollection.AddSingleton<IJsonTransformCustomOperation, TrimTransformOperation>();

            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, IfIsNotEqualsConditionalKeyOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, IfIsEqualsConditionalKeyOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, ExistsConditionalKeyOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, NotExistsTransformConditionalOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, IfNotEmptyConditionalKeyOperation>();
            serviceCollection.AddSingleton<IJsonTransformConditionalCustomOperation, NotNullConditionalTransformOperation>();
        }

        /// <summary>
        /// Register the JsonTransform engine along with all built-in transformation functions in the service collection.
        /// This is a convenience method that combines <see cref="RegisterJsonCustomTransformFunctions"/> with
        /// registering the <see cref="JsonTransform"/> engine itself.
        /// </summary>
        /// <param name="serviceCollection">The service collection to register dependencies into.</param>
        public static void AddJsonTransform(this IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterJsonCustomTransformFunctions();
            serviceCollection.AddSingleton<JsonTransform>();
        }
    }
}
