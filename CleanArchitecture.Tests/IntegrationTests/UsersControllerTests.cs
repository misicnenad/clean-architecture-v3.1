using Autofac;
using Autofac.Extensions.DependencyInjection;

using CleanArchitecture.API;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Tests.IntegrationTests
{
    public class TasksControllerTests
    {
        private const string _apiUrl = "api/users";

        [Fact]
        public async Task CreateUser_ReturnsOk()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var hostBuilder = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHost(conf =>
                {
                    conf.UseTestServer();
                    conf.UseStartup<Startup>();
                    conf.UseEnvironment(Environments.Staging);
                    conf.UseConfiguration(config);
                    conf.ConfigureTestContainer<ContainerBuilder>(builder =>
                    {
                        var mockIdNumberService = new Mock<IIdNumberValidationService>();
                        mockIdNumberService.Setup(service => 
                                service.IsIdNumberValid(It.IsAny<string>()))
                            .ReturnsAsync(true);

                        builder.Register(c => mockIdNumberService.Object)
                            .As<IIdNumberValidationService>();
                    });
                });

            var host = hostBuilder.Start();
            var client = host.GetTestClient();


            // Act
            var validIdNumber = "12345";
            var response = await client.PostAsJsonAsync($"{_apiUrl}", new User
            {
                FirstName = "John",
                LastName = "Doe",
                IdNumber = validIdNumber
            });

            // Assert
            response.EnsureSuccessStatusCode();
        }

        //[Fact]
        //public async Task CreateUser_With_Invalid_ID_Number_Returns_BadRequest()
        //{
        //    // Arrange
        //    var config = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    var hostBuilder = new HostBuilder()
        //        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //        .ConfigureWebHost(conf =>
        //        {
        //            conf.UseTestServer();
        //            conf.UseStartup<Startup>();
        //            conf.UseEnvironment(Environments.Staging);
        //            conf.UseConfiguration(config);
        //        });

        //    var host = hostBuilder.Start();
        //    var client = host.GetTestClient();


        //    // Act
        //    var validIdNumber = "12345";
        //    var response = await client.PostAsJsonAsync($"{_apiUrl}", new User
        //    {
        //        FirstName = "John",
        //        LastName = "Doe",
        //        IdNumber = validIdNumber
        //    });

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //}
    }
}
