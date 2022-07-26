using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Semestre
    {
        public Semestre()
        {
            Alumnos = new HashSet<Alumno>();
        }

        public int IdSemestre { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
    }
}
