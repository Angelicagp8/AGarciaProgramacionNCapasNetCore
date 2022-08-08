using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Semestre
    {
        [Display(Name = "Semestre")]
        public int IdSemestre { get; set; }
        public string? Nombre { get; set; }
        public List<object> Semestres { get; set; }
    }
}
