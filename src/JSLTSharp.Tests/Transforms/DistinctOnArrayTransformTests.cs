using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class DistinctOnArrayTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void EmptyArrayValues()
        {
            TestJsonTransformation(@"{
                                        'values': [  ]
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
        public void NullArrayValues()
        {
            TestJsonTransformation(@"{
                                        'values': null
                                    }",
                                    @"{
                                        'vals': {
                                         '$.values->Distinct()->loop(e)': {
                                                'COMP_CODE': '$.e'
                                          }
                                        }
                                    }",
                                    @"{ 'vals': null }");
        }

        [TestMethod]
        public void StringArrayValues()
        {
            TestJsonTransformation(@"{
                                        'values': [ '1', '2', '3', '1', '3' ]
                                    }",
                                    @"{
                                        'vals': {
                                         '$.values->Distinct()->loop(e)': {
                                                'COMP_CODE': '$.e'
                                          }
                                        }
                                    }",
                                    @"{ 
                                        'vals': [ { 'COMP_CODE': '1' }, { 'COMP_CODE': '2' }, { 'COMP_CODE': '3' } ]
                                    }");
        }
    }
}
