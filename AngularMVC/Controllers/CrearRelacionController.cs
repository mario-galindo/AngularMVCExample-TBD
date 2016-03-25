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
    public class CrearRelacionController : Controller
    {
        // GET: CrearRelacion
        public ActionResult Index()
        {
            return View();
        }

        public string GetTables(string baseDatos)
        {
            ArrayList tables = new ArrayList();

            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string use = "use " + baseDatos;
            string query = "select TABLE_NAME from INFORMATION_SCHEMA.TABLES";
            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
                while (res.Read())
                {
                    tables.Add(res.GetValue(0));
                }


                //var jsonSerialiser = new JavaScriptSerializer();
                //var json = jsonSerialiser.Serialize(dataBases);
                var json = JsonConvert.SerializeObject(tables);
                return json;
            }
            catch (Exception)
            {
                return "false";

            }
        }

        public string GetCamposRight(string tableRight, string baseDatos)
        {
            ArrayList camposRight = new ArrayList();

            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string use = "use " + baseDatos;
            string query = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tableRight + "'";
            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
                while (res.Read())
                {
                    camposRight.Add(res.GetValue(0));
                }


                //var jsonSerialiser = new JavaScriptSerializer();
                //var json = jsonSerialiser.Serialize(dataBases);
                var json = JsonConvert.SerializeObject(camposRight);
                return json;
            }
            catch (Exception)
            {
                return "false";

            }
        }

        public string GetCamposLeft(string tableLeft, string baseDatos)
        {
            ArrayList camposLeft = new ArrayList();

            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string use = "use " + baseDatos;
            string query = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tableLeft + "'";
            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
                while (res.Read())
                {
                    camposLeft.Add(res.GetValue(0));
                }


                //var jsonSerialiser = new JavaScriptSerializer();
                //var json = jsonSerialiser.Serialize(dataBases);
                var json = JsonConvert.SerializeObject(camposLeft);
                return json;
            }
            catch (Exception)
            {
                return "false";

            }
        }

        public string CrearForeignKey(string baseDatos, string tablaDer, string nombreRel, string campoDer, string tablaIzq, string campoIzq)
        {
            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string use = "use " + baseDatos;
            string foreignKey = "alter table " + tablaDer + " add constraint " + nombreRel + " foreign key(" + campoDer + ") references " + tablaIzq + "(" + campoIzq + ")";

            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                manejoDB.EjecutarSQL(foreignKey);
                manejoDB.Desconectar();
                return "true";
            }
            catch (SqlException e)
            {

                return e.Message.ToString();
                //throw;
            }
        }

        
    
    }
}