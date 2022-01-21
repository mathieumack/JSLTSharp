using FluentAssertions.Json;
using JSLTSharp.JsonTransforms.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace JSLTSharp.Tests
{
    public abstract class BaseTestsClass
    {
        protected JsonTransform FlowFactory()
        {
            var serviceCollection = new ServiceCollection();

            // Register JsonTransform custom functions
            serviceCollection.RegisterJsonCustomTransformFunctions();

            serviceCollection.AddLogging(configure => configure.AddConsole());

            serviceCollection.AddSingleton<JsonTransform>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetRequiredService<JsonTransform>();
        }

        public void TestJsonTransformation(string jsonContent, string transformation, string expected)
        {
            var flow = FlowFactory();

            var result = flow.Transform(jsonContent,
                                        transformation);

            var jtokenExpected = JToken.Parse(expected);
            var jtokenResult = JToken.Parse(result);

            jtokenResult.Should().BeEquivalentTo(jtokenExpected);
        }
    }
}