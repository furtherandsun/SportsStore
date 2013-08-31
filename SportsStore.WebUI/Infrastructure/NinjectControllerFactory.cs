using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;

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
        /// Constructor, creates a new standard kernel and adds bindings.
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
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //    new Product() { Name = "Football", Price = 25 },
            //    new Product() { Name = "Surf Board", Price = 179 },
            //    new Product() { Name = "Running Shoes", Price = 95 }
            //}.AsQueryable());

            NinjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
        }

        /// <summary>
        /// Gets the appropiate controller object from the Ninject kernel. 
        /// Ninject is able to find the controller even at times where it
        /// might not have  a binding for it.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType">Controller type to instantiate.</param>
        /// <returns>Controller</returns>
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController) NinjectKernel.Get(controllerType);
        }

    }
}