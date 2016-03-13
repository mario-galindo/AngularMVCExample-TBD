using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Data.SqlClient;
using Newtonsoft.Json;



namespace AngularMVC.Controllers
{
    public class crearTablaController : Controller
    {
        // GET: crearTabla
        public ActionResult Index()
        {
            return View();
        }

        public string GetDataBases()
        {
            ArrayList dataBases = new ArrayList();

            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string query = "SELECT name FROM master.dbo.sysdatabases";
            try
            {
                manejoDB.conectar("sa", "root");
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
                while (res.Read())
                {
                    dataBases.Add(res.GetValue(0));
                }


                //var jsonSerialiser = new JavaScriptSerializer();
                //var json = jsonSerialiser.Serialize(dataBases);
                var json = JsonConvert.SerializeObject(dataBases);
                return json;
            }
            catch (Exception)
            {
                return "false";

            }
        }

        public string GetDataTypes()
        {
            ArrayList dataTypes = new ArrayList();

            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string query = "select name from sys.types";
            try
            {
                manejoDB.conectar("sa", "root");
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
                while (res.Read())
                {
                    dataTypes.Add(res.GetValue(0));
                }


                //var jsonSerialiser = new JavaScriptSerializer();
                //var json = jsonSerialiser.Serialize(dataBases);
                var json = JsonConvert.SerializeObject(dataTypes);
                return json;
            }
            catch (Exception)
            {
                return "false";

            }
        }
        
        public string crearTabla(string baseDato,string nombreTabla,string campos){

            return "";
        }
    }
}