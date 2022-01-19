using FluentAssertions.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class IfElseIfConditionsObjectTests : BaseTestsClass
    {
        [TestMethod]
        public void TestIfElseSelectorIfNoElseResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @" {
                                        '->if(->ifnotempty($.isString2))' : {
                                            'field1' : 'OK',
                                            'field2' : 'OK'
                                        }
                                    }",
                                    @"{ }");
        }

        [TestMethod]
        public void TestIfElseSelectorIfValidResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifnotempty($.isString))' : {
                                            'field1' : 'OK',
                                            'field2' : 'OK',
                                            '->elseif()' : {
                                                'field1NOK' : 'NOK',
                                                'field2NOK' : 'NOK'
                                            }
                                        }
                                    }",
                                    @"{
                                            'field1' : 'OK',
                                            'field2' : 'OK'
                                        }");
        }

        [TestMethod]
        public void TestIfElseSelectorIfNotValidElseResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifnotempty($.isStringInvalid))' : {
                                            'field1' : 'OK',
                                            'field2' : 'OK',
                                            '->elseif()' : {
                                                'field1NOK' : 'NOK',
                                                'field2NOK' : 'NOK'
                                            }
                                        }
                                    }",
                                    @"{
                                        'field1NOK' : 'NOK',
                                        'field2NOK' : 'NOK'
                                    }");
        }

        [TestMethod]
        public void TestIfElseSelectorIfNotValidElseIfValidElseResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifnotempty($.isStringInvalid))' : {
                                            'field1' : 'OK',
                                            'field2' : 'OK',
                                            '->elseif(->ifnotempty($.isString))' : {
                                                'field1OK' : 'OK',
                                                'field2OK' : 'OK'
                                            },
                                            '->elseif()' : {
                                                'field1NOK' : 'NOK',
                                                'field2NOK' : 'NOK'
                                            }
                                        }
                                    }",
                                    @"{
                                        'field1OK' : 'OK',
                                        'field2OK' : 'OK'
                                    }");
        }

        [TestMethod]
        public void TestIfElseSelectorIfNotValidElseIfNotValidElseResult()
        {
            TestJsonTransformation(@"{
                                        'isString': 'hello' 
                                    }",
                                    @"{
                                        '->if(->ifnotempty($.isStringInvalid))' : {
                                            'field1' : 'OK',
                                            'field2' : 'OK',
                                            '->elseif(->ifnotempty($.isStringInvalid2))' : {
                                                'field1' : 'OK',
                                                'field2' : 'OK'
                                            },
                                            '->elseif()' : {
                                                'field1NOK' : 'NOK',
                                                'field2NOK' : 'NOK'
                                            }
                                        }
                                    }",
                                    @"{
                                        'field1NOK' : 'NOK',
                                        'field2NOK' : 'NOK'
                                    }");
        }
    }
}
