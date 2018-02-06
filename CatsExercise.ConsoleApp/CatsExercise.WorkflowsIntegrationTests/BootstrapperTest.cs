using CatsExercise.ConsoleApp;
using CatsExercise.Interfaces.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Unity;

namespace CatsExercise.WorkflowsIntegrationTests
{
    [TestClass]
    public class BootstrapperTest
    {
        private static HttpListener _listener;
        private Bootstrapper _targetClass;
        private const string _baseAddress = "http://localhost/TestData/";

        private const string _testDataFolder = "WorkflowTestData";

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

        private void Initialize(string testFileName)
        {
            var config = BuildConfiguration(testFileName);

            InitializeListener(config);

            _targetClass = new Bootstrapper();
            _targetClass.ConfigureContainer(config);
        }

        private void ConfigureContainerAndRun(IConfiguration configuration)
        {
            var container = new UnityContainer()
                .RegisterInstance(configuration)
                .RegisterSingleton<IContainerConfigurator, Services.ContainerConfigurator>("servicesConfigurator")
                .RegisterSingleton<IContainerConfigurator, Reporting.ContainerConfigurator>("reportingConfigurator")
                .RegisterSingleton<IContainerConfigurator, Workflows.ContainerConfigurator>("workflowsConfigurator");

            var solutionConfigurators = container.ResolveAll<IContainerConfigurator>();
            foreach (var configurator in solutionConfigurators)
            {
                configurator.RegisterInternalImplementations(container);
            }
        }


        [TestMethod]
        public async Task Bootstrapper_NormalInput_NormalOutput()
        {
            // arrange
            Initialize("people.json");
            var expected = "Male\r\n  -Garfield\r\n  -Jim\r\n  -Max\r\n  -Tom\r\nFemale\r\n  -Garfield\r\n  -Simba\r\n  -Tabby\r\n";

            // act
            var result = await _targetClass.Run();
            _listener.Stop();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Bootstrapper_Empty_ArgumentNullException()
        {
            // arrange
            Initialize("empty.json");

            // act
            try
            {
                var result = await _targetClass.Run();
            }
            finally
            {
                _listener.Stop();
            }

            // assert expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public async Task Bootstrapper_WrongPetType_NormalOutput()
        {
            // arrange
            Initialize("wrongPetType.json");

            // act
            try
            {
                var result = await _targetClass.Run();
            }
            finally
            {
                _listener.Stop();
            }

            // assert expected exception
        }
    }
}
