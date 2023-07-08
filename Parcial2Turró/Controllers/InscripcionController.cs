using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Parcial2Turró.Models;

namespace Parcial2Turró.Controllers
{
    public class InscripcionController : Controller
    {
        private readonly PwadbContext _context;

        public InscripcionController(PwadbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Crear(Inscripcion inscripcionModel)
        {
            Alumno alumno = getAlumno(inscripcionModel.Dnialumno);
            Materium materia = getMateria(inscripcionModel.Idmateria);

            //checkeo que el alumno no tenga una inscripcion a esta materia
            bool alreadySignedUp = _context.Inscripcions.Any(i => i.Dnialumno == alumno.Dni && i.Idmateria == materia.Id);
            if (alreadySignedUp)
            {
                //llamo a Index de forma un poco rustica, seria mejor tener una funcion
                ViewBag.materias = new SelectList(_context.Materia, "Id", "Id");
                ViewBag.alumnos = new SelectList(_context.Alumnos.Select(a => new { Dni = a.Dni, Apellido = a.Apellido + " - " + a.Dni }), "Dni", "Apellido");
                ViewBag.ERROR = "El alumno ya esta inscripto en la materia.";
                return View("Index");
            }

            //checkeo que la materia tenga cupo
            if (materia.Cupo <= 0)
            {
                ViewBag.materias = new SelectList(_context.Materia, "Id", "Id");
                ViewBag.alumnos = new SelectList(_context.Alumnos.Select(a => new { Dni = a.Dni, Apellido = a.Apellido + " - " + a.Dni }), "Dni", "Apellido");
                ViewBag.ERROR = "La materia no tiene cupo disponible.";
                return View("Index");
            }

            //checkeo que falten mas de 24 horas hasta el inicio de la materia
            DateTime now = DateTime.Now;
            if (materia.FechaInicio.Subtract(now).TotalHours < 24)
            {
                ViewBag.materias = new SelectList(_context.Materia, "Id", "Id");
                ViewBag.alumnos = new SelectList(_context.Alumnos.Select(a => new { Dni = a.Dni, Apellido = a.Apellido + " - " + a.Dni }), "Dni", "Apellido");
                ViewBag.ERROR = "No se puede inscribir en la materia. Faltan menos de 24 horas para que comience.";
                return View("Index");
            }

            decimal discount = 0; //ninguna beca
            if (alumno.SituacionBeca == "Media")
            {
                discount = 0.5m; // 50%
            }
            else if (alumno.SituacionBeca == "Completa")
            {
                discount = 0.9m; // 90%
            }

            decimal abono = materia.ImporteInscripcion * (1 - discount);

            Inscripcion inscripcion = new Inscripcion()
            {
                Dnialumno = inscripcionModel.Dnialumno,
                Idmateria = inscripcionModel.Idmateria,
                Abono = abono,
                DnialumnoNavigation = alumno,
                IdmateriaNavigation = materia
            };
            _context.Inscripcions.Add(inscripcion);
            materia.Cupo--;//disminuyo el cupo por 1
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            ViewBag.materias = new SelectList(_context.Materia, "Id", "Id");
            ViewBag.alumnos = new SelectList(_context.Alumnos.Select(a => new { Dni = a.Dni, Apellido = a.Apellido + " - " + a.Dni }), "Dni", "Apellido");
            return View();
        }

        private Materium getMateria(string id)
        {
            Materium materia = _context.Materia.FirstOrDefault(m => m.Id == id);
            return materia;
        }

        private Alumno getAlumno(string dni)
        {
            Alumno alumno = _context.Alumnos.FirstOrDefault(a => a.Dni == dni);
            return alumno;
        }
    }
}
