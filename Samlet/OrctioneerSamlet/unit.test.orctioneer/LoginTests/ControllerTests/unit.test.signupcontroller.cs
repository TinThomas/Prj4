using System.Collections.Generic;
using System.IO;
using LoginVue.Controllers;
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

        //Setup
        public unit_test_signupcontroller()
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
            Xunit.Assert.IsType<BadRequestResult>(result);
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
            Xunit.Assert.IsType<BadRequestResult>(result);
        }
    }
}