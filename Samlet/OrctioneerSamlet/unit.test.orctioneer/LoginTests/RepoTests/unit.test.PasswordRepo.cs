using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;
using Xunit;


namespace unit.test.orctioneer.LoginTests.RepoTests
{
    
    public class unit_test_PasswordRepo
    {
        private PasswordRepository _uut;
        
        private SqliteConnection userdb;
        private SqliteConnection passdb;
        private SqliteConnection walletdb;

        public unit_test_PasswordRepo()
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

            _uut = new PasswordRepository(passContext);
        }

        [Theory]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef", "Password1", true)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190", "TestKode1", true)]
        [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190", "Testing", false)]
        [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef", "YouShallNotPass", false)]
        [InlineData("Invalid-UserId", "Password1", false)]
        public async void unit_test_ValidatePassword(string id, string password, bool exp)
        {
            //Arrange
            PasswordEntity test = new PasswordEntity()
            {
                UserId = id,
                Password = password
            };
            //Act
            var result = await _uut.validatePassword(test);
            //Assert
            Xunit.Assert.Equal(exp,result);
        }

        [Fact]
        public async void unit_test_CreatePassword()
        {
            //Arrange
            PasswordEntity test = new PasswordEntity()
            {
                UserId = "Test-id-1",
                Password = BCrypt.Net.BCrypt.HashPassword("TestPass")
            };
            PasswordEntity control = new PasswordEntity()
            {
                UserId = "Test-id-1",
                Password = "TestPass"
            };
            //Act
            var create = await _uut.CreatePassword(test);
            var result = await _uut.validatePassword(control);
            //Assert
            Xunit.Assert.Equal(true,result);
        }

        [Fact]
        public async void unit_test_DeletePassword()
        {
            //Arrange
            PasswordEntity test = new PasswordEntity()
            {
                UserId = "Test-id-1",
                Password = BCrypt.Net.BCrypt.HashPassword("TestPass")
            };
            PasswordEntity control = new PasswordEntity()
            {
                UserId = "Test-id-1",
                Password = "TestPass"
            };
            //Act 
            var create = await _uut.CreatePassword(test);
            var delete = await _uut.DeletePassword(control.UserId);
            var result = await _uut.validatePassword(control);
            //Assert
            Xunit.Assert.Equal(false,result);
        }

        [Fact]
        public async void unit_test_updatePassword()
        {
            //Arrange
            PasswordEntity test = new PasswordEntity()
            {
                UserId = "Test-id-1",
                Password = BCrypt.Net.BCrypt.HashPassword("TestPass")
            };
            PasswordEntity control = new PasswordEntity()
            {
                UserId = "Test-id-1",
                Password = "UpdatedPass"
            };
            //Act 
            var create = await _uut.CreatePassword(test);
            test.Password = BCrypt.Net.BCrypt.HashPassword("UpdatedPass");
            var update = await _uut.updatePassword(test);
            var result = await _uut.validatePassword(control);
            //Assert
            Xunit.Assert.Equal(true,result);
        }


    }
}