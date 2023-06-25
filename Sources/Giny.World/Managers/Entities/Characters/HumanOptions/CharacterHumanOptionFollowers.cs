using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Giny.World.Managers.Entities.Characters.HumanOptions
{
    public class CharacterHumanOptionFollowers : CharacterHumanOption
    {
        private List<ServerEntityLook> m_looks;

        public CharacterHumanOptionFollowers()
        {
            m_looks = new List<ServerEntityLook>();
        }

        public void Insert(int index,ServerEntityLook look)
        {
            this.m_looks.Insert(index, look);
        }
        public void Add(ServerEntityLook look)
        {
            this.m_looks.Add(look);
        }
        public void Remove(int index)
        {
            m_looks.RemoveAt(index);
        }
        public void Remove(ServerEntityLook look)
        {
            m_looks.Remove(look);
        }
        public override HumanOption GetHumanOption(Character character)
        {
            List<IndexedEntityLook> looks = new List<IndexedEntityLook>();

            for (byte i = 0; i < m_looks.Count; i++)
            {
                looks.Add(new IndexedEntityLook(m_looks[i].ToEntityLook(), i));
            }

            return new HumanOptionFollowers()
            {
                followingCharactersLook = looks.ToArray(),
            };

        }
    }
}