

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Core.Images
{
    public interface IImage
    {
        string Title { get; }
        string ImageUrl { get; }
    }

    public interface IImagesRepository
    {
        /// <summary>
        /// Get List of Images
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<IEnumerable<IImage>> GetImagesAsync(string tags);
    }
}
