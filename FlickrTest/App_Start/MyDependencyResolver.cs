using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.IocContainer;
using FlickrRepository;

namespace FlickrTest
{

    public static class Dependencies
    {
        public static IDependencyResolver CreateDr()
        {
            var dr = new MyDependencyResolver();

#if DEBUG
            dr.IoC.BindCachableRepositories();
#else
            dr.IoC.BindRepositories();
#endif
            return dr;
        }
    }

    public class MyDependencyResolver : IDependencyResolver
    {

        public readonly IoC IoC = new IoC();


        public void RegisterSingleTone<T, TI>() where TI : T
        {
            IoC.RegisterSingleTone<T, TI>();
        }

        public void RegisterSingleTone<T, TI>(TI instance) where TI : T
        {
            IoC.Register<T>(instance);
        }

        public T GetType<T>() where T : class
        {
            var result = IoC.GetObject<T>();
            return result;
        }

        public object GetService(Type serviceType)
        {
            var result = IoC.CreateInstance(serviceType);
            return result;
        }

        private readonly object[] _nullData = new object[0];
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var result = IoC.CreateInstance(serviceType);
            return result == null ? _nullData : new[] { result };
        }

    }
}