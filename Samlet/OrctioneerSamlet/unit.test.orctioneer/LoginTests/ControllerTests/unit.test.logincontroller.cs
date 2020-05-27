using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet.Interfaces.Login;
using Microsoft.Extensions.Configuration;
using Moq;
using NSubstitute;
using OrctioneerSamlet;
using OrctioneerSamlet.Controllers;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using Xunit;

namespace unit.test.orctioneer
{
    public class unit_test_logincontroller
    {
        private IConfiguration _configuraion;
        private LoginController _uut;
        
        //In memory dbs for testing.
        private SqliteConnection userdb;
        private SqliteConnection passdb;
        private SqliteConnection walletdb;

        //Setup
        public unit_test_logincontroller()
        {
            //Adding config to tests. 
            var confbuilder = new ConfigurationBuilder();
            confbuilder.AddJsonFile("appsettings.json");
            _configuraion = confbuilder.Build();
            
            //Setting up In memory dbs.
            userdb = new SqliteConnection("DataSource=:memory:");
            passdb = new SqliteConnection("DataSource=:memory:");
            walletdb = new SqliteConnection("DataSource=:memory:");
            userdb.Open();
            passdb.Open();
            walletdb.Open();
            
            //Setting up contexts.
            var userbuild = new DbContextOptionsBuilder<UserModelContext>()
                .UseSqlite(userdb).Options;
            var passbuild = new DbContextOptionsBuilder<PassModelContext>()
                .UseSqlite(passdb).Options;
            var walletbuild = new DbContextOptionsBuilder<WalletContext>()
                .UseSqlite(walletdb).Options;
            var userContext = new UserModelContext(userbuild);
            var passContext = new PassModelContext(passbuild);
            var walletContext = new WalletContext(walletbuild);
            //Drop and create
            userContext.Database.EnsureDeleted();
            userContext.Database.EnsureCreated();
            passContext.Database.EnsureDeleted();
            passContext.Database.EnsureCreated();
            walletContext.Database.EnsureDeleted();
            walletContext.Database.EnsureCreated();
            //Seeding data to test on
            SeedUsers.seedUsers(userContext,passContext,walletContext);
            
            //Setting uut
            _uut = new LoginController(_configuraion,passContext,userContext);
        }

        [Theory]
        [InlineData("Admin")]
        [InlineData("TestUser1")]
        public async void unit_test_PostUsername_Username_Succes(string name)
        {
            //Arrange
            UsernameEntity test = new UsernameEntity()
            {
                Username = name,
                Email = null
            };
            //Act
            var result = await _uut.PostUsername(test);
            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async void unit_test_PostUsername_Username_Fail()
        {
            UsernameEntity test = new UsernameEntity()
            {
                Username = "Test",
                Email = null
            };
            //Act
            var result = await _uut.PostUsername(test);
            //Assert
            Xunit.Assert.IsType<BadRequestResult>(result);
        }
        [Theory]
        [InlineData("Admin")]
        [InlineData("TestUser1")]
        public async void unit_test_PostUsername_Username_ReturnContent(string name)
        {
            UsernameEntity test = new UsernameEntity()
            {
                Username = name,
                Email = null
            };
            //Act
            var result = await _uut.PostUsername(test);
            var okResult = result as OkObjectResult;
            var content = okResult.Value as string;
            //Assert
            Xunit.Assert.Equal(36,content.Length);
        }

        [Fact]
        public async void unit_test_PostUsername_Username_string_content()
        {
            //Arrange
            var control = "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef";
            UsernameEntity test = new UsernameEntity()
            {
                Username = "Admin",
                Email = null
            };
            //Act
            var result = await _uut.PostUsername(test);
            var okResult = result as OkObjectResult;
            var content = okResult.Value as string;
            
            //Assert
            Xunit.Assert.Equal(control,content);
        }
        
        [Theory]
        [InlineData("Admin@Orctioneer.com")]
        [InlineData("Test@Orctioneer.com")]
        public async void unit_test_PostUsername_Email_Succes(string email)
        {
            //Arrange
            UsernameEntity test = new UsernameEntity()
            {
                Username = null,
                Email = email
            };
            //Act
            var result = await _uut.PostUsername(test);
            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void unit_test_PostUsername_Email_Fail()
        {
            //Arrange
            UsernameEntity test = new UsernameEntity()
            {
                Username = null,
                Email = "Test@Test.com"
            };
            //Act
            var result = await _uut.PostUsername(test);
            //Assert
            Xunit.Assert.IsType<BadRequestResult>(result);
        }
        [Theory]
        [InlineData("Admin@Orctioneer.com")]
        [InlineData("Test@Orctioneer.com")]
        public async void unit_test_PostUsername_Email_ReturnContent(string email)
        {
            //Arrange
            UsernameEntity test = new UsernameEntity()
            {
                Username = null,
                Email = email
            };
            //Act
            var result = await _uut.PostUsername(test);
            var okResult = result as OkObjectResult;
            var content = okResult.Value as string;
            //Assert
            Xunit.Assert.Equal(36,content.Length);
        }

        [Theory]
        [InlineData("Admin", "Password1")]
        [InlineData("TestUser1", "TestKode1")]
        public async void unit_test_PostPassword_Success(string username, string pass)
        {
            //Arrange
            UsernameEntity user = new UsernameEntity() {Username = username};
            //Act
            var PostUser = await _uut.PostUsername(user);
            var OkUser = PostUser as OkObjectResult;
            var userId = OkUser.Value as string;
            PasswordEntity test = new PasswordEntity() {Password = pass, UserId = userId};

            var result = await _uut.PostPassword(test);
            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async void unit_test_PostPassword_fail()
        {
            //Arrange
            UsernameEntity user = new UsernameEntity() {Username = "Admin"};
            //Act
            var PostUser = await _uut.PostUsername(user);
            var OkUser = PostUser as OkObjectResult;
            var userId = OkUser.Value as string;
            PasswordEntity test = new PasswordEntity() {Password = "Testing", UserId = userId};

            var result = await _uut.PostPassword(test);
            //Assert
            Xunit.Assert.IsType<BadRequestResult>(result);
        }
        
        [Theory]
        [InlineData("Admin", "Password1")]
        [InlineData("TestUser1", "TestKode1")]
        public async void unit_test_PostPassword_Success_Content(string username, string pass)
        {
            //Arrange
            UsernameEntity user = new UsernameEntity() {Username = username};
            //Act
            var PostUser = await _uut.PostUsername(user);
            var OkUser = PostUser as OkObjectResult;
            var userId = OkUser.Value as string;
            PasswordEntity test = new PasswordEntity() {Password = pass, UserId = userId};

            var result = await _uut.PostPassword(test);
            var okResult = result as OkObjectResult;
            var JWT = okResult.Value as string;
            //Assert
            Xunit.Assert.Equal(316,JWT.Length);

        }
    }
}