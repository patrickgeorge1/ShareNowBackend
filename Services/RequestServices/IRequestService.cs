using System;
using ShareNowBackend.Models;
namespace ShareNowBackend.Services.RequestServices
{
	public interface IRequestService
	{
		void AddRequest(Request request);
		Request? GetRequest(long id);
	}
}

