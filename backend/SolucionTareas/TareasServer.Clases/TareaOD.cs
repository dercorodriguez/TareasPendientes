using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareasServer.Clases
{
    public class TareaOD
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public bool Completada { get; set; }

        public string Estado { get; set; } = null!;
    }
}
