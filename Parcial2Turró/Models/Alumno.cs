using System.ComponentModel.DataAnnotations;

namespace Parcial2Turró.Models;

public partial class Alumno
{
    //unico campo que realmente necesita verificacion (8 char de longitud). el campo required en la form me asegura que los campos estan por lo menos con algo
    [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "La longitud del campo debe ser de 8 caracteres")]
    public string Dni { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Domicilio { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? Foto { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
