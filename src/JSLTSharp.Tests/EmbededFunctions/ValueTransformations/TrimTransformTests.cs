using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class TrimTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void TestLeadingAndTrailingWhitespace()
        {
            TestJsonTransformation(@"{
                                        'value': '  hello  '
                                    }",
                                    @"{
                                        'result': '$.value->Trim()'
                                    }",
                                    @"{
                                        'result': 'hello'
                                    }");
        }

        [TestMethod]
        public void TestLeadingWhitespaceOnly()
        {
            TestJsonTransformation(@"{
                                        'value': '   hello'
                                    }",
                                    @"{
                                        'result': '$.value->Trim()'
                                    }",
                                    @"{
                                        'result': 'hello'
                                    }");
        }

        [TestMethod]
        public void TestTrailingWhitespaceOnly()
        {
            TestJsonTransformation(@"{
                                        'value': 'hello   '
                                    }",
                                    @"{
                                        'result': '$.value->Trim()'
                                    }",
                                    @"{
                                        'result': 'hello'
                                    }");
        }

        [TestMethod]
        public void TestNoWhitespace()
        {
            TestJsonTransformation(@"{
                                        'value': 'hello'
                                    }",
                                    @"{
                                        'result': '$.value->Trim()'
                                    }",
                                    @"{
                                        'result': 'hello'
                                    }");
        }

        [TestMethod]
        public void TestNumber()
        {
            TestJsonTransformation(@"{
                                        'value': 123
                                    }",
                                    @"{
                                        'result': '$.value->Trim()'
                                    }",
                                    @"{
                                        'result': 123
                                    }");
        }

        [TestMethod]
        public void TestNull()
        {
            TestJsonTransformation(@"{
                                        'value': null
                                    }",
                                    @"{
                                        'result': '$.value->Trim()'
                                    }",
                                    @"{
                                        'result': null
                                    }");
        }
    }
}
