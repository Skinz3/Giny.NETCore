using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Network.Messages;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Handlers;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Experiences;
using Giny.World.Network;
using Giny.World.Records.Characters;
using Giny.World.Records.Guilds;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Giny.World.Managers.Guilds
{
    public class Guild
    {
        public static int BOSS_RANK_ID = 1;
        public static int OFFICER_RANK_ID = 2;
        public static int INITIATE_RANK_ID = 3;
        public static int NEWBIE_RANK_ID = 4;

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

        public bool Join(Character character, int rankId)
        {
            if (Record.Members.Count == GuildsManager.MaxMemberCount)
            {
                return false;
            }

            GuildMemberRecord memberRecord = new GuildMemberRecord(character, rankId);
            Record.Members.Add(memberRecord);

            NotifyMemberJoin(character.Name);

            Record.UpdateLater();
            OnlineMembers.TryAdd(character.Id, character);
            character.OnGuildJoined(this, memberRecord);

            RefreshMembers();
            AddGlobalActivity(GuildGlobalActivityTypeEnum.PLAYER_FLOW, character.Id.ToString(), character.Name, PlayerFlowEventTypeEnum.JOIN.ToString());

            return true;
        }

        public bool CreateRank(int order, int gfxId, bool modifiable, GuildRightsEnum[] rights, int id, string name)
        {
            GuildRankRecord rankRecord = new GuildRankRecord(order, gfxId, modifiable, rights, id, name);
            Record.Ranks.Add(rankRecord);
            Record.UpdateLater();
            RefreshRanks();
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
            RefreshBulletin(character);

            foreach (var onlineMember in OnlineMembers.Values.Where(x => x.Id != character.Id))
                onlineMember.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 224, character.Name);
        }
        public void OnDisconnected(Character character)
        {
            OnlineMembers.TryRemove(character.Id);

            foreach (var onlineMember in OnlineMembers.Values)
            {
                onlineMember.Client.Send(new GuildMemberLeavingMessage(false, character.Id));
                onlineMember.Client.Send(new GuildMemberOnlineStatusMessage(character.Id, false));
            }
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

            string memberName = "";

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
                memberName = target.Name;
            }
            else
            {
                CharacterRecord characterRecord = CharacterRecord.GetCharacterRecord(member.CharacterId);
                characterRecord.GuildId = 0;

                memberName = characterRecord.Name;
            }

            Record.Members.Remove(member);

            Record.UpdateLater();

            RefreshMembers();
            source.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 177, memberName);
            NotifyMemberLeave(memberName);

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

        public void NotifyMemberJoin(string memberName)
        {
            foreach (var onlineMember in OnlineMembers.Values)
            {
                onlineMember.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 1423, memberName);
            }
        }

        public void NotifyMemberLeave(string memberName)
        {
            foreach (var onlineMember in OnlineMembers.Values)
            {
                onlineMember.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 1430, memberName);
            }
        }

        public void AddExp(GuildMemberRecord member, long amount)
        {
            var level = Level;

            Experience += amount;
            member.GivenExperience += amount;
            Record.UpdateLater();

            if (Level > level)
            {
                Send(new GuildLevelUpMessage(Level));

                AddGlobalActivity(GuildGlobalActivityTypeEnum.LEVELUP, Level.ToString());

                foreach (var onlineMember in OnlineMembers.Values)
                {
                    onlineMember.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 208, Level);
                }
            }

        }

        public void AddGlobalActivity(GuildGlobalActivityTypeEnum type, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {
            Record.GlobalActivities.Add(new GuildGlobalActivityRecord()
            {
                Date = DateTime.Now,
                Type = type,
                Param1 = param1,
                Param2 = param2,
                Param3 = param3,
                Param4 = param4,
                Param5 = param5
            });

            Record.UpdateLater();
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

        public int GetRankNextId()
        {
            var lastRank = Record.Ranks.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();

            if (lastRank == 0)
                return 1;

            return lastRank + 1;
        }

        public int GetRankNextOrder()
        {
            var lastRank = Record.Ranks.OrderByDescending(x => x.Order).Select(x => x.Order).FirstOrDefault();

            if (lastRank == 0)
                return 1;

            return lastRank + 1;
        }

        public void ChangeRank(GuildMemberRecord member, RankInformation rankInformation)
        {
            if (member.HasRight(GuildRightsEnum.RIGHT_MANAGE_RANKS_AND_RIGHTS, this))
            {
                var rankRecord = GetGuildRank(rankInformation.id);

                if (rankRecord != null)
                {
                    rankRecord.Name = rankInformation.name;
                    rankRecord.Order = rankInformation.order;
                    rankRecord.GfxId = rankInformation.gfxId;

                    Record.UpdateLater();
                    RefreshRanks();
                    AddGlobalActivity(GuildGlobalActivityTypeEnum.RANK, GuildRankActivityTypeEnum.UPDATE.ToString(), rankRecord.Id.ToString());
                }
            }
        }

        public void ChangeRights(GuildMemberRecord member, int rankId, int[] rights)
        {
            if (member.HasRight(GuildRightsEnum.RIGHT_MANAGE_RANKS_AND_RIGHTS, this))
            {
                var rankRecord = GetGuildRank(rankId);

                if (rankRecord != null)
                {
                    rankRecord.Rights = rights.Select(x => (GuildRightsEnum)x).ToArray();

                    Record.UpdateLater();
                    RefreshRights(rankId, rights);
                }
            }
        }

        public void ChangeNote(GuildMemberRecord member, GuildMemberRecord target, string note)
        {
            if (member.HasRight(GuildRightsEnum.RIGHT_MANAGE_MEMBERS_NOTE, this))
            {
                target.Note = note;
                target.NoteLastEditDate = DateTime.Now;
                Record.UpdateLater();
                RefreshMember(target);
            }
        }

        [Annotation]
        public void ChangeParameters(GuildMemberRecord member, GuildMemberRecord target, byte experienceGivenPercent, int rank)
        {
            if ((member.CharacterId == target.CharacterId && member.HasRight(GuildRightsEnum.RIGHT_MANAGE_SELF_XP_CONTRIBUTION, this)) || (member.CharacterId != target.CharacterId && member.HasRight(GuildRightsEnum.RIGHT_MANAGE_ALL_XP_CONTRIBUTION, this)))
            {
                target.ExperienceGivenPercent = experienceGivenPercent;
            }

            if (member.HasRight(GuildRightsEnum.RIGHT_ASSIGN_RANKS, this))
            {
                target.Rank = rank;

                Character character = GetOnlineMember(member.CharacterId);

                if (character != null)
                {
                    character.SendGuildMembership();
                }
            }

            RefreshMember(target);
            Record.UpdateLater();
        }

        public GuildRankRecord GetGuildRank(int id)
        {
            return Record.Ranks.FirstOrDefault(x => x.Id == id);
        }

        public Character GetOnlineMember(long id)
        {
            return OnlineMembers.TryGetValue(id);
        }
        public Character GetOnlineMember(string name)
        {
            return OnlineMembers.Values.FirstOrDefault(x => x.Name == name);
        }

        public void SetBulletin(Character source, string content)
        {
            if (content == string.Empty)
            {
                return;
            }

            Record.Bulletin = new GuildBulletinRecord()
            {
                Content = content,
                MemberId = source.Id,
                Timestamp = DateTime.Now.GetUnixTimeStamp(),
                MemberName = source.Name,
            };

            Record.Bulletin.LastNotifiedTimestamp = DateTime.Now.GetUnixTimeStamp();


            Record.UpdateLater();
            RefreshBulletin();
        }


        public void SetMotd(Character source, string content)
        {
            if (content.Length > GuildsManager.MotdMaxLength || content == string.Empty)
            {
                return;
            }

            Record.Motd = new GuildMotdRecord()
            {
                Content = content,
                MemberId = source.Id,
                Timestamp = DateTime.Now.GetUnixTimeStamp(),
                MemberName = source.Name,
            };

            Record.UpdateLater();

            RefreshMotd();

        }

        public void RefreshMotd()
        {
            foreach (var character in OnlineMembers.Values)
            {
                RefreshMotd(character);
            }
        }

        public void RefreshBulletin()
        {
            foreach (var character in OnlineMembers.Values)
            {
                RefreshBulletin(character);
            }
        }

        public void RefreshMembers()
        {
            foreach (var character in OnlineMembers.Values)
            {
                character.Client.Send(GetGuildInformationsMembersMessage());
            }
        }

        public void RefreshMember(GuildMemberRecord member)
        {
            foreach (var character in OnlineMembers.Values)
            {
                character.Client.Send(GetGuildInformationsMemberUpdateMessage(member));
            }
        }

        public void RefreshRanks()
        {
            foreach (var character in OnlineMembers.Values)
            {
                character.Client.Send(GetGuildRanksMessage());
            }
        }

        public void RefreshRights(int rankId, int[] rights)
        {
            foreach (var character in OnlineMembers.Values)
            {
                character.Client.Send(new UpdateGuildRightsMessage(rankId, rights));
            }
        }

        public void RefreshBulletin(Character character, bool notify = false)
        {
            if (Record.Bulletin.Content == string.Empty)
            {
                return;
            }

            character.Client.Send(new GuildBulletinMessage()
            {
                content = Record.Bulletin.Content,
                timestamp = Record.Bulletin.Timestamp,
                memberId = Record.Bulletin.MemberId,
                memberName = Record.Bulletin.MemberName,
            });
        }

        public void RefreshMotd(Character character)
        {
            if (Record.Motd.Content == string.Empty)
            {
                return;
            }

            character.Client.Send(new GuildMotdMessage()
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
                guildName = Record.Name,
                guildLevel = Level
            };
        }
        public GuildInformations GetGuildInformations()
        {
            return new GuildInformations()
            {
                guildEmblem = Record.Emblem.ToSocialEmblem(),
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
            };
        }

        [Annotation]
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

            return new GuildInformationsMembersMessage(Record.Members.Select(x => x.ToGuildMember(this)).ToArray());
        }

        public GuildJoinedMessage GetGuildJoinedMessage(GuildMemberRecord member)
        {
            return new GuildJoinedMessage()
            {
                guildInfo = GetGuildInformations(),
                rankId = member.Rank,
            };
        }

        public GuildRanksMessage GetGuildRanksMessage()
        {
            return new GuildRanksMessage(Record.Ranks.Select(x => x.ToGuildRankInformation()).ToArray());
        }



        public RecruitmentInformationMessage GetRecruitmentInformationMessage()
        {
            return new RecruitmentInformationMessage(GetGuildRecruitmentInformation());
        }

        public GuildHousesInformationMessage GetGuildHousesInformationMessage()
        {
            return new GuildHousesInformationMessage(new HouseInformationsForGuild[0]);
        }

        public GuildInformationsPaddocksMessage GetGuildInformationsPaddocksMessage()
        {
            return new GuildInformationsPaddocksMessage(10, new PaddockContentInformations[0]);
        }

        public GuildFactsMessage GetGuildFactsMessage()
        {
            return new GuildFactsMessage()
            {
                infos = GetGuildFactSheetInformations(),
                creationDate = Record.CreationDate.GetUnixTimeStamp(),
                members = Record.Members.Select(x => x.ToCharacterMinimalGuildPublicInformations()).ToArray(),
            };
        }

        public GuildRecruitmentInformation GetGuildRecruitmentInformation()
        {
            return new GuildRecruitmentInformation()
            {
                socialId = (int)Id,
                recruitmentType = (byte)SocialRecruitmentTypeEnum.AUTOMATIC,
                recruitmentTitle = "TEST",
                recruitmentText = "TEST TEST",
                selectedLanguages = new int[0],
                selectedCriterion = new int[0],
                minLevel = 100,
                minLevelFacultative = false,
                minSuccess = 100,
                minSuccessFacultative = false,
                invalidatedByModeration = false,
                lastEditPlayerName = "Davidax",
                lastEditDate = 0,
                recruitmentAutoLocked = false
            };
        }


        public GuildFactSheetInformations GetGuildFactSheetInformations()
        {
            return new GuildFactSheetInformations()
            {
                leaderId = 1,
                nbMembers = (short)Record.Members.Count(),
                lastActivityDay = 0,
                recruitment = GetGuildRecruitmentInformation(),
                nbPendingApply = 0,
                guildId = (int)Id,
                guildName = Record.Name,
                guildLevel = Level,
                guildEmblem = Record.Emblem.ToSocialEmblem(),
            };
        }

        public GuildInformationsMemberUpdateMessage GetGuildInformationsMemberUpdateMessage(GuildMemberRecord member)
        {
            return new GuildInformationsMemberUpdateMessage()
            {
                member = member.ToGuildMember(this)
            };
        }

        public GuildLogbookInformationMessage GetGuildLogbookInformationMessage()
        {
            return new GuildLogbookInformationMessage()
            {
                globalActivities = GetGuildGlobalLogBookEntry(),
                chestActivities = GetGuildChestLogBookEntry()
            };
        }

        public GuildLogbookEntryBasicInformation[] GetGuildChestLogBookEntry()
        {
            List<GuildLogbookEntryBasicInformation> entries = new List<GuildLogbookEntryBasicInformation>();
            return entries.ToArray();
        }

        public GuildLogbookEntryBasicInformation[] GetGuildGlobalLogBookEntry()
        {
            List<GuildLogbookEntryBasicInformation> entries = new List<GuildLogbookEntryBasicInformation>();

            var sortedList = Record.GlobalActivities.Select((value, index) => new { Value = value, Index = index })
                              .OrderByDescending(item => item.Index)
                              .Select(item => item.Value)
                              .ToList();

            int index = 1;
            foreach (var activity in sortedList)
            {
                if (activity.Type == GuildGlobalActivityTypeEnum.RANK)
                {
                    GuildRankActivityTypeEnum rankActivityType = (GuildRankActivityTypeEnum)Enum.Parse(typeof(GuildRankActivityTypeEnum), activity.Param1);
                    var rankRecord = GetGuildRank(int.Parse(activity.Param2));

                    if (rankRecord != null)
                    {
                        entries.Add(new GuildRankActivity((byte)rankActivityType, rankRecord.ToGuildRankMinimalInformation(), index, activity.Date.GetUnixTimeStampDouble()));
                        index++;
                    }
                }
                else if (activity.Type == GuildGlobalActivityTypeEnum.LEVELUP)
                {
                    entries.Add(new GuildLevelUpActivity(byte.Parse(activity.Param1), index, activity.Date.GetUnixTimeStampDouble()));
                    index++;
                }
                else if (activity.Type == GuildGlobalActivityTypeEnum.PLAYER_FLOW)
                {
                    PlayerFlowEventTypeEnum playerFlowEventType = (PlayerFlowEventTypeEnum)Enum.Parse(typeof(PlayerFlowEventTypeEnum), activity.Param3);

                    entries.Add(new GuildPlayerFlowActivity(int.Parse(activity.Param1), activity.Param2, (byte)playerFlowEventType, index, activity.Date.GetUnixTimeStampDouble()));
                    index++;
                }
            }

            return entries.ToArray();
        }
    }
}