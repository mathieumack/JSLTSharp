using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class ToBooleanTransformTests: BaseTestsClass
    {
        [TestMethod]
        public void TestString_True()
        {
            TestJsonTransformation(@"{
                                        'bool': true
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': true
                                    }");
        }
        [TestMethod]
        public void TestString_False()
        {
            TestJsonTransformation(@"{
                                        'bool': false
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': false
                                    }");
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void TestString_WithUpperCase()
        {
            TestJsonTransformation(@"{
                                        'bool': False
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': false
                                    }");
        }

        [TestMethod]
        public void TestString_Empty()
        {
            TestJsonTransformation(@"{
                                        'bool': ''
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': null
                                    }");
        }

        [TestMethod]
        public void TestString_Other()
        {
            TestJsonTransformation(@"{
                                        'bool': 'qsdf321qfd'
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': null
                                    }");
        }

        [TestMethod]
        public void TestString_Other_WithDefault()
        {
            TestJsonTransformation(@"{
                                        'bool': 'qsdf321qsdf'
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean(false)'
                                    }",
                                    @"{
                                        'bool': false
                                    }");
        }

        [TestMethod]
        public void TestNull_WithoutDefault()
        {
            TestJsonTransformation(@"{
                                        'bool': null
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': null
                                    }");
        }

        [TestMethod]
        public void TestNull_WithDefault()
        {
            TestJsonTransformation(@"{
                                        'bool': null
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean(true)'
                                    }",
                                    @"{
                                        'bool': true
                                    }");
        }

        [TestMethod]
        public void TestObject()
        {
            TestJsonTransformation(@"{
                                        'bool': {
                                           'obj':12 
                                        }
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': null
                                    }");
        }

        [TestMethod]
        public void TestBoolean_True()
        {
            TestJsonTransformation(@"{
                                        'bool': true
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': true
                                    }");
        }

        [TestMethod]
        public void TestBoolean_False()
        {
            TestJsonTransformation(@"{
                                        'bool': false
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': false
                                    }");
        }

        [TestMethod]
        public void TestBoolean_IntToTrue()
        {
            TestJsonTransformation(@"{
                                        'bool': 1
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': true
                                    }");
        }

        [TestMethod]
        public void TestBoolean_IntToFalse()
        {
            TestJsonTransformation(@"{
                                        'bool': 0
                                    }",
                                    @"{
                                        'bool': '$.bool->ToBoolean()'
                                    }",
                                    @"{
                                        'bool': false
                                    }");
        }
    }
}
