using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Look
{
    public class ServerSubentityLook
    {
        public SubEntityBindingPointCategoryEnum Category
        {
            get;
            private set;
        }

        public byte BindingPointIndex
        {
            get;
            private set;
        }

        public ServerEntityLook SubActorLook
        {
            get;
            set;
        }
        public ServerSubentityLook(SubEntityBindingPointCategoryEnum category, byte bindingPointIndex, ServerEntityLook subActorLook)
        {
            this.Category = category;
            this.BindingPointIndex = bindingPointIndex;
            this.SubActorLook = subActorLook;
        }

        public SubEntity ToSubEntity()
        {
            return new SubEntity((byte)Category, BindingPointIndex, SubActorLook.ToEntityLook());
        }

        public ServerSubentityLook Clone()
        {
            return new ServerSubentityLook(Category, BindingPointIndex, SubActorLook.Clone());
        }
    }
}
