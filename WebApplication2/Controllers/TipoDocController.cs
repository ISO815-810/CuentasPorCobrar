using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TipoDocController : Controller
    {
        private PGDbContext db = new PGDbContext();

        // GET: TipoDoc
        public ActionResult Index()
        {
            var tipoDocumento = db.TipoDocumento.Include(t => t.CuentaContable).Include(t => t.Estado);
            return View(tipoDocumento.ToList());
        }

        // GET: TipoDoc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumento tipoDocumento = db.TipoDocumento.Find(id);
            if (tipoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumento);
        }

        // GET: TipoDoc/Create
        public ActionResult Create()
        {
            ViewBag.idcuentacontable = new SelectList(db.CuentaContable, "Id", "Descripcion");
            ViewBag.idestado = new SelectList(db.Estado, "Id", "Descripcion");
            return View();
        }

        // POST: TipoDoc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,idestado,idcuentacontable")] TipoDocumento tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                db.TipoDocumento.Add(tipoDocumento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcuentacontable = new SelectList(db.CuentaContable, "Id", "Descripcion", tipoDocumento.idcuentacontable);
            ViewBag.idestado = new SelectList(db.Estado, "Id", "Descripcion", tipoDocumento.idestado);
            return View(tipoDocumento);
        }

        // GET: TipoDoc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumento tipoDocumento = db.TipoDocumento.Find(id);
            if (tipoDocumento == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcuentacontable = new SelectList(db.CuentaContable, "Id", "Descripcion", tipoDocumento.idcuentacontable);
            ViewBag.idestado = new SelectList(db.Estado, "Id", "Descripcion", tipoDocumento.idestado);
            return View(tipoDocumento);
        }

        // POST: TipoDoc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,idestado,idcuentacontable")] TipoDocumento tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDocumento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcuentacontable = new SelectList(db.CuentaContable, "Id", "Descripcion", tipoDocumento.idcuentacontable);
            ViewBag.idestado = new SelectList(db.Estado, "Id", "Descripcion", tipoDocumento.idestado);
            return View(tipoDocumento);
        }

        // GET: TipoDoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumento tipoDocumento = db.TipoDocumento.Find(id);
            if (tipoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumento);
        }

        // POST: TipoDoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDocumento tipoDocumento = db.TipoDocumento.Find(id);
            db.TipoDocumento.Remove(tipoDocumento);
            db.SaveChanges();
            return RedirectToAction("Index");
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
