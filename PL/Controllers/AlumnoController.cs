using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet] //ACTION VERB : Define la accion que realizará el método 
        public ActionResult GetAll() // ActionMethod: Métodos que  tenemos en el controlador
        {
            ML.Alumno alumno = new ML.Alumno();

            ML.Result result = BL.Alumno.GetAll();

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
        public JsonResult GrupoGetByIdPlantel(int IdPlantel)
        {
            ML.Result result = BL.Grupo.GetByIdPlantel(IdPlantel);

            return Json(result.Objects); //JsonRequestBehavior.AllowGet);
        }
    }
}
