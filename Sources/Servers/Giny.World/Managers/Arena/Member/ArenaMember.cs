using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Arena.Group;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Arena.Member
{
    public class ArenaMember
    {
        public Character Character { get; private set; }

        public ArenaGroup Group => Team.Group;

        public ArenaMemberCollection Team { get; private set; }

        public PvpArenaStepEnum Step { get; private set; }

        public bool Accepted { get; private set; }

        public ArenaMember(Character character, ArenaMemberCollection team)
        {
            Character = character;
            Team = team;
            Accepted = false;
        }

        private void UpdateRegistrationStatus(bool registered)
        {
            Send(new GameRolePlayArenaRegistrationStatusMessage(registered, (byte)Step, (int)Group.Type));
        }

        public void UpdateStep(bool registered, PvpArenaStepEnum step)
        {
            Step = step;
            UpdateRegistrationStatus(registered);
        }

        public void Send(NetworkMessage message)
        {
            Character.Client.Send(message);
        }

        public void Request()
        {
            long[] memberIds = Team.GetMemberIds();
            double[] memberIdsAsDouble = Array.ConvertAll(memberIds, x => (double)x);
            Send(new GameRolePlayArenaFightPropositionMessage(0, memberIdsAsDouble, (short)Group.RequestDuration));
        }

        private void UpdateFighterStatus()
        {
            Group.Send(new GameRolePlayArenaFighterStatusMessage(0, (int)Character.Id, Accepted));
        }

        public void Answer(bool accept)
        {
            Accepted = accept;
            UpdateFighterStatus();

            if (Accepted)
            {
                if (Group.Accepted && Group.Ready)
                {
                    Group.StartFighting();
                }
            }
            else
            {
                Character.UnregisterArena();
                Team.ForEach(x =>
                {
                    x.UpdateStep(true, PvpArenaStepEnum.ARENA_STEP_REGISTRED);
                    x.Accepted = false;
                });
            }
        }
    }

}
