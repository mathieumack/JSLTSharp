using FluentAssertions.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace JSLTSharp.Tests.Transforms
{
    [TestClass]
    public class ConcatStringTransformTests : BaseTestsClass
    {
        [TestMethod]
        public void TestValidResult()
        {
            TestJsonTransformation(@"{
                                        'start': 'sample'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-value result)'
                                    }",
                                    @"{
                                        'complete': 'sample-value result'
                                    }");
        }

        [TestMethod]
        public void TestNumber()
        {
            TestJsonTransformation(@"{
                                        'start': 123
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-value result)'
                                    }",
                                    @"{
                                        'complete': '123-value result'
                                    }");
        }

        [TestMethod]
        public void TestNull()
        {
            TestJsonTransformation(@"{
                                        'start': null
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-value result)'
                                    }",
                                    @"{
                                        'complete': '-value result'
                                    }");
        }

        [TestMethod]
        public void MissingParameter()
        {
            TestJsonTransformation(@"{
                                        'start': 'sample'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString()'
                                    }",
                                    @"{
                                        'complete': 'sample'
                                    }");
        }

        [TestMethod]
        public void MoreThanOneParameter()
        {
            TestJsonTransformation(@"{
                                        'start': 'sample',
                                        'end': 'value result'
                                    } ",
@" {
    'complete': '->ConcatString($.start,-,$.end)'
}",
                                    @"{
                                        'complete': 'sample-value result'
                                    }");
        }

        [TestMethod]
        public void MoreThanOneParameterMissingSourceToken()
        {
            TestJsonTransformation(@"{
                                        'end': 'value result'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-,$.end)'
                                    }",
                                    @"{
                                        'complete': '-value result'
                                    }");
        }


        [TestMethod]
        public void MoreThanOneParameterWithSourceToken()
        {
            TestJsonTransformation(@"{
                                        'start': 'sample',
                                        'end': 'value result'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-,$.end)'
                                    }",
                                    @"{
                                        'complete': 'sample-value result'
                                    }");
        }


        [TestMethod]
        public void MoreThanOneParameterWithSourceTokenAndSameParameter()
        {
            TestJsonTransformation(@"{
                                        'start': 'sample',
                                        'end': 'value result'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString($.start,-,$.end)'
                                    }",
                                    @"{
                                        'complete': 'samplesample-value result'
                                    }");
        }


        [TestMethod]
        public void TokenMissingFromDataSource()
        {
            TestJsonTransformation(@"{
                                        'start': 'sample',
                                        'end': 'value result'
                                    } ",
                                    @" {
                                        'complete': '->ConcatString($.beginning,-,$.end)'
                                    }",
                                    @"{
                                        'complete': '-value result'
                                    }");
        }

        [TestMethod]
        public void TokenNotAStringInDataSource()
        {
            TestJsonTransformation(@"{
                                        'start': 'Lyon',
                                        'end': 69009
                                    } ",
                                    @" {
                                        'complete': '->ConcatString($.start,-,$.end)'
                                    }",
                                    @"{
                                        'complete': 'Lyon-69009'
                                    }");
        }
    }
}
