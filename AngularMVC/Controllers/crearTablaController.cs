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
                manejoDB.conectar(Session["user"].ToString(), Session["password"].ToString());
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
                manejoDB.conectar(Session["user"].ToString(), Session["password"].ToString());
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

        public string crearTabla(string baseDatos, string nombreTabla, string[][] campos)
        {
            string query = "create table " + baseDatos + ".dbo." + nombreTabla + "(";
            string _campos = "";
            string campoPk = "";
            string Pk_Listos = "";
            

            for (int x = 0; x < campos.Length; x++)
            {

                string isPk = Convert.ToString(campos[x][0]);
                string isNotNull = Convert.ToString(campos[x][0]);
                //campoPk = campos[x][1];
                string isDatetime = campos[x][2];
                

                string tmpTamano = campos[x][2] == "int" ? "" : "  (" + campos[x][3] + ")";
                string tmp;



                if (isDatetime == "datetime" || isDatetime == "bigint" || isDatetime == "bit" || isDatetime == "date" || isDatetime == "datetime" || isDatetime == "float" || isDatetime == "geography" || isDatetime == "geometry" || isDatetime == "hierarchyid" || isDatetime == "image" || isDatetime == "money" || isDatetime == "ntext" || isDatetime == "real" || isDatetime == "smalldatetime" || isDatetime == "smallint" || isDatetime == "smallmoney" || isDatetime == "sql_variant" || isDatetime == "text" || isDatetime == "tinyint" || isDatetime == "uniqueidentifier" || isDatetime == "xml")
                {
                    tmp = campos[x][1] + " " + campos[x][2];
                }
                else {

                    tmp = campos[x][1] + " " + campos[x][2] + tmpTamano;
                
                }


                if (isPk == "True" || isNotNull == "True")
                {
                   _campos = _campos + tmp + "" + " NOT NULL" + ",";
                    campoPk += campos[x][1] + ",";
                    
                }
                else {
                    _campos = _campos + tmp + ",";
                }

               

                
                
            }

            string aux = campoPk.TrimEnd(',');
            string allPk = "primary key(" + aux + ")";


            query = query + _campos + allPk + " )";

            try
            {
                conexionBaseDatos manejoDB = new conexionBaseDatos();
                manejoDB.conectar(Session["user"].ToString(), Session["password"].ToString());
                manejoDB.EjecutarSQL(query);
                manejoDB.Desconectar();

                return "ok";
            }
            catch (SqlException e)
            {
                return e.Message.ToString();
                //throw;
            }
            
            
        }
    }
}