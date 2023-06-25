using Giny.Protocol.Messages;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Sequences
{
    public class FightSequence : IDisposable
    {
        private readonly List<CharacterFighter> m_acknowledgedBy = new List<CharacterFighter>();
        private readonly List<FightSequence> m_children = new List<FightSequence>();

        public FightSequence(int id, SequenceTypeEnum type, Fighter author)
        {
            Id = id;
            Type = type;
            Author = author;
            StartTime = DateTime.Now;
        }

        public int Id
        {
            get;
        }

        public SequenceTypeEnum Type
        {
            get;
        }

        public Fighter Author
        {
            get;
        }

        public Fight Fight => Author.Fight;

        public FightSequence Parent
        {
            get;
            private set;
        }

        public FightSequence BranchRoot
        {
            get
            {
                var current = this;
                while (current.Parent != null)
                    current = current.Parent;

                return current;
            }
        }

        public bool Ended
        {
            get;
            private set;
        }

        public IReadOnlyCollection<FightSequence> Children => m_children.AsReadOnly();

        public DateTime StartTime
        {
            get;
        }

        public DateTime EndTime => StartTime + Duration;

        public ReadOnlyCollection<CharacterFighter> AcknowledgedBy => m_acknowledgedBy.AsReadOnly();

        public bool Acknowledge(int id, CharacterFighter fighter)
        {
            if (id == Id)
            {
                if (!m_acknowledgedBy.Contains(fighter))
                    m_acknowledgedBy.Add(fighter);

                return true;
            }

            return Children.Any(x => x.Acknowledge(id, fighter));
        }

        public void AddChildren(FightSequence sequence)
        {
            sequence.Parent = this;
            m_children.Add(sequence);
        }

        public bool IsChild(FightSequence sequence, bool recursive = true)
        {
            return m_children.Any(x => x == sequence) || (!recursive || m_children.Any(x => x.IsChild(sequence)));
        }

        public IEnumerable<FightSequence> EnumerateSequences()
        {
            yield return this;

            foreach (var child in Children.SelectMany(x => x.EnumerateSequences()))
            {
                yield return child;
            }
        }

        public TimeSpan Duration => DurationWithoutChildren + Children.Aggregate(TimeSpan.Zero, (prev, x) => prev + x.Duration);

        // just an approximation
        protected virtual TimeSpan DurationWithoutChildren
        {
            get
            {
                switch (Type)
                {
                    case SequenceTypeEnum.SEQUENCE_SPELL:
                    case SequenceTypeEnum.SEQUENCE_MOVE:
                    case SequenceTypeEnum.SEQUENCE_CHARACTER_DEATH:
                        return TimeSpan.FromSeconds(2);
                    case SequenceTypeEnum.SEQUENCE_GLYPH_TRAP:
                    case SequenceTypeEnum.SEQUENCE_TRIGGERED:
                        return TimeSpan.FromSeconds(1);
                    case SequenceTypeEnum.SEQUENCE_TURN_END:
                    case SequenceTypeEnum.SEQUENCE_TURN_START:
                        return TimeSpan.Zero;
                    default:
                        return TimeSpan.Zero;
                }
            }
        }

        public void EndSequence()
        {
            if (Ended)
                return;

            // check every children has been ended
            foreach (var child in Children)
                child.EndSequence();

            Ended = true;
            Fight.SequenceManager.OnSequenceEnded(this);

            if (Parent == null)
                Fight.Send(new SequenceEndMessage((short)this.Id, this.Author.Id, (byte)this.Type));
        }

        public void Dispose()
        {
            EndSequence();
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
