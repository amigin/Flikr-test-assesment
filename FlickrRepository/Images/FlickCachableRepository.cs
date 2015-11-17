using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Images;

namespace FlickrRepository.Images
{
    public class FlickCachableRepository : IImagesRepository
    {
        // Ho many seconds cache item is valid
        private const int SecondsValid = 60;

        private class CacheItem
        {
            public IImage[] Image { get; set; }
            public DateTime PutTime { get; set; }

            public static CacheItem Create(IImage[] image)
            {
                return new CacheItem
                {
                    PutTime = DateTime.UtcNow,
                    Image = image
                };
            }
        }


        private readonly ImagesRepository _flickrRepository = new ImagesRepository();

        private readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();

        private readonly Dictionary<string, CacheItem> _cache = new Dictionary<string, CacheItem>();

        private IImage[] GetImagesFromCache(string key)
        {
            _lockSlim.EnterReadLock();
            try
            {
                var result = _cache.ContainsKey(key) ? _cache[key] : null;

                if (result == null)
                    return null;

                return (DateTime.UtcNow - result.PutTime).TotalSeconds > SecondsValid ? null : result.Image;
            }
            finally
            {
                _lockSlim.ExitReadLock();
            }
        }

        private void PutImagesToCache(string key, IImage[] images)
        {
            _lockSlim.EnterWriteLock();
            try
            {
                if (_cache.ContainsKey(key))
                    _cache[key] = CacheItem.Create(images);
                else
                    _cache.Add(key, CacheItem.Create(images));
            }
            finally
            {
                _lockSlim.ExitWriteLock();
            }
        }


        public async Task<IEnumerable<IImage>> GetImagesAsync(string tags)
        {
            var imagesFromCache = GetImagesFromCache(tags);
            if (imagesFromCache != null)
                return imagesFromCache;

            imagesFromCache = (await _flickrRepository.GetImagesAsync(tags)).ToArray();
            PutImagesToCache(tags, imagesFromCache);
            return imagesFromCache;
        }
    }
}
