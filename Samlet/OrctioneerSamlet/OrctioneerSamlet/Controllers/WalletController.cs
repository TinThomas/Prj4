using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : Controller
    {
        private IWalletRepository _wallet;

        public WalletController(WalletContext db)
        {
            _wallet = new WalletRepository(db);
        }
        
        [HttpGet("getBalance")]
        public async Task<IActionResult> getBalance()
        {
            var balance = await _wallet.getAmount(User.Identity.Name);
            return Ok(balance);
        }
        
        [HttpGet("getWallet")]
        public async Task<IActionResult> getWallet()
        {
            var wallet = await _wallet.getDetails(User.Identity.Name);
            if (wallet != null)
            {
                return Ok(wallet);
            }

            return BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateWallet([FromBody] WalletEntity wallet)
        {
            wallet.userID = User.Identity.Name;
            var wait = await _wallet.addWallet(wallet);
            if (wait > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateWallet([FromBody] WalletEntity wallet)
        {
            wallet.userID = User.Identity.Name;
            int wait = await _wallet.updateWallet(wallet);
            if (wait > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            _wallet.DeleteCard(User.Identity.Name);
            return Ok();
        }
    }
}