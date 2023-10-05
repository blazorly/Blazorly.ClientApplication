namespace BlazorlyClientApp
{
    public class MemCache
    {
        public Dictionary<string, CacheObject> cacheList = new Dictionary<string, CacheObject>();
        public MemCache() { }

        public void SetValue<T>(string key, T value)
        {
            key = key.ToLower();
            var cacheObject = new CacheObject()
            {
                Value = value,
                Expiry = DateTime.Now.AddHours(1),
            };

            if (cacheList.ContainsKey(key))
            {
                cacheList[key] = cacheObject;
            }
            else
            {
                cacheList.Add(key, cacheObject);
            }
        }

        public T GetValue<T>(string key) 
        {
            key = key.ToLower();
            if (!cacheList.ContainsKey(key)) return default;

            var cacheObject = cacheList[key];
            if (cacheObject.Expiry > DateTime.Now)
                return (T)cacheObject.Value;

            return default;
        }
    }

    public class CacheObject
    {
        public object Value { get; set; }

        public DateTime Expiry { get; set; }
    }
}
