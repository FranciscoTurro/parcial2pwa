using System.ComponentModel.DataAnnotations;

namespace Parcial2Turró.Models;

public partial class Materium
{
    [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "La longitud del campo debe ser de 10 caracteres")]
    public string Id { get; set; } = null!;

    public int Cupo { get; set; }

    public string Sede { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public string Turno { get; set; } = null!;

    public decimal ImporteInscripcion { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
