using CatsExercise.Models;
using CatsExercise.Services;
using CatsExercise.ServicesTests.Comparers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static CatsExercise.Models.Enums.Gender;
using static CatsExercise.Models.Enums.PetType;

namespace CatsExercise.ServicesTests
{
    [TestClass]
    public class OwnerServiceTest
    {
        private static HttpListener _listener;
        private OwnerService _targetClass;
        private OwnerComparer _ownerComparer = new OwnerComparer();

        private const string _baseAddress = "http://localhost/TestData/" ;
        private const string _testDataFolder = "TestData";

        private IConfiguration BuildConfiguration(string testFileName)
        {
            var dict = new Dictionary<string, string>
            {
                {"BaseServicePath", _baseAddress},
                {"EntitiesPaths:Owner", testFileName}
            };

            var builder = new ConfigurationBuilder();

            builder.AddInMemoryCollection(dict);
            return builder.Build();
        }


        private void Initialize(string testFileName)
        {
            var config = BuildConfiguration(testFileName);

            InitializeListener(config);

            _targetClass = new OwnerService(config);
        }

        private static void InitializeListener(IConfiguration config)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(config.GetSection("BaseServicePath").Value);

            _listener.Start();

            var httpListenerContext = _listener.GetContextAsync().ContinueWith(t =>
            {
                var request = t.Result.Request;

                var response = t.Result.Response;
                var path = Path.Combine(Directory.GetCurrentDirectory(), _testDataFolder, config.GetSection("EntitiesPaths")["Owner"]);
                using (response.OutputStream)
                {
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        fs.CopyTo(response.OutputStream);
                    };
                }
            });
        }

        [TestMethod]
        public void All_NormalInput_NormalResult()
        {
            //arrange
            Initialize("people.json");
            var expected = new Owner[]
            {
                new Owner
                {
                    Name = "Bob",
                    Gender = Male,
                    Age = 23,
                    Pets = new Pet[]
                    {
                        new Pet
                        {
                            Name = "Garfield",
                            PetType = Cat
                        },
                        new Pet
                        {
                            Name = "Fido",
                            PetType = Dog
                        }
                    }
                },
                new Owner
                {
                    Name = "Jennifer",
                    Gender = Female,
                    Age = 18,
                    Pets = new Pet[]
                    {
                        new Pet
                        {
                            Name = "Garfield",
                            PetType = Cat
                        }
                    }
                },
                new Owner
                {
                    Name = "Steve",
                    Gender = Male,
                    Age = 45,
                },
                new Owner
                {
                    Name = "Fred",
                    Gender = Male,
                    Age = 40,
                    Pets = new Pet[]
                    {
                        new Pet
                        {
                            Name = "Tom",
                            PetType = Cat
                        },
                        new Pet
                        {
                            Name = "Max",
                            PetType = Cat,
                        },
                        new Pet
                        {
                            Name = "Sam",
                            PetType = Dog
                        },
                        new Pet
                        {
                            Name = "Jim",
                            PetType = Cat
                        }
                    }
                },
                new Owner
                {
                    Name = "Samantha",
                    Gender = Female,
                    Age = 40,
                    Pets = new Pet[]
                    {
                        new Pet
                        {
                            Name = "Tabby",
                            PetType = Cat
                        }
                    }
                },
                new Owner
                {
                    Name = "Alice",
                    Gender = Female,
                    Age = 64,
                    Pets = new Pet[]
                    {
                        new Pet
                        {
                            Name = "Simba",
                            PetType = Cat
                        },
                        new Pet
                        {
                            Name = "Nemo",
                            PetType = Fish
                        }
                    }
                }
            };

            //act
            var result = _targetClass.All().Result;
            _listener.Stop();

            var resultArray = result.ToArray();
            // assert

            CollectionAssert.AreEqual(expected, resultArray, _ownerComparer);
        }

        [TestMethod]
        public void All_Empty_Null()
        {
            //arrange
            Initialize("empty.json");
            Owner[] expected = null;

            //act
            var result = _targetClass.All().Result;
            _listener.Stop();

            // assert

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public async Task All_WrongPetType_JsonSerializationException()
        {
            //arrange
            Initialize("wrongPetType.json");

            //act
            try
            {
                var result = await _targetClass.All();
            }
            finally
            {
                _listener.Stop();
            }

            // assert ExpectedException
        }
    }
}
