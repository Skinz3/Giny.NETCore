using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Generic
{
    public class GenericActionsManager : Singleton<GenericActionsManager>
    {
        private Dictionary<GenericActionEnum, MethodInfo> m_handlers = new Dictionary<GenericActionEnum, MethodInfo>();

        [StartupInvoke(StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            foreach (var method in typeof(GenericActions).GetMethods())
            {
                var attribute = method.GetCustomAttribute<GenericActionHandlerAttribute>();

                if (attribute != null)
                {
                    m_handlers.Add(attribute.ActionEnum, method);
                }
            }
        }
        public bool IsHandled(IGenericActionParameter parameter)
        {
            return m_handlers.ContainsKey(parameter.ActionIdentifier);
        }
        public bool Handle(Character character, IGenericActionParameter parameter)
        {
            if (m_handlers.ContainsKey(parameter.ActionIdentifier))
            {
                MethodInfo handler = m_handlers[parameter.ActionIdentifier];

                handler.Invoke(null, new object[] { character, parameter });
                return true;
            }
            else
            {
                character.ReplyWarning("Unknown action identifier: " + parameter.ActionIdentifier);
                return false;
            }
        }
    }
    public class GenericActionInvoker
    {
        public MethodInfo Method
        {
            get;
            private set;
        }
        public int ParametersCount
        {
            get;
            private set;
        }

        public GenericActionInvoker(MethodInfo method, int parametersCount)
        {
            this.Method = method;
            this.ParametersCount = parametersCount;
        }
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class GenericActionHandlerAttribute : Attribute
    {
        public GenericActionEnum ActionEnum
        {
            get;
            private set;
        }
        public GenericActionHandlerAttribute(GenericActionEnum actionEnum)
        {
            this.ActionEnum = actionEnum;
        }
    }
}
