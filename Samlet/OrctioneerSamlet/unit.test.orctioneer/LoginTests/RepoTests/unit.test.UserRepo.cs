using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;
using Xunit;

namespace unit.test.orctioneer.LoginTests.RepoTests
{
    public class unit_test_UserRepo
    {
        private UserRepository _uut;
        
        private SqliteConnection userdb;
        private SqliteConnection passdb;
        private SqliteConnection walletdb;

        public unit_test_UserRepo()
        {
            //Setting up In memory dbs.
            userdb = new SqliteConnection("DataSource=:memory:");
            passdb = new SqliteConnection("DataSource=:memory:");
            walletdb = new SqliteConnection("DataSource=:memory:");
            userdb.Open();
            passdb.Open();
            walletdb.Open();
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
            
            _uut = new UserRepository(walletContext,userContext);
        }

        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef", "Svend")]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190", "Hans")]
        public async void unit_test_GetThisUser(string id, string name)
        {
            //Arrange
            
            //Act
            var result = await _uut.getThisUser(id);
            //Assert
            Xunit.Assert.Equal(name,result.FirstName);
        }

        [Fact]
        public async void unit_test_getUser()
        {
            //Arrange
            
            //Act
            var result = await _uut.getUser("Admin");
            //Assert
            Xunit.Assert.Equal("Hansen",result.LastName);
        }

        [Fact]
        public async void unit_test_getEmail()
        {
            //Arrange
            
            //Act
            var result = await _uut.getEmail("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            //Assert
            Xunit.Assert.Equal("Admin@Orctioneer.com",result);
        }

        [Fact]
        public async void unit_test_addUser()
        {
            //Arrange
            UserEntity test = new UserEntity()
            {
                Address = new AddressEntity()
                {
                    Address = "Test",
                    AddressId = 1,
                    City = "TestTown",
                    Contry = "TestLand",
                    Zipcode = 1234,
                },
                FirstName = "Testie",
                LastName = "McTest",
                userID = "abc",
                wallet = null
            };
            //Act
            var insert = await _uut.addUser(test);
            var result = await _uut.getThisUser("abc");
            //Assert
            Xunit.Assert.Equal("McTest",result.LastName);
        }
        
        [Fact]
        public async void unit_test_addUser_onAddress()
        {
            //Arrange
            UserEntity test = new UserEntity()
            {
                Address = new AddressEntity()
                {
                    Address = "Test",
                    AddressId = 1,
                    City = "TestTown",
                    Contry = "TestLand",
                    Zipcode = 1234,
                },
                FirstName = "Testie",
                LastName = "McTest",
                userID = "abc",
                wallet = null
            };
            //Act
            var insert = await _uut.addUser(test);
            var result = await _uut.getThisUser("abc");
            //Assert
            Xunit.Assert.Equal("TestTown",result.Address.City);
        }

        [Fact]
        public async void unit_test_updateUser()
        {
            //Arrange
            UserEntity test = new UserEntity()
            {
                FirstName = "Test",
                LastName = "McTest",
                userID = "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef"
            };
            //Act
            var update = _uut.updateUser(test);
            //Assert
        }

        [Fact]
        public async void unit_test_deleteUser()
        {
            //Arrange
            
            //Act
            var delete = await _uut.DeleteUser("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            var result = await _uut.getThisUser("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            //Assert
            Xunit.Assert.Equal(null,result);
        }
    }
}