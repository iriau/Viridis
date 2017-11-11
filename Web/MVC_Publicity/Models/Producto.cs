using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MVC_Publicity.Models
{
    public class Producto
    {
         //string  Cve_Producto;
         //  string  Descripcion;
         //  double  Peso;
         //  double Precio ;


     //public  Producto(string Cve_Producto,string Descripcion,double Peso, double Precio) {
     //       this.Cve_Producto = Cve_Producto;
     //       this.Descripcion = Descripcion;
     //       this.Peso = Peso;
     //       this.Precio = Precio;
     //   }

        //public string GetCve_Producto() {
        //    return Cve_Producto;
        //}

        //public string GetDescripcion()
        //{
        //    return Descripcion;
        //}

        //public double GetPeso()
        //{
        //    return Peso;
        //}

        //public double GetPrecio() {
        //    return Precio;
        //}
        public string Cve_Producto { get; set; }
        public string Descripcion { get; set; }
        public double Peso { get; set; }
        public double Precio { get; set; }
        public Image Imagen { get; set; }
    }
}