using event_scheduler.api.Data.Repository;
using event_scheduler.api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace event_scheduler.api.Features.User;


public interface IUserService
{
    Task<GlobalResponse<UserDto>> GetUser(Guid id);
}
public class UserService : IUserService
{
    private readonly RepositoryContext _repository;
    public UserService(RepositoryContext repository)
    {
        _repository = repository;
    }
    public async Task<GlobalResponse<UserDto>> GetUser(Guid id)
    {
        var user = await _repository.Users.Where(u => u.Id == id).Select(x => new UserDto()
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email

        }).FirstOrDefaultAsync();

        if (user == null)
        {
            return new GlobalResponse<UserDto>(false, "get user failed", errors: [$"user with id: {id} not found"]);
        }

        return new GlobalResponse<UserDto>(true, "get user success", user);

    }
}