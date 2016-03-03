using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularMVC.Controllers
{
    public class HomeController : Controller
    {




        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProducts() {

            var db = new ProductosDBEntities();
            var products = db.Productos.ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        /*
        public bool logearse(string user,string pass) {

            conexionBaseDatos db = new conexionBaseDatos();

            try
            {
                if (user == "sa")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        
        }*/



    }
}