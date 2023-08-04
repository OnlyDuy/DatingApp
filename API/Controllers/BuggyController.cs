using API.Data;
using API.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        // Xác thực người dùng trước khi truy cập vào phương thức này (ở đây sẽ là đăng nhập)
        [Authorize]
        // kiểm tra lỗi 401
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        // Xác thực kiểm tra người dùng
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);

            if (thing == null) return NotFound();

            return Ok(thing);
        }

        // Xác thực kiểm tra lỗi dấu gạch ngang máy chủ
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1);
            
            var thingToReturn = thing != null ? thing.ToString() : "Thing is null";

            if (thingToReturn == null) {
                return NotFound();
            }

            return thingToReturn;
        }

        // Bad-resquest
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}