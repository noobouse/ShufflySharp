﻿using System.Collections.Generic;

namespace CommonShuffleLibraries
{
    class QueueItemCollection  
    {
        private readonly IEnumerable<QueueItem> queueItems;

        public QueueItemCollection(IEnumerable<QueueItem> queueItems)
        {
            this.queueItems = queueItems;
        }

        public QueueItem GetByChannel(string channel)
        {
            foreach (QueueItem queueWatcher in queueItems)
            {
                if(queueWatcher.Channel==channel || channel.IndexOf(queueWatcher.Channel.Replace("*",""))==0)
                {
                    return queueWatcher;
                }
            }
            return null;
        }
    }
} 