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

        public string edicionCampos(string baseDatos, string tabla, string nuevo, string accion,string campo,string tipoData)
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
    }
}