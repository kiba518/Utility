using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Utility 
{ 
    public class RuntimeCache
    {
        private readonly Object _locker = new object();
        /// <summary>
        /// 用法 GlobalStatic.RuntimeCache.SetCache("name", "kiba", DateTime.Now.AddMinutes(2));
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration"> 获取或设置一个值，该值指示是否应在给定时段内未访问，就会对它进行是否逐出缓存项。</param>
        /// <param name="absoluteExpiration"> 获取或设置一个值，该值指示是否应在指定的持续时间之后逐出缓存项。</param>
        public void SetCache(String key, object value, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("无效Key");

            if (slidingExpiration == null && absoluteExpiration == null)
            {
                throw new ArgumentException("需要一个策略值");
            }
            else
            {
                lock (_locker)
                {
                    var item = new CacheItem(key, value);
                    var policy = CreatePolicy(slidingExpiration, absoluteExpiration);
                    MemoryCache.Default.Add(item, policy);
                }
            }
        }


        public T GetCache<T>(String key)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("无效Key"); 
            return (T)MemoryCache.Default[key];
        }
        public object GetCache(String key)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("无效Key");
            return MemoryCache.Default[key];
        }
        private CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }
        public T RemoveCache<T>(String key)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("无效Key");
            return (T)MemoryCache.Default.Remove(key);
        }
    }
}
