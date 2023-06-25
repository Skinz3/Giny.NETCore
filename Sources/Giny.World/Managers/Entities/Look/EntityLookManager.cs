using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Protocol.Enums;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Look
{
    public class EntityLookManager : Singleton<EntityLookManager>
    {
        public string ConvertToString(ServerEntityLook look)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            int num = 0;
            stringBuilder.Append(look.BonesId);
            if (look.Skins == null || !look.Skins.Any())
            {
                num++;
            }
            else
            {
                stringBuilder.Append("|".ConcatCopy(num + 1));
                num = 0;
                stringBuilder.Append(string.Join(",", look.Skins));
            }
            if (look.Colors == null)
            {
                num++;
            }
            else
            {
                stringBuilder.Append("|".ConcatCopy(num + 1));
                num = 0;

                List<string> values = new List<string>();

                int i = 0;
                foreach (var color in look.Colors)
                {
                    i++;
                    values.Add(i + "=" + color);
                }

                stringBuilder.Append(string.Join(",", values));

            }
            if (look.Scales == null)
            {
                num++;
            }
            else
            {
                stringBuilder.Append("|".ConcatCopy(num + 1));
                num = 0;
                stringBuilder.Append(string.Join<short>(",", look.Scales));
            }
            if (look.SubEntities.Count() == 0)
            {
                num++;
            }
            else
            {
                List<string> subEntitiesAsString = new List<string>();
                foreach (var sub in look.SubEntities)
                {
                    StringBuilder subBuilter = new System.Text.StringBuilder();
                    subBuilter.Append((sbyte)sub.Category);
                    subBuilter.Append("@");
                    subBuilter.Append(sub.BindingPointIndex);
                    subBuilter.Append("=");
                    subBuilter.Append(ConvertToString(sub.SubActorLook));
                    subEntitiesAsString.Add(subBuilter.ToString());
                }
                stringBuilder.Append("|".ConcatCopy(num + 1));
                stringBuilder.Append(string.Join<string>(",",
                    from entry in subEntitiesAsString
                    select entry));
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
        private Tuple<int, int> ParseIndexedColor(string str)
        {
            int num = str.IndexOf("=");
            bool flag = str[num + 1] == '#';
            int item = int.Parse(str.Substring(0, num));
            int item2 = int.Parse(str.Substring(num + (flag ? 2 : 1), str.Length - (num + (flag ? 2 : 1))), flag ? NumberStyles.HexNumber : NumberStyles.Integer);
            return Tuple.Create(item, item2);
        }
        public int[] GetConvertedColors(IEnumerable<int> colors)
        {
            int[] col = new int[colors.Count()];
            for (int i = 0; i < colors.Count(); i++)
            {
                var color = Color.FromArgb(colors.ToArray()[i]);
                col[i] = i + 1 << 24 | color.ToArgb() & 16777215;
            }
            return col;
        }
        public ServerEntityLook Parse(string str)
        {
            if (str == string.Empty)
                return null;

            if (str.StartsWith("["))
            {
                str = new string(str.Skip(3).ToArray());
            }

            if (string.IsNullOrEmpty(str) || str[0] != '{')
            {
                throw new System.Exception("Incorrect EntityLook format : " + str);
            }
            int i = 1;
            int num = str.IndexOf('|');
            if (num == -1)
            {
                num = str.IndexOf("}");
                if (num == -1)
                {
                    throw new System.Exception("Incorrect EntityLook format : " + str);
                }
            }
            short bones = short.Parse(str.Substring(i, num - i));
            i = num + 1;
            short[] skins = new short[0];
            if ((num = str.IndexOf('|', i)) != -1 || (num = str.IndexOf('}', i)) != -1)
            {
                skins = str.Substring(i, num - i).ParseCollection(short.Parse);
                i = num + 1;
            }
            Tuple<int, int>[] source = new Tuple<int, int>[0];
            if ((num = str.IndexOf('|', i)) != -1 || (num = str.IndexOf('}', i)) != -1)
            {
                source = str.Substring(i, num - i).ParseCollection(ParseIndexedColor);
                i = num + 1;
            }
            short[] scales = new short[0];
            if ((num = str.IndexOf('|', i)) != -1 || (num = str.IndexOf('}', i)) != -1)
            {
                scales = str.Substring(i, num - i).ParseCollection(short.Parse);
                i = num + 1;
            }
            List<ServerSubentityLook> list = new List<ServerSubentityLook>();

            while (i < str.Length && str[i] != '}')
            {
                int num2 = str.IndexOf('@', i, 3);
                int num3 = str.IndexOf('=', num2 + 1, 3);
                byte category = byte.Parse(str.Substring(i, num2 - i));
                byte b = byte.Parse(str.Substring(num2 + 1, num3 - (num2 + 1)));
                int num4 = 0;
                int num5 = num3 + 1;
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                do
                {
                    stringBuilder.Append(str[num5]);
                    if (str[num5] == '{')
                    {
                        num4++;
                    }
                    else
                    {
                        if (str[num5] == '}')
                        {
                            num4--;
                        }
                    }
                    num5++;
                }
                while (num4 > 0);
                list.Add(new ServerSubentityLook((SubEntityBindingPointCategoryEnum)category, b, Parse(stringBuilder.ToString())));
                i = num5 + 1;
            }
            List<int> colors = new List<int>();
            foreach (var color in source)
            {
                colors.Add(color.Item2);
            }
            return new ServerEntityLook(bones, skins, colors, scales, list);
        }

        public ServerEntityLook CreateLookFromBones(short bonesId, short scale)
        {
            return new ServerEntityLook(bonesId, new List<short>(), new List<int>(), new List<short>() { scale }, new ServerSubentityLook[0]);
        }

        private ServerEntityLook GetMountLook(Character character, ServerEntityLook mountLook)
        {
            ServerEntityLook newLook = mountLook.Clone();
            newLook.SetColors(GetConvertedColors(mountLook.Colors));

            ServerSubentityLook actorSub = new ServerSubentityLook(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_MOUNT_DRIVER, 0,
                new ServerEntityLook(2, character.Look.Skins, character.Look.Colors, character.Look.Scales, character.Look.SubEntities));
            newLook.SubEntities.Add(actorSub);
            return newLook;
        }

        public ServerEntityLook CreatePetMountLook(Character character, string itemLook)
        {
            ServerEntityLook look = Parse(itemLook);
            look.SetColors(character.Look.Colors.Skip(2).Take(3));
            return GetMountLook(character, look);
        }

        public ServerEntityLook CreatePetLook(Character character, string itemLook)
        {
            var look = character.Look.Clone();
            var petLook = Parse(itemLook);
            petLook.SetColors(character.Look.Colors.Skip(2).Take(3));
            look.ActorLook.SubEntities.Add(new ServerSubentityLook(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_PET, 0, petLook));

            return look;
        }
        /// <summary>
        /// 5138 --> aura bleue
        /// 5139 --> aura jaune
        /// 5140 --> aura rouge
        /// 5141 --> aura verte
        /// 5142 --> aura multicolore
        /// 5143 --> aura blanche
        /// </summary>
        public short GetAuraBones(Character character, short emoteId)
        {
            switch (emoteId)
            {
                case CharacterLevelRewardManager.EMOTE_100:
                    return (short)(character.Level >= 200 ? 170 : 169);
                case CharacterLevelRewardManager.EMOTE_OMEGA_100:
                    return 4829;
                case CharacterLevelRewardManager.EMOTE_OMEGA_200:
                    return 4830;
                case CharacterLevelRewardManager.EMOTE_OMEGA_300:
                    return 4831;
                case CharacterLevelRewardManager.EMOTE_OMEGA_400:
                    return 4832;
                case CharacterLevelRewardManager.EMOTE_OMEGA_500:
                    return 4833;
                case 203:
                    return 5140;
            }

            return 0;
        }
    }
}
