using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace AltaCatalogo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }



        private BitmapImage ObtenImagen(string cCod)
        {
            BitmapImage img;

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            try
            {

                using (SqlCommand cmd = cn.CreateCommand())
                {

                    cn.Open();
                    cmd.CommandText = "Select iImagen from  Producto Where cCod='" + cCod + "'";

                    Byte[] arrImg = (Byte[])cmd.ExecuteScalar();
                    MemoryStream ms = new MemoryStream(arrImg);
                    BitmapImage bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = ms;
                    bitmapimage.EndInit();
                    return bitmapimage;


                }

            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

        private void imagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                txt_NomImg.Text = openFileDialog.FileName.ToString();
                var uri = new Uri(openFileDialog.FileName.ToString());
                BitmapImage TestImage=new BitmapImage(uri); ;
                boxpic.Source = TestImage;
            }


        }

        private void guardaFotoDB(string cCodProd,string  cNomProd, string cDescripcion, float dCostoPza, float dPeso, string cUnidad,string iImag)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            try
            {

                MemoryStream ms = new MemoryStream();
                FileStream fs = new FileStream(iImag, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                ms.SetLength(fs.Length);
                fs.Read(ms.GetBuffer(), 0, (int)fs.Length);
                byte[] arrImg = ms.GetBuffer();
                ms.Flush();
                ms.Close();


                using (SqlCommand cmd = cn.CreateCommand())
                {


                    cn.Open();
                    cmd.CommandText = "insert into Producto ( cCod,cNomProd,cDescripcion ,dCostoPza ,dPeso,bActive ,cUnidad ,iImagen) values (@cCod,@cNomProd,@cDescripcion,@dCostoPza,@dPeso,@bActive ,@cUnidad ,@iImagen)";
                    cmd.Parameters.Add("@cCod", SqlDbType.VarChar).Value = cCodProd;
                    cmd.Parameters.Add("@cNomProd", SqlDbType.VarChar).Value = cNomProd;
                    cmd.Parameters.Add("@cDescripcion", SqlDbType.VarChar).Value = cDescripcion;
                    cmd.Parameters.Add("@dCostoPza", SqlDbType.Float).Value = dCostoPza;
                    cmd.Parameters.Add("@dPeso", SqlDbType.Float).Value = dPeso;
                    cmd.Parameters.Add("@bActive", SqlDbType.Bit).Value = true;
                    cmd.Parameters.Add("@cUnidad", SqlDbType.VarChar).Value = cUnidad;
                    cmd.Parameters.Add("@iImagen", SqlDbType.VarBinary).Value = arrImg;

                    cmd.ExecuteNonQuery();
                    cn.Close();

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            string cCodProd = txtCodProd.Text.ToString().Trim();
            string cNomProd= txtNomProd.Text.ToString();
            string cDescripcion = txtDescProd.Text.ToString(); 
            string dCostoPza = txtCostoProd.Text.ToString(); 
            string dPeso = txtPesoProd.Text.ToString(); 
            string cUnidad = txtUnidadProd.Text.ToString();
            string iImagen = txt_NomImg.Text.ToString();

            guardaFotoDB(cCodProd, cNomProd, cDescripcion, float.Parse(dCostoPza), float.Parse(dPeso), cUnidad, iImagen);

        }

        private void muestraIm_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage TestImage;
            try
            {
                boxpic.Stretch = Stretch.Fill;
                TestImage = ObtenImagen(txtCodProd.Text.ToString().Trim());
                boxpic.Source = TestImage;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
