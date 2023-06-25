using Giny.Core.Collections;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.History
{
    public class MovementHistory
    {
        public static readonly int HistoryEntriesLimit = 60;

        readonly LimitedStack<MovementHistoryEntry> m_underlyingStack = new LimitedStack<MovementHistoryEntry>(HistoryEntriesLimit);

        public MovementHistory(Fighter owner)
        {
            Owner = owner;
        }

        public void OnMove(List<CellRecord> path)
        {
            foreach (var cell in path.Take(path.Count - 1))
                RegisterEntry(cell);
        }

        public void OnCellChanged(CellRecord position)
        {
            RegisterEntry(position);
        }

        public Fighter Owner
        {
            get;
        }

        int CurrentRound => Owner.Fight.RoundNumber;

        public void RegisterEntry(CellRecord cell)
        {
            m_underlyingStack.Push(new MovementHistoryEntry(cell, CurrentRound));
        }

        public MovementHistoryEntry GetPreviousPosition() => m_underlyingStack.Count > 0 ? m_underlyingStack.Peek() : null;

        public MovementHistoryEntry PopPreviousPosition() => m_underlyingStack.Count > 0 ? m_underlyingStack.Pop() : null;

        public MovementHistoryEntry PopWhile(Predicate<MovementHistoryEntry> predicate)
        {
            var entry = GetPreviousPosition();

            while (entry != null && predicate(entry))
                entry = PopPreviousPosition();

            return entry;
        }

        public MovementHistoryEntry GetPreviousPosition(int lifetime)
        {
            var previous = GetPreviousPosition();

            return CurrentRound - previous?.Round < lifetime ? previous : null;
        }

        public MovementHistoryEntry PopPreviousPosition(int lifetime)
        {
            var previous = GetPreviousPosition(lifetime);

            if (previous != null)
                PopPreviousPosition();

            return previous;
        }

        public MovementHistoryEntry PopWhile(Predicate<MovementHistoryEntry> predicate, int lifetime)
        {
            var entry = PopPreviousPosition(lifetime);

            while (entry != null && predicate(entry) && CurrentRound - entry.Round < lifetime)
                entry = PopPreviousPosition();

            return entry;
        }

        public IEnumerable<MovementHistoryEntry> GetEntries() => m_underlyingStack;

        public IEnumerable<MovementHistoryEntry> GetEntries(Predicate<MovementHistoryEntry> predicate) => m_underlyingStack.Where(entry => predicate(entry));

        public IEnumerable<MovementHistoryEntry> GetEntries(int lifetime) => GetEntries(x => CurrentRound - x.Round < lifetime);
    }
}
