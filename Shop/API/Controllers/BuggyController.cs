using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var x = 5;
            var c = 0;
            int i = x / c;
            return BadRequest(new ApiResponse(500));
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        
        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest();
        }
    }
}