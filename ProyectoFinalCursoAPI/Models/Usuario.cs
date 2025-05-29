using System;
using System.Collections.Generic;

namespace ProyectoFinalCursoAPI.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string? Contraseña { get; set; }

    public string? Rol { get; set; }

    public string? Favoritos { get; set; }
}
