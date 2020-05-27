using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using LoginVue.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet.Interfaces.Login;
using Microsoft.Extensions.Configuration;
using Moq;
using OrctioneerSamlet;
using OrctioneerSamlet.Controllers;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using Xunit;

namespace unit.test.orctioneer
{
    public class unit_test_signupcontroller
    {
        private SignupController _uut;

        //In memory dbs for testing.
        private SqliteConnection userdb;
        private SqliteConnection passdb;
        private SqliteConnection walletdb;
        
        private IConfiguration _configuraion;
        private LoginController supportLogin;

        //Setup
        public unit_test_signupcontroller()
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
            
            //Mocking login
            supportLogin = new LoginController(_configuraion,passContext,userContext);
            //Setting uut
            _uut = new SignupController(passContext,userContext);
        }

        [Fact]
        public async void unit_test_Create_success()
        {
            //Arrange
            HeaderRequest test = new HeaderRequest()
            {
                Username = "Test",
                Email = "Test@Test.com",
                Password = "TestTest1"
            };
            //Act
            var result = await _uut.Create(test);
            //Assert
            Xunit.Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void unit_test_Create_userexist()
        {
            //Arrange
            HeaderRequest test = new HeaderRequest()
            {
                Username = "TestUser1",
                Email = "Test@Test.com",
                Password = "TestTest1"
            };
            //Act
            var result = await _uut.Create(test);
            //Assert
            Xunit.Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void unit_test_Create_Emailexist()
        {
            //Arrange
            HeaderRequest test = new HeaderRequest()
            {
                Username = "TestEmail",
                Email = "Test@Orctioneer.com"
            };
            //Act
            var result = await _uut.Create(test);
            //Assert
            Xunit.Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void unit_test_update_Username()
        {
            //Arrange
            var context = new Mock<HttpContext>();
            var identity = new GenericIdentity("test");
            identity.AddClaim(new Claim(ClaimTypes.Name, "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef"));
            var principal = new GenericPrincipal(identity,new[]{"User"});
            context.Setup(s => s.User).Returns(principal);
            _uut.ControllerContext.HttpContext = context.Object;
            HeaderRequest setup = new HeaderRequest()
            {
                Username = "blank",
                Email = "Test@Test.com",
                Password = "HelloWorld"
            };
            HeaderRequest test = new HeaderRequest()
            {
                Username = "Testing",
                Email = null,
                Password = null
            };
            UsernameEntity objtoassert = new UsernameEntity()
            {
                Email = null,
                UserId = null,
                Username = test.Username
            };
            //Act
            var add = await _uut.Create(setup);
            var update = await _uut.Update(test);
            var result = await supportLogin.PostUsername(objtoassert);
            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }
    }
}