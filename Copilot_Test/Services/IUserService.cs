using System.Collections.Generic;
using Copilot_Test.DTOs;
using Copilot_Test.Request;

namespace Copilot_Test.Services
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();
        UserDto? GetById(int id);
        UserDto Create(CreateUserRequest request);
        bool Update(int id, UpdateUserRequest request);
        bool Delete(int id);
    }
}
