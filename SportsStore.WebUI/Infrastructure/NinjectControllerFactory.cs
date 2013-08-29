using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure
{
    /// <summary>
    /// Custom controller factory powered by Ninject.
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
            //throw new NotImplementedException();
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