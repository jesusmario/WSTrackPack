using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WSTrackPack.Entidades;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using System.Data;

namespace WSTrackPack
{
    /// <summary>
    /// Descripción breve de WSTrackPack
    /// </summary>
    [WebService(Namespace = "http://www.trackpack-ws.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSTrackPack : System.Web.Services.WebService
    {
        [WebMethod]
        public Orden ObtenerHistorialOrden(string numRastreo)
        {
            Orden o = new Orden();
            string conString = "SERVER=;" + "DATABASE=;" + "UID=;" + "PASSWORD=;";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(conString))
                {
                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.CommandText = "SELECT o.numeroRastreo, o.fecha, " +
                            "h.descripcion, h.fecha AS 'fechaH', h.estado AS 'estadoH', h.ciudad AS 'ciudadH'," +
                            "d.nombre, d.calle, d.numero, d.avenida, d.colonia, d.cp, d.ciudad, d.estado, d.referencia, p.tamanio" +
                            " FROM orden o INNER JOIN historial h ON o.id = h.idOrden INNER JOIN destinatario d ON o.idDestinatario = d.id INNER JOIN paquete p ON p.id = o.idPaquete WHERE o.numeroRastreo = '" + numRastreo + "';";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        MySqlDataReader reader = cmd.ExecuteReader();

                        
                        List<Historial> historiales = new List<Historial>();

                        while (reader.Read())
                        {
                            Historial h = new Historial();
                            h.Fecha = reader["fechaH"].ToString();
                            h.Descripcion = reader["descripcion"].ToString();
                            h.Ciudad = reader["ciudadH"].ToString();
                            h.Estado = reader["estadoH"].ToString();
                            historiales.Add(h);

                            Destinatario d = new Destinatario();
                            d.Nombre = reader["nombre"].ToString();
                            d.Calle = reader["calle"].ToString();
                            d.Numero = reader["numero"].ToString();
                            d.Avenida = reader["avenida"].ToString();
                            d.Colonia = reader["colonia"].ToString();
                            d.Cp = reader["cp"].ToString();
                            d.Ciudad = reader["ciudad"].ToString();
                            d.Estado = reader["estado"].ToString();
                            d.Referencia = reader["referencia"].ToString();


                            
                            Paquete p = new Paquete();
                            p.Tamanio = reader["tamanio"].ToString();
       

                            o.NumeroRastreo = reader["numeroRastreo"].ToString();
                            o.Fecha = reader["fecha"].ToString();
                            o.Paquete = p;
                            o.Destinatario = d;
                            o.Historiales = historiales;
                        }
                        
;                        
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            
            return o;

        }

        

        [WebMethod]
        public String ObtenerPrecioEnvio(string distancia, string pesoPaquete)
        {
            double distanciaNueva = Convert.ToDouble(distancia);
            double pesoPaqueteNuevo = Convert.ToDouble(pesoPaquete);
            double precio = 0;
            if (distanciaNueva < 500)
            {
                if (pesoPaqueteNuevo >= 20) { precio = (distanciaNueva * 40) / 100; }else
                if (pesoPaqueteNuevo >= 15) { precio = (distanciaNueva * 35) / 100; }else
                if (pesoPaqueteNuevo >= 10) { precio = (distanciaNueva * 30) / 100; }else
                if (pesoPaqueteNuevo >= 5) { precio = (distanciaNueva * 25) / 100; }else
                if (pesoPaqueteNuevo < 5) { precio = (distanciaNueva * 20) / 100; }
            }
            else
            {
                if (pesoPaqueteNuevo >= 15) { precio = (distanciaNueva * 20) / 100; }else
                if (pesoPaqueteNuevo >= 10) { precio = (distanciaNueva * 15) / 100; }else
                if (pesoPaqueteNuevo >= 5) { precio = (distanciaNueva * 11) / 100; }else
                if (pesoPaqueteNuevo < 5) { precio = (distanciaNueva * 8) / 100; }
            }
            String precioTotal = Convert.ToString(precio);
            return precioTotal;
        } 
    }
}
