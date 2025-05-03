using System.Linq;
using System.Web.Mvc;
using EncuestasMVC.Models;

namespace EncuestasMVC.Controllers
{
    [AllowAnonymous]
    public class PublicoController : Controller
    {
        private EncuestasDbContext db = new EncuestasDbContext();

        public ActionResult Responder(string id)
        {
            var encuesta = db.Encuestas.Include("Campos").FirstOrDefault(e => e.Token == id);

            if (encuesta == null)
            {
                return HttpNotFound("Encuesta no encontrada");
            }

            ViewBag.EncuestaNombre = encuesta.Nombre;
            ViewBag.Token = id;
            return View(encuesta.Campos.ToList());

           // return View(encuesta); // Luego creamos esta vista
        }

        [HttpPost]
        public ActionResult Responder(string token, FormCollection form)
        {
            var encuesta = db.Encuestas.Include("Campos").FirstOrDefault(e => e.Token == token);

            if (encuesta == null)
            {
                return HttpNotFound("Encuesta no encontrada");
            }

            foreach (var campo in encuesta.Campos)
            {
                var valor = form[campo.Id.ToString()];

                // Validar si es requerido
                if (campo.EsRequerido && string.IsNullOrWhiteSpace(valor))
                {
                    ModelState.AddModelError("", $"El campo '{campo.Titulo}' es obligatorio.");
                    ViewBag.EncuestaNombre = encuesta.Nombre;
                    ViewBag.Token = token;
                    return View(encuesta.Campos.ToList()); // volver a mostrar el formulario
                }

                db.Respuestas.Add(new RespuestaCampo
                {
                    CampoId = campo.Id,
                    Valor = valor,
                    TokenEncuesta = token
                });
            }

            db.SaveChanges();

            return View("Gracias");
        }

    }
}
