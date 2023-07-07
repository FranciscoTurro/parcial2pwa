using Microsoft.AspNetCore.Mvc;
using Parcial2Turró.Models;
using Parcial2Turró.ViewModels;

namespace Parcial2Turró.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly PwadbContext _context;

        public AlumnoController(PwadbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Alumno> lista = _context.Alumnos.ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Detalle(string dni)
        {
            Alumno alumno = getAlumno(dni);
            if (alumno == null) return NotFound();
            AlumnoVM alumnovm = new AlumnoVM { oAlumno = alumno };
            return View(alumnovm);
        }

        [HttpPost]
        public IActionResult Detalle(AlumnoVM alumnoModel)
        {
            string fileName = UploadFile(alumnoModel);

            Alumno alumnoExistente = _context.Alumnos.FirstOrDefault(a => a.Dni == alumnoModel.oAlumno.Dni);
            if (alumnoExistente == null) return NotFound();

            alumnoExistente.Dni = alumnoModel.oAlumno.Dni;
            alumnoExistente.Apellido = alumnoModel.oAlumno.Apellido;
            alumnoExistente.Nombre = alumnoModel.oAlumno.Nombre;
            alumnoExistente.Domicilio = alumnoModel.oAlumno.Domicilio;
            alumnoExistente.FechaNacimiento = alumnoModel.oAlumno.FechaNacimiento;
            alumnoExistente.Email = alumnoModel.oAlumno.Email;

            if (fileName != null)
                alumnoExistente.Foto = fileName;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(AlumnoVM alumnoModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string fileName = UploadFile(alumnoModel);

            Alumno alumno = new Alumno()
            {
                Dni = alumnoModel.oAlumno.Dni,
                Nombre = alumnoModel.oAlumno.Nombre,
                Apellido = alumnoModel.oAlumno.Apellido,
                Domicilio = alumnoModel.oAlumno.Domicilio,
                Email = alumnoModel.oAlumno.Email,
                FechaNacimiento = alumnoModel.oAlumno.FechaNacimiento,
                Foto = fileName
            };

            _context.Alumnos.Add(alumno);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Baja(string dni)
        {
            Alumno alumno = getAlumno(dni);
            if (alumno == null) return NotFound();
            _context.Alumnos.Remove(alumno);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private Alumno getAlumno(string dni)
        {
            Alumno alumno = _context.Alumnos.FirstOrDefault(a => a.Dni == dni);
            return alumno;
        }

        private string UploadFile(AlumnoVM alumnoModel)
        {
            string fileName = null;
            if (alumnoModel.PhotoPath != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "images/alumnos");
                fileName = Guid.NewGuid().ToString() + "-" + alumnoModel.PhotoPath.FileName;
                string fileRoute = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(fileRoute, FileMode.Create))
                {
                    alumnoModel.PhotoPath.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
