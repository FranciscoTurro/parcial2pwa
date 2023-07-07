﻿using Microsoft.AspNetCore.Mvc;
using Parcial2Turró.Models;

namespace Parcial2Turró.Controllers
{
    public class MateriaController : Controller
    {
        private readonly PwadbContext _context;

        public MateriaController(PwadbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Detalle(string id)
        {
            Materium materia = getMateria(id);
            if (materia == null) return NotFound();
            return View(materia);
        }

        [HttpPost]
        public IActionResult Detalle(Materium materiaModel)
        {
            _context.Materia.Update(materiaModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Materium materiaModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Materia.Add(materiaModel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(string id)
        {
            Materium materia = getMateria(id);
            if (materia == null) return NotFound();
            _context.Materia.Remove(materia);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private Materium getMateria(string id)
        {
            Materium materia = _context.Materia.FirstOrDefault(m => m.Id == id);
            return materia;
        }

        public IActionResult Index()
        {
            List<Materium> materias = _context.Materia.ToList();
            return View(materias);
        }
    }
}
