using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Parties;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Parties
{
    public class PartyHandler
    {
        [MessageHandler]
        public static void HandlePartyInvitationRequest(PartyInvitationRequestMessage message, WorldClient client)
        {
            WorldClient target = WorldServer.Instance.GetClient(message.target);

            if (target != null)
            {
                client.Character.InviteParty(target.Character);
            }
            else
            {
                int partyId = client.Character.HasParty ? client.Character.Party.Id : 0;
                client.Character.OnPartyJoinError(partyId, PartyJoinErrorEnum.PARTY_JOIN_ERROR_PLAYER_NOT_FOUND);
            }
        }
        [MessageHandler]
        public static void HandlePartyInvitationDetailsRequest(PartyInvitationDetailsRequestMessage message, WorldClient client)
        {
            Party party = PartyManager.Instance.GetParty(message.partyId);

            if (party != null)
            {
                client.Send(new PartyInvitationDetailsMessage((byte)party.Type, party.PartyName, party.Leader.Id,
                    party.Leader.Name, party.Leader.Id, party.GetPartyInvitationMembersInformations(),
                    party.GetPartyGuestsInformations(), party.Id));
            }
            else
            {
                client.Character.OnPartyJoinError(message.partyId, PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND);
            }
        }
        [MessageHandler]
        public static void HandlePartyAcceptInvitation(PartyAcceptInvitationMessage message, WorldClient client)
        {
            var party = client.Character.GuestedParties.Find(x => x.Id == message.partyId);
            if (party != null)
            {
                party.AcceptInvitation(client.Character);
            }
            else
            {
                client.Character.OnPartyJoinError(message.partyId, PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND);
            }
        }
        [MessageHandler]
        public static void HandlePartyRefuseInvitation(PartyRefuseInvitationMessage message, WorldClient client)
        {
            Party party = PartyManager.Instance.GetParty(message.partyId);

            if (party != null)
            {
                party.RefuseInvation(client.Character);
            }
        }
        [MessageHandler]
        public static void HandlePartyCancelInvitation(PartyCancelInvitationMessage message, WorldClient client)
        {
            if (client.Character.HasParty)
            {
                client.Character.Party.CancelInvitation(client.Character, (int)message.guestId);
            }
        }
        [MessageHandler]
        public static void HandlePartyLeaveRequest(PartyLeaveRequestMessage message, WorldClient client)
        {
            if (client.Character.HasParty)
            {
                client.Character.Party.Leave(client.Character);
            }
            else
            {
                client.Character.OnPartyJoinError(0, PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND);
            }
        }
        [MessageHandler]
        public static void HandlePartyAbdicateThrone(PartyAbdicateThroneMessage message, WorldClient client)
        {
            if (client.Character.HasParty)
            {
                var target = client.Character.Party.GetMember((long)message.playerId);

                if (target != null)
                    client.Character.Party.Abdicate(target);
            }
            else
            {
                client.Character.OnPartyJoinError(0, PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND);
            }
        }
        [MessageHandler]
        public static void HandlePartyKickRequest(PartyKickRequestMessage message, WorldClient client)
        {
            if (client.Character.HasParty)
            {
                var target = client.Character.Party.GetMember((long)message.playerId);

                if (target != null)
                    client.Character.Party.Kick(target, client.Character);
            }
            else
            {
                client.Character.OnPartyJoinError(0, PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND);
            }
        }
    }
}