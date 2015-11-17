using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using Core.Images;

namespace FlickrRepository.Images
{
    public class ImageEntity : IImage
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }


    public class ImagesRepository : IImagesRepository
    {
        /*private readonly List<IImage> _mockData = new List<IImage>
        {
            new ImageEntity {Id = Guid.NewGuid().ToString(), Caption = "Image 1", Url = "http://www.gettyimages.co.uk/gi-resources/images/Homepage/Hero/UK/CMS_Creative_164657191_Kingfisher.jpg"},
            new ImageEntity {Id = Guid.NewGuid().ToString(), Caption = "Image 2", Url = "http://i1008.photobucket.com/albums/af201/visuallightbox/Butterfly/13.jpg"},
            new ImageEntity {Id = Guid.NewGuid().ToString(), Caption = "Image 3", Url = "http://www.joomlack.fr/images/demos/demo2/on-top-of-earth.jpg"}
        };*/
        // To see the raw data, have a look at
        // http://api.flickr.com/services/feeds/photos_public.gne?tags=cats

        public async Task<IEnumerable<IImage>> GetImagesAsync(string tags)
        {
            var nodes = await LoadDataAsync(tags);
            var images = new List<IImage>();

            foreach (XmlNode item in nodes)
            {
                var image = new ImageEntity { Title = item.FirstChild.InnerText };
                var xmlAttributeCollection = item.ChildNodes[9].Attributes;
                if (xmlAttributeCollection != null)
                {
                    image.ImageUrl = xmlAttributeCollection["href"].Value;
                }
                if (!image.ImageUrl.Contains("creativecommons"))
                {
                    images.Add(image);
                }
            }

            return images;
        }

        /// <summary>
        /// Load data from flickr
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private static async Task<XmlNodeList> LoadDataAsync(string tags)
        {
            var webClient = new WebClient();
            var stream = await
                webClient.OpenReadTaskAsync(
                    new Uri($"http://api.flickr.com/services/feeds/photos_public.gne?tags={tags}"));

            var xdoc = new XmlDocument();//xml doc used for xml parsing
            xdoc.Load(stream);
            var xNodelst = xdoc.GetElementsByTagName("entry");
            return xNodelst;
        }
    }
}
