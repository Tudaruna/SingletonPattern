using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LcashLib
{
    public class CacheManager
    {
        private static CacheManager instance;
        private Dictionary<string, object> cache;

        private CacheManager()
        {
            cache = new Dictionary<string, object>();
        }

        public static CacheManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CacheManager();
                }
                return instance;
            }
        }

        public void AddData(string key, object data)
        {
            cache[key] = data;
        }

        public object GetData(string key)
        {
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            return null;
        }
    }
}
