using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VareDatabase.Repo;
using VareDatabase.Interfaces;
using VareDatabase.Models;
using VareDatabase.Repo.Auction;

namespace VareDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemEntityController : Controller
    {
        private DatabaseLogic _dbLogic;
        private string json;

        public ItemEntityController()
        {
            var db = new DBContext.VareDataModelContext();
            IItemRepository repo = new ItemRepository(db);
            var unit = new AuctionUnitOfWork(db);
            var dbLogic = new DatabaseLogic(unit, repo);
            _dbLogic = dbLogic;
        }

        [HttpGet]
        [Route("Home/item/{id?}")]
        //Get on ID
        public ActionResult<string> GetItem(int id)
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetSingle(id), Formatting.Indented);
            return json;
        }

        [HttpGet]
        [Route("Home/item")]
        public ActionResult<string> GetAllItems()
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetAll(), Formatting.Indented);
            return json;
        }
        //populær
        //[HttpGet]
        //[Route("Home/item/pop")]
        //public ActionResult<string> GetPopularItems()
        //{
        //    var items = _dbLogic.GetAll();
        //    List<ItemEntity> popularItems = new List<ItemEntity>();
        //    foreach(var item in items)
        //    {
        //        if(popularItems.Count == 0 && item.Bids.Count > 0)
        //        {
        //            popularItems.Add(item);
        //        }
        //        else if(item.Bids.Count > popularItems.Last().Bids.Count && item.Bids.Count > 0)
        //        {
        //            //iterate down to find right index to insert
        //            for(int i = 0; i < popularItems.Count; i++)
        //            {
        //                if(popularItems[i].Bids.Count < item.Bids.Count)
        //                {
        //                    var temp = popularItems[i];
        //                    popularItems[i] = item;
        //                    popularItems[i] = temp;

        //                }
        //            }
        //        }
        //        else if(popularItems.Count < 10)
        //        {
        //            popularItems.Add(item);
        //        }
        //    }
        //    json = JsonConvert.SerializeObject(popularItems, Formatting.Indented);
        //    return json;
        //}
        //newest
        [HttpGet]
        [Route("Home/item/new")]
        public ActionResult<string> GetNewestItems()
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetAll(), Formatting.Indented);
            return json;
        }
        //about to expire
        [HttpGet]
        [Route("Home/item/expire")]
        public ActionResult<string> GetExpiringItems()
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetAll(), Formatting.Indented);
            return json;
        }
        [HttpGet]
        [Route("Home/item/tag/{search?}")]
        public ActionResult<string> GetTag(string search)
        {
            json = JsonConvert.SerializeObject(_dbLogic.Search(search), Formatting.Indented);
            return json;
        }
        
        //Post = Create
        [HttpPost("Item")]
        public async Task<IActionResult> CreateEntity([FromBody]ItemEntity item)
        {
            Console.WriteLine("Adding item with title: " + item.Title);
            _dbLogic.AddItem(item);
            _dbLogic.Save();

            return Ok(item);
        }

        [HttpPut]
        //Update eller replace
        public void EditItemEntity(int id, ItemEntity item)
        {
            //En eller anden update func MANGLER HER
            _dbLogic.Save();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(ItemEntity item)
        { 
            _dbLogic.Delete(item);
            _dbLogic.Save();
            return Ok();
        }
        [HttpPost("CreateImage")]
        public async Task<IActionResult> UploadPicture(IFormFile file)
        {
            if (file.Length > 0)
            {
                string imgFolder = @"..\images";
                string path = Path.Combine(imgFolder, file.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                string newFileName = Guid.NewGuid().ToString();
                System.IO.File.Move(path, Path.Combine(imgFolder,newFileName));
            }
            return Ok(file.Length);
        }
        [HttpGet("GetPicture")]
        public async Task<ActionResult<string>> LoadPicture(string path)
        {
            Byte[] b = System.IO.File.ReadAllBytes(path);
            return File(b, "image/jpg");
        }
    }
}
