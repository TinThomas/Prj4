using System;
using VareDatabase.DBContext;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using VareDatabase.Repo;
using VareDatabase.Models;
using VareDatabase.Repo.Auction;
using Shouldly;
using System.Collections.Generic;

namespace BackEndUnitTest
{
    public class RepositoryTests
    {
        [Fact]
        public void AddItem_Calls_Repository()
        {
            var testObject = new DbTestModel { Id = 1 };
            var context = new Mock<VareDataModelContext>();

            var mockRepo = new Mock<Repository<DbTestModel>>(context.Object);
            mockRepo.Setup(x => x.Add(testObject));
            mockRepo.Object.Add(testObject);
            //assert
            mockRepo.Verify(x => x.Add(testObject), Times.Once());
        }
        [Fact]
        public void GetById_Returns_TestObject()
        {
            var testObject = new DbTestModel { Id = 1 };
            var context = new Mock<VareDataModelContext>();
            var mockRepo = new Mock<Repository<DbTestModel>>(context.Object);
            //act
            mockRepo.Setup(x => x.Get(1)).Returns(testObject);

            //assert 
            mockRepo.Object.Get(1).ShouldBe(testObject);
            mockRepo.Verify(x => x.Get(testObject.Id), Times.Once());
        }
        [Fact]
        public void Remove_ProperMethodCalled()
        {
            var testObject = new DbTestModel { Id = 1 };
            var context = new Mock<VareDataModelContext>();
            var mockRepo = new Mock<Repository<DbTestModel>>(context.Object);

            //act
            mockRepo.Setup(x => x.Delete(testObject));
            mockRepo.Object.Delete(testObject);
            mockRepo.Setup(x => x.Get(1));

            //assert
            mockRepo.Object.Get(1).ShouldBe(null);
            mockRepo.Verify(x => x.Delete(testObject), Times.Once());
        }
        [Fact]
        public void GetAll_ReturnsAllObjects()
        {
            var testObject = new DbTestModel() { Id = 1 };
            var testList = new List<DbTestModel>() { testObject };

            var context = new Mock<VareDataModelContext>();
            var mockRepo = new Mock<Repository<DbTestModel>>(context.Object);
            //act
            mockRepo.Setup(x => x.Get()).Returns(testList);

            //assert 
            mockRepo.Object.Get().ShouldContain(testObject);
            mockRepo.Verify(x => x.Get(), Times.Once());
        }
        [Fact]
        public void Update_ObjectPropertyChanged()
        {
            var testObject = new DbTestModel() { Id = 1, Property = false };
            var originalTestObject = new DbTestModel();
            originalTestObject = testObject;
            var context = new Mock<VareDataModelContext>();
            var mockRepo = new Mock<Repository<DbTestModel>>(context.Object);
            //act
            testObject.Property = true;
            mockRepo.Setup(x => x.Update(testObject));
            mockRepo.Object.Update(testObject);
            mockRepo.Setup(x => x.Get(1)).Returns(testObject);
            //assert
            mockRepo.Object.Get(1).Property.ShouldBeTrue();
            
            mockRepo.Verify(x => x.Update(testObject), Times.Once());
        }
    }
}
