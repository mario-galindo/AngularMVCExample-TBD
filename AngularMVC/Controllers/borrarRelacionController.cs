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
    public class borrarRelacionController : Controller
    {
        // GET: borrarRelacion
        public ActionResult Index()
        {
            return View();
        }

        public string GetConstraints(string baseDatos, string id)
        {
            ArrayList relaciones = new ArrayList();

            conexionBaseDatos manejoDB = new conexionBaseDatos();

            string use = "use " + baseDatos;
            string query = "select fk.name from sys.foreign_keys fk where fk.parent_object_id = "+ id +"";
            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
               
                while (res.Read())
                {
                    relaciones.Add(res.GetValue(0));
                }


                //var jsonSerialiser = new JavaScriptSerializer();
                //var json = jsonSerialiser.Serialize(dataBases);
                var json = JsonConvert.SerializeObject(relaciones);
                return json;
            }
            catch (Exception)
            {
                return "false";

            }
        }

        public string borrarConstraints(string baseDatos, string constraintSelected,string tabla)
        {
            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string use = "use " + baseDatos;
            string borrar = "alter table "+tabla+" drop constraint "+ constraintSelected +" ";

            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                manejoDB.EjecutarSQL(borrar);
                manejoDB.Desconectar();
                return "true";
            }
            catch (SqlException e)
            {

                return e.Message.ToString();
                //throw;
            }
        }

        public string GetTablesID(string baseDatos,string tabla)
        {
            ArrayList tablesID = new ArrayList();

            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string use = "use " + baseDatos;
            string query = "select st.object_id from sys.tables st where st.name = '"+tabla+"'";
            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
                while (res.Read())
                {
                    tablesID.Add(res.GetValue(0));
                    
                }


                //var jsonSerialiser = new JavaScriptSerializer();
                //var json = jsonSerialiser.Serialize(dataBases);
                var json = JsonConvert.SerializeObject(tablesID);
                return json;
            }
            catch (Exception)
            {
                return "false";

            }
        }
    }
}