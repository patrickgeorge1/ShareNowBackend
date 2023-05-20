using System;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services.InvitationServices
{
	public class InvitationService : IInvitationService
	{
        private static Dictionary<long, Invitation> _invitations = new Dictionary<long, Invitation>();

        public InvitationService()
		{
		}

        public void AddInvitation(Invitation invitation)
        {
            _invitations.Add(invitation.Id, invitation);
        }

        public Invitation? GetInvitation(long id)
        {
            if (_invitations.TryGetValue(id, out Invitation? invitation))
            {
                return invitation;
            }
            else
            {
                return null;
            }
        }
    }
}

