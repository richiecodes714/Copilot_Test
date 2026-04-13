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
        // Simple in-memory store for demonstration/testing purposes.
        private static readonly List<UserDto> _users = new();
        private static readonly object _lock = new();
        private static int _nextId;

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            lock (_lock)
            {
                return Ok(_users);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDto> GetById(int id)
        {
            lock (_lock)
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user is null) return NotFound();
                return Ok(user);
            }
        }

        [HttpPost]
        public ActionResult<UserDto> Create([FromBody] CreateUserRequest request)
        {
            if (request is null) return BadRequest();

            var id = Interlocked.Increment(ref _nextId);
            var user = new UserDto { Id = id, Name = request.Name, Email = request.Email };

            lock (_lock)
            {
                _users.Add(user);
            }

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] UpdateUserRequest request)
        {
            if (request is null) return BadRequest();

            lock (_lock)
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user is null) return NotFound();

                user.Name = request.Name ?? user.Name;
                user.Email = request.Email ?? user.Email;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            lock (_lock)
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user is null) return NotFound();
                _users.Remove(user);
            }

            return NoContent();
        }

        // DTOs and request models moved to their own files.
    }
}