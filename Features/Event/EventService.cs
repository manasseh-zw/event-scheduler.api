using event_scheduler.api.Data.Models;
using event_scheduler.api.Data.Repository;
using event_scheduler.api.Features.User;
using event_scheduler.api.Mapping;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace event_scheduler.api.Features.Event;

public interface IEventService
{
    Task<GlobalResponse<EventDto>> CreateEvent(Guid userId, CreateEventDto createEventRequest);
    Task<GlobalResponse<EventDto>> GetEvent(Guid id);
    Task<GlobalResponse<EventDto>> GetEvents(Guid userId);
    Task<GlobalResponse<EventDto>> UpdateEvent(Guid id, UpdateEventDto updateEventRequest);
    Task<GlobalResponse<EventDto>> DeleteEvent(Guid id);

}
public class EventService : IEventService
{
    private readonly RepositoryContext _repository;
    public EventService(RepositoryContext repository)
    {
        _repository = repository;
    }

    public async Task<GlobalResponse<EventDto>> CreateEvent(Guid userId, CreateEventDto createEventRequest)
    {
        var userExists = await _repository.Users.AnyAsync(u => u.Id == userId);

        if (!userExists)
        {
            return new GlobalResponse<EventDto>(false, "create event failed", errors: [$"user with id: {userId} not found"]);
        };

        var newEvent = new EventModel()
        {
            UserId = userId,
            Name = createEventRequest.Name,
            Description = createEventRequest.Description,
            Location = createEventRequest.Location,
            Date = DateTime.UtcNow
        };

        var validationResult = new EventValidator().Validate(newEvent);

        if (!validationResult.IsValid)
        {
            return new GlobalResponse<EventDto>(false, "create event failed", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        await _repository.Events.AddAsync(newEvent);
        await _repository.SaveChangesAsync();

        var eventToReturn = new EventDto()
        {
            Id = newEvent.Id,
            Name = newEvent.Name,
            Description = newEvent.Description,
            Location = newEvent.Location,
            Date = newEvent.Date,
            Attended = newEvent.Attended

        };

        return new GlobalResponse<EventDto>(true, "create event success", eventToReturn);
    }

    public Task<GlobalResponse<EventDto>> GetEvent(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<GlobalResponse<EventDto>> GetEvents(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<GlobalResponse<EventDto>> UpdateEvent(Guid id, UpdateEventDto updateEventRequest)
    {
        throw new NotImplementedException();
    }

    public Task<GlobalResponse<EventDto>> DeleteEvent(Guid id)
    {
        throw new NotImplementedException();
    }




}