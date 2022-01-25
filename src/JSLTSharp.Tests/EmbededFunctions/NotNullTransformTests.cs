using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class NotNull : BaseTestsClass
    {
        [TestMethod]
        public void NotNullValue()
        {
            TestJsonTransformation(@"{
                                        'value': 123
                                    }",
                                    @"{ 
                                        '->if(->notnull($.value))': {
                                            'result': '$.value'
                                        }
                                    }",
                                    @"{
                                        'result': 123
                                    }");
        }

        [TestMethod]
        public void NullValue()
        {
            TestJsonTransformation(@"{
                                        'value': null
                                    }",
                                    @"{ 
                                        '->if(->notnull($.value))': {
                                            'result': '$.value'
                                        }
                                    }",
                                    @"{ }");
        }
    }
}
