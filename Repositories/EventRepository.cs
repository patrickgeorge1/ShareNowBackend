using Google.Cloud.Firestore;
using ShareNowBackend.Models;

namespace ShareNowBackend.Repositories;
public class EventRepository
{
    private readonly BaseRepository<Event> _repository;
    public EventRepository()
    {
        // This should be injected - This is just an example.
        _repository = new BaseRepository<Event>(Collection.Events);
    }

    public async Task<List<Event>> GetAllAsync() => await _repository.GetAllAsync<Event>();

    public async Task<Event?> GetAsync(Event entity) => (Event?)await _repository.GetAsync(entity);

    public async Task<Event> AddAsync(Event entity) => await _repository.AddAsync(entity);

    public async Task<Event> UpdateAsync(Event entity) => await _repository.UpdateAsync(entity);

    public async Task DeleteAsync(Event entity) => await _repository.DeleteAsync(entity);

    public async Task<List<Event>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Event>(query);
}

