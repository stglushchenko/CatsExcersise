using System;
using System.Collections.Generic;
using System.Linq;
using CatsExercise.Models;
using CatsExercise.Reporting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CatsExercise.Models.Enums.Gender;
using static CatsExercise.Models.Enums.PetType;

namespace CatsExercise.ReportingTests
{
    [TestClass]
    public class CatsReportingServiceTest
    {
        CatsReportingService _targetClass;

        [TestInitialize]
        public void Initialize()
        {
            _targetClass = new CatsReportingService();
        }

        private static void CompareLookupWithDictionary(Dictionary<string, IEnumerable<string>> expected, ILookup<string, string> actual)
        {
            var dictionaryFromLookup = actual.ToDictionary(x => x.Key, x => x.AsEnumerable());
            CollectionAssert.AreEqual(expected.Keys, dictionaryFromLookup.Keys);
            foreach(var key in expected.Keys)
            {
                CollectionAssert.AreEqual(expected[key].ToArray(), actual[key].ToArray());
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupCatNamesByOwnerGender_NullParam_ArgumentException()
        {
            // arrange

            // act
            _targetClass.GroupCatNamesByOwnerGender(null);

            // assert is hangled by ExpectedException
        }

        [TestMethod]
        public void GroupCatNamesByOwnerGender_NoPets_ReturnAtLeastThosePetsWhichPresented()
        {
            // arrange
            var owners = new Owner[] {
                    new Owner
                    {
                        Name = "John",
                        Gender = Male
                    },
                    new Owner
                    {
                        Name = "Ana",
                        Gender = Female,
                        Pets = new Pet[]
                        {
                            new Pet
                            {
                                PetType = Cat,
                                Name = "A"
                            }
                        }
                    }
                };

            var expected = new Dictionary<string, IEnumerable<string>>
            {
                [nameof(Female)] = new[] { "A" }
            };

            // act
            var result = _targetClass.GroupCatNamesByOwnerGender(owners);

            // assert
            CompareLookupWithDictionary(expected, result);
        }

        [TestMethod]
        public void GroupCatNamesByOwnerGender_NormalInput_Pass()
        {
            // arrange
            var owners = new Owner[] {
                    new Owner
                    {
                        Gender = Male,
                        Pets = new Pet[]
                        {
                            new Pet()
                            {
                                PetType = Cat,
                                Name = "E"
                            },
                        }
                    },
                    new Owner
                    {
                        Gender = Male,
                        Pets = new Pet[]
                        {
                            new Pet()
                            {
                                PetType = Dog,
                                Name = "C"
                            },
                            new Pet()
                            {
                                PetType = Cat,
                                Name = "D"
                            },
                            new Pet()
                            {
                                PetType = Fish,
                                Name = "B"
                            },
                            new Pet()
                            {
                                PetType = Cat,
                                Name = "A"
                            },
                        }
                    },
                    new Owner
                    {
                        Gender = Female,
                        Pets = new Pet[]
                        {
                            new Pet()
                            {
                                PetType = Cat,
                                Name = "F"
                            },
                        }
                    }
                };

            var temp = new Dictionary<string, IEnumerable<string>>();
            temp[nameof(Male)] = new[] { "A", "D", "E" };
            temp[nameof(Female)] = new[] { "F" };

            var expected = temp;

            // act
            var result = _targetClass.GroupCatNamesByOwnerGender(owners);

            // assert
            CompareLookupWithDictionary(expected, result);
        }

    }
}
