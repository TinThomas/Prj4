using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute.ReturnsExtensions;
using OrctioneerSamlet;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;
using Xunit;

namespace unit.test.orctioneer.LoginTests.RepoTests
{

    public class unit_test_AddressRepo
    {
        
        private AddressRepository _uut;
        
        private SqliteConnection userdb;
        private SqliteConnection passdb;
        private SqliteConnection walletdb;

        public unit_test_AddressRepo()
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
            SeedUsers.seedUsers(userContext, passContext, walletContext);

            _uut = new AddressRepository(walletContext);
        }

        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef",true)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190",true)]
        [InlineData("Test-id-0",false)]
        public async void unit_test_getAddress(string id, bool exp)
        {
            //Arrange
            
            //Act
            var result = await _uut.GetAddress(id);
            //Assert
            Xunit.Assert.Equal(exp,result != null);
        }
        
        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef","Randomstreet 200")]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190","CoolStreet")]
        public async void unit_test_getAddress_value(string id, string address)
        {
            //Arrange
            
            //Act
            var result = await _uut.GetAddress(id);
            //Assert
            Xunit.Assert.Equal(address,result.Address);
        }

        [Fact]
        public async void unit_test_addAddress()
        {
            //Arrange
            AddressEntity test = new AddressEntity()
            {
                Address = "TestStreet",
                City = "TestTown",
                Contry = "TestContry",
                Zipcode = 1234
            };
            //Act
            var create = await _uut.addAddress(test, "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            var result = await _uut.GetAddress("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            //Assert
            Xunit.Assert.Equal(test.Address,result.Address);
        }
        
        [Fact]
        public async void unit_test_updateAddress()
        {
            //Arrange
            AddressEntity test = new AddressEntity()
            {
                Address = "TestStreet",
                City = "TestTown",
                Contry = "TestContry",
                Zipcode = 1234
            };
            //Act
            var create = await _uut.updateAddress(test, "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            var result = await _uut.GetAddress("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            //Assert
            Xunit.Assert.Equal(test.Address,result.Address);
        }
        
        [Fact]
        public async void unit_test_DeleteAddress()
        {
            //Arrange

            //Act
            var create = await _uut.DeleteAddress("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            var result = await _uut.GetAddress("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            //Assert
            Xunit.Assert.Equal(null,result);
        }
    }
}