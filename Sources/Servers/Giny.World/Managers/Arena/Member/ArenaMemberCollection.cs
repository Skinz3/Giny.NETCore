using Giny.Core.Network.Messages;
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
    public class ArenaMemberCollection
    {
        private List<ArenaMember> m_members = new List<ArenaMember>();

        public bool Empty
        {
            get
            {
                return m_members.Count == 0;
            }
        }
        public ArenaGroup Group
        {
            get;
            set;
        }
        public bool IsFull
        {
            get
            {
                return m_members.Count == (int)Group.Type;
            }
        }
        public bool Accepted
        {
            get
            {
                return m_members.All(x => x.Accepted);
            }
        }
        public ArenaMemberCollection(ArenaGroup group)
        {
            this.Group = group;
        }
        public long[] GetMemberIds()
        {
            return Array.ConvertAll(m_members.ToArray(), x => x.Character.Id);
        }
        public void ForEach(Action<ArenaMember> action)
        {
            foreach (var member in GetMembers())
            {
                action(member);
            }
        }
        public void Send(NetworkMessage message)
        {
            ForEach(x => x.Send(message));
        }
        public ArenaMember AddMember(ArenaGroup group, Character character)
        {
            if (MemberExist(character))
            {
                throw new Exception("Member is already registered is this ArenaMemberCollection");
            }

            ArenaMember member = new ArenaMember(character, this);
            this.m_members.Add(member);

            return member;
        }

        public bool RemoveMember(Character character)
        {
            var member = m_members.FirstOrDefault(m => m.Character == character);
            if (member != null)
            {
                m_members.Remove(member);
                return true; // Indicates successful removal
            }

            return false; // Indicates the member was not found
        }

        public ArenaMember GetMember(Character character)
        {
            return this.m_members.FirstOrDefault(x => x.Character == character);
        }
        public ArenaMember[] GetMembers()
        {
            return this.m_members.ToArray();
        }
        private bool MemberExist(Character character)
        {
            return GetMember(character) != null;
        }

    }
}
