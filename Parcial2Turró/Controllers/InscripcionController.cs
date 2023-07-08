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
            Inscripcion inscripcion = new Inscripcion()
            {
                Dnialumno = inscripcionModel.Dnialumno,
                Idmateria = inscripcionModel.Idmateria,
                Abono = materia.ImporteInscripcion,
                DnialumnoNavigation = alumno,
                IdmateriaNavigation = materia
            };
            _context.Inscripcions.Add(inscripcion);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
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
