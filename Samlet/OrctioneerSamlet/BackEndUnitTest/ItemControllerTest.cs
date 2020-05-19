using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VareDatabase.Controllers;
using VareDatabase.Interfaces;
using VareDatabase.Repo;
using VareDatabase.Models;
using Xunit;
using Newtonsoft.Json;

namespace BackEndUnitTest
{
    
    public class ItemControllerTest
    {
        private ItemEntityController uut;
        string json;

        [Fact]
        public void TestGetItem()
        {
            //Arange
            var uut = new ItemEntityController();
            var mock = new Mock<IItemRepository>();
            mock.Setup(x => x.Get(It.IsAny<int>())).Returns(new ItemEntity());


            var Item1 = new ItemEntity()
            {
                ItemId = 1,
                Title = "Elven Bow BUY NOW",
                BuyOutPrice = 5000 ,
                ExpirationDate = new DateTime(2020,12,24,12,0,0,DateTimeKind.Utc), //"12 / 24 / 2020 12:00:00 AM ",
                DateCreated = new DateTime(2020,5,13,7,02,24,DateTimeKind.Utc),  //5/13/2020 7:02:24 PM
                UserIdSeller = 12,
                DescriptionOfItem = "Powerful bow that is best at the range of 30 - 50 meters",
                Sold = false,
                Image = null
            };

            json = JsonConvert.SerializeObject(Item1, Formatting.Indented);

            //Act
            var result = uut.GetItem(1);
            
            //Assert
            Assert.Equal(result,json);
        }
    }
}
