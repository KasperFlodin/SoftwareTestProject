using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SoftwareTestProjectTests
{
    public class AuthenticationTest
    {
        [Fact]
        public void AuthenticationView()
        {
            // testing our Home page for a markUpMatch
            //Arange
            var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("");

            //Act
            var cut = ctx.RenderComponent<Home>();

            //Assert
            cut.MarkupMatches("<h1>Hello, world!</h1>");
        }

        [Fact]
        public void Test_ConnectionString()
        {
            // Testing our connection string
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Act
            bool isConnected = false;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    isConnected = true;
                }
                catch (Exception)
                {
                    isConnected = false;
                }
            }
            // Assert
            Assert.True(isConnected, "Failed to connect to the database using the connection string.");
        }

    }

}
