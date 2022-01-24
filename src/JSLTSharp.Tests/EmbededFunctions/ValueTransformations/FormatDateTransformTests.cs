using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class FormatDateTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void TestValidResult()
        {
            TestJsonTransformation(@"{
                                        'dateField': '01/01/2022'
                                    }",
                                    @"{
                                        'result': '$.dateField->FormatDate(s)'
                                    }",
                                    @"{
                                        'result': '2022-01-01T00:00:00'
                                    }");
        }
    }
}
