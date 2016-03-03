using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace AngularMVC
{
    public class conexionBaseDatos
    {
        public string user;
        public string pass;

        public SqlConnection MiConexion;


        public void conectar(string usuario, string password)
        {
            user = usuario;
            pass = password;

            MiConexion = new SqlConnection("Data Source=GALINDO-PC;Initial Catalog=master;User ID='" + user + "';Password='" + pass + "'");
            MiConexion.Open();

        }




        public void EjecutarSQL(String Query)
        {
            SqlCommand MiComando = new SqlCommand(Query, this.MiConexion);
            MiComando.ExecuteNonQuery();

        }

        public void Desconectar()
        {
            MiConexion.Close();
        }
    }
}