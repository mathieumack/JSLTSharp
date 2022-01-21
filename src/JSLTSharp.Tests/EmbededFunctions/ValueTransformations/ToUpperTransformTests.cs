using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class ToUpperTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void TestValidResult()
        {
            TestJsonTransformation(@"{
                                        'lower': 'azerty'
                                    }",
                                    @"{
                                        'UPPER': '$.lower->ToUpper()'
                                    }",
                                    @"{
                                        'UPPER': 'AZERTY'
                                    }");
        }

        [TestMethod]
        public void TestNumber()
        {
            TestJsonTransformation(@"{
                                        'lower': 123
                                    }",
                                    @"{
                                        'UPPER': '$.lower->ToUpper()'
                                    }",
                                    @"{
                                        'UPPER': 123
                                    }");
        }

        [TestMethod]
        public void TestNull()
        {
            TestJsonTransformation(@"{
                                        'lower': null
                                    }",
                                    @"{
                                        'UPPER': '$.lower->ToUpper()'
                                    }",
                                    @"{
                                        'UPPER': null
                                    }");
        }
    }
}
