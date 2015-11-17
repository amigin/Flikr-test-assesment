using System;
using System.Web;

namespace FlickrTest.Cache
{
    public static class CacheHelper<T>
    {
        /// <summary>
        /// Put a reference typed class CacheItem, to prevent boxing/unboxing.
        /// I'll make consume more memory, but reducing Boxing/Unboxing operation increases perfomance when we are reading value
        /// </summary>
        public class CacheItem
        {
            public T Value { get; set; }
        }

        /// <summary>
        /// Simple cache helper
        /// </summary>
        /// <param name="key">The cache key used to reference the item.</param>
        /// <param name="function">The underlying method that referes to the object to be stored in the cache.</param>
        /// <returns>The item</returns>
        public static T Get(string key, Func<T> function)
        {
            return typeof (T).IsValueType
                ? GetValueTypedValue(key, function)
                : GetRefTypedValue(key, function);
        }

        private static T GetValueTypedValue(string key, Func<T> function)
        {
            var result = (CacheItem)HttpContext.Current.Cache[key];
            if (result != null) return result.Value;

            result = new CacheItem { Value = function.Invoke() };
            HttpContext.Current.Cache.Add(key, result, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return result.Value;
        }

        private static T GetRefTypedValue(string key, Func<T> function)
        {
            var result = HttpContext.Current.Cache[key];
            if (result != null) return (T) result;

            result = function.Invoke();
            HttpContext.Current.Cache.Add(key, result, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return (T)result;
        }

    }
}