using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class RootArrayTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void NominalCase()
        {
            TestJsonTransformation(@"{
                                        'values': [ 1, 2 ]
                                    }",
                                    @"[
                                        {
                                            '$.values->loop(entry)': {
                                                'field' : '$.entry'
                                            }
                                        }
                                    ]",
                                    @"[
                                        {
                                            'field': 1
                                        },
                                        {
                                            'field': 2
                                        }
                                    ]");
        }

        [TestMethod]
        public void NominalCaseWithNull()
        {
            TestJsonTransformation(@"{
                                        'values': null
                                    }",
                                    @"[
                                        {
                                            '$.values->loop(entry)': {
                                                'field' : '$.entry'
                                            }
                                        }
                                    ]",
                                    @"[ ]");
        }

        [TestMethod]
        public void NominalCaseWithInvalidSelector()
        {
            TestJsonTransformation(@"{
                                        'values2': null
                                    }",
                                    @"[
                                        {
                                            '$.values->loop(entry)': {
                                                'field' : '$.entry'
                                            }
                                        }
                                    ]",
                                    @"[ ]");
        }

        [TestMethod]
        public void FixedAndDynamicValuesCase()
        {
            TestJsonTransformation(@"{
                                        'values': [ 3, 4 ]
                                    }",
                                    @"{
                                        'result' : [
                                            {
                                                'object1': 'value 1'
                                            },
                                            {
                                                'object1': 'value 2'
                                            },
                                            {
                                                '$.values->loop(entry)': {
                                                    'object1' : '$.entry'
                                                }
                                            }
                                        ]
                                    }",
                                    @"{
                                        'result' : [
                                            {
                                                'object1': 'value 1'
                                            },
                                            {
                                                'object1': 'value 2'
                                            },
                                            {
                                                'object1': 3
                                            },
                                            {
                                                'object1': 4
                                            }
                                        ]
                                    }");
        }

        [TestMethod]
        public void FixedAndDynamicNoValuesCase()
        {
            TestJsonTransformation(@"{
                                        'values': null
                                    }",
                                    @"{
                                        'result' : [
                                            {
                                                'object1': 'value 1'
                                            },
                                            {
                                                'object1': 'value 2'
                                            },
                                            {
                                                '$.values->loop(entry)': {
                                                    'object1' : '$.entry'
                                                }
                                            }
                                        ]
                                    }",
                                    @"{
                                        'result' : [
                                            {
                                                'object1': 'value 1'
                                            },
                                            {
                                                'object1': 'value 2'
                                            }
                                        ]
                                    }");
        }

        [TestMethod]
        public void RemoveIfEmptyIfEmptyCase()
        {
            TestJsonTransformation(@"{
                                        'values': [ ]
                                    }",
                                    @"[
                                        {
                                            '$.values->loop(entry)->RemoveIfEmpty()': {
                                                'field' : '$.entry'
                                            }
                                        }
                                    ]",
                                    @"[ ]");
        }

        [TestMethod]
        public void NullIfEmptyCase()
        {
            TestJsonTransformation(@"{
                                        'values': [ ]
                                    }",
                                    @"[
                                        {
                                            '$.values->loop(entry)->NullIfEmpty()': {
                                                'field' : '$.entry'
                                            }
                                        }
                                    ]",
                                    @"[ ]");
        }

        [TestMethod]
        public void WithoutObject()
        {
            TestJsonTransformation(@"{
                                        'values': [ 1, 2 ]
                                    }",
                                    @"[
                                        {
                                            '$.values->loop(entry)': '$.entry'
                                        }
                                    ]",
                                    @"[ 1, 2 ]");
        }


        [TestMethod]
        public void WithoutObjectWithMapping()
        {
            TestJsonTransformation(@"{
                                        'values': [ {'field1':1,'field2':2}, {'field1':3,'field2':4} ]
                                    }",
                                    @"[
                                        {
                                            '$.values->loop(entry)': '$.entry.field1'
                                        }
                                    ]",
                                    @"[ 1, 3 ]");
        }
    }
}
