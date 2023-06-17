using Google.Cloud.Firestore;
using ShareNowBackend.Models;

namespace ShareNowBackend.Repositories;

public class RequestRepository
{
    private readonly BaseRepository<Request> _repository;
    public RequestRepository()
    {
        // This should be injected - This is just an example.
        _repository = new BaseRepository<Request>(Collection.Requests);
    }

    public async Task<List<Request>> GetAllAsync() => await _repository.GetAllAsync<Request>();

    public async Task<Request?> GetAsync(Request entity) => (Request?)await _repository.GetAsync(entity);

    public async Task<Request> AddAsync(Request entity) => await _repository.AddAsync(entity);

    public async Task<Request> UpdateAsync(Request entity) => await _repository.UpdateAsync(entity);

    public async Task DeleteAsync(Request entity) => await _repository.DeleteAsync(entity);

    public async Task<List<Request>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Request>(query);

    // This is specific to users.
    public async Task<List<Request>> GetRequestsByStatus(RequestStatus status)
    {
        var requestsWithStatuses = new List<Request>()
        {
            new()
            {
                Status=status
            }
        };

        var query = _repository._firestoreDb.Collection(Collection.Requests.ToString()).WhereIn(nameof(Request.Status), requestsWithStatuses);
        return await this.QueryRecordsAsync(query);
    }
}
