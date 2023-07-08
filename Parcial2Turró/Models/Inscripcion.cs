using System;
using System.Collections.Generic;

namespace Parcial2Turró.Models;

public partial class Inscripcion
{
    public string Dnialumno { get; set; } = null!;

    public string Idmateria { get; set; } = null!;

    public decimal Abono { get; set; }

    public virtual Alumno DnialumnoNavigation { get; set; } = null!;

    public virtual Materium IdmateriaNavigation { get; set; } = null!;
}
