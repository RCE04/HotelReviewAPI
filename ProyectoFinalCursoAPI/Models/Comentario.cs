using System;
using System.Collections.Generic;

namespace ProyectoFinalCursoAPI.Models;

public partial class Comentario
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string? ComentarioLugar { get; set; }

    public int? LugarId { get; set; }

    public string Puntuacion { get; set; } = null!;
}
