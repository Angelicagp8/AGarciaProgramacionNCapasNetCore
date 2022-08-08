using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ML
{
    public class Alumno
    {
        public int IdAlumno { get; set; }

        [Required] //Decoradores
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Ingrese el Apellido Paterno")]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }


        public string? ApellidoMaterno { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }


        public ML.Semestre Semestre { get; set; }
        public ML.Horario Horario { get; set; }
        public List<object> Alumnos { get; set; }
        public string Imagen { get; set; }
        public bool Status { get; set; }
    }
}
