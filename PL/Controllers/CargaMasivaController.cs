using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult CargaMasivaAlumno()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]
        public IActionResult CargaMasivaAlumno(ML.Alumno alumno)
        {
            IFormFile archivo= Request.Form.Files["FileExcel"];

            if(HttpContext.Session.GetString("PathArchivo") == null) //Validar si no existe un archivo
            if(archivo != null)
            {
                    if(archivo.Length > 0)
                    {
                        string FileName = Path.GetFileName(archivo.FileName) ;
                        string folderPath = _configuration["PathFolder:value"];
                        string extensioArchivo = Path.GetExtension(archivo.FileName).ToLower();
                        string extensionModulo = _configuration["TipoExcel"];  //Varible Globla

                        if ( extensioArchivo == extensionModulo)
                        {
                            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(FileName)) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                            if (!System.IO.File.Exists(filePath))
                            {
                                using( FileStream stream = new FileStream(filePath, FileMode.Create))
                                {
                                    archivo.CopyTo(stream);  //Guardar una copia de mi archivo
                                }

                                string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                                ML.Result resultAlumnos = BL.Alumno.ConvertirExcelDataTable(connectionString);
                                if (resultAlumnos.Correct)
                                {
                                    //
                                }
                                else
                                {
                                    ViewBag.Message = "No se encontraron registros / Tenia Errores";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Seleccione un archivo valido (.xlsx)";
                        }
                           

                    }
                    else
                    {
                        ViewBag.Mesaage = "No tiene datos el archivo"
                    }
            }
            else
            {
                //Realizar la carga
            }
            return View();
        }
    }
}
