using CRUDWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CRUDWebAPI.Controllers
{
    public class EstudiantesController : Controller
    {
        private C2BEntities1 db = new C2BEntities1();
        // GET: Estudiantes
         public ActionResult Index()
        {
            IEnumerable<Alumno> l = null;
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:58908/api/");
                var respuesta = cliente.GetAsync("Alumnos/ObtenerEstudiantes");
                respuesta.Wait();

                var resultado=respuesta.Result;

                var lectura=resultado.Content.ReadAsAsync<IList<Alumno>>();
                lectura.Wait();

                l = lectura.Result;
            }
            return View(l);
        }

        public ActionResult Modificar(int id)
        {
            Alumno a = null;

            return View(a);
        }

        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(Alumno a)
        {
            using (var cliente=new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:58908/api/Alumnos/");
                var alta = cliente.PostAsJsonAsync<Alumno>("NuevoAlumno", a);
                alta.Wait();

                var result = alta.Result;
                if(result.IsSuccessStatusCode)
                    return RedirectToAction("Index");   
            }
            ModelState.AddModelError(String.Empty, "Error en el Servidor, Contacta con tu Proveedor!!!");    
            return View(a);
        }

       
        public ActionResult Borrar(int id)
        {
            using (var cliente=new HttpClient())
            {
                cliente.BaseAddress= new Uri("http://localhost:58908/api/Alumnos/");
                var r = cliente.DeleteAsync("/Borrar/" + id.ToString());
                r.Wait();

                var resultado=r.Result;
                
                return RedirectToAction("Index");
                
            }   
        }

    }
}