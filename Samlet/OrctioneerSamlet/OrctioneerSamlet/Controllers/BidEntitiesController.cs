using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public BidEntitiesController(VareDataModelContext _context)
        {
            var db = new DBContext.VareDataModelContext();
            IBidRepository repo = new BidRepository(db);
            var unit = new AuctionUnitOfWork(db);
            var dbLogic = new DatabaseLogic(unit, null, repo);
            _dbLogic = dbLogic;
        }

        [HttpPost("newBid")]
        public async Task<IActionResult> CreateBid(BidEntity bid)
        {
            Console.WriteLine("Added bid");
            _dbLogic.CreateBid(bid);
            _dbLogic.Save();
            return Ok(bid);
        }
        [HttpGet]
        [Route("items/{id}/bids")]
        public async Task<ActionResult<string>> GetBidsFromItem( int itemId)
        {
            json = JsonConvert.SerializeObject(_dbLogic.GetBidsFromItem(itemId), Formatting.Indented);
            return json;
        }
    }
}
