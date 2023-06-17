using Google.Cloud.Firestore;
using ShareNowBackend.Models;

namespace ShareNowBackend.Repositories;

public class InvitationRepository
{
    private readonly BaseRepository<Invitation> _repository;
    public InvitationRepository()
    {
        // This should be injected - This is just an example.
        _repository = new BaseRepository<Invitation>(Collection.Invitations);
    }

    public async Task<List<Invitation>> GetAllAsync() => await _repository.GetAllAsync<Invitation>();

    public async Task<Invitation?> GetAsync(Invitation entity) => (Invitation?)await _repository.GetAsync(entity);

    public async Task<Invitation> AddAsync(Invitation entity) => await _repository.AddAsync(entity);

    public async Task<Invitation> UpdateAsync(Invitation entity) => await _repository.UpdateAsync(entity);

    public async Task DeleteAsync(Invitation entity) => await _repository.DeleteAsync(entity);

    public async Task<List<Invitation>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Invitation>(query);

    public async Task<List<Invitation>> GetUsGetAllInvitationsFromDonorIderWhereCity(string donorId)
    {
        var invitationsWithDonorIds = new List<Invitation>()
        {
            new()
            {
                DonatorId=donorId
            }
        };

        var query = _repository._firestoreDb.Collection(Collection.Invitations.ToString()).WhereIn(nameof(Invitation.DonatorId), invitationsWithDonorIds);
        return await this.QueryRecordsAsync(query);
    }
}
