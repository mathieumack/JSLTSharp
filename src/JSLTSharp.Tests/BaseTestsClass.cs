using FluentAssertions.Json;
using JSLTSharp.JsonTransforms.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace JSLTSharp.Tests
{
    public abstract class BaseTestsClass
    {
        /// <summary>
        /// Initialize a new service collection environment and create a new JsonTransform object
        /// </summary>
        /// <returns></returns>
        protected JsonTransform ServiceCollectionInitializer()
        {
            var serviceCollection = new ServiceCollection();

            // Register JsonTransform custom functions
            serviceCollection.RegisterJsonCustomTransformFunctions();

            serviceCollection.AddLogging(configure => configure.AddConsole());

            serviceCollection.AddSingleton<JsonTransform>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetRequiredService<JsonTransform>();
        }

        /// <summary>
        /// Shared unit test to test the transformation of a json content
        /// </summary>
        /// <param name="jsonContent"></param>
        /// <param name="transformation"></param>
        /// <param name="expected"></param>
        public void TestJsonTransformation(string jsonContent, string transformation, string expected)
        {
            var flow = ServiceCollectionInitializer();

            var result = flow.Transform(jsonContent,
                                        transformation);

            var jtokenExpected = JToken.Parse(expected);
            var jtokenResult = JToken.Parse(result);

            jtokenResult.Should().BeEquivalentTo(jtokenExpected);
        }
    }
}