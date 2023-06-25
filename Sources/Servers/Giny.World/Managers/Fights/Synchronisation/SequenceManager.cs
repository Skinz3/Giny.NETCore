using Giny.Core;
using Giny.Protocol.Messages;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Synchronisation
{
    public class SequenceManager
    {
        private List<FightSequence> m_sequencesRoot
        {
            get;
            set;
        }
        public FightSequence CurrentSequence
        {
            get;
            private set;
        }

        public bool IsSequencing => CurrentRootSequence != null && !CurrentRootSequence.Ended;

        private int m_nextSequenceId = 1;

        public FightSequence CurrentRootSequence
        {
            get;
            private set;
        }
        private Fight Fight
        {
            get;
            set;
        }
        public SequenceManager(Fight fight)
        {
            this.Fight = fight;
            this.m_sequencesRoot = new List<FightSequence>();
        }
        public bool AcknowledgeAction(CharacterFighter fighter, int sequenceId)
        {
            return m_sequencesRoot.Any(x => x.Acknowledge(sequenceId, fighter));
        }
        public FightSequence StartSequence(SequenceTypeEnum type)
        {
            var id = m_nextSequenceId++;
            var sequence = new FightSequence(id, type, Fight.FighterPlaying);
            StartSequence(sequence);
            Fight.OnSequenceStarted(sequence);
            return sequence;
        }

        private void StartSequence(FightSequence sequence)
        {
            if (!IsSequencing)
            {
                if (sequence.Parent != null)
                {
                    Logger.Write($"Sequence {sequence.Type} is a root and cannot have a parent", Channels.Warning);
                }

                m_sequencesRoot.Add(sequence);
                CurrentRootSequence = sequence;
            }
            else
            {
                if (CurrentSequence != null)
                {
                    CurrentSequence.AddChildren(sequence);
                }
                else
                {
                    return;
                }
            }


            CurrentSequence = sequence;

            // just send the root sequence
            if (sequence.Parent == null)
                Fight.Send(new SequenceStartMessage((byte)sequence.Type, sequence.Author.Id));
        }
        public void EndAllSequences()
        {
            foreach (var sequence in m_sequencesRoot.ToArray())
                sequence.EndSequence();
        }

        public void OnSequenceEnded(FightSequence sequence)
        {
            if (CurrentSequence != sequence)
            {
                Logger.Write($"Sequence incoherence {sequence} instead of {CurrentSequence}", Channels.Critical);
            }

            CurrentSequence = sequence.Parent;
            Fight.OnSequenceEnded(sequence);
        }

        public void ResetSequences()
        {
            m_sequencesRoot.Clear();
            CurrentSequence = null;
            m_nextSequenceId = 1;
        }
    }
}
