using System;
using System.Drawing;
using System.Text.Json;
using System.Xml.Linq;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;
using ShareNowBackend.Repositories;

namespace ShareNowBackend.Services;

public class UserService
{
    private Dictionary<string, User> _users;
    private UserRepository _userRepository;

    private readonly ILogger<UserController> _logger;


    public UserService(ILogger<UserController> logger, UserRepository userRepository)
	{
        _logger = logger;
        _users = new Dictionary<string, User>();
        _userRepository = userRepository;

    }

    public async Task<User> AddUser(User user)
    {
        //_users[user.Id] = user;
        return await _userRepository.AddAsync(user);
    }

    public User? GetUser(string id)
    {
        if (_users.TryGetValue(id, out User? user))
        {
            return user;
        }
        else
        {
            return null;
        }
    }

    public Dictionary<string, User> GetAllUsers()
    {
        return _users;
    }

    public User? DeserializeUser(JsonElement userJson)
    {
        try
        {
            string name = userJson.GetProperty("Name").GetString() ?? throw new Exception("Could not deserialize json to user.");
            return new User(name);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {}", e.Message);
            return null;
        }
    }
}

