using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Alumno
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();
            StreamReader archivo = new StreamReader(@"C:\Users\digis\Documents\LayoutAlumno.txt"); //@Especifica que la ruta es texto

            string line;
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            line = archivo.ReadLine();

            while ((line = archivo.ReadLine()) != null)
            {
                string[] datos = line.Split('|');

                ML.Alumno alumno = new ML.Alumno();

                alumno.Nombre = datos[0];
                alumno.ApellidoPaterno = datos[1];
                alumno.ApellidoMaterno = datos[2];
                alumno.Email = datos[3];

                alumno.Semestre = new ML.Semestre();
                alumno.Semestre.IdSemestre = Convert.ToInt32(datos[4]);

                alumno.Horario = new ML.Horario();
                alumno.Horario.Turno = datos[5];

                alumno.Horario.Grupo = new ML.Grupo();
                alumno.Horario.Grupo.IdGrupo = Convert.ToInt32(datos[6]);

                result = BL.Alumno.Add(alumno);

                if (result.Correct == false)
                {
                    resultErrores.Objects.Add(
                        "No se inserto el Nombre : " + alumno.Nombre + " " +
                        "No se inserto el ApellidoPaterno : " + alumno.ApellidoPaterno + " " +
                        "No se inserto el ApellidoMaterno : " + alumno.ApellidoMaterno + " " +
                        "No se inserto el Email : " + alumno.Email + " " +
                        "No se inserto el IdSemestre : " + alumno.Semestre.IdSemestre + " " +
                        "No se inserto el Turno : " + alumno.Horario.Turno + " " +
                        "No se inserto el IdGrupo : " + alumno.Horario.Grupo.IdGrupo + " " +
                        result.ErrorMessage
                        );
                }
            }

            archivo.Close();

            if (resultErrores.Objects != null)
            {
                TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\ErroresCargaMasiva.txt");

                foreach (string error in resultErrores.Objects)
                {
                    tw.WriteLine(error);
                    Console.WriteLine(error);
                }
                tw.Close();
            }

            
            return result;
        }
    }
}
