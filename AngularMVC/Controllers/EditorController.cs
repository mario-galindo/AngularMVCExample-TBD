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
    public class EditorController : Controller
    {
        // GET: Editor
        public ActionResult Index()
        {
            return View();
        }

        public string execQueryRows(string baseDatos, string query)
        { 
            
             ArrayList datasources = new ArrayList();
             string use = "use " + baseDatos;

            conexionBaseDatos manejoDB = new conexionBaseDatos();
           
            try
            {
                manejoDB.conectar("sa", "root");
                manejoDB.EjecutarSQL(use);
                SqlDataReader res = manejoDB.EjecutarSQL2(query);
                int columnas;
                
                while (res.Read())
                {

                    for (int i = 0; i < res.FieldCount; i++)
                    {
                        datasources.Add(res.GetValue(i));     
                    }

                  
                }


                //datasources.Add(columnas = res.FieldCount);

               
                var json = JsonConvert.SerializeObject(datasources);
                return json; 
               
            }
            catch (Exception)
            {
                return "false";

            }
        }
        
    }
    
}