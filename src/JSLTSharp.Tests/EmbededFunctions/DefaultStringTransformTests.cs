using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class DefaultStringTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void NullField()
        {
            TestJsonTransformation(@"{
                                        'value': null
                                    }",
                                    @"{
                                        'result': '$.value->defaultstring()'
                                    }",
                                    @"{
                                        'result': ''
                                    }");
        }

        [TestMethod]
        public void EmptyField()
        {
            TestJsonTransformation(@"{
                                        'values': [ ]
                                    }",
                                    @"{
                                        'vals': {
                                         '$.values->Distinct()->loop(e)->RemoveIfEmpty()': {
                                                'COMP_CODE': '$.e'
                                          }
                                        }
                                    }",
                                    @"{ }");
        }

        [TestMethod]
        public void NonExistToken()
        {
            TestJsonTransformation(@"{
                                        'value': '123'
                                    }",
                                    @"{
                                        'result': '$.value1->defaultstring()'
                                    }",
                                    @"{
                                        'result': ''
                                    }");
        }

        [TestMethod]
        public void NonEmptyField()
        {
            TestJsonTransformation(@"{
                                        'value': '123'
                                    }",
                                    @"{
                                        'result': '$.value->defaultstring()'
                                    }",
                                    @"{
                                        'result': '123'
                                    }");
        }
    }
}
