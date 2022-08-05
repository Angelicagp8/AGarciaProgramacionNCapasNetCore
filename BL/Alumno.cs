using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

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
                    var query = context.Database.ExecuteSqlRaw($"AlumnoAdd  '{alumno.Nombre}' , '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.Email}',{alumno.Semestre.IdSemestre},'{alumno.Horario.Turno}',{alumno.Horario.Grupo.IdGrupo},'{alumno.Imagen}'");

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

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGarciaGenJulioContext context = new DL.AGarciaGenJulioContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AlumnoUpdate {alumno.IdAlumno}, '{alumno.Nombre}', '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.Email}', {alumno.Semestre.IdSemestre}, {alumno.Horario.IdHorario},'{alumno.Horario.Turno}',{alumno.Horario.Grupo.IdGrupo},'{alumno.Imagen}',{alumno.Status}");

                    if (query > 0)
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

        public static ML.Result GetAll(ML.Alumno alumnoBusquedaAbierta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.AGarciaGenJulioContext context = new DL.AGarciaGenJulioContext())
                {
                    var query = context.Alumnos.FromSqlRaw($"AlumnoGetAll '{alumnoBusquedaAbierta.Nombre}','{alumnoBusquedaAbierta.ApellidoPaterno}','{alumnoBusquedaAbierta.ApellidoMaterno}'").ToList();
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
                            alumno.Imagen = obj.Imagen;
                            alumno.Status = obj.Status.Value;
                            alumno.Semestre = new ML.Semestre();
                            alumno.Semestre.IdSemestre = obj.IdSemestre.Value;
                            alumno.Semestre.Nombre = obj.NombreSemestre;
                            alumno.Horario = new ML.Horario();
                            alumno.Horario.IdHorario = obj.IdHorario;
                            alumno.Horario.Turno = obj.Turno;
                            alumno.Horario.Grupo = new ML.Grupo();
                            alumno.Horario.Grupo.IdGrupo = obj.IdGrupo;
                            alumno.Horario.Grupo.Nombre = obj.NombreGrupo;
                            alumno.Horario.Grupo.Plantel = new ML.Plantel();
                            alumno.Horario.Grupo.Plantel.IdPlantel = obj.IdPlantel;
                            alumno.Horario.Grupo.Plantel.Nombre = obj.NombrePlantel;


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
                            alumno.Imagen = objAlumno.Imagen;
                            alumno.Status = objAlumno.Status.Value;
                            alumno.Semestre = new ML.Semestre();
                            alumno.Semestre.IdSemestre = objAlumno.IdSemestre.Value;
                            alumno.Horario = new ML.Horario();
                            alumno.Horario.IdHorario = objAlumno.IdHorario;
                            alumno.Horario.Turno = objAlumno.Turno;
                            alumno.Horario.Grupo = new ML.Grupo();
                            alumno.Horario.Grupo.IdGrupo = objAlumno.IdGrupo;
                            alumno.Horario.Grupo.Nombre = objAlumno.NombreGrupo;
                            alumno.Horario.Grupo.Plantel = new ML.Plantel();
                            alumno.Horario.Grupo.Plantel.IdPlantel = objAlumno.IdPlantel;
                            alumno.Horario.Grupo.Plantel.Nombre = objAlumno.NombrePlantel;

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
        public static ML.Result ConvertirExcelDataTable(string connectionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableAlumno = new DataTable();

                        da.Fill(tableAlumno);

                        if(tableAlumno.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach( DataRow row in tableAlumno.Rows){
                                ML.Alumno alumno = new ML.Alumno();
                                alumno.Nombre = row[0].ToString();
                                alumno.ApellidoPaterno  = row[1].ToString();
                                alumno.ApellidoMaterno = row[2].ToString();
                                alumno.Email = row[3].ToString();

                                alumno.Semestre = new ML.Semestre();
                                alumno.Semestre.IdSemestre = int.Parse(row[4].ToString());

                                alumno.Horario = new ML.Horario();
                                alumno.Horario.Turno = row[5].ToString();

                                alumno.Horario.Grupo = new ML.Grupo();
                                alumno.Horario.Grupo.IdGrupo = int.Parse(row[6].ToString());

                                result.Objects.Add(alumno);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableAlumno;

                        if (tableAlumno.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }

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

        public static ML.Result ValidarExcel(List<object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i = 1;

                foreach (ML.Alumno alumno in Objects)
                {
                    ML.ExcelErrores error = new ML.ExcelErrores();
                    error.IdRegistro = i;

                    if (alumno.Nombre == "")
                    {
                        error.Message += "Ingrese en nombre ";
                    }
                    if (alumno.ApellidoPaterno == "")
                    {
                        error.Message += "Ingrese Apellido Paterno";
                    }
                    if (alumno.ApellidoMaterno == "")
                    {
                        error.Message += "Ingrese Apellido Materno";
                    }
                    if (alumno.Email == "")
                    {
                        error.Message += "Ingrese Email";
                    }
                    if (alumno.Semestre.IdSemestre.ToString() == "")
                    {
                        error.Message += "Ingrese Semestre";
                    }
                    if (alumno.Horario.Turno == "")
                    {
                        error.Message += "Ingrese Turno";
                    }
                    if (alumno.Horario.Grupo.IdGrupo.ToString() == "")
                    {
                        error.Message += "Ingrese Grupo";
                    }

                    if(error.Message != null)
                    {
                        result.Objects.Add(error);
                    }

                    i++;
                }

            }
            catch (Exception)
            {

                throw;
            }




            return result;
        }
    }
}