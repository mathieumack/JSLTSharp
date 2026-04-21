using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class ToDecimalTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void TestFloat_ToDecimal()
        {
            TestJsonTransformation(@"{
                                        'value': 13246.51
                                    }",
                                    @"{
                                        'result': '$.value->ToDecimal()'
                                    }",
                                    @"{
                                        'result': 13246.51
                                    }");
        }

        [TestMethod]
        public void TestInteger_ToDecimal()
        {
            TestJsonTransformation(@"{
                                        'value': 100
                                    }",
                                    @"{
                                        'result': '$.value->ToDecimal()'
                                    }",
                                    @"{
                                        'result': 100.0
                                    }");
        }

        [TestMethod]
        public void TestString_ValidNumber()
        {
            TestJsonTransformation(@"{
                                        'value': '42.5'
                                    }",
                                    @"{
                                        'result': '$.value->ToDecimal()'
                                    }",
                                    @"{
                                        'result': 42.5
                                    }");
        }

        [TestMethod]
        public void TestString_InvalidNumber()
        {
            TestJsonTransformation(@"{
                                        'value': 'notanumber'
                                    }",
                                    @"{
                                        'result': '$.value->ToDecimal()'
                                    }",
                                    @"{
                                        'result': null
                                    }");
        }

        [TestMethod]
        public void TestBoolean_True()
        {
            TestJsonTransformation(@"{
                                        'value': true
                                    }",
                                    @"{
                                        'result': '$.value->ToDecimal()'
                                    }",
                                    @"{
                                        'result': 1.0
                                    }");
        }

        [TestMethod]
        public void TestBoolean_False()
        {
            TestJsonTransformation(@"{
                                        'value': false
                                    }",
                                    @"{
                                        'result': '$.value->ToDecimal()'
                                    }",
                                    @"{
                                        'result': 0.0
                                    }");
        }

        [TestMethod]
        public void TestNull()
        {
            TestJsonTransformation(@"{
                                        'value': null
                                    }",
                                    @"{
                                        'result': '$.value->ToDecimal()'
                                    }",
                                    @"{
                                        'result': null
                                    }");
        }
    }
}
