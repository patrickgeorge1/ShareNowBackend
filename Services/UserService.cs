using System;
using System.Drawing;
using System.Text.Json;
using System.Xml.Linq;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services;

public class UserService
{
    private Dictionary<long, User> _users;

    private readonly ILogger<UserController> _logger;


    public UserService(ILogger<UserController> logger)
	{
        _logger = logger;
        _users = new Dictionary<long, User>();

    }

    public void AddUser(User user)
    {
        _users[user.Id] = user;
    }

    public User? GetUser(long id)
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

