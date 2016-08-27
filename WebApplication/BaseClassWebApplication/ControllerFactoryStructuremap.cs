using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace WebApplication.BaseClassWebApplication
{
    public class ControllerFactoryStructuremap : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {
            if (controllerType == null && requestContext.HttpContext.Request.Url != null)
                throw new InvalidOperationException(string.Format("Page not found: {0}",
                    requestContext.HttpContext.Request.Url.AbsoluteUri.
                    ToString(CultureInfo.InvariantCulture)));
           return  SmObjectFactory.Container.GetInstance(controllerType) as Controller;
        }
    }
}