using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class IfConditionsObjectTests : BaseTestsClass
    {
        [TestMethod]
        public void TestIfNotEmptySelectorTrueResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifnotempty($.isString))' : {
                                            'field1' : 'Ok',
                                            'field2' : 'Ok'
                                        }
                                    }",
                                    @"{
                                        'field1' : 'Ok',
                                        'field2' : 'Ok'
                                    }");
        }

        [TestMethod]
        public void TestIfNotEmptyvalueFalseResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifnotempty())' : {
                                            'field1' : 'Ok',
                                            'field2' : 'Ok'
                                        }
                                    }",
                                    @"{ }");
        }

        [TestMethod]
        public void TestIfNotEmptyValueTrueResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifnotempty(hi))' : {
                                            'field1' : 'Ok',
                                            'field2' : 'Ok'
                                        }
                                    }",
                                    @"{
                                        'field1' : 'Ok',
                                        'field2' : 'Ok'
                                    }");
        }

        [TestMethod]
        public void TestIfFalseResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifisequals(hello,hell))' : {
                                            'field1' : 'Ok',
                                            'field2' : 'Ok'
                                        }
                                    }",
                                    @"{ }");
        }

        [TestMethod]
        public void TestIfFalseResultMultiple()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifisequals(hello,hello)->ifisequals($.isString,hell,string))' : {
                                            'field1' : 'Ok',
                                            'field2' : 'Ok'
                                        }
                                    }",
                                    @"{ }");
        }
        [TestMethod]
        public void TestIfTrueResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifisequals(hello,hello))' : {
                                            'field1' : 'Ok',
                                            'field2' : 'Ok'
                                        }
                                    }",
                                    @"{
                                        'field1' : 'Ok',
                                        'field2' : 'Ok'
                                    }");
        }

        [TestMethod]
        public void TestIfTrueResultWithSelector()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifisequals($.isString,hello,string))' : {
                                            'field1' : 'Ok',
                                            'field2' : 'Ok'
                                        }
                                    }",
                                    @"{
                                        'field1' : 'Ok',
                                        'field2' : 'Ok'
                                    }");
        }

        [TestMethod]
        public void TestIfTrueResultMultiple()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifisequals(hello,hello)->ifisequals($.isString,hello,string))' : {
                                            'field1' : 'Ok',
                                            'field2' : 'Ok'
                                        }
                                    }",
                                    @"{
                                        'field1' : 'Ok',
                                        'field2' : 'Ok'
                                    }");
        }
    }
}
