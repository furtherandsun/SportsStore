using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure
{
    /// <summary>
    /// Custom controller factory that includes dependency injection with Ninject.
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {

        private IKernel NinjectKernel
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public NinjectControllerFactory()
        {
            NinjectKernel = new StandardKernel();
            AddBindings();
        }

        /// <summary>
        /// Add bindings to the Ninject kernel.
        /// </summary>
        private void AddBindings()
        {
            //Create mock IProductRepository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product() { Name = "Football", Price = 25 }, 
                new Product() { Name = "Surf Board", Price = 179 },
                new Product() { Name = "Running Shoes", Price = 95 }
            }.AsQueryable());

            NinjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }

        /// <summary>
        /// Gets the appropiate controller object from the Ninject kernel.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController) NinjectKernel.Get(controllerType);
        }

    }
}