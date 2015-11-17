using System.Collections.Generic;
using FlickrTest.Cache;
using FlickrTest.Models;

namespace FlickrTest.Repository
{
    /// <summary>
    /// Acting as a cacheable repository for Flickr
    /// </summary>
    public class FlickrCacheableRepository : IRepository
    {
        readonly FlickrRepository _flickrRepository;

        public FlickrCacheableRepository() {
            _flickrRepository = new FlickrRepository();
        }

        /// <summary>
        /// Returns image based on the tag
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public List<FlickrImage> GetImagesByTags(string tags)
        {
            // Load the data from the cache if it exist
            return CacheHelper<List<FlickrImage>>.Get(tags, () => _flickrRepository.GetImagesByTags(tags));
            
        }
    }
}
