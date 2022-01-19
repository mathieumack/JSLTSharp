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
                                        'start': 'exakis'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-nelite)'
                                    }",
                                    @"{
                                        'complete': 'exakis-nelite'
                                    }");
        }

        [TestMethod]
        public void TestNumber()
        {
            TestJsonTransformation(@"{
                                        'start': 123
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-nelite)'
                                    }",
                                    @"{
                                        'complete': '123-nelite'
                                    }");
        }

        [TestMethod]
        public void TestNull()
        {
            TestJsonTransformation(@"{
                                        'start': null
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-nelite)'
                                    }",
                                    @"{
                                        'complete': '-nelite'
                                    }");
        }

        [TestMethod]
        public void MissingParameter()
        {
            TestJsonTransformation(@"{
                                        'start': 'exakis'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString()'
                                    }",
                                    @"{
                                        'complete': 'exakis'
                                    }");
        }

        [TestMethod]
        public void MoreThanOneParameter()
        {
            TestJsonTransformation(@"{
                                        'start': 'exakis',
                                        'end': 'nelite'
                                    } ",
                                    @" {
                                        'complete': '->ConcatString($.start,-,$.end)'
                                    }",
                                    @"{
                                        'complete': 'exakis-nelite'
                                    }");
        }

        [TestMethod]
        public void MoreThanOneParameterMissingSourceToken()
        {
            TestJsonTransformation(@"{
                                        'end': 'nelite'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-,$.end)'
                                    }",
                                    @"{
                                        'complete': '-nelite'
                                    }");
        }


        [TestMethod]
        public void MoreThanOneParameterWithSourceToken()
        {
            TestJsonTransformation(@"{
                                        'start': 'exakis',
                                        'end': 'nelite'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString(-,$.end)'
                                    }",
                                    @"{
                                        'complete': 'exakis-nelite'
                                    }");
        }


        [TestMethod]
        public void MoreThanOneParameterWithSourceTokenAndSameParameter()
        {
            TestJsonTransformation(@"{
                                        'start': 'exakis',
                                        'end': 'nelite'
                                    } ",
                                    @" {
                                        'complete': '$.start->ConcatString($.start,-,$.end)'
                                    }",
                                    @"{
                                        'complete': 'exakisexakis-nelite'
                                    }");
        }


        [TestMethod]
        public void TokenMissingFromDataSource()
        {
            TestJsonTransformation(@"{
                                        'start': 'exakis',
                                        'end': 'nelite'
                                    } ",
                                    @" {
                                        'complete': '->ConcatString($.beginning,-,$.end)'
                                    }",
                                    @"{
                                        'complete': '-nelite'
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
