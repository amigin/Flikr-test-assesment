using System.Collections.Generic;

namespace FlickrTest.Models
{
    /// <summary>
    /// The Flickr image model
    /// </summary>
    public class FlickrImage
    {
        /// <summary>
        /// The title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The image url
        /// </summary>
        public string ImageUrl { get; set; }
    }


    public class GetImagesViewModel
    {
        public IEnumerable<FlickrImage> Images { get; set; }
        public string Tags { get; set; }
    }
}