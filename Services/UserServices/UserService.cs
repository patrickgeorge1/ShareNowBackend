using System;
using System.Drawing;
using System.Xml.Linq;
using ShareNowBackend.Controllers;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services.UserServices;

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
        _logger.LogInformation($"Dictionary size: {_users.Count}");

    }

    public User? GetUser(long id)
    {
        _logger.LogInformation($"Dictionary size: {_users.Count}");
        if (_users.TryGetValue(id, out User? user))
        {
            return user;
        }
        else
        {
            return null;
        }
    }
}

