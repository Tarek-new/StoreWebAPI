using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.ResponseStatusModules;

namespace StoreWebAPI.Controllers
{
    public class BuggyController : BaseController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("Test")]
        [Authorize]

        public ActionResult<string> GetText()
        {
            return "Some Text";
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(1000);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }
    }
}
