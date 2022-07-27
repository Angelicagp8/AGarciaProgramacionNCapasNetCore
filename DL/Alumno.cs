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
        public string? Imagen { get; set; }

        public virtual Semestre? IdSemestreNavigation { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }

        //Alias
        public string NombreSemestre { get; set; }
        public string NombreGrupo { get; set; }
        public string NombrePlantel { get; set; }

        //Propiedades del SP
        public int IdHorario { get; set; }
        public string Turno { get; set; }
        public int IdGrupo { get; set; }
        public int IdPlantel { get; set; }
    }
}
