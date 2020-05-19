//using System;
//using VareDatabase.DBContext;
//using Xunit;
//using Moq;
//using Microsoft.EntityFrameworkCore;
//using VareDatabase.Repo;

//namespace BackEndUnitTest
//{
//    public class RepositoryTests
//    {
//        [Fact]
//        public void Add_ProperMethodCalled()
//        {
//            var testModel = new DbTestModel();
//            var context = new Mock<VareDataModelContext>();
//            var dbSetMock = new Mock<DbSet<DbTestModel>>();

//            context.Setup(x => x.Set<DbTestModel>()).Returns(dbSetMock.Object);
//            dbSetMock.Setup(x => x.Add(It.IsAny<DbTestModel>())).Returns(testModel);

//            //act
//            var repo = new Repository<DbTestModel>(context.Object);
//            repo.Add(testModel);

//            //assert
//            context.Verify(x => x.Set<DbTestModel>());
//            dbSetMock.Verify(x => x.Add(It.Is<DbTestModel>(y => y == testModel)));
//        }
//    }
//}
