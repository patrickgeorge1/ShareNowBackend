using Google.Cloud.Firestore;


namespace ShareNowBackend.Repositories;

public interface IBaseRepository<T>
{
    /// <summary>
    /// Gets all record from the repository.
    /// </summary> 
    /// <returns>a records of type T</returns>
    Task<List<T>> GetAllAsync<T>() where T : IBaseFirestoreData;

    /// <summary>
    /// Gets a record from the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>a record of type T</returns>
    Task<object> GetAsync<T>(T entity) where T : IBaseFirestoreData;

    /// <summary>
    /// Adds a record to the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>a record of type T</returns>
    Task<T> AddAsync<T>(T entity) where T : IBaseFirestoreData;

    /// <summary>
    /// Updates a record in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>a record of type T</returns>
    Task<T> UpdateAsync<T>(T entity) where T : IBaseFirestoreData;

    /// <summary>
    /// Adds a record to the repository.
    /// </summary>
    /// <param name="entity"></param> S
    Task DeleteAsync<T>(T entity) where T : IBaseFirestoreData;

    /// <summary>
    /// Query all record from the repository.
    /// </summary> 
    /// <returns>a records of type T</returns>
    Task<List<T>> QueryRecordsAsync<T>(Query query) where T : IBaseFirestoreData;
}

/// <summary>
///     Represents the base repository.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseRepository<T> : IBaseRepository<T>
{
    private readonly Collection _collection;
    public FirestoreDb _firestoreDb;

    public BaseRepository(Collection collection)
    {
        // This should live in the appsetting file and injected - This is just an example.
        _collection = collection;
        var filepath = @"C:\Users\patri\Desktop\ShareNow backend\sharenow-a3665-firebase-adminsdk-iw8th-7bb863e448.json";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
        _firestoreDb = FirestoreDb.Create("sharenow-a3665");
    }

    /// <inheritdoc />
    public async Task<List<T>> GetAllAsync<T>() where T : IBaseFirestoreData
    {
        Query query = _firestoreDb.Collection(_collection.ToString());
        var querySnapshot = await query.GetSnapshotAsync();
        var list = new List<T>();
        foreach (var documentSnapshot in querySnapshot.Documents)
        {
            if (!documentSnapshot.Exists) continue;
            var data = documentSnapshot.ConvertTo<T>();
            if (data == null) continue;
            data.Id = documentSnapshot.Id;
            list.Add(data);
        }

        return list;
    }

    /// <inheritdoc />
    public async Task<object> GetAsync<T>(T entity) where T : IBaseFirestoreData
    {
        var docRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
        var snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            var usr = snapshot.ConvertTo<T>();
            usr.Id = snapshot.Id;
            return usr;
        }

        return null;
    }

    /// <inheritdoc />
    public async Task<T> AddAsync<T>(T entity) where T : IBaseFirestoreData
    {
        // make Firestore Id same with entity id
        var id = Guid.NewGuid().ToString();
        entity.Id = id;

        var colRef = _firestoreDb.Collection(_collection.ToString());
        //var doc = await colRef.AddAsync(entity);
        var doc = await colRef.Document(id).SetAsync(entity);
        // GO GET RECORD FROM DATABASE:
        // return (T) await GetAsync(entity); 
        return entity;
    }

    /// <inheritdoc />
    public async Task<T> UpdateAsync<T>(T entity) where T : IBaseFirestoreData
    {
        var recordRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
        await recordRef.SetAsync(entity, SetOptions.MergeAll);
        // GO GET RECORD FROM DATABASE:
        // return (T)await GetAsync(entity);
        return entity;
    }

    /// <inheritdoc />
    public async Task DeleteAsync<T>(T entity) where T : IBaseFirestoreData
    {
        var recordRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
        await recordRef.DeleteAsync();
    }

    /// <inheritdoc />
    public async Task<List<T>> QueryRecordsAsync<T>(Query query) where T : IBaseFirestoreData
    {
        var querySnapshot = await query.GetSnapshotAsync();
        var list = new List<T>();
        foreach (var documentSnapshot in querySnapshot.Documents)
        {
            if (!documentSnapshot.Exists) continue;
            var data = documentSnapshot.ConvertTo<T>();
            if (data == null) continue;
            data.Id = documentSnapshot.Id;
            list.Add(data);
        }

        return list;
    }
}