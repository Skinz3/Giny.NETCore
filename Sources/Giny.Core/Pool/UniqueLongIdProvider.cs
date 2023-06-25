using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giny.Core.Pool
{
    public class UniqueLongIdProvider
    {
        protected readonly ConcurrentQueue<long> m_freeIds = new ConcurrentQueue<long>();

        protected long m_highestId;

        public UniqueLongIdProvider()
        {
            m_highestId = 0;
        }

        public UniqueLongIdProvider(long lastId)
        {
            m_highestId = lastId;
        }

        public UniqueLongIdProvider(IEnumerable<long> freeIds)
        {
            foreach (var freeId in freeIds)
            {
                m_freeIds.Enqueue(freeId);
            }
        }

        public virtual long Pop()
        {
            long id;

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

        public virtual long Peek()
        {
            long id;

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
        public virtual void Push(long freeId)
        {
            m_freeIds.Enqueue(freeId);
        }

        protected virtual long Next()
        {
            return Interlocked.Increment(ref m_highestId);
        }
    }
}
