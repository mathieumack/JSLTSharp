using FluentAssertions.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class SupportSpacesInFieldsTests : BaseTestsClass
    {
        [TestMethod]
        public void SpaceInOutputField()
        {
            TestJsonTransformation(@"{
                                        'value': 123
                                    }",
                                    @"{
                                        'result 1': '$.value'
                                    }",
                                    @"{
                                        'result 1': 123
                                    }");
        }

        [TestMethod]
        public void SpaceInInputField()
        {
            TestJsonTransformation(@"{
                                        'value 1': 123
                                    }",
                                    @"{
                                        'result': [ 'value 1' ]
                                    }",
                                    @"{
                                        'result': [ 'value 1' ]
                                    }");
        }

        [TestMethod]
        public void SpaceInInputSubField()
        {
            TestJsonTransformation(@"{
                                        'value1': {
                                            'subfield 1': 123
                                        }
                                    }",
                                    @"{
                                        'result': '$.value1.[\'subfield 1\']'
                                    }",
                                    @"{
                                        'result': 123
                                    }");
        }


        [TestMethod]
        public void SpaceInInputMultipleField()
        {
            TestJsonTransformation(@"{
                                        'value 1': {
                                            'subfield 1': 123
                                        }
                                    }",
                                    @"{
                                        'result': '$.[\'value 1\'].[\'subfield 1\']'
                                    }",
                                    @"{
                                        'result': 123
                                    }");
        }
    }
}
