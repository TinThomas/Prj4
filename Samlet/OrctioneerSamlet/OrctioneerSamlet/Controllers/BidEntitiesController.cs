using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VareDatabase.DBContext;
using VareDatabase.Models;
using VareDatabase.Repo.Auction;
using VareDatabase.Interfaces;
using VareDatabase.Repo;
using VareDatabase.Interfaces.Auction;
using Newtonsoft.Json;

namespace VareDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidEntitiesController : Controller
    {
        private DatabaseLogic _dbLogic;
        private string json;
        private JsonSerializerSettings serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };


        public BidEntitiesController(VareDataModelContext _context)
        {
            var db = new DBContext.VareDataModelContext();
            IBidRepository repo = new BidRepository(db);
            IItemRepository itemRepo = new ItemRepository(db);
            var unit = new AuctionUnitOfWork(db);
            var dbLogic = new DatabaseLogic(unit, itemRepo, repo);
            _dbLogic = dbLogic;
        }

        [Authorize]
        [HttpPost("newBid")]
        public async Task<IActionResult> CreateBid([FromBody]BidEntity bid)
        {
            bid.UserIdBuyer = User.Identity.Name;
            _dbLogic.CreateBid(bid);
            _dbLogic.Save();
            return Ok(bid);
        }
        [HttpGet]
        [Route("GetBidsFromItem/{id}")]
        public  ActionResult<string> GetBidsFromItem( int id)
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetBidsFromItem(id), Formatting.Indented, serializerSettings);
            return json;
        }

        [HttpGet]
        [Route("GetBidsFromUser/{id}")]
        public ActionResult<string> GetBidsByUserId(string id)
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetBidsByUserId(id), Formatting.Indented, serializerSettings);
            return json;
        }

        [HttpGet]
        [Route("GetBidsFromUserSorted/{id}")]
        public ActionResult<string> GethighestBidOnItem(int id)
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetBidsForItemSorted(id), Formatting.Indented, serializerSettings);
            return json;
        }

    }
}
