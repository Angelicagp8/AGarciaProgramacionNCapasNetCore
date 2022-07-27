using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGarciaGenJulioContext context = new DL.AGarciaGenJulioContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AlumnoAdd  '{alumno.Nombre}' , '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.Email}',{alumno.Semestre.IdSemestre}");

                    if (query > 0 )
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }


            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AGarciaGenJulioContext context = new DL.AGarciaGenJulioContext())
                {
                    var query = context.Alumnos.FromSqlRaw($"AlumnoGetAll").ToList();
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();

                            alumno.IdAlumno = obj.IdAlumno;
                            alumno.Nombre = obj.Nombre;
                            alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumno.ApellidoMaterno = obj.ApellidoMaterno;
                            alumno.Email = obj.Email;
                            alumno.Semestre = new ML.Semestre();
                            alumno.Semestre.IdSemestre = obj.IdSemestre.Value;


                            result.Objects.Add(alumno);
                              
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGarciaGenJulioContext context = new DL.AGarciaGenJulioContext())
                {
                    var objAlumno = context.Alumnos.FromSqlRaw($"AlumnoGetById {IdAlumno}").AsEnumerable().FirstOrDefault();
                    

                    if (objAlumno != null)
                    {
                        
                            ML.Alumno alumno = new ML.Alumno();

                            alumno.IdAlumno = objAlumno.IdAlumno;
                            alumno.Nombre = objAlumno.Nombre;
                            alumno.ApellidoPaterno = objAlumno.ApellidoPaterno;
                            alumno.ApellidoMaterno = objAlumno.ApellidoMaterno;
                            alumno.Email = objAlumno.Email;
                            alumno.Semestre = new ML.Semestre();
                            alumno.Semestre.IdSemestre = objAlumno.IdSemestre.Value;

                        result.Object = alumno;

                        
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}