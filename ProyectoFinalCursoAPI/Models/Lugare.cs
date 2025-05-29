using System;
using System.Collections.Generic;

namespace ProyectoFinalCursoAPI.Models;

public partial class Lugare
{
    public int Id { get; set; }

    public string? NombreLugar { get; set; }

    public string? Direccion { get; set; }

    public string? Descripcion { get; set; }

    public string? Precio { get; set; }

    public string Imagen { get; set; } = null!;
}
