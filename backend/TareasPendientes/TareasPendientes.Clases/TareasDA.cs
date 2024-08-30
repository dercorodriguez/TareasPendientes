using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace TareasPendientes.Clases
{
    public class TareasDA
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo[0] es requerido")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El campo[0] es requerido")]
        public string Descripcion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        [Required(ErrorMessage = "El campo[0] es requerido")]
        public bool Completada { get; set; }
        [Required(ErrorMessage = "El campo[0] es requerido")]
        public string Estado { get; set; }
    }
}
