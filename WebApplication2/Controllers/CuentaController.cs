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
    public class CuentaController : Controller
    {
        private PGDbContext db = new PGDbContext();

        // GET: Cuenta
        public ActionResult Index()
        {
            return View(db.CuentaContable.ToList());
        }

        // GET: Cuenta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaContable cuentaContable = db.CuentaContable.Find(id);
            if (cuentaContable == null)
            {
                return HttpNotFound();
            }
            return View(cuentaContable);
        }

        // GET: Cuenta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cuenta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Descripcion")] CuentaContable cuentaContable)
        {
            if (ModelState.IsValid)
            {
                db.CuentaContable.Add(cuentaContable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cuentaContable);
        }

        // GET: Cuenta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaContable cuentaContable = db.CuentaContable.Find(id);
            if (cuentaContable == null)
            {
                return HttpNotFound();
            }
            return View(cuentaContable);
        }

        // POST: Cuenta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Descripcion")] CuentaContable cuentaContable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentaContable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cuentaContable);
        }

        // GET: Cuenta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaContable cuentaContable = db.CuentaContable.Find(id);
            if (cuentaContable == null)
            {
                return HttpNotFound();
            }
            return View(cuentaContable);
        }

        // POST: Cuenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CuentaContable cuentaContable = db.CuentaContable.Find(id);
            db.CuentaContable.Remove(cuentaContable);
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
