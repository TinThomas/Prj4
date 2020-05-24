using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Routing;
using LoginVue.Controllers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrctioneerSamlet;
using OrctioneerSamlet.Controllers;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using Xunit;

namespace unit.test.orctioneer
{
    public class unit_test_walletcontroller
    {
        private WalletController _uut;

        //In memory dbs for testing.
        private SqliteConnection userdb;
        private SqliteConnection passdb;
        private SqliteConnection walletdb;

        //Setup
        public unit_test_walletcontroller()
        {
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
            _uut = new WalletController(walletContext);
        }

        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef",5000)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190",10000)]
        public async void unit_test_getBalance(string uid, int amount)
        {
            //Arrange
            var context = new Mock<HttpContext>();
            var identity = new GenericIdentity("");
            identity.AddClaim(new Claim(ClaimTypes.Name, uid));
            var principal = new GenericPrincipal(identity,new[]{""});
            context.Setup(s => s.User).Returns(principal);

            _uut.ControllerContext.HttpContext = context.Object;
            WalletEntity test = new WalletEntity()
            {
                Amount = amount
            };

            //Act
            _uut.CreateWallet(test);
            var result = await _uut.getBalance();
            var okResult = result as OkObjectResult;
            var content = okResult.Value;

            //Assert
            Xunit.Assert.Equal(amount,content);
        }

        [Fact]
        public async void Create()
        {
            //Arrange
            var context = new Mock<HttpContext>();
            var identity = new GenericIdentity("test");
            identity.AddClaim(new Claim(ClaimTypes.Name, "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef"));
            var principal = new GenericPrincipal(identity,new[]{"User"});
            context.Setup(s => s.User).Returns(principal);

            _uut.ControllerContext.HttpContext = context.Object;
            WalletEntity test = new WalletEntity()
            {
                Amount = 2000
            };
            //Act
            var result = await _uut.CreateWallet(test);

            //Assert
            Xunit.Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void unit_test_addMoney()
        {
   
            //Arrange
            var context = new Mock<HttpContext>();
            var identity = new GenericIdentity("test");
            identity.AddClaim(new Claim(ClaimTypes.Name, "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef"));
            var principal = new GenericPrincipal(identity,new[]{"User"});
            context.Setup(s => s.User).Returns(principal);
            _uut.ControllerContext.HttpContext = context.Object;
            
            WalletEntity test = new WalletEntity()
            {
                Amount = 2000
            };
            //Act
            _uut.CreateWallet(test);
            test.Amount = 6000;
            _uut.addMoney(test);
            var result = await _uut.getBalance();
            var okResult = result as OkObjectResult;
            var content = okResult.Value;
            //Assert
            Xunit.Assert.Equal(8000,content);
        }

        [Fact]
        public async void unit_test_update()
        {
            //Arrange
            var context = new Mock<HttpContext>();
            var identity = new GenericIdentity("test");
            identity.AddClaim(new Claim(ClaimTypes.Name, "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef"));
            var principal = new GenericPrincipal(identity,new[]{"User"});
            context.Setup(s => s.User).Returns(principal);
            _uut.ControllerContext.HttpContext = context.Object;
            
            WalletEntity test = new WalletEntity()
            {
                Amount = 0,
                card = new CardEntity()
                {
                    CardNumber = 10210310310310,
                    ExpireMonth = 02,
                    ExpireYear = 2052,
                    CVVnumber = 211
                }
            };
            
            //Act
            var result = await _uut.UpdateWallet(test);
            
            //Assert
            Xunit.Assert.IsType<OkResult>(result);
            
        }
    }
}