using Giny.Core;
using Giny.ORM.Attributes;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Look
{
    public class ServerEntityLook
    {
        public const short PetScale = 80;

        public const short AuraScale = 90;

        public const short PetmountScale = 100;

        public const short DefaultLookScale = 100;

        public short BonesId
        {
            get;
            private set;
        }

        public List<short> Skins
        {
            get;
            private set;
        }

        public List<int> Colors
        {
            get;
            private set;
        }

        public List<short> Scales
        {
            get;
            private set;
        }
        public short Scale
        {
            get
            {
                return (short)(Scales.Count == 0 ? DefaultLookScale : Scales[0]);
            }
            set
            {
                if (Scales.Count == 0)
                {
                    Scales.Add(value);
                }
                else
                {
                    Scales[0] = value;
                }
            }
        }
        public List<ServerSubentityLook> SubEntities
        {
            get;
            private set;
        }

        public bool IsRiding
        {
            get
            {
                return this.SubEntities.Find(x => x.Category == SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_MOUNT_DRIVER) != null;
            }
        }
        public ServerEntityLook ActorLook
        {
            get
            {
                return !IsRiding ? this : GetMountDriverLook();
            }
        }
        public ServerEntityLook()
        {
            this.Skins = new List<short>();
            this.Colors = new List<int>();
            this.Scales = new List<short>();
            this.SubEntities = new List<ServerSubentityLook>();
        }

        public ServerEntityLook(short bonesId, IEnumerable<short> skins, IEnumerable<int> colors, IEnumerable<short> scales, IEnumerable<ServerSubentityLook> subEntity)
        {
            this.BonesId = bonesId;
            this.Skins = skins.ToList();
            this.Colors = colors.ToList();
            this.Scales = scales.ToList();
            this.SubEntities = subEntity.ToList();
        }

        private ServerEntityLook GetMountDriverLook()
        {
            if (!this.IsRiding)
            {
                Logger.Write("Unable to retreive mount driver look. The entity is not riding.", Channels.Warning);
                return null;
            }
            return this.GetSubEntity(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_MOUNT_DRIVER).SubActorLook;
        }
        public ServerSubentityLook GetSubEntity(SubEntityBindingPointCategoryEnum category)
        {
            return SubEntities.Find(x => x.Category == category);
        }
        public void AddSkin(short skinId)
        {
            var look = ActorLook;
            if (!look.Skins.Contains(skinId))
                look.Skins.Add(skinId);
            else
                Logger.Write("Unable to add skin to entity, already exists.", Channels.Warning);
        }
        public void RemoveSubEntities()
        {
            this.SubEntities.Clear();
        }
        public void SetColors(IEnumerable<int> colors)
        {
            ActorLook.Colors = colors.ToList();
        }

        public EntityLook ToEntityLook()
        {
            return new EntityLook(BonesId, Skins.ToArray(), Colors.ToArray(), Scales.ToArray(), SubEntities.Select(x => x.ToSubEntity()).ToArray());
        }

        public void RemoveSkin(short skinId)
        {
            var look = ActorLook;
            if (!look.Skins.Contains(skinId))
                Logger.Write("Unable to remove skin to entity, dosent exists.", Channels.Warning);
            else
                look.Skins.Remove(skinId);
        }
        public void SetBones(short bonesId)
        {
            this.BonesId = bonesId;
        }
        public ServerEntityLook Clone()
        {
            return new ServerEntityLook(BonesId, Skins.ToArray(), Colors.ToArray(), Scales.ToArray(), SubEntities.Select(x => x.Clone()).ToArray());
        }

        public bool RemoveFirstSubentity(Predicate<ServerSubentityLook> predicate)
        {
            foreach (var subEntity in SubEntities.ToArray())
            {
                if (predicate(subEntity))
                {
                    SubEntities.Remove(subEntity);
                    return true;
                }
            }
            return false;
        }

        public void AddAura(short bonesId)
        {
            this.SubEntities.Add(new ServerSubentityLook(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_BASE_FOREGROUND,
                0, EntityLookManager.Instance.CreateLookFromBones(bonesId, AuraScale)));
        }
        public bool RemoveAura()
        {
            return RemoveFirstSubentity(x => x.Category == SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_BASE_FOREGROUND);
        }

        [CustomDeserialize]
        private static ServerEntityLook Deserialize(string str)
        {
            return EntityLookManager.Instance.Parse(str);
        }

        [CustomSerialize]
        private static string Serialize(ServerEntityLook look)
        {
            return EntityLookManager.Instance.ConvertToString(look);
        }

        public void Rescale(double lookScale)
        {
            this.Scale = (short)(this.Scale * lookScale);
        }
    }
}
