using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.World.Modules;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    public class CriteriaManager : Singleton<CriteriaManager>
    {
        public const char OR_SPLITTER = '|';

        public const char AND_SPLITTER = '&';

        private static readonly Dictionary<string, Type> m_handlers = new Dictionary<string, Type>();

        [StartupInvoke(StartupInvokePriority.FifthPass)]
        public static void Intialize()
        {
            foreach (var type in AssemblyCore.GetTypes())
            {
                CriteriaAttribute attribute = type.GetCustomAttribute<CriteriaAttribute>();

                if (attribute != null)
                {
                    m_handlers.Add(attribute.Identifier, type);
                }
            }
        }
        private Criteria GetHandler(string identifier, string criteriaFull)
        {
            Type type = null;

            if (m_handlers.TryGetValue(identifier, out type))
            {
                Criteria instance = (Criteria)Activator.CreateInstance(type);
                instance.CriteriaFull = criteriaFull;
                return instance;
            }
            else
            {
                return null;
            }


        }
        [WIP("and or")]
        public bool EvaluateCriterias(WorldClient client, string criterias) // todo, and > OR
        {
            if (criterias == null || criterias == string.Empty)
                return true;

            foreach (var criteria in criterias.Split('&'))
            {
                string criteriaIndentifier = new string(criteria.Take(2).ToArray());

                Criteria handler = GetHandler(criteriaIndentifier, criteria);

                if (handler != null)
                {
                    if (!handler.Eval(client))
                    {
                        return false;
                    }
                }
                else
                {
                    if (client.Account.Role > ServerRoleEnum.Player)
                        client.Character.Reply("Unknown criteria indentifier: " + criteriaIndentifier + ". Skeeping criteria");
                    return true;
                }
            }

            return true;


        }
    }
    public class CriteriaAttribute : Attribute
    {
        public string Identifier
        {
            get;
            set;
        }

        public CriteriaAttribute(string indentifier)
        {
            this.Identifier = indentifier;
        }
    }
}
