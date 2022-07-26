using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Alumno
    {
        public Alumno()
        {
            Horarios = new HashSet<Horario>();
        }

        public int IdAlumno { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email { get; set; }
        public int? IdSemestre { get; set; }

        public virtual Semestre? IdSemestreNavigation { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
    }
}
