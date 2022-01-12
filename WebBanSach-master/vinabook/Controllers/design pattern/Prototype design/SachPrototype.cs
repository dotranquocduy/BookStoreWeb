using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vinabook.Controllers.design_pattern.Prototype_design
{
    public interface SachPrototype
    {
        SachPrototype Clone();
    }
}