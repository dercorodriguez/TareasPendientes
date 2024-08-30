using System;
using System.Collections.Generic;

namespace Tareas.Server.Models;

public partial class TareasPendiente
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public bool Completada { get; set; }

    public string Estado { get; set; } = null!;
}
