using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class CastTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void CastAtRootNotFoundedObject()
        {
            TestJsonTransformation(@"{
                                        'value': 123
                                    } ",
                                    @" {
                                        'result': '$.PRData->Cast(date10)'
                                    }",
                                    @"{
                                            'result': '0000-00-00'
                                        }");
        }

        [TestMethod]
        public void CastAtRootFoundedObject()
        {
            TestJsonTransformation(@"{
                                        'value': 123456789.123456
                                    } ",
                                    @" {
                                        'result': '$.value->Cast(decimal13.2)'
                                    }",
                                    @"{
                                        'result': '123456789.123'
                                    }");
        }

        [TestMethod]
        public void CastInArrayFoundedObject()
        {
            TestJsonTransformation(@"{
                                        'values': [ { 'value': 123456789.123456 } ]
                                    } ",
                                    @" {
                                        'results': {
                                            '$.values->loop(e)' : { 'value' : '$.e.value->Cast(decimal13.2)' }
                                        } 
                                    }",
                                    @"{
                                        'results': [ { 'value' : '123456789.123' } ]
                                    }");
        }

        [TestMethod]
        public void CastInArrayNullValueFoundedObject()
        {
            TestJsonTransformation(@"{
                                        'values': [ { 'value': null } ]
                                    } ",
                                    @" {
                                        'results': {
                                            '$.values->loop(e)' : { 'value' : '$.e.value->Cast(date10)' }
                                        }
                                    }",
                                    @"{
                                        'results': [ { 'value' : '0000-00-00' } ]
                                    }");
        }

        [TestMethod]
        public void CastInArrayNotFoundedObject()
        {
            TestJsonTransformation(@"{
                                        'values': [ { 'value': null } ]
                                    } ",
                                    @" {
                                        'results': {
                                            '$.values->loop(e)' : { 'value' : '$.e.valuenotfounded->Cast(decimal13.2)' }
                                        }
                                    }",
                                    @"{
                                        'results': [ { 'value' : null } ]
                                    }");
        }
    }
}
