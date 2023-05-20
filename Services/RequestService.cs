using System;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services
{
	public class RequestService
    {
        private static Dictionary<long, Request> _requests = new Dictionary<long, Request>();


        public RequestService()
		{
		}

        public void AddRequest(Request request)
        {
            _requests.Add(request.Id, request);
        }

        public Request? GetRequest(long id)
        {
            if (_requests.TryGetValue(id, out Request? request))
            {
                return request;
            }
            else
            {
                return null;
            }
        }
    }
}

