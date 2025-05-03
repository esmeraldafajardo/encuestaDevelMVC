using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EncuestasMVC.Models;

namespace EncuestasMVC.Controllers
{
    [Authorize]
    public class EncuestasController : Controller
    {
        private EncuestasDbContext db = new EncuestasDbContext();

        // GET: Encuestas
        public ActionResult Index()
        {
            return View(db.Encuestas.ToList());
        }

        // GET: Encuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Encuestas.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            return View(encuesta);
        }

        // GET: Encuestas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Descripcion,Token")] Encuesta encuesta)

        {
            if (ModelState.IsValid)
            {
                encuesta.Token = Guid.NewGuid().ToString();
                db.Encuestas.Add(encuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(encuesta);
        }

        // GET: Encuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Encuestas.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            return View(encuesta);
        }

        // POST: Encuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion")] Encuesta encuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(encuesta);
        }

        // GET: Encuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Encuestas.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            return View(encuesta);
        }

        // POST: Encuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var encuesta = db.Encuestas.Include("Campos").FirstOrDefault(e => e.Id == id);

            // Eliminar primero los campos relacionados
            foreach (var campo in encuesta.Campos.ToList())
            {
                db.Campos.Remove(campo);
            }

            db.Encuestas.Remove(encuesta);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Resultados(int id)
        {
            var respuestas = db.Respuestas
                .Where(r => r.Campo.EncuestaId == id)
                .Select(r => new EncuestasMVC.Models.RespuestaViewModel
                {
                    Token = r.TokenEncuesta,
                    Campo = r.Campo.Titulo,
                    Valor = r.Valor
                })
                .ToList();

            ViewBag.Encuesta = db.Encuestas.Find(id);
            return View(respuestas);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
