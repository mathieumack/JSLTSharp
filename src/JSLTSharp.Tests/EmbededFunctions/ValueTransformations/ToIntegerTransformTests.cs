using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class ToIntegerTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void TestValidResult()
        {
            TestJsonTransformation(@"{
                                        'float': 13246.51,
                                        'bool': true, 
                                        'string': '123456789'
                                    }",
                                    @"{
                                        'float': '$.float->ToInteger()',
                                        'bool': '$.bool->ToInteger()', 
                                        'string': '$.string->ToInteger()'
                                    }",
                                    @"{
                                        'float': 13246,
                                        'bool': 1, 
                                        'string': 123456789
                                    }");
        }
    }
}
