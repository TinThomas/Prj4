﻿using System;
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
using System.Security.Principal;

namespace VareDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemEntityController : Controller
    {
        private string json;
        private AuctionUnitOfWork unitOfWork;
        private IItemRepository _dbLogic;

        private JsonSerializerSettings serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public ItemEntityController()
        {
            unitOfWork = new AuctionUnitOfWork();
            _dbLogic = unitOfWork.ItemRepository;
        }

        [HttpGet]
        [Route("item/{id?}")]
        //Get on ID
        public ActionResult<string> GetItem(int id)
        {
            json = JsonConvert.SerializeObject(_dbLogic.Get(id), Formatting.Indented, serializerSettings);
            return json;
        }

        [HttpGet]
        [Route("item")]
        public ActionResult<string> GetAllItems()
        {
            var items = _dbLogic.Get();
            json = JsonConvert.SerializeObject(items, Formatting.Indented, serializerSettings);
            return json;
        }
        //populær
        [HttpGet]
        [Route("item/pop")]
        public ActionResult<string> GetPopularItems()
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetMostPopularItems(), Formatting.Indented, serializerSettings);
            return json;
        }
        //newest
        [HttpGet]
        [Route("item/new")]
        public ActionResult<string> GetNewestItems()
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetNewestFirst(), Formatting.Indented, serializerSettings);
            return json;
        }
        //about to expire
        [HttpGet]
        [Route("item/expire")]
        public ActionResult<string> GetExpiringItems()
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetExpiringFirst(), Formatting.Indented, serializerSettings);
            return json;
        }
        [HttpGet]
        [Route("item/tag/{search?}")]
        public ActionResult<string> GetTag(string search)
        {
            json = JsonConvert.SerializeObject(_dbLogic.Search(search), Formatting.Indented, serializerSettings);
            return json;
        }
        
        //Post = Create
        [HttpPost("item")]
        public async Task<IActionResult> CreateEntity([FromBody]ItemEntity item)
        {
            Console.WriteLine("Adding item with title: " + item.Title);
            item.DateCreated = DateTime.Now;
            _dbLogic.Add(item);
            _dbLogic.GenerateTags(item);
            unitOfWork.Commit();
            return Ok(item);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(ItemEntity item)
        { 
            _dbLogic.Delete(item);
            unitOfWork.Commit();
            return Ok();
        }
        [HttpPost("CreateImage")]
        public async Task<ActionResult<string>> UploadPicture(IFormFile file)
        {

            string path = await _dbLogic.UploadPicture(file);
            return path;
        }
        [HttpGet("GetPicture")]
        public async Task<ActionResult<string>> LoadPicture(string path)
        {
            Byte[] b = System.IO.File.ReadAllBytes(path);
            return File(b, "image/jpg");
        }
    }
}
