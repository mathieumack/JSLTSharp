using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class LoopArrayTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void NominalCase()
        {
            TestJsonTransformation(@"{
                                        'values': [ 1, 2 ]
                                    }",
                                    @"{
                                        'hello' : {
                                            '$.values->loop(entry)': {
                                                'field' : '$.entry'
                                            }
                                        }
                                    }",
                                    @"{
                                        'hello' : [
                                        {
                                            'field': 1
                                        },
                                        {
                                            'field': 2
                                        }]
                                    }");
        }
    }
}
