using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;
using Xunit;

namespace unit.test.orctioneer.LoginTests.RepoTests
{
    public class unit_test_WalletRepo
    {
        private WalletRepository _uut;
        
        private SqliteConnection userdb;
        private SqliteConnection passdb;
        private SqliteConnection walletdb;

        public unit_test_WalletRepo()
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
            
            _uut = new WalletRepository(walletContext);
        }

        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef",5000)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190",10000)]
        [InlineData("",0)]
        public async void unit_test_getAmount(string name, int amount)
        {
            //Arrange
            //Act
            var result = await _uut.getAmount(name);
            //Assert
            Xunit.Assert.Equal(amount,result);
        }
        
        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef",1000,6000)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190",4000,14000)]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef",-1000,4000)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190",-4000,6000)]
        public async void unit_test_setAmount(string name, int amount,int total)
        {
            //Arrange
            //Act
             await _uut.setAmount(name,amount);
             var result = await _uut.getAmount(name);
            //Assert
            Xunit.Assert.Equal(total,result);
        }

        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef")]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190")]
        public async void unit_test_getDetails(string name)
        {
            //Arrange
            
            //Act
            var result = await _uut.getDetails(name);
            //Assert
            Xunit.Assert.NotNull(result);
        }
        
        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef", 5000)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190",10000)]
        public async void unit_test_getDetails_amount(string name,int amount)
        {
            //Arrange
            
            //Act
            var result = await _uut.getDetails(name);
            //Assert
            Xunit.Assert.Equal(amount,result.Amount);
        }

        [Fact]
        public async void unit_test_addWallet()
        {
            //Arrange
            WalletEntity test = new WalletEntity()
            {
                Amount = 100,
                card = new CardEntity()
                {
                    CardNumber = 10000,
                    CVVnumber = 321,
                    ExpireMonth = 02,
                    ExpireYear = 32
                },
                userID = "test"
            };
            //Act
            _uut.addWallet(test);
            var result = await _uut.getAmount("test");
            //Assert
            Xunit.Assert.Equal(test.Amount,result);
        }

        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef", 5000)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190",10000)]
        public async void unit_test_updateWallet(string name, int amount)
        {
            //Arrange
            WalletEntity test = new WalletEntity()
            {
                Amount = amount,
                card = new CardEntity()
                {
                    CardId = 1,
                    CardNumber = 20202020202020,
                    CVVnumber = 200,
                    ExpireMonth = 10,
                    ExpireYear = 24
                },
                userID = name
            };
            //Act
            var result = await _uut.updateWallet(test);
            //Assert
            //forventer 2 da det er 2 DbSets der skal arbejds på. 
            Xunit.Assert.Equal(2,result);
        }

        [Fact]
        public async void unit_test_updateWallet_checkContent()
        {
            //Arrange
            WalletEntity test = new WalletEntity()
            {
                Amount = 2000,
                card = new CardEntity()
                {
                    CardId = 1,
                    CardNumber = 20202020202020,
                    CVVnumber = 200,
                    ExpireMonth = 10,
                    ExpireYear = 24
                },
                userID = "f8ac5f4b-d637-4bc4-acd2-cd940663f3ef"
            };

            //Act
            var update = await _uut.updateWallet(test);
            var result = await _uut.getDetails("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
            //Assert
            Xunit.Assert.Equal(test.card.CardNumber,result.card.CardNumber);
        }
        
    }
}