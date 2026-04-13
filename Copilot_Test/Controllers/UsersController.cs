using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Copilot_Test.DTOs;
using Copilot_Test.Request;

namespace Copilot_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Services.IUserService _service;

        public UsersController(Services.IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            var users = _service.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDto> GetById(int id)
        {
            var user = _service.GetById(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserDto> Create([FromBody] CreateUserRequest request)
        {
            if (request is null) return BadRequest();

            var user = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] UpdateUserRequest request)
        {
            if (request is null) return BadRequest();

            var ok = _service.Update(id, request);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var ok = _service.Delete(id);
            if (!ok) return NotFound();
            return NoContent();
        }

        // DTOs and request models moved to their own files.
    }
}