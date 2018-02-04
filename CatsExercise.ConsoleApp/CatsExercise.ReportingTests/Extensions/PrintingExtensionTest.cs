using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using CatsExercise.Reporting.Extensions;
using System.Collections;
using System.Collections.Generic;

namespace CatsExercise.ReportingTests.Extensions
{
    [TestClass]
    public class PrintingExtensionTest
    {
        [TestMethod]
        public void ToFormattedResult_Empty_Empty()
        {
            // aggange
            ILookup<string, string> emptyInput = new Dictionary<string, string>().ToLookup(x=>x.Key, x => x.Value);
            var expected = string.Empty;

            // act
            var result = emptyInput.ToFormattedResult();

            //assert exception should be thrown
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToFormattedResult_NormalInput_NormalOutput()
        {
            // aggange
            var inputData = new (string groupName, string item)[]
            {
                ("Male","A"),
                ("Male","C"),
                ("Male","B"),
                ("Female","E"),
                ("Female","D"),
            };

            var input = inputData.ToLookup(x => x.groupName, x => x.item);
            var expected = "Male\r\n  -A\r\n  -C\r\n  -B\r\nFemale\r\n  -E\r\n  -D\r\n";

            // act
            var result = input.ToFormattedResult();

            //assert exception should be thrown
            Assert.AreEqual(expected, result);
        }
    }
}
