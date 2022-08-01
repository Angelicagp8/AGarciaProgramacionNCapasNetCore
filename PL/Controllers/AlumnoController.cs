using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet] //ACTION VERB : Define la accion que realizará el método 
        public ActionResult GetAll() // ActionMethod: Métodos que  tenemos en el controlador
        {
            ML.Alumno alumno = new ML.Alumno();

            alumno.Nombre = (alumno.Nombre == null) ? "" : alumno.Nombre;
            alumno.ApellidoPaterno = (alumno.ApellidoPaterno == null) ? "" : alumno.ApellidoPaterno;
            alumno.ApellidoMaterno = (alumno.ApellidoMaterno == null) ? "" : alumno.ApellidoMaterno;

            ML.Result result = BL.Alumno.GetAll(alumno);

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
            }
            else
            {
                //Error
            }
            return View(alumno); //ACTION RESULT: Tipos de retorno EJEMPLO: ActionResult -> Vista en HTML
        }

        [HttpPost] //ACTION VERB : Define la accion que realizará el método 
        public ActionResult GetAll(ML.Alumno alumno) // ActionMethod: Métodos que  tenemos en el controlador
        {
            alumno.Nombre = (alumno.Nombre == null) ? "" : alumno.Nombre;
            alumno.ApellidoPaterno = (alumno.ApellidoPaterno == null) ? "" : alumno.ApellidoPaterno;
            alumno.ApellidoMaterno = (alumno.ApellidoMaterno == null) ? "" : alumno.ApellidoMaterno;

            ML.Result result = BL.Alumno.GetAll(alumno);

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
            }
            else
            {
                //Error
            }
            return View(alumno); //ACTION RESULT: Tipos de retorno EJEMPLO: ActionResult -> Vista en HTML
        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Semestre = new ML.Semestre();
            ML.Result resultSemestre = BL.Semestre.GetAll(); //Llamar al BL para traer la información de los semestres
            ML.Result resultPlantel = BL.Plantel.GetAll();

            if (resultSemestre.Correct && resultPlantel.Correct)
            {
                if (IdAlumno == null)// ADD
                {
                    alumno.Semestre = new ML.Semestre();
                    alumno.Semestre.Semestres = resultSemestre.Objects;
                    alumno.Horario = new ML.Horario();
                    alumno.Horario.Grupo = new ML.Grupo();
                    alumno.Horario.Grupo.Plantel = new ML.Plantel();
                    alumno.Horario.Grupo.Plantel.Planteles = resultPlantel.Objects;

                    return View(alumno);
                }
                else //UPDATE
                {
                    ML.Result result = new ML.Result();
                    if (result.Correct)
                    {
                        alumno = (ML.Alumno)result.Object; //Unboxing                  
                        alumno.Semestre.Semestres = resultSemestre.Objects;
                        alumno.Horario = new ML.Horario();
                        alumno.Horario.Grupo = new ML.Grupo();
                        alumno.Horario.Grupo.Plantel = new ML.Plantel();

                        ML.Result resultGrupo = BL.Grupo.GetByIdPlantel(alumno.Horario.Grupo.IdGrupo);
                        alumno.Horario.Grupo.Grupos = resultGrupo.Objects;
                        alumno.Horario.Grupo.Plantel.Planteles = resultPlantel.Objects;
                        return View(alumno);
                    }
                    else
                    {
                        //Mostrar mensaje de Error 
                        return View("Modal");
                    }
                }

            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al realizar la consulta" + resultSemestre.ErrorMessage;
                return View("Modal");
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            IFormFile imagen = Request.Form.Files["fuImage"]; 
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen); 
                alumno.Imagen = Convert.ToBase64String(ImagenByte);  
                                                                     
            }

            if (alumno.IdAlumno == 0) //ADD
            {
                ML.Result result = BL.Alumno.Add(alumno);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro existoso";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error" + result.ErrorMessage;
                }
            }
            else  //UPDATE
            {
                ML.Result result = new ML.Result();
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualización existosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                }
            }


            return View("Modal");
        }

        public ActionResult UpdateStatus(int IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            ML.Result result = BL.Alumno.GetById(IdAlumno);

            if(result.Correct)
            {
                
                alumno = (ML.Alumno)result.Object; //unboxing 

                // Con IF
                //if(alumno.Status)
                //{
                //    alumno.Status = false;
                //}
                //else
                //{
                //    alumno.Status = true;
                //}

                // Operador Ternarnio
                alumno.Status = (alumno.Status) ? false : true;

                ML.Result resultupdate = BL.Alumno.Update(alumno);

                if (resultupdate.Correct)
                {
                    //MEnsaje de actualización
                }
                else{
                    //Error 
                }
            }
            else
            {
                //Error
            }

            return View("Modal");
        }


        public JsonResult GrupoGetByIdPlantel(int IdPlantel)
        {
            ML.Result result = BL.Grupo.GetByIdPlantel(IdPlantel);

            return Json(result.Objects); //JsonRequestBehavior.AllowGet);
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
