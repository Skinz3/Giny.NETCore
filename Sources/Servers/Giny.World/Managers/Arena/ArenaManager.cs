using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Arena.Group;
using Giny.World.Managers.Arena.Member;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Arena
{
    public class ArenaManager : Singleton<ArenaManager>
    {
        public static int MaxPlayersPerFights = 3;

        public static int KolizeumMapId = 81788928;

        private static readonly double QueueRefreshDelaySeconds = 0.2f;
        public List<ArenaGroup> ArenaGroups { get; private set; }
        private static List<Character> arenaQueue1v1;
        private static List<Character> arenaQueue3v3Solo;
        private static List<Character> arenaQueue3v3Team;
        private Task queueProcessingTask;

        public ArenaManager()
        {
            ArenaGroups = new List<ArenaGroup>();
            arenaQueue1v1 = new List<Character>();
            arenaQueue3v3Solo = new List<Character>();
            arenaQueue3v3Team = new List<Character>();
            StartQueueProcessingTask();
        }

        private void StartQueueProcessingTask()
        {
            queueProcessingTask = Task.Factory.StartNewDelayed((int)(QueueRefreshDelaySeconds * 1000), () =>
            {
                ProcessArenaQueue(arenaQueue1v1, PvpArenaTypeEnum.ARENA_TYPE_1VS1);
                ProcessArenaQueue(arenaQueue3v3Solo, PvpArenaTypeEnum.ARENA_TYPE_3VS3_SOLO);
                ProcessArenaQueue(arenaQueue3v3Team, PvpArenaTypeEnum.ARENA_TYPE_3VS3_TEAM);
            });
        }

        private void ProcessArenaQueue(List<Character> queue, PvpArenaTypeEnum type)
        {
            try
            {
                lock (queue)
                {
                    foreach (var character in new List<Character>(queue))
                    {
                        var suitableGroup = FindSuitableGroup(character, type);
                        if (suitableGroup != null && suitableGroup.CanChallenge(character))
                        {
                            suitableGroup.AddCharacter(character);
                            queue.Remove(character);

                            if (suitableGroup.Ready)
                            {
                                suitableGroup.StartFighting();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        private ArenaGroup FindSuitableGroup(Character character, PvpArenaTypeEnum type)
        {
            var suitableGroups = ArenaGroups.Where(group => group.Type == type);
            var group = suitableGroups.FirstOrDefault(g => !g.Ready && !g.ContainsIp(character.Client.Ip));

            if (group == null)
            {
                group = new ArenaGroup(type);
                ArenaGroups.Add(group);
            }

            return group;
        }

        public void Register(Character character, PvpArenaTypeEnum type)
        {
            List<Character> queue;
            switch (type)
            {
                case PvpArenaTypeEnum.ARENA_TYPE_1VS1:
                    queue = arenaQueue1v1;
                    break;
                case PvpArenaTypeEnum.ARENA_TYPE_3VS3_SOLO:
                    queue = arenaQueue3v3Solo;
                    break;
                case PvpArenaTypeEnum.ARENA_TYPE_3VS3_TEAM:
                    queue = arenaQueue3v3Team;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            lock (queue)
            {
                Logger.Write($"Adding {character.Name} to Arena {type}");
                queue.Add(character);
            }
        }

        public void Unregister(Character character, PvpArenaTypeEnum type)
        {
            List<Character> queue;
            switch (type)
            {
                case PvpArenaTypeEnum.ARENA_TYPE_1VS1:
                    queue = arenaQueue1v1;
                    break;
                case PvpArenaTypeEnum.ARENA_TYPE_3VS3_SOLO:
                    queue = arenaQueue3v3Solo;
                    break;
                case PvpArenaTypeEnum.ARENA_TYPE_3VS3_TEAM:
                    queue = arenaQueue3v3Team;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            lock (queue)
            {
                Logger.Write($"Removing {character.Name} to Arena {type}");
                queue.Remove(character);
            }

            var group = ArenaGroups.FirstOrDefault(g => g.GetAllMembers().Any(m => m.Character == character));
            group?.RemoveCharacter(character);
        }
        public static class TaskExtensions
        {
            public static Task StartNewDelayed(int delay, Action action)
            {
                return Task.Delay(delay).ContinueWith(_ => action());
            }
        }
    }

}

