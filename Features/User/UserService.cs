using event_scheduler.api.Data.Repository;
using event_scheduler.api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace event_scheduler.api.Features.User;


public interface IUserService
{
    Task<GlobalResponse<UserDto>> GetUser(Guid id);
    Task<GlobalResponse<List<EventDto>>> GetEvents(Guid userId);
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


    public async Task<GlobalResponse<List<EventDto>>> GetEvents(Guid userId)
    {
        var userExists = await _repository.Users.AnyAsync(u => u.Id == userId);

        if (!userExists)
        {
            return new GlobalResponse<List<EventDto>>(false, "get user events failed", errors: [$"user with id: {userId} not found"]);
        }

        var events = await _repository.Events.Where(e => e.UserId == userId).Select(x => new EventDto()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Location = x.Location,
            Date = x.Date,
            Attended = x.Attended
        }).ToListAsync();


        return new GlobalResponse<List<EventDto>>(true, "get user events", events);
    }
}