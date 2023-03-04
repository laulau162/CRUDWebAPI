using CRUDWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CRUDWebAPI.Controllers
{
    public class AlumnosController : ApiController
    {
        private C2BEntities1 db = new C2BEntities1();
        [ResponseType(typeof(Alumno))]
        public Alumno ObtenerEstudiante(int id)
        {
            return db.Alumnos.FirstOrDefault(e => e.Id == id);
        }
        [HttpGet]
        public IEnumerable<Alumno> ObtenerEstudiantes()
        {
            return db.Alumnos;
        }

        
       

        [HttpPost]
        public IHttpActionResult NuevoAlumno (Alumno a)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos Incorrectos!!!");
            db.Alumnos.Add(a);
            db.SaveChanges();
            return Ok();

        }

        [HttpPut]
        public IHttpActionResult ModificarAlumno(Alumno a)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos Incorrectos!!!");
            var antiguo=db.Alumnos.FirstOrDefault(al=>al.Id==a.Id);
            if(antiguo!=null)
            {
                antiguo.Nombre = a.Nombre;  
                antiguo.Apellidos= a.Apellidos;
                //antiguo = a;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            db.Alumnos.Remove(db.Alumnos.FirstOrDefault(al => al.Id == id));
            db.SaveChanges();
            return Ok();
        }
    }
}
