using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Images;
using FlickrTest.Models;

namespace FlickrTest.Controllers
{
    public class ImgsController : Controller
    {
        private readonly IImagesRepository _imagesRepository;

        public ImgsController(IImagesRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Index(string tags)
        {
            var result = await _imagesRepository.GetImagesAsync(tags);
            return Json(result.Select(itm => new {title = itm.Title, url = itm.ImageUrl}), JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetImages(string id)
        {
            var viewModel = new GetImagesViewModel
            {
                Tags = id,
                Images =
                    (await _imagesRepository.GetImagesAsync(id)).Select(
                        itm => new FlickrImage {Title = itm.Title, ImageUrl = itm.ImageUrl})
            };

            return View(viewModel);
        }

    }

}