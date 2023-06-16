using Google.Cloud.Firestore;
using ShareNowBackend.Models;

namespace ShareNowBackend.Repositories;

public class UserRepository
{
    private readonly BaseRepository<User> _repository;
    public UserRepository()
    {
        // This should be injected - This is just an example.
        _repository = new BaseRepository<User>(Collection.Users);
    }

    public async Task<List<User>> GetAllAsync() => await _repository.GetAllAsync<User>();

    public async Task<User?> GetAsync(User entity) => (User?)await _repository.GetAsync(entity);

    public async Task<User> AddAsync(User entity) => await _repository.AddAsync(entity);

    public async Task<User> UpdateAsync(User entity) => await _repository.UpdateAsync(entity);

    public async Task DeleteAsync(User entity) => await _repository.DeleteAsync(entity);

    public async Task<List<User>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<User>(query);
}
