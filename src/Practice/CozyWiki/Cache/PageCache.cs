﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyWiki.Container;

namespace CozyWiki.Cache
{
    public class PageCache
    {
        private ICacheContainer<string, CacheBlock> CachePool { get; set; }

        private readonly object Locker = new object();

        private int maxSize { get; set; }
        public int MaxSize
        {
            get
            {
                return maxSize;
            }
            set
            {
                maxSize = value;
                if (value > 0)
                {
                    CachePool = new MRUContainer<string, CacheBlock>(MaxSize);
                }
                else if (value <= 0)
                {
                    CachePool = new NormalCacheContainer<string, CacheBlock>();
                }
            }
        }

        public CacheBlock GetCache(string key)
        {
            lock (Locker)
            {
                return CachePool.Get(key);
            }
        }

        public void Clear()
        {
            lock (Locker)
            {
                CachePool.Clear();
            }
        }

        public void Update(string key, CacheBlock block)
        {
            lock (Locker)
            {
                CachePool.Update(key, block);
            }
        }

        public void RemoveAll(Predicate<CacheBlock> pre)
        {
            lock (Locker)
            {
                CachePool.RemoveAll(pre);
            }
        }
    }
}
