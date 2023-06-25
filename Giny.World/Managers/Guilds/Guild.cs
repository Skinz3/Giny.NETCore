using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Network.Messages;
using Giny.ORM;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Experiences;
using Giny.World.Network;
using Giny.World.Records.Characters;
using Giny.World.Records.Guilds;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Guilds
{
    public class Guild
    {
        public long Id => Record.Id;

        public GuildRecord Record
        {
            get;
            private set;
        }
        private ConcurrentDictionary<long, Character> OnlineMembers
        {
            get;
            set;
        }

        public byte Level => ExperienceManager.Instance.GetGuildLevel(Experience);

        public long ExperienceLowerBound => ExperienceManager.Instance.GetGuildXPForLevel(Level);

        public long ExperienceUpperBound => ExperienceManager.Instance.GetGuildXPForNextLevel(Level);

        public long Experience
        {
            get
            {
                return Record.Experience;
            }
            set
            {
                Record.Experience = value;
            }
        }

        public Guild(GuildRecord record)
        {
            this.Record = record;
            this.OnlineMembers = new ConcurrentDictionary<long, Character>();
        }

        public bool Join(Character character, bool owner)
        {
            if (Record.Members.Count == GuildsManager.MaxMemberCount)
            {
                return false;
            }

            GuildMemberRecord memberRecord = new GuildMemberRecord(character, owner);
            Record.Members.Add(memberRecord);
            Record.UpdateElement();
            OnlineMembers.TryAdd(character.Id, character);
            character.OnGuildJoined(this, memberRecord);
            return true;
        }

        /*
         * If the member is connected from the guild point of view (when character loading is complete)
         */
        public bool IsMemberConnected(long characterId)
        {
            return OnlineMembers.ContainsKey(characterId);
        }

        public void OnConnected(Character character)
        {
            OnlineMembers.TryAdd(character.Id, character);
            RefreshMotd(character);
        }
        public void OnDisconnected(Character character)
        {
            OnlineMembers.TryRemove(character.Id);
        }

        public void Leave(Character source, GuildMemberRecord member)
        {
            /* if (member.Rights == GuildRightsBitEnum.GUILD_RIGHT_BOSS)
             {
                 return;
             } */
            if (source.Guild != this)
            {
                return;
            }

            bool kicked = source.Id != member.CharacterId;

            if (IsMemberConnected(member.CharacterId))
            {
                Character target = GetOnlineMember(member.CharacterId);

                if (member == null)
                {
                    Logger.Write("Unable to kick member from guild " + Id + ". The player is not yet connected.", Channels.Warning);
                    return;
                }

                OnlineMembers.TryRemove(target.Id);

                target.OnGuildKick(this);
            }
            else
            {
                CharacterRecord characterRecord = CharacterRecord.GetCharacterRecord(member.CharacterId);
                characterRecord.GuildId = 0;
            }


            Record.Members.Remove(member);

            Record.UpdateElement();

            Send(new GuildMemberLeavingMessage()
            {
                kicked = kicked,
                memberId = member.CharacterId
            });

            if (Record.Members.Count == 0)
            {
                GuildsManager.Instance.RemoveGuild(this);
            }
        }

        public void AddExp(GuildMemberRecord member, long amount)
        {
            var level = this.Level;

            Experience += amount;
            member.GivenExperience += amount;
            Record.UpdateElement();

            if (this.Level > level)
            {
                this.Send(new GuildLevelUpMessage(Level));

                foreach (var guildMember in OnlineMembers.Values)
                {
                    guildMember.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 208, Level);
                }
            }

        }

        public long AdjustGivenExperience(Character character, long delta)
        {
            int num = (int)(character.Level - this.Level);
            long result;
            for (int i = GuildsManager.XpPerGap.Length - 1; i >= 0; i--)
            {
                if ((double)num > GuildsManager.XpPerGap[i][0])
                {
                    result = (long)((double)delta * GuildsManager.XpPerGap[i][1] * 0.01);
                    return result;
                }
            }
            result = (long)((double)delta * GuildsManager.XpPerGap[0][1] * 0.01);
            return result;
        }

        /*
         * 1 meneur
         * 2 bras droit
         */
        [WIP]
        public void ChangeParameters(GuildMemberRecord member, byte experienceGivenPercent, int rank)
        {

            if (member.Rank == 1)
            {
                return;
            }
            //      member.Rights = (GuildRightsBitEnum)rights;
            member.ExperienceGivenPercent = experienceGivenPercent;
            member.Rank = rank;
            Record.UpdateElement();

            Character character = GetOnlineMember(member.CharacterId);

            if (character != null)
            {

                character.SendGuildMembership();
            }
        }

        public Character GetOnlineMember(long id)
        {
            return OnlineMembers.TryGetValue(id);
        }
        public void SetMotd(Character source, string content)
        {
            if (content.Length > GuildsManager.MotdMaxLength || content == string.Empty)
            {
                return;
            }

            Record.Motd = new GuildMotd()
            {
                Content = content,
                MemberId = source.Id,
                Timestamp = DateTime.Now.GetUnixTimeStamp(),
                MemberName = source.Name,
            };

            Record.UpdateElement();

            RefreshMotd();

        }
        public void RefreshMotd()
        {
            foreach (var character in OnlineMembers.Values)
            {
                RefreshMotd(character);
            }
        }
        public void RefreshMotd(Character member)
        {
            if (Record.Motd.Content == string.Empty)
            {
                return;
            }

            member.Client.Send(new GuildMotdMessage()
            {
                content = Record.Motd.Content,
                memberId = Record.Motd.MemberId,
                memberName = Record.Motd.MemberName,
                timestamp = Record.Motd.Timestamp,
            });


        }
        public BasicGuildInformations GetBasicGuildInformations()
        {
            return new BasicGuildInformations()
            {
                guildId = (int)Id,
                guildLevel = Level,
                guildName = Record.Name,
            };
        }
        public GuildInformations GetGuildInformations()
        {
            return new GuildInformations()
            {
                guildEmblem = Record.Emblem.ToGuildEmblem(),
                guildId = (int)Id,
                guildLevel = Level,
                guildName = Record.Name,
            };
        }

        public bool CanAddMember()
        {
            return Record.Members.Count < GuildsManager.MaxMemberCount;
        }

        public void Send(NetworkMessage message)
        {
            foreach (var character in OnlineMembers.Values)
            {
                character.Client.Send(message);
            }
        }
        [WIP("paddocks?")]
        public GuildInformationsGeneralMessage GetGuildInformationsGeneralMessage()
        {
            return new GuildInformationsGeneralMessage()
            {
                abandonnedPaddock = false,
                creationDate = Record.CreationDate.GetUnixTimeStamp(),
                experience = Record.Experience,
                expLevelFloor = ExperienceLowerBound,
                expNextLevelFloor = ExperienceUpperBound,
                level = Level,
                nbConnectedMembers = (short)OnlineMembers.Count,
                nbTotalMembers = (short)Record.Members.Count,
            };
        }
        [WIP]
        public GuildInformationsMembersMessage GetGuildInformationsMembersMessage()
        {
            foreach (var member in Record.Members.ToArray()) // remove this loop
            {
                var record = CharacterRecord.GetCharacterRecord(member.CharacterId);

                if (record == null)
                {
                    Record.Members.Remove(member);
                }
            }

            return new GuildInformationsMembersMessage()
            {
                members = Record.Members.Select(x => x.ToGuildMember(this)).ToArray(),
            };
        }
    }
}
