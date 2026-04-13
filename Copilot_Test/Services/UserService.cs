using System.Collections.Generic;
using System.Linq;
using Copilot_Test.DTOs;
using Copilot_Test.Request;
using Copilot_Test.Repositories;

namespace Copilot_Test.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<UserDto> GetAll() => _repo.GetAll();

        public UserDto? GetById(int id) => _repo.GetById(id);

        public UserDto Create(CreateUserRequest request)
        {
            var maxId = _repo.GetAll().Any() ? _repo.GetAll().Max(u => u.Id) : 0;
            var user = new UserDto { Id = maxId + 1, Name = request.Name, Email = request.Email };
            return _repo.Add(user);
        }

        public bool Update(int id, UpdateUserRequest request)
        {
            var existing = _repo.GetById(id);
            if (existing is null) return false;
            existing.Name = request.Name ?? existing.Name;
            existing.Email = request.Email ?? existing.Email;
            return _repo.Update(existing);
        }

        public bool Delete(int id) => _repo.Delete(id);
    }
}
