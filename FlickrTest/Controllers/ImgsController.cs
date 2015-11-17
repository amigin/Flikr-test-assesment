using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Images;

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

    }
}