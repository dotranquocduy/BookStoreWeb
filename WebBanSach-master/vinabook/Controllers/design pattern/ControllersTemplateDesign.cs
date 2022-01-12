using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Vinabook.Controllers.design_pattern
{
    public abstract class ControllersTemplateDesign: Controller
    {
        protected abstract void PrintRoutes();
        //protected abstract void PrintDepedencyInjection(); // kĩ thuật dependency injection

        public void PrintTemplateMethod()
        {
            
            PrintRoutes();
            //PrintDepedencyInjection();
        }
        
    }
}