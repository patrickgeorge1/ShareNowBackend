using System;
using ShareNowBackend.Models;

namespace ShareNowBackend.Services
{
	public class InvitationService
	{
        private Dictionary<long, Invitation> _invitations;

        public InvitationService()
		{
            _invitations = new Dictionary<long, Invitation>();
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

