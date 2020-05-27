using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;
using Xunit;

namespace unit.test.orctioneer.LoginTests.RepoTests
{

    public class unit_test_UsernameRepo

    {
    private UsernameRepository _uut;
    private SqliteConnection userdb;
    private SqliteConnection passdb;

    private SqliteConnection walletdb;

    public unit_test_UsernameRepo()
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

        _uut = new UsernameRepository(userContext);
    }

    [Fact]
    public async void unit_test_addUser()
    {
        //Arrange
        UsernameEntity test = new UsernameEntity()
        {
            Username = "Test",
            Email = "Test@test.dk",
            UserId = "myid"
        };
        //Act
        var wait = await _uut.addUser(test);
        var result = await _uut.validateUsername(test.Username);
        //Assert
        Xunit.Assert.NotEmpty(result);
    }

    [Theory]
    [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef","Admin")]
    [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190","TestUser1")]
    public async void unit_test_validateUser(string id, string name)
    {
        //Arrange
        
        //Act
        var result = await _uut.validateUsername(name);
        //Assert
        Xunit.Assert.Equal(id,result);
    }
    
    [Theory]
    [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef","Admin@Orctioneer.com")]
    [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190","Test@Orctioneer.com")]
    public async void unit_test_validateEmail(string id, string email)
    {
        //Arrange
        
        //Act
        var result = await _uut.validateEmail(email);
        //Assert
        Xunit.Assert.Equal(id,result);
    }

    [Theory]
    [InlineData("Admin",false)]
    [InlineData("TestUser1",false)]
    [InlineData("test",true)]
    [InlineData("NotHere",true)]
    public async void unit_test_checkUser_name(string condition, bool exp)
    {
        //Arrange
        UsernameEntity test = new UsernameEntity()
        {
            Username = condition,
            Email = null
        };
        //Act
        var result = await _uut.CheckUser(test);
        //Assert
        Xunit.Assert.Equal(exp,result);
    }
    [Theory]
    [InlineData("Admin@Orctioneer.com",false)]
    [InlineData("Test@Orctioneer.com",false)]
    [InlineData("test@test.com",true)]
    [InlineData("NotHere@myTest.com",true)]
    public async void unit_test_checkUser_email(string condition, bool exp)
    {
        //Arrange
        UsernameEntity test = new UsernameEntity()
        {
            Username = null,
            Email = condition
        };
        //Act
        var result = await _uut.CheckUser(test);
        //Assert
        Xunit.Assert.Equal(exp,result);
    }

    [Theory]
    [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef", "newUser1")]
    [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190", "FormerTestUser1")]
    public async void unit_test_updateUsername(string id, string username)
    {
        //Arrange
        UsernameEntity test = new UsernameEntity()
        {
            UserId = id,
            Username = username
        };
        //Act
        var update = await _uut.updateUsername(test);
        var result = await _uut.validateUsername(test.Username);
        //Assert
        Xunit.Assert.Equal(id,result);
    }
    
    [Theory]
    [InlineData("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef","test@Orctioneer.com")]
    [InlineData("f2aac55f-1cba-404e-8a2b-b3e65c438190","testing@Orctioneer.com")]
    public async void unit_test_updateEmail(string id, string email)
    {
        //Arrange
        UsernameEntity test = new UsernameEntity()
        {
            UserId = id,
            Email = email
        };
        //Act
        var update = await _uut.updateEmail(test);
        var result = await _uut.validateEmail(test.Email);
        //Assert
        Xunit.Assert.Equal(id,result);
    }

    [Fact]
    public async void unit_test_DeleteUser()
    {
        //Arrange
        
        //Act
        var delete = await _uut.DeleteUser("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
        var result = await _uut.validateUsername("f8ac5f4b-d637-4bc4-acd2-cd940663f3ef");
        //Assert
        Xunit.Assert.Equal(null,result);
    }
    }
}