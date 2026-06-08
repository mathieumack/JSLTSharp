using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class ToLowerTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void TestValidResult()
        {
            TestJsonTransformation(@"{
                                        'upper': 'AZERTY'
                                    }",
                                    @"{
                                        'lower': '$.upper->ToLower()'
                                    }",
                                    @"{
                                        'lower': 'azerty'
                                    }");
        }

        [TestMethod]
        public void TestNumber()
        {
            TestJsonTransformation(@"{
                                        'upper': 123
                                    }",
                                    @"{
                                        'lower': '$.upper->ToLower()'
                                    }",
                                    @"{
                                        'lower': 123
                                    }");
        }

        [TestMethod]
        public void TestNull()
        {
            TestJsonTransformation(@"{
                                        'upper': null
                                    }",
                                    @"{
                                        'lower': '$.upper->ToLower()'
                                    }",
                                    @"{
                                        'lower': null
                                    }");
        }

        [TestMethod]
        public void TestMixedCase()
        {
            TestJsonTransformation(@"{
                                        'value': 'Hello World'
                                    }",
                                    @"{
                                        'result': '$.value->ToLower()'
                                    }",
                                    @"{
                                        'result': 'hello world'
                                    }");
        }
    }
}
