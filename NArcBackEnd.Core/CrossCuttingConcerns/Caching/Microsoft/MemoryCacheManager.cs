using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NArcBackEnd.Core.Utilities.IoC;
using System.Text.RegularExpressions;

namespace NArcBackEnd.Core.CrossCuttingConcerns.Caching.Microsoft
{
    // cache - 2
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            // program.csde direk olarak çalışmayan yapılar için nasıl injection gececeğinin örneğidir.
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object data, int duration)
        {
            _memoryCache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            // bir değer tipi için cache i alma
            // verilen item da şu key de herhangi bir cache var mi ? 
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            // genel keye göre cache alma!
            // verilen key de herhangi bir cache var mı?
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            // burada değeri almaya çalış demektir. 
            // 2.parametre bir data aldığından bir data yoksa out _ ile direk olarak gönderebilirsin.
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            // bu yapıdaki tüm cacheleri siler.
            _memoryCache.Remove(key);
        }

        // bu sadece belirtilen cache' i siler.
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {

                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);


                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
