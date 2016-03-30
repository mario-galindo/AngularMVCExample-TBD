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
    public class editarCamposController : Controller
    {
        // GET: editarCampos
        public ActionResult Index()
        {
            return View();
        }

        public string edicionCampos(string baseDatos, string tabla, string nuevo, string accion,string campo,string tipoData,string tipoSele)
        {
            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string use = "use " + baseDatos;
            string QuerytoExecute = "";

            if (accion == "1")
            {
                QuerytoExecute = "alter table " + tabla + " add  " + nuevo + " varchar(20)";
            }
            else if (accion == "2")
            {
                QuerytoExecute = "alter table " + tabla + " drop column  " + campo + "";
            }
            else if (accion == "3")
            {
                QuerytoExecute = "alter table "+ tabla +" alter column "+ campo +" "+ tipoData +"";
            }
            else if (accion == "4")
            {
                
               QuerytoExecute = "alter table "+ tabla +" add primary key("+ campo +")";
            }
            else if (accion == "5")
            {
                QuerytoExecute = "alter table "+ tabla +" alter column "+ campo +" "+ tipoSele + " not null";
            }
            else if (accion == "6")
            {
                QuerytoExecute = "alter table " + tabla + " alter column " + campo + " " + tipoSele + "  null";
            }
            
            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                manejoDB.EjecutarSQL(QuerytoExecute);
                manejoDB.Desconectar();
                return "true";
            }
            catch (SqlException e)
            {

                return e.Message.ToString();
                //throw;
            }
        }


        public string getTipo(string baseDatos,string tabla,string campo,string tipoSele)
        {
            ArrayList tipo = new ArrayList();

            conexionBaseDatos manejoDB = new conexionBaseDatos();
            string query = "select DATA_TYPE from INFORMATION_SCHEMA.COLUMNS IC where TABLE_NAME = '"+ tabla + "' and COLUMN_NAME = '" + campo + "'";
            string use = "use " + baseDatos;

            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
                while (res.Read())
                {
                    tipo.Add(res.GetValue(0));
                }


                //var jsonSerialiser = new JavaScriptSerializer();
                //var json = jsonSerialiser.Serialize(dataBases);
                var json = JsonConvert.SerializeObject(tipo);
                return json;
            }
            catch (Exception)
            {
                return "false";

            }
        }
    }
}