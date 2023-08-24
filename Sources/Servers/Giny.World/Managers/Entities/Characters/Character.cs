
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO.Configuration;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Api;
using Giny.World.Handlers.Roleplay.Maps.Paths;
using Giny.World.Managers.Achievements;
using Giny.World.Managers.Bidshops;
using Giny.World.Managers.Breeds;
using Giny.World.Managers.Criterias;
using Giny.World.Managers.Dialogs;
using Giny.World.Managers.Dialogs.DialogBox;
using Giny.World.Managers.Entities.Characters.HumanOptions;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Managers.Exchanges;
using Giny.World.Managers.Exchanges.Jobs;
using Giny.World.Managers.Exchanges.Trades;
using Giny.World.Managers.Experiences;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Formulas;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Guilds;
using Giny.World.Managers.Items;
using Giny.World.Managers.Items.Collections;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Maps.Elements;
using Giny.World.Managers.Maps.Teleporters;
using Giny.World.Managers.Parties;
using Giny.World.Managers.Shortcuts;
using Giny.World.Managers.Skills;
using Giny.World.Managers.Spells;
using Giny.World.Managers.Stats;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Achievements;
using Giny.World.Records.Bidshops;
using Giny.World.Records.Breeds;
using Giny.World.Records.Characters;
using Giny.World.Records.Guilds;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using Giny.World.Records.Npcs;
using Giny.World.Records.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters
{
    public class Character : Entity
    {
        public override long Id => Record.Id;

        public override string Name => Record.Name;

        public CharacterRecord Record
        {
            get;
            private set;
        }

        public Party Party
        {
            get;
            set;
        }

        public List<Party> GuestedParties
        {
            get;
            set;
        }

        public bool HasParty => Party != null;

        public GameContextEnum? Context
        {
            get;
            private set;
        }

        public bool Busy => Dialog != null || RequestBox != null || ChangeMap || Collecting || IsMoving || IsDead();

        public CharacterFighter Fighter
        {
            get;
            private set;
        }
        public GeneralShortcutBar GeneralShortcutBar
        {
            get;
            private set;
        }
        public SpellShortcutBar SpellShortcutBar
        {
            get;
            private set;
        }
        private List<CharacterHumanOption> HumanOptions
        {
            get;
            set;
        }

        public EntityStats Stats => Record.Stats;

        public BreedRecord Breed
        {
            get;
            private set;
        }
        public Guild Guild
        {
            get;
            private set;
        }
        [Annotation("useless?")]
        public GuildMemberRecord GuildMember
        {
            get;
            private set;
        }
        public bool HasGuild => Record.GuildId != 0;

        public bool JustCreatedOrReplayed
        {
            get;
            set;
        }
        public WorldClient Client
        {
            get;
            private set;
        }
        public DateTime LastSalesChatMessage
        {
            get;
            set;
        }
        public DateTime LastSeekChatMessage
        {
            get;
            set;
        }
        public bool ChangeMap
        {
            get;
            private set;
        }
        public override short CellId
        {
            get
            {
                return Record.CellId;
            }
            set
            {
                Record.CellId = value;
            }
        }


        private short m_level;

        public short Level
        {
            get
            {
                return m_level;
            }

            private set
            {
                this.m_level = value;
                this.LowerBoundExperience = ExperienceManager.Instance.GetCharacterXPForLevel(Level);
                this.UpperBoundExperience = ExperienceManager.Instance.GetCharacterXPForNextLevel(Level);
            }
        }


        public short SafeLevel
        {
            get
            {
                return Level > ExperienceManager.MaxLevel ? ExperienceManager.MaxLevel : Level;
            }
        }
        public override DirectionsEnum Direction
        {
            get
            {
                return Record.Direction;
            }
            set
            {
                Record.Direction = value;
            }
        }
        public bool IsMoving
        {
            get;
            private set;
        }
        public short[] MovementKeys
        {
            get;
            private set;
        }
        public long LowerBoundExperience
        {
            get;
            private set;
        }
        public long UpperBoundExperience
        {
            get;
            private set;
        }
        public long Experience
        {
            get
            {
                return this.Record.Experience;
            }
            private set
            {
                this.Record.Experience = value;

                if (value >= this.UpperBoundExperience && this.Level < ExperienceManager.MaxLevelOmega || value < this.LowerBoundExperience)
                {
                    short level = this.Level;
                    this.Level = ExperienceManager.Instance.GetCharacterLevel(Record.Experience);
                    short difference = (short)(this.Level - level);
                    this.OnLevelChanged(level, difference);
                }
            }
        }


        public override ServerEntityLook Look
        {
            get
            {
                return Record.GetActiveLook();
            }
            set
            {
                Record.Look = value;
            }
        }
        public bool CharacterLoadingComplete
        {
            get;
            set;
        }

        public short MovedCell
        {
            get;
            set;
        }
        public bool Fighting => Fighter != null;

        public Inventory Inventory
        {
            get;
            private set;
        }

        public int? AchievementPoints
        {
            get;
            private set;
        }

        public FighterRefusedReasonEnum CanRequestFight(Character target)
        {
            FighterRefusedReasonEnum result;

            if (target.Fighting || target.Busy)
            {
                result = FighterRefusedReasonEnum.OPPONENT_OCCUPIED;
            }
            else
            {
                if (this.Fighting || this.Busy)
                {
                    result = FighterRefusedReasonEnum.IM_OCCUPIED;
                }
                else
                {
                    if (target == this)
                    {
                        result = FighterRefusedReasonEnum.FIGHT_MYSELF;
                    }
                    else
                    {
                        if (this.ChangeMap || target.ChangeMap || target.Map != Map || !Map.Position.AllowFightChallenges)
                        {
                            result = FighterRefusedReasonEnum.WRONG_MAP;
                        }
                        else
                        {
                            result = FighterRefusedReasonEnum.FIGHTER_ACCEPTED;
                        }
                    }
                }
            }
            return result;
        }




        public BankItemCollection BankItems
        {
            get;
            private set;
        }
        public Dialog Dialog
        {
            get;
            set;
        }
        public RequestBox RequestBox
        {
            get;
            set;
        }

        public bool Collecting
        {
            get;
            set;
        }
        public List<SkillRecord> SkillsAllowed
        {
            get;
            private set;
        }
        public double XpBonusPercent
        {
            get;
            set;
        }
        public double XpRatioMount
        {
            get;
            set;
        }
        public double XpGuildGivenPercent
        {
            get;
            set;
        }
        public double XpAlliancePrismBonusPercent
        {
            get;
            set;
        }
        [Annotation("pokefus , companions (verification cellule)")]
        public int FighterCount => 1;

        public Character(WorldClient client, CharacterRecord record) : base(null)
        {
            this.Record = record;
            this.Client = client;
            this.CharacterLoadingComplete = false;
            this.Level = ExperienceManager.Instance.GetCharacterLevel(Experience);
            this.Breed = BreedRecord.GetBreed(record.BreedId);

            this.Inventory = new Inventory(this, CharacterItemRecord.GetCharacterItems(this.Id));

            this.Inventory.ApplyEquipementItemsEffects();

            this.BankItems = new BankItemCollection(this, BankItemRecord.GetBankItems(Client.Account.Id));
            this.GuestedParties = new List<Party>();
            this.GeneralShortcutBar = new GeneralShortcutBar(this);
            this.SpellShortcutBar = new SpellShortcutBar(this);
            this.HumanOptions = new List<CharacterHumanOption>();

            this.SkillsAllowed = SkillsManager.Instance.GetAllowedSkills(this);
            this.Collecting = false;

            this.AchievementPoints = Record.Achievements.Where(x => x.Finished).Sum(x => x.Record.Points);
        }


        private void CheckSoldItems()
        {
            BidShopItemRecord[] bidHouseItems = BidshopsManager.Instance.GetSoldItem(this).ToArray();

            if (bidHouseItems.Count() > 0)
            {
                foreach (var item in bidHouseItems)
                {
                    Client.WorldAccount.BankKamas += item.Price;
                    Client.WorldAccount.UpdateElement();
                    BidshopsManager.Instance.RemoveItem(item.BidShopId, item);
                }

                Client.Send(new ExchangeOfflineSoldItemsMessage(bidHouseItems.Select(x => x.GetObjectItemQuantityPriceDateEffects()).ToArray()));

            }

        }

        public void OnInitiateFight(Fight fight)
        {
            if (Party != null)
            {
                Party.OnInitiateFight(this, fight);
            }
        }

        public void DestroyContext()
        {
            Client.Send(new GameContextDestroyMessage());
            this.Context = null;
        }
        public void SendServerExperienceModificator()
        {
            Client.Send(new ServerExperienceModificatorMessage((short)(ConfigManager<WorldConfig>.Instance.XpRate * 100d)));
        }
        public void DebugHighlightCells(Color color, IEnumerable<CellRecord> cells)
        {
            Client.Send(new DebugHighlightCellsMessage(color.ToArgb(), cells.Select(x => x.Id).ToArray()));
        }
        public void DebugClearHighlightCells()
        {
            Client.Send(new DebugClearHighlightCellsMessage());
        }
        public void CreateContext(GameContextEnum context)
        {
            if (Context.HasValue)
            {
                DestroyContext();
            }

            this.Context = context;
            Client.Send(new GameContextCreateMessage((byte)Context));
        }



        public void CreateHumanOptions()
        {
            this.HumanOptions.Add(new CharacterHumanOptionFollowers());

            if (Record.ActiveOrnamentId > 0)
            {
                HumanOptions.Add(HumanOptionsManager.Instance.CreateHumanOptionOrnament(this));
            }
            if (Record.ActiveTitleId > 0)
            {
                HumanOptions.Add(HumanOptionsManager.Instance.CreateHumanOptionTitle(this));
            }

            if (Guild != null)
            {
                HumanOptions.Add(HumanOptionsManager.Instance.CreateHumanOptionGuild());
            }

            CharacterEventApi.HumanOptionsCreated(this);

        }

        public void UpdateSpells(short oldLevel, short newLevel)
        {
            foreach (var spell in Record.Spells)
            {
                if (spell.ActiveSpellRecord.MinimumLevel > oldLevel && spell.ActiveSpellRecord.MinimumLevel <= Level)
                {
                    if (SpellShortcutBar.CanAdd())
                    {
                        SpellShortcutBar.Add(spell.SpellId);
                        TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 3, spell.SpellId);
                    }
                }
            }
            RefreshShortcuts();
        }
        public void LearnSpell(short spellId, bool notify)
        {
            if (!HasSpell(spellId))
            {
                var spell = new CharacterSpell(spellId);
                Record.Spells.Add(spell);

                if (spell.Learned(this) && SpellShortcutBar.CanAdd())
                {
                    SpellShortcutBar.Add(spellId);

                    if (notify)
                    {
                        RefreshShortcuts();
                        TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 3, spellId);
                    }
                }

                if (notify)
                {
                    RefreshSpells();
                }
            }
        }
        public bool HasSpell(short spellId)
        {
            return Record.Spells.Any(x => x.ActiveSpellRecord.Id == spellId);
        }
        public void RefreshSpells()
        {
            Client.Send(new SpellListMessage(false, Record.Spells.Select(x => x.GetSpellItem(this)).ToArray()));
        }
        public void OnExchangeError(ExchangeErrorEnum error)
        {
            this.Client.Send(new ExchangeErrorMessage((byte)error));
        }
        public void OnChatError(ChatErrorEnum error)
        {
            Client.Send(new ChatErrorMessage((byte)error));
        }
        public bool AddKamas(long value)
        {
            if (value <= long.MaxValue)
            {
                if (Record.Kamas + value >= Inventory.MaximumKamas)
                {
                    Record.Kamas = Inventory.MaximumKamas;
                }
                else
                    Record.Kamas += value;

                Inventory.RefreshKamas();
                return true;
            }
            return false;
        }
        public bool RemoveKamas(long value)
        {
            if (Record.Kamas >= value)
            {
                Record.Kamas -= value;
                Inventory.RefreshKamas();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void OnKamasGained(long amount)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 45, new object[] { amount });
        }
        public void OnKamasLost(long amount)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 46, new object[] { amount });
        }
        public void RefreshShortcuts()
        {
            SpellShortcutBar.Refresh();
            GeneralShortcutBar.Refresh();
        }
        public void RefreshJobs()
        {
            Client.Send(new JobCrafterDirectorySettingsMessage(Record.Jobs.Select(x => x.GetDirectorySettings()).ToArray()));
            Client.Send(new JobDescriptionMessage(Record.Jobs.Select(x => x.GetJobDescription()).ToArray()));
            Client.Send(new JobExperienceMultiUpdateMessage(Record.Jobs.Select(x => x.GetJobExperience()).ToArray()));
        }


        public void RefreshGuild()
        {
            if (HasGuild)
            {
                Guild = GuildsManager.Instance.GetGuild(Record.GuildId);
                GuildMember = Guild.Record.GetMember(Id);
                SendGuildMembership();
            }
        }

        public void SendKnownZaapList()
        {
            Client.Send(new KnownZaapListMessage(TeleportersManager.Instance.GetMaps(TeleporterTypeEnum.TELEPORTER_ZAAP).Select(x => (double)x).ToArray()));
        }

        public void PlaySpellAnimOnMap(short cellId, short spellId, short spellLevel, DirectionsEnum direction)
        {
            Map.Instance.Send(new GameRolePlaySpellAnimMessage(Id, cellId, spellId, spellLevel, (short)direction));
            this.Direction = direction;
        }
        public void PlaySpellAnim(short cellId, short spellId, short spellLevel, DirectionsEnum direction)
        {
            Client.Send(new GameRolePlaySpellAnimMessage(Id, cellId, spellId, spellLevel, (short)direction));
            this.Direction = direction;
        }
        public void SendGuildMembership()
        {
            Client.Send(new GuildMembershipMessage()
            {
                guildInfo = Guild.GetGuildInformations(),
                rankId = GuildMember.Rank,
            });
        }
        public CharacterJob GetJob(JobTypeEnum jobType)
        {
            return Record.Jobs.FirstOrDefault(x => x.JobId == (byte)jobType);
        }
        public CharacterJob GetJob(byte jobType)
        {
            return Record.Jobs.FirstOrDefault(x => x.JobId == jobType);
        }
        public void AddJobExp(byte jobType, long amount)
        {
            CharacterJob job = GetJob(jobType);
            short currentLevel = job.Level;
            long highest = ExperienceManager.Instance.GetCharacterXPForLevel(ExperienceManager.MaxLevel);

            if (job.Experience + amount > highest)
                job.Experience = highest;
            else
                job.Experience += amount;

            Client.Send(new JobExperienceUpdateMessage(job.GetJobExperience()));

            if (currentLevel != job.Level)
            {
                Client.Send(new JobLevelUpMessage((byte)job.Level, job.GetJobDescription()));
                this.SkillsAllowed = SkillsManager.Instance.GetAllowedSkills(this);
            }

        }

        public void OpenCraftExchange(SkillRecord skill)
        {
            this.OpenDialog(new CraftExchange(this, skill));
        }
        public void OpenRuneTradeExchange()
        {
            this.OpenDialog(new RuneTradeExchange(this));
        }
        public void OpenSmithmagicExchange(SkillRecord skill)
        {
            this.OpenDialog(new SmithmagicExchange(this, skill));
        }
        public void OpenBuySellExchange(Npc npc, ItemRecord[] itemToSell, short tokenId)
        {
            this.OpenDialog(new BuySellExchange(this, npc, itemToSell, tokenId));
        }
        public void OpenNpcTradeExchange(Npc npc, NpcActionRecord action)
        {
            this.OpenDialog(new NpcTradeExchange(this, npc, action));
        }
        public void OpenBuyExchange(BidShopRecord bidshop)
        {
            this.OpenDialog(new BuyExchange(this, bidshop));
        }
        public void OpenSellExchange(BidShopRecord bidshop)
        {
            this.OpenDialog(new SellExchange(this, bidshop));
        }
        public void OpenBank()
        {
            this.OpenDialog(new BankExchange(this, BankItems));
        }
        public void OpenZaap(MapElement element)
        {
            this.OpenDialog(new ZaapDialog(this, element));
        }
        public void OpenBookDialog(int documentId)
        {
            this.OpenDialog(new BookDialog(this, documentId));
        }
        public void OpenGuildCreationDialog()
        {
            this.OpenDialog(new GuildCreationDialog(this));
        }
        public void OpenZaapi(MapElement element)
        {
            this.OpenDialog(new ZaapiDialog(this, element));
        }
        public void TalkToNpc(Npc npc, NpcActionRecord action)
        {
            this.OpenDialog(new NpcTalkDialog(this, npc, action));
        }

        public void OpenDialog(Dialog dialog)
        {
            if (!Busy)
            {
                try
                {
                    this.Dialog = dialog;
                    this.Dialog.Open();
                }
                catch
                {
                    ReplyError("Impossible d'éxecuter l'action.");
                    LeaveDialog();
                }
            }
            else
            {
                ReplyError("Unable to open dialog while busy...");
            }
        }
        public void LeaveDialog()
        {
            if (this.Dialog == null && !this.IsInRequest())
            {
                this.ReplyWarning("Unknown dialog...");
                return;
            }
            else
            {
                if (this.IsInRequest())
                {
                    this.CancelRequest();
                }
                if (this.Dialog != null)
                    this.Dialog.Close();
            }
        }
        public void CancelRequest()
        {
            if (this.IsInRequest())
            {
                if (this.IsRequestSource())
                {
                    this.RequestBox.Cancel();
                }
                else
                {
                    if (this.IsRequestTarget())
                    {
                        this.DenyRequest();
                    }
                }
            }
        }
        public bool IsInDialog()
        {
            return Dialog != null;
        }
        public bool IsInDialog<T>() where T : Dialog
        {
            return Dialog != null && Dialog is T;
        }
        public bool HasRequestBoxOpen<T>() where T : RequestBox
        {
            return RequestBox != null && RequestBox is T;
        }
        public bool IsInDialog(DialogTypeEnum type)
        {
            if (Dialog == null)
                return false;
            return Dialog.DialogType == type;
        }
        public bool IsInExchange(ExchangeTypeEnum type)
        {
            var exchange = GetDialog<Exchange>();
            if (exchange != null)
                return exchange.ExchangeType == type;
            else
                return false;
        }
        public void DenyRequest()
        {
            if (this.IsInRequest() && this.RequestBox.Target == this)
            {
                this.RequestBox.Deny();
            }
        }
        public bool IsRequestSource()
        {
            return this.IsInRequest() && this.RequestBox.Source == this;
        }
        public bool IsRequestTarget()
        {
            return this.IsInRequest() && this.RequestBox.Target == this;
        }
        public bool IsInRequest()
        {
            return this.RequestBox != null;
        }
        public ShortcutBar GetShortcutBar(ShortcutBarEnum barEnum)
        {
            switch (barEnum)
            {
                case ShortcutBarEnum.GENERAL_SHORTCUT_BAR:
                    return GeneralShortcutBar;
                case ShortcutBarEnum.SPELL_SHORTCUT_BAR:
                    return SpellShortcutBar;
            }

            throw new Exception("Unknown shortcut bar, " + barEnum);
        }
        public T GetDialog<T>() where T : Dialog
        {
            return (T)Dialog;
        }
        public void OpenUIByObject(ObjectUITypeEnum type, int itemUId)
        {
            Client.Send(new ClientUIOpenedByObjectMessage()
            {
                type = (byte)type,
                uid = itemUId,
            });
        }
        public void OpenRequestBox(RequestBox box)
        {
            box.Source.RequestBox = box;
            this.RequestBox = box;
            box.Open();
        }
        public void RefreshStats()
        {
            var stats = Record.Stats.GetCharacterCharacteristicsInformations(this);
            Client.Send(new CharacterStatsListMessage(stats));
        }

        public void OnStatUpgradeResult(StatsUpgradeResultEnum result, short nbCharacBoost)
        {
            Client.Send(new StatsUpgradeResultMessage((byte)result, nbCharacBoost));
        }
        public void SpawnPoint()
        {
            MapRecord targetMap = MapRecord.GetMap(Record.SpawnPointMapId);

            if (targetMap.HasZaap())
            {
                TeleportToZaap(targetMap);
            }
            else
            {
                Teleport(targetMap);
            }
        }
        public void TeleportToZaap(MapRecord map)
        {
            Teleport(map, map.GetNearCell(InteractiveTypeEnum.ZAAP16));
        }
        public void SetDirection(DirectionsEnum direction)
        {
            Record.Direction = direction;
            SendMap(new GameMapChangeOrientationMessage(new ActorOrientation(Id, (byte)direction)));
        }
        public void Teleport(long mapId, short? cellId = null)
        {
            Teleport(MapRecord.GetMap(mapId), cellId);
        }
        public void Teleport(MapRecord teleportMap, short? cellId = null)
        {
            if (Fighting)
                return;
            if (Busy)
                return;

            if (teleportMap != null)
            {
                if (Record.MapId != teleportMap.Id)
                {
                    ChangeMap = true;
                }
                else
                {
                    if (cellId.HasValue && CellId == cellId.Value && Map != null && Map.Id == teleportMap.Id)
                    {
                        /*
                         * La destination se trouve être la carte courante, la téléportation est annulée.
                         */
                        TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 597);
                        return;
                    }
                }

                if (cellId < 0 || cellId > 560)
                    cellId = teleportMap.RandomWalkableCell().Id;

                if (!cellId.HasValue || (cellId.HasValue && !teleportMap.IsCellWalkable(cellId.Value)))
                {
                    cellId = teleportMap.RandomWalkableCell().Id;
                }

                MovementKeys = null;

                this.IsMoving = false;


                if (cellId != null)
                    this.Record.CellId = cellId.Value;


                this.Record.MapId = teleportMap.Id;
                if (Map != null)
                    Map.Instance.RemoveEntity(this.Id);

                CurrentMapMessage(teleportMap.Id);
            }
            else
            {
                /*
                 * Téléportation impossible, il n'existe pas de destination possible.
                 */
                TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 493);
            }
        }
        public void EndMove()
        {
            this.Record.CellId = this.MovedCell;
            this.MovedCell = 0;
            this.IsMoving = false;
            this.MovementKeys = null;

            var element = Map.Instance.GetElements<MapElement>().Where(x => x.Record.CellId == this.Record.CellId).FirstOrDefault();

            if (element != null && element.Record.Skill != null && element.Record.Skill.ActionIdentifier == GenericActionEnum.Teleport)
            {
                Map.Instance.UseInteractive(this, element.Record.Identifier, 0);
            }
        }
        public void CancelMove(short cellId)
        {
            IsMoving = false;
            Record.CellId = cellId;
            Client.Send(new BasicNoOperationMessage());
        }
        public void MoveOnMap(short[] keyMovements)
        {
            if (!Busy)
            {
                short clientCellId = PathReader.ReadCell(keyMovements.First());

                if (clientCellId == CellId)
                {
                    if (Look.RemoveAura())
                    {
                        RefreshLookOnMap();
                    }

                    this.Direction = (DirectionsEnum)PathReader.GetDirection(keyMovements.Last());
                    this.MovedCell = PathReader.ReadCell(keyMovements.Last());
                    this.IsMoving = true;
                    this.MovementKeys = keyMovements;
                    this.SendMap(new GameMapMovementMessage(keyMovements, 0, this.Id));
                }
                else
                {
                    this.NoMove();
                }
            }
            else
            {
                this.NoMove();
            }
        }
        public void RefreshEmotes()
        {
            Client.Send(new EmoteListMessage(Record.KnownEmotes.ToArray()));
        }
        public bool LearnEmote(short id)
        {
            if (!Record.KnownEmotes.Contains(id))
            {
                Record.KnownEmotes.Add(id);
                Client.Send(new EmoteAddMessage(id));
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ForgetEmote(byte id)
        {
            if (Record.KnownEmotes.Contains(id))
            {
                Record.KnownEmotes.Remove(id);
                Client.Send(new EmoteRemoveMessage(id));
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PlayEmote(short emoteId)
        {
            EmoteRecord template = EmoteRecord.GetEmote(emoteId);

            if (!ChangeMap)
            {
                if (Look.RemoveAura())
                    RefreshLookOnMap();

                if (template.IsAura)
                {
                    short bonesId = EntityLookManager.Instance.GetAuraBones(this, emoteId);
                    this.Look.AddAura(bonesId);
                    this.RefreshLookOnMap();
                }
                else
                {
                    this.SendMap(new EmotePlayMessage(Id, Client.Account.Id, emoteId, 0));
                }
            }
        }

        public void OnItemAdded(CharacterItemRecord item)
        {
            // nothing todo, idols stuff
        }
        public void OnItemRemoved(CharacterItemRecord item)
        {
            // nothing todo, idols stuff
        }
        public void NotifyItemGained(int gid, int quantity)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, new object[] { quantity, gid });
        }
        public void NotifyItemSelled(int gid, int quantity, long price)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 65, new object[] { price, string.Empty, gid, quantity });
        }
        public void NotifyItemLost(int gid, int quantity)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 22, new object[] { quantity, gid });
        }
        [Annotation]
        public void OnLevelChanged(short oldLevel, short amount)
        {
            if (CharacterLoadingComplete)
            {
                this.SendMap(new CharacterLevelUpInformationMessage(Name, Id, Level));
                Client.Send(new CharacterLevelUpMessage(Level));

            }

            UpdateSpells(oldLevel, Level);

            if (Level > oldLevel)
            {
                if (oldLevel <= ExperienceManager.MaxLevel)
                {
                    if (Level > ExperienceManager.MaxLevel)
                    {
                        amount = (short)(ExperienceManager.MaxLevel - oldLevel);
                    }

                    Record.Stats.Life.Base += (5 * amount);

                    this.Stats[CharacteristicEnum.STATS_POINTS].Base += (short)(5 * amount);
                }

            }

            AchievementManager.Instance.OnPlayerChangeLevel(this);
            // remove this (achievements do the job) ?
            CharacterLevelRewardManager.Instance.OnCharacterLevelUp(this, oldLevel, Level);

            if (HasParty)
            {
                Party.UpdateMember(this);
            }

            if (CharacterLoadingComplete)
            {
                RefreshActorOnMap();
                RefreshStats();
            }
        }

        public void OnCharacterLoadingComplete()
        {
            this.CharacterLoadingComplete = true;
            Client.Send(new CharacterLoadingCompleteMessage());
            OnConnected();

        }
        private void OnConnected()
        {
            TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89, new string[0]); // not only when just created in th

            if (WorldServer.Instance.GetServerType() == GameServerTypeEnum.SERVER_TYPE_EPIC)
            {
                /*
                 * Bienvenue sur le serveur Épique. La mort contre les monstres est définitive
                 */
                TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 440);
            }

            this.Client.Send(new AlmanachCalendarDateMessage(1)); // for monsters!

            if (JustCreatedOrReplayed)
            {
                foreach (var item in InitialItemRecord.GetInitialItemRecords())
                {
                    Inventory.AddItem(item.GId, item.Quantity, true);
                }
            }

            this.Reply(ConfigManager<WorldConfig>.Instance.WelcomeMessage, Color.CornflowerBlue);
            CheckSoldItems();
            Guild?.OnConnected(this);
        }
        public void NoMove()
        {
            this.Client.Send(new GameMapNoMovementMessage((short)Point.X, (short)Point.Y));
        }

        /*    public void RegisterArena()
            {
                this.ArenaMember = ArenaProvider.Instance.Register(this);
                this.ArenaMember.UpdateStep(true, PvpArenaStepEnum.ARENA_STEP_REGISTRED);
            }
            public void UnregisterArena()
            {
                if (InArena)
                {
                    ArenaProvider.Instance.Unregister(this);
                    this.ArenaMember.UpdateStep(false, PvpArenaStepEnum.ARENA_STEP_UNREGISTER);
                    this.ArenaMember = null;
                }
            }
            public void AnwserArena(bool accept)
            {
                if (InArena)
                {
                    ArenaMember.Anwser(accept);
                }
            }
        */

        [Annotation]
        public void RefreshArenaInfos()
        {
            var infos = new ArenaRankInfos(new ArenaRanking(1, 2), new ArenaLeagueRanking(1, 1, 200, 100, 1), 1, 2, 3);
            Client.Send(new GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage(infos, infos, infos));

        }
        [Annotation]
        public void OnEnterMap()
        {

            this.ChangeMap = false;

            if (this.IsInDialog())
                this.LeaveDialog();

            if (!Fighting)
            {
                this.Map.Instance.AddEntity(this);

                this.Map.Instance.SendMapComplementary(Client);
                this.Map.Instance.SendMapFightCount(Client);

                foreach (Character current in this.Map.Instance.GetEntities<Character>())
                {
                    if (current.IsMoving)
                    {
                        Client.Send(new GameMapMovementMessage(current.MovementKeys, 0, current.Id));
                        Client.Send(new BasicNoOperationMessage());
                    }
                }

                Client.Send(new BasicNoOperationMessage());
                Client.Send(new BasicTimeMessage(DateTime.Now.GetUnixTimeStampDouble(), 1));
            }
            if (HasParty)
            {
                Party.UpdateMember(this);
            }
        }

        public bool IsDead()
        {
            return Record.HardcoreInformations.IsDead();
        }
        public void OnDisconnected()
        {
            Record.UpdateElement();

            Record.InGameContext = false;
            Guild?.OnDisconnected(this);


            if (Dialog != null)
                Dialog.Close();

            if (IsInRequest())
                CancelRequest();

            if (HasParty)
                Party.Leave(this);

            if (Fighting)
            {
                Fighter.OnDisconnected();
            }
            else
            {
                Map?.Instance?.RemoveEntity(this.Id);
            }
        }
        object ApplyPolice(object value, bool bold, bool underline)
        {
            if (bold)
                value = "<b>" + value + "</b>";
            if (underline)
                value = "<u>" + value + "</u>";
            return value;
        }
        public void Reply(object value, bool bold = false, bool underline = false)
        {
            value = ApplyPolice(value, bold, underline);
            Client.Send(new TextInformationMessage(0, 0, new string[] { value.ToString() }));
        }
        public void ReplyWarning(object value)
        {
            Reply(value, Color.DarkOrange, false, false);
        }
        public void ReplyError(object value)
        {
            Reply(value, Color.DarkRed, false, false);
        }
        public void Reply(object value, Color color, bool bold = false, bool underline = false)
        {
            value = ApplyPolice(value, bold, underline);
            Client.Send(new TextInformationMessage(0, 0, new string[] { string.Format("<font color=\"#{0}\">{1}</font>", color.ToArgb().ToString("X"), value) }));
        }
        public void TextInformation(TextInformationTypeEnum msgType, short msgId, params object[] parameters)
        {
            Client.Send(new TextInformationMessage((byte)msgType, msgId,
                (from entry in parameters
                 select entry.ToString()).ToArray()));
        }
        public void CurrentMapMessage(long mapId)
        {
            Client.Send(new CurrentMapMessage(mapId));
        }
        public ActorAlignmentInformations GetActorAlignmentInformations()
        {
            return new ActorAlignmentInformations(0, 0, 0, 0);
        }
        public override GameRolePlayActorInformations GetActorInformations()
        {
            return new GameRolePlayCharacterInformations(GetActorAlignmentInformations(),
                 Id, new EntityDispositionInformations(CellId, (byte)Direction),
                 Look.ToEntityLook(), Name, new HumanInformations(GetActorRestrictions(), Record.Sex, HumanOptions.Select(x => x.GetHumanOption(this)).ToArray()),
                 Record.AccountId);
        }
        public bool LearnOrnament(short id, bool notify)
        {
            if (!Record.KnownOrnaments.Contains(id))
            {
                Record.KnownOrnaments.Add(id);
                if (notify)
                    Client.Send(new OrnamentGainedMessage(id));
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool ForgetOrnament(short id, bool notify)
        {
            if (Record.KnownOrnaments.Contains(id))
            {
                Record.KnownOrnaments.Remove(id);

                if (Record.ActiveOrnamentId == id)
                {
                    RemoveAllHumanOption<CharacterHumanOptionOrnament>(true);
                }

                if (notify)
                {
                    Client.Send(new OrnamentLostMessage(id));
                }
                return true;
            }
            return false;

        }
        public bool HasOrnament(short id)
        {
            return Record.KnownOrnaments.Contains(id);
        }
        public bool ActiveOrnament(short id)
        {
            if (id == 0)
            {
                RemoveAllHumanOption<CharacterHumanOptionOrnament>(false);
                Record.ActiveOrnamentId = 0;
                RefreshActorOnMap();
                return true;
            }
            if (Record.KnownOrnaments.Contains(id))
            {
                RemoveAllHumanOption<CharacterHumanOptionOrnament>(false);
                Record.ActiveOrnamentId = id;
                HumanOptions.Add(HumanOptionsManager.Instance.CreateHumanOptionOrnament(this));
                RefreshActorOnMap();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool LearnTitle(short id)
        {
            if (!Record.KnownTitles.Contains(id))
            {
                Record.KnownTitles.Add(id);
                Client.Send(new TitleGainedMessage(id));
                return true;

            }
            return false;
        }

        public bool ForgetTitle(short id)
        {
            if (Record.KnownTitles.Contains(id))
            {
                if (Record.ActiveTitleId == id)
                {
                    ActiveTitle(0);
                }
                Record.KnownTitles.Remove(id);
                Client.Send(new TitleLostMessage(id));
                return true;
            }
            return false;

        }
        public bool HasTitle(short id)
        {
            return Record.KnownTitles.Contains(id) ? true : false;
        }

        public bool ActiveTitle(short id)
        {
            if (id == 0)
            {
                Record.ActiveTitleId = id;
                RemoveAllHumanOption<CharacterHumanOptionTitle>(true);
                return true;
            }
            if (HasTitle(id))
            {
                if (Record.ActiveTitleId == id)
                    return false;

                Record.ActiveTitleId = id;
                RemoveAllHumanOption<CharacterHumanOptionTitle>(true);
                AddHumanOption(HumanOptionsManager.Instance.CreateHumanOptionTitle(this), true);
                return true;

            }
            return false;

        }
        public ActorRestrictionsInformations GetActorRestrictions()
        {
            return new ActorRestrictionsInformations(false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false);
        }

        public void SetExperience(long value)
        {
            if (this.Level >= ExperienceManager.MaxLevelOmega)
            {
                return;
            }
            Experience = value;

            if (Experience >= this.UpperBoundExperience || Experience < this.LowerBoundExperience)
            {
                this.Level = ExperienceManager.Instance.GetCharacterLevel(Experience);
            }

            RefreshStats();

        }
        public void AddExperience(long value, bool notify = true)
        {
            SetExperience(Experience + value);

            if (notify)
            {
                this.Client.Send(new CharacterExperienceGainMessage(value, 0, 0, 0));
            }
        }
        public void UseItem(int uid, bool send)
        {
            var item = this.Inventory.GetItem(uid);

            if (item != null)
            {
                if (ItemsManager.Instance.UseItem(Client.Character, item))
                    this.Inventory.RemoveItem(item.UId, 1);

                if (send)
                {
                    this.RefreshStats();
                }
            }
        }
        public void AddHumanOption(CharacterHumanOption characterHumanOption, bool notify = false)
        {
            HumanOptions.Add(characterHumanOption);

            if (notify)
            {
                RefreshActorOnMap();
            }
        }
        public T GetHumanOption<T>() where T : CharacterHumanOption
        {
            return HumanOptions.OfType<T>().FirstOrDefault();
        }
        public void RemoveHumanOption(CharacterHumanOption characterHumanOption, bool notify = false)
        {
            HumanOptions.Remove(characterHumanOption);

            if (notify)
            {
                RefreshActorOnMap();
            }
        }
        public void RemoveAllHumanOption<T>(bool notify) where T : CharacterHumanOption
        {
            if (HumanOptions.RemoveAll(x => x is T) > 0 && notify)
            {
                RefreshActorOnMap();
            }
        }
        public CharacterSpell? GetSpellByBase(short spellId)
        {
            return Record.Spells.FirstOrDefault(x => x.SpellId == spellId);
        }
        public CharacterSpell? GetSpellByVariant(short spellId)
        {
            return Record.Spells.FirstOrDefault(x => x.VariantSpellRecord != null && x.VariantSpellRecord.Id == spellId);
        }
        public CharacterSpell? GetSpell(short spellId)
        {
            return Record.Spells.FirstOrDefault(x => x.ActiveSpellRecord.Id == spellId);
        }

        public void SendTitlesAndOrnamentsList()
        {
            Client.Send(new TitlesAndOrnamentsListMessage(Record.KnownTitles.ToArray(), Record.KnownOrnaments.ToArray(), Record.ActiveTitleId, Record.ActiveOrnamentId)); ;
        }

        public void RefreshAchievements()
        {
            IEnumerable<AchievementAchieved> achievementAchieveds = Record.Achievements.Where(x => x.Finished).Select(x => x.GetAchievementAchieved(Id));
            Client.Send(new AchievementListMessage(achievementAchieveds.ToArray()));
        }
        public IEnumerable<CharacterAchievement> GetFinishedAchievements()
        {
            return Record.Achievements.Where(x => x.Finished);
        }

        public IEnumerable<CharacterAchievement> GetStartedAchievements()
        {
            return Record.Achievements.Where(x => !x.Finished);
        }

        public CharacterAchievement? GetAchievement(int id)
        {
            return Record.Achievements.FirstOrDefault(x => x.AchievementId == id);
        }

        public void ReachAchievementObjective(AchievementRecord record, AchievementObjectiveRecord objective)
        {
            var characterAchievement = GetAchievement((int)record.Id);


            if (characterAchievement == null)
            {
                characterAchievement = new CharacterAchievement((short)record.Id, SafeLevel);
                characterAchievement.Initialize();
                Record.Achievements.Add(characterAchievement);
            }

            if (characterAchievement.Finished)
            {
                return;
            }

            characterAchievement.ReachObjective(objective.Id);

            if (characterAchievement.Finished)
            {
                ReachAchievement(record);
            }

        }
        public void ReachAchievement(AchievementRecord achievement)
        {
            var characterAchievement = GetAchievement((int)achievement.Id);

            if (characterAchievement == null)
            {
                characterAchievement = new CharacterAchievement((short)achievement.Id, SafeLevel);
                characterAchievement.Initialize();

                Record.Achievements.Add(characterAchievement);
            }

            if (characterAchievement.Rewarded)
            {
                return;
            }

            characterAchievement.Achieve();

            AchievementPoints += achievement.Points;

            Client.Send(new AchievementFinishedMessage(
                (AchievementAchievedRewardable)characterAchievement.GetAchievementAchieved(Id)));

            AchievementManager.Instance.OnPlayerReachObjective(this);
        }



        public void SendAchievementDetailedList(short categoryId)
        {
            var startedAchievements = new List<Achievement>();
            var finishedAchievements = new List<Achievement>();

            var list = AchievementRecord.GetAchievementsByCategory(categoryId);

            foreach (var record in list)
            {
                var characterAchievement = GetAchievement((int)record.Id);

                if (characterAchievement != null)
                {
                    if (characterAchievement.Finished)
                    {
                        finishedAchievements.Add(characterAchievement.GetAchievement(this));
                    }
                    else
                    {
                        startedAchievements.Add(characterAchievement.GetAchievement(this));
                    }
                }
                else
                {
                    startedAchievements.Add(record.GetAchievement(this));
                }
            }

            Client.Send(new AchievementDetailedListMessage(startedAchievements.ToArray(), finishedAchievements.ToArray()));
        }
        public bool IsAchievementFinished(long id)
        {
            return Record.Achievements.Any(x => x.AchievementId == id && x.Finished);
        }
        public void RewardAllAchievements()
        {
            foreach (var achievement in Record.Achievements.Where(x => !x.Rewarded && x.Finished).ToArray())
            {
                RewardAchievement(achievement.AchievementId);
            }
        }



        public void RewardAchievement(short achievementId)
        {
            CharacterAchievement achievement = Record.Achievements.FirstOrDefault(x => x.AchievementId == achievementId);

            if (achievement == null || achievement.Rewarded || !achievement.Finished)
            {
                Client.Send(new AchievementRewardErrorMessage(achievementId));
                return;
            }

            AchievementRecord record = AchievementRecord.GetAchievement(achievementId);

            foreach (var reward in record.Rewards)
            {
                ApplyAchievementReward(achievement, record, reward);
            }

            achievement.Rewarded = true;
            Client.Send(new AchievementRewardSuccessMessage(achievementId));
        }

        private void ApplyAchievementReward(CharacterAchievement characterAchievement, AchievementRecord achievementRecord, AchievementRewardRecord achievementRewardRecord)
        {
            foreach (var emoteId in achievementRewardRecord.EmotesReward)
            {
                LearnEmote((byte)emoteId);
            }

            for (int i = 0; i < achievementRewardRecord.ItemsReward.Count; i++)
            {
                var itemId = achievementRewardRecord.ItemsReward[i];
                var quantity = achievementRewardRecord.ItemsQuantityReward[i];

                Inventory.AddItem(itemId, quantity);
            }

            foreach (var titleId in achievementRewardRecord.TitlesReward)
            {
                LearnTitle((short)titleId);
            }

            foreach (var ornamentId in achievementRewardRecord.OrnamentsReward)
            {
                LearnOrnament((short)ornamentId, true);
            }

            if (achievementRewardRecord.KamasRatio > 0)
            {
                var kamas = AchievementsFormulas.Instance.GetKamasReward(achievementRewardRecord.KamasScaleWithPlayerLevel, achievementRecord.Level,
                      achievementRewardRecord.KamasRatio, 1, characterAchievement.FinishedLevel);

                AddKamas(kamas);
            }

            if (achievementRewardRecord.ExperienceRatio > 0)
            {
                var experience = AchievementsFormulas.Instance.GetExperienceReward(characterAchievement.FinishedLevel, 0, achievementRecord.Level, achievementRewardRecord.ExperienceRatio, 1);
                AddExperience(experience);
            }
        }


        [Annotation("still working?")]
        public void Restat()
        {
            this.Stats[CharacteristicEnum.VITALITY].Base = 0;
            this.Stats.Agility.Base = 0;
            this.Stats.Intelligence.Base = 0;
            this.Stats.Chance.Base = 0;
            this.Stats.Strength.Base = 0;
            this.Stats.Wisdom.Base = 0;
            this.Record.Stats[CharacteristicEnum.STATS_POINTS].Base = (short)(5 * SafeLevel - 5);
            RefreshStats();
        }
        public CharacterFighter CreateFighter(FightTeam team)
        {
            if (Look.RemoveAura())
                RefreshLookOnMap();

            this.MovementKeys = null;
            this.IsMoving = false;
            this.Map.Instance.RemoveEntity(this.Id);
            this.DestroyContext();
            this.CreateContext(GameContextEnum.FIGHT);
            this.RefreshStats();
            SendGameFightStartingMessage(team.Fight);
            this.Fighter = new CharacterFighter(this, team, GetCell());
            return Fighter;
        }

        public void RejoinMap(long mapId, FightTypeEnum fightType, bool winner, bool spawnJoin)
        {
            DestroyContext();
            CreateContext(GameContextEnum.ROLE_PLAY);
            this.RefreshStats();
            this.Fighter = null;

            if (spawnJoin && !winner)
            {
                if (Client.Account.Role >= ServerRoleEnum.Administrator)
                {
                    CurrentMapMessage(Record.MapId);
                }
                else
                {
                    SpawnPoint();
                }
            }
            else
            {
                if (mapId == Record.MapId)
                {
                    CurrentMapMessage(mapId);
                }
                else
                {
                    Teleport(mapId);
                }
            }

        }
        [Annotation("monsters? int[0]")]
        private void SendGameFightStartingMessage(Fight fight)
        {
            this.Client.Send(new GameFightStartingMessage((byte)fight.FightType,
            (short)fight.Id, (double)fight.BlueTeam.TeamId, (double)fight.RedTeam.TeamId, fight.ContainsBoss(), new int[0]));
        }
        public void DisplayNotification(string message)
        {
            Client.Send(new NotificationByServerMessage(24, new string[] { message }, true));
        }
        public void DisplayNotificationError(string message)
        {
            Client.Send(new NotificationByServerMessage(30, new string[] { message }, true));
        }
        public void DisplaySystemMessage(bool hangUp, short msgId, params string[] parameters)
        {
            Client.Send(new SystemMessageDisplayMessage(hangUp, msgId, parameters));
        }
        public void DisplayPopup(byte lockDuration, string author, string content)
        {
            Client.Send(new PopupWarningMessage(lockDuration, author, content));
        }
        public PlayerStatus GetPlayerStatus()
        {
            return new PlayerStatus((byte)PlayerStatusEnum.PLAYER_STATUS_AVAILABLE);
        }
        public override string ToString()
        {
            return "Character: " + Name;
        }




        public void AddFollower(ServerEntityLook look)
        {
            GetHumanOption<CharacterHumanOptionFollowers>().Add(look);
        }

        public void RemoveFollower(ServerEntityLook look)
        {
            GetHumanOption<CharacterHumanOptionFollowers>().Remove(look);
        }

        public PartyMemberInformations GetPartyMemberInformations()
        {
            return new PartyMemberInformations(Stats.LifePoints, Stats.MaxLifePoints, Stats[CharacteristicEnum.MAGIC_FIND].TotalInContext(),
                0, Stats[CharacteristicEnum.INITIATIVE].Total(), 0, (short)Map.Position.X, (short)Map.Position.Y,
                Record.MapId, Map.SubareaId, GetPlayerStatus(),
                new PartyEntityBaseInformation[0], Id, Name, Level, Look.ToEntityLook(), Record.BreedId, Record.Sex);
        }




        public PartyInvitationMemberInformations GetPartyInvitationMemberInformations()
        {
            return new PartyInvitationMemberInformations((short)Map.Position.X, (short)Map.Position.Y,
                Map.Id, Map.SubareaId, new PartyEntityBaseInformation[0], Id, Name, Level, Look.ToEntityLook(), Record.BreedId, Record.Sex);
        }
        public PartyGuestInformations GetPartyGuestInformations(Party party)
        {
            return new PartyGuestInformations(Id, party.Leader.Id, Name, Look.ToEntityLook(), Record.BreedId, Record.Sex, GetPlayerStatus(),
                new PartyEntityBaseInformation[0]);

        }
        public void OnPartyJoinError(int partyId, PartyJoinErrorEnum reason)
        {
            Client.Send(new PartyCannotJoinErrorMessage((byte)reason, partyId));
        }
        public void OnSubareaChange(SubareaRecord? oldSubarea, SubareaRecord subarea)
        {
            AchievementManager.Instance.OnPlayerChangeSubarea(this);
        }

        public void OnGameContextReady(double mapId)
        {

        }
        public void InviteParty(Character character)
        {
            if (!this.HasParty)
            {
                Party party = PartyManager.Instance.CreateParty(this);
                party.Create(this, character);
            }
            else
            {
                if (!Party.IsFull)
                {
                    Party.OnInvited(character, this);
                }
            }
        }

        public void ReconnectToFight()
        {
            this.Map = MapRecord.GetMap(Record.MapId);

            this.CurrentMapMessage(Map.Id);

            this.Map.Instance.SendMapComplementary(Client);

            Fighter = FightManager.Instance.GetConnectedFighter(this);

            if (this.Fighter == null)
            {
                return;
            }

            SendGameFightStartingMessage(Fighter.Fight);

            Fighter.OnReconnect(this);


        }

        public void OnGuildCreate(GuildCreationResultEnum result)
        {
            Client.Send(new GuildCreationResultMessage((byte)result));

            if (result == GuildCreationResultEnum.GUILD_CREATE_OK)
            {
                CharacterItemRecord item = this.Inventory.GetFirstItem(1575, 1);

                if (item != null)
                {
                    Client.Character.Inventory.RemoveItem(item, 1);
                }

                Dialog.Close();
            }
        }
        public void OnGuildJoined(Guild guild, GuildMemberRecord memberRecord)
        {
            this.Guild = guild;
            this.GuildMember = memberRecord;
            this.Record.GuildId = Guild.Id;

            this.AddHumanOption(HumanOptionsManager.Instance.CreateHumanOptionGuild(), true);

            Client.Send(new GuildJoinedMessage(Guild.GetGuildInformations(), memberRecord.Rank));
        }
        public void OnGuildKick(Guild guild)
        {
            Guild = null;
            GuildMember = null;
            Record.GuildId = 0;
            RemoveAllHumanOption<CharacterHumanOptionGuild>(true);
            Client.Send(new GuildLeftMessage());
        }

        public CharacterMinimalInformations GetCharacterMinimalInformations()
        {
            return new CharacterMinimalInformations(Level, Id, Name);
        }


    }

}
