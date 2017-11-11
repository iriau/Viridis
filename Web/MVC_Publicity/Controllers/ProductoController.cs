using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Publicity.Models;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;


namespace MVC_Publicity.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            List<Producto> P = new List<Producto>();
           //Obtener informacion de todos los productos

            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MVC_Publicity"].ToString()))
                {
                    SqlCommand command = new SqlCommand("[dbo].[ObtenImagen]", conn);
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@cCod", id_Plantel);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        P.Add(new Producto
                        {
                            Cve_Producto = reader.GetString(0),
                            Descripcion = reader.GetString(1),
                            Peso = reader.GetDouble(2),
                            Precio = reader.GetDouble(3),
                            //Imagen; 
                         });

                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            { }
              return View(P);
        }


        public Bitmap ObtenImagen(string cCod)
        {
            Bitmap img;

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["MVC_Publicity"].ConnectionString;

            try
            {

                using (SqlCommand cmd = cn.CreateCommand())
                {

                    cn.Open();
                    cmd.CommandText = "Select iImagen from  Producto Where cCod='" + cCod + "'";

                    Byte[] arrImg = (Byte[])cmd.ExecuteScalar();
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(arrImg);
                    Bitmap bitmapimage = new Bitmap(ms);
                    //bitmapimage.BeginInit();
                    //bitmapimage.StreamSource = ms;
                    //bitmapimage.EndInit();
                    return bitmapimage;


                }

            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
    }
}