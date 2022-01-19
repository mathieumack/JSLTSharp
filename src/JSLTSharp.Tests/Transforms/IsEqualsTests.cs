using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class IsEqualsTests : BaseTestsClass
    {
        [TestMethod]
        public void TestNotTokenValue()
        {
            TestJsonTransformation(@"{ 'isString': 'hello', } ",
                                    @"{
                                        'field1->ifisequals(hello,hello)' : 'Ok',
                                        'field2->ifisequals(hello,hello2)' : 'NotOk' }",
                                    @"{
                                        'field1': 'Ok'
                                    }");
        }

        [TestMethod]
        public void TestAllValues()
        {
            TestJsonTransformation(@"{ 
                                        'isNull': null,
                                        'isNotNull': '',
                                        'isString': 'hello',
                                        'isBoolean': true 
                                    }",
                                    @"{
                                        'field1-display->ifisequals($.isNull,null,null)': 'Ok',
                                        'field1-not-display->ifisequals($.isNotNull,null,null)': 'Ok',
                                        'field2-display->ifisequals($.isString,hello,string)': 'Ok',
                                        'field2-not-display->ifisequals($.isString,helloV2,string)': 'Ok',
                                        'field3-display->ifisequals($.isBoolean,true,boolean)': 'Ok',
                                        'field3-not-display->ifisequals($.isBoolean,false,boolean)': 'Ok'
                                    }",
                                    @"{
                                        'field1-display': 'Ok',
                                        'field2-display': 'Ok',
                                        'field3-display': 'Ok'
                                    }");
        }
    }
}
