using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;

namespace AngularMVC.Controllers
{
    public class HomeController : Controller
    {




        // GET: Home
        public ActionResult Index()
        {
            return View();
            //return PartialView();
        }

        /*
        public JsonResult GetProducts() {

            conexionBaseDatos BD = new conexionBaseDatos();
            string Query = "select name from sys.databases";
            DataTable dt = new DataTable();

            

            BD.conectar("sa", "root");
            BD.EjecutarSQL(Query);
            BD.Desconectar();

            
            
            var db = new ProductosDBEntities();
            var products = db.Productos.ToList();

            JsonResult result = JsonConvert.SerializeObject((BD.EjecutarSQL(Query), "Title"), Newtonsoft.Json.Formatting.Indented);
            
            return result;
        }

    
        
        public string DataTableToJsonWithJsonNet(DataTable table)
        {
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            return jsonString;
        }

        */




    }
}