using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Publicity.Models;

namespace MVC_Publicity.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            List<Alumno> LA = new List<Alumno>();
            Alumno uno = new Alumno();
            uno.Nombre = "IRIS";
            uno.Materia = "Prog";
            LA.Add(uno);


            Alumno dos = new Alumno();
            dos.Nombre = "DIANA";
            dos.Materia = "Conta";
            LA.Add(dos);
            
            return View(LA);
        }
    }
}