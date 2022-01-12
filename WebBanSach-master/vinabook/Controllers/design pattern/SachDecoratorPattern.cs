using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vinabook.Models;
using Vinabook.Controllers.design_pattern.Facade_design;
namespace Vinabook.Controllers.design_pattern
{
  
  
   public interface ISach
    {
        List<Sach> usesach();
    }

    public class SachKhuyenMaiDecorator : ISach
    {
        ControllerFacade Facade=new ControllerFacade();
        public List<Sach> usesach()
        {
            return Facade.DB_DesPrice_Book().Take(5).ToList();
        }

    }
    public class SachYeuThichDecorator : ISach
    {
        ControllerFacade Facade = new ControllerFacade();
        public List<Sach> usesach()
        {
            return Facade.DB_Hint_Book().Take(10).ToList();
        }

    }
    public class SachMoiNhapDecorator : ISach
    {
        ControllerFacade Facade = new ControllerFacade();
        public List<Sach> usesach()
        {
            return Facade.DB_NCN_Book().Take(5).ToList();
        }

    }





}