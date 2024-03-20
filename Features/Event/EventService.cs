using event_scheduler.api.Data.Models;
using event_scheduler.api.Data.Repository;
using event_scheduler.api.Features.User;
using event_scheduler.api.Mapping;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace event_scheduler.api.Features.Event;

public interface IEventService
{
    Task<GlobalResponse<EventDto>> CreateEvent(Guid userId, CreateEventDto createEventRequest);
    Task<GlobalResponse<EventDto>> GetEvent(Guid id);
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
            Date = Convert.ToDateTime(createEventRequest.Date).ToUniversalTime()
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

    public async Task<GlobalResponse<EventDto>> GetEvent(Guid id)
    {
        var eventToReturn = await _repository.Events.Where(e => e.Id == id).Select(x => new EventDto()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Location = x.Location,
            Date = x.Date,
            Attended = x.Attended
        }).FirstOrDefaultAsync();

        if (eventToReturn == null)
        {
            return new GlobalResponse<EventDto>(false, "get event failed", errors: [$"event with id: {id} not found"]);
        }

        return new GlobalResponse<EventDto>(true, "get event success", eventToReturn);
    }

    public async Task<GlobalResponse<EventDto>> UpdateEvent(Guid id, UpdateEventDto updateEventRequest)
    {
        var existingEvent = await _repository.Events.FindAsync(id);
        if (existingEvent == null) return new GlobalResponse<EventDto>(false, "delete team failed", errors: [$"team with id{id} not found"]);

        var eventToBePatched = new UpdateEventDto()
        {
            Name = existingEvent.Name,
            Description = existingEvent.Description,
            Location = existingEvent.Location,
            Date = existingEvent.Date
        };

        var patchedEventDto = Patcher.Patch(updateEventRequest, eventToBePatched);


        existingEvent.Name = patchedEventDto.Name;
        existingEvent.Description = patchedEventDto.Description;
        existingEvent.Location = patchedEventDto.Location;
        existingEvent.Date = Convert.ToDateTime(patchedEventDto.Date).ToUniversalTime();


        var validationResult = new EventValidator().Validate(existingEvent);
        if (!validationResult.IsValid)
        {
            return new GlobalResponse<EventDto>(false, "update event failed", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        await _repository.SaveChangesAsync();

        return new GlobalResponse<EventDto>(true, "update event success");
    }

    public async Task<GlobalResponse<EventDto>> DeleteEvent(Guid id)
    {
        var eventToDelete = await _repository.Events.FindAsync(id);

        if (eventToDelete == null)
        {
            return new GlobalResponse<EventDto>(false, "delete event failed");
        }
        _repository.Events.Remove(eventToDelete);
        await _repository.SaveChangesAsync();

        return new GlobalResponse<EventDto>(true, "delete event success");
    }

}