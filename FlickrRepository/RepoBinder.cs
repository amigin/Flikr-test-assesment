using Common.IocContainer;
using Core.Images;
using FlickrRepository.Images;

namespace FlickrRepository
{
    public static class RepoBinder
    {
        public static void BindRepositories(this IoC ioc)
        {
            ioc.Register<IImagesRepository>(new ImagesRepository());
        }


        public static void BindCachableRepositories(this IoC ioc)
        {
            ioc.Register<IImagesRepository>(new FlickCachableRepository());
        }
    }
}
