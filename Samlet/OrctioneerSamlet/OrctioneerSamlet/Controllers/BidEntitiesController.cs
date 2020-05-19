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
        private IBidRepository _bidLogic;
        private AuctionUnitOfWork unitOfWork;
        private string json;
        private JsonSerializerSettings serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };


        public BidEntitiesController(AuctionUnitOfWork uow)
        {
            unitOfWork = uow;
            uow = new AuctionUnitOfWork();
            _bidLogic = uow.BidRepository;
        }

        [Authorize]
        [HttpPost("newBid")]
        public async Task<IActionResult> CreateBid([FromBody]BidEntity bid)
        {
            while (User.Identity.Name == null)
            {}
            bid.UserIdBuyer = User.Identity.Name;
            bid.Created = DateTime.Now;
            _bidLogic.Add(bid);
            unitOfWork.Commit();
            return Ok(bid);
        }
        [HttpGet]
        [Route("GetBidsFromItem/{id}")]
        public  ActionResult<string> GetBidsFromItem( int id)
        {
            json = JsonConvert.SerializeObject(_bidLogic.GetBidsFromItem(id), Formatting.Indented, serializerSettings);
            return json;
        }

        [HttpGet]
        [Route("GetBidsFromUser/{id}")]
        public ActionResult<string> GetBidsByUserId(string id)
        {
            json = JsonConvert.SerializeObject(_bidLogic.GetBidsByUser(id), Formatting.Indented, serializerSettings);
            return json;
        }

        [HttpGet]
        [Route("GetBidsFromUserSorted/{id}")]
        public ActionResult<string> GethighestBidOnItem(int id)
        {
            json = JsonConvert.SerializeObject(_bidLogic.GethighestBidOnItem(id), Formatting.Indented, serializerSettings);
            return json;
        }

    }
}
