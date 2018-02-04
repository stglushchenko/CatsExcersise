using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CatsExercise.Reporting;

namespace CatsExercise.ReportingTests.Extensions
{
    [TestClass]
    public class PrintingExtensionTest
    {
        LookupPrintingService _targetClass;

        [TestInitialize]
        public void Initialize()
        {
            _targetClass = new LookupPrintingService();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrintItemsWithHyphens_Null_ArgumentNullException()
        {
            // aggange

            // act
            var result = _targetClass.PrintItemsWithHyphens(null);

            //assert exception should be thrown
        }

        [TestMethod]
        public void PrintItemsWithHyphens_Empty_Empty()
        {
            // aggange
            ILookup<string, string> emptyInput = new Dictionary<string, string>().ToLookup(x=>x.Key, x => x.Value);

            var expected = string.Empty;

            // act
            var result = _targetClass.PrintItemsWithHyphens(emptyInput);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PrintItemsWithHyphens_NormalInput_NormalOutput()
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
            var result = _targetClass.PrintItemsWithHyphens(input);

            //assert exception should be thrown
            Assert.AreEqual(expected, result);
        }
    }
}
