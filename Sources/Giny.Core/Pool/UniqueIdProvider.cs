using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giny.Core.Pool
{
    public class UniqueIdProvider
    {
        protected readonly ConcurrentQueue<int> m_freeIds = new ConcurrentQueue<int>();

        protected int m_highestId;

        public UniqueIdProvider()
        {
            m_highestId = 0;
        }

        public UniqueIdProvider(int lastId)
        {
            m_highestId = lastId;
        }

        public UniqueIdProvider(IEnumerable<int> freeIds)
        {
            foreach (var freeId in freeIds)
            {
                m_freeIds.Enqueue(freeId);
            }
        }

        public virtual int Pop()
        {
            int id;

            if (!m_freeIds.IsEmpty)
            {
                if (!m_freeIds.TryDequeue(out id))
                {
                    // if we can't dequeue, we return the next id
                    return Next();
                }
            }
            else
                return Next();

            return id;
        }

        public virtual int Peek()
        {
            int id;

            if (!m_freeIds.IsEmpty)
            {
                if (!m_freeIds.TryPeek(out id))
                {
                    // if we can't dequeue, we return the next id
                    return m_highestId + 1;
                }
            }
            else
                return m_highestId + 1;

            return id;
        }

        /// <summary>
        /// Indicate that the given id is free
        /// </summary>
        /// <param name="freeId"></param>
        public virtual void Push(int freeId)
        {
            m_freeIds.Enqueue(freeId);
        }

        protected virtual int Next()
        {
            return Interlocked.Increment(ref m_highestId);
        }
    }
}
