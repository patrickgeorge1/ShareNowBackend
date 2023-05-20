using System;
using ShareNowBackend.Models;
namespace ShareNowBackend.Services.InvitationServices
{
	public interface IInvitationService
	{
		void AddInvitation(Invitation invitation);
		Invitation? GetInvitation(long id);
	}
}

