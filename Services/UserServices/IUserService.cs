namespace ShareNowBackend.Services.UserServices;

using System;
using ShareNowBackend.Models;

public interface IUserService
{
	void AddUser(User user);
	User? GetUser(long id);
}

