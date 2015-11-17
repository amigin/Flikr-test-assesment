using System.Collections.Generic;
using FlickrTest.Models;

namespace FlickrTest.Repository
{
    /// <summary>
    /// The Repository interface
    /// </summary>
    interface IRepository
    {
        List<FlickrImage> GetImagesByTags(string tags);
    }
}
