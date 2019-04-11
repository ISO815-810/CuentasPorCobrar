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
    public class TipoMovController : Controller
    {
        private PGDbContext db = new PGDbContext();

        // GET: TipoMov
        public ActionResult Index()
        {
            return View(db.TipoMovimiento.ToList());
        }

        // GET: TipoMov/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMovimiento tipoMovimiento = db.TipoMovimiento.Find(id);
            if (tipoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMovimiento);
        }

        // GET: TipoMov/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMov/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] TipoMovimiento tipoMovimiento)
        {
            if (ModelState.IsValid)
            {
                db.TipoMovimiento.Add(tipoMovimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoMovimiento);
        }

        // GET: TipoMov/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMovimiento tipoMovimiento = db.TipoMovimiento.Find(id);
            if (tipoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMovimiento);
        }

        // POST: TipoMov/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] TipoMovimiento tipoMovimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoMovimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoMovimiento);
        }

        // GET: TipoMov/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMovimiento tipoMovimiento = db.TipoMovimiento.Find(id);
            if (tipoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMovimiento);
        }

        // POST: TipoMov/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoMovimiento tipoMovimiento = db.TipoMovimiento.Find(id);
            db.TipoMovimiento.Remove(tipoMovimiento);
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
