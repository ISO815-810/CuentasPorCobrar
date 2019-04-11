using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication2.Models;
using WebApplication2.Models.AccountModel;

namespace WebApplication2.Controllers
{
    public class TransaccionsController : Controller
    {
        private PGDbContext db = new PGDbContext();
        private HttpClient client;
        private AccountRequest accountRequest;

        // GET: Transaccions
        public ActionResult Index()
        {
            var transaccion = db.Transaccion.Include(t => t.Cliente).Include(t => t.TipoDocumento).Include(t => t.TipoMovimiento);
            return View(transaccion.ToList());
        }

        // GET: Transaccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccion.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // GET: Transaccions/Create
        public ActionResult Create()
        {
            ViewBag.idcliente = new SelectList(db.Cliente, "Id", "Identificacion");
            ViewBag.idtipodocumento = new SelectList(db.TipoDocumento, "Id", "Descripcion");
            ViewBag.idtipomovimiento = new SelectList(db.TipoMovimiento, "Id", "Descripcion");
            return View();
        }

        // POST: Transaccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AsientoContable,NumeroDocumento,FechaRealizada,HoraRealizada,MontoTotal,idtipodocumento,idcliente,idtipomovimiento")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Transaccion.Add(transaccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcliente = new SelectList(db.Cliente, "Id", "Identificacion", transaccion.idcliente);
            ViewBag.idtipodocumento = new SelectList(db.TipoDocumento, "Id", "Descripcion", transaccion.idtipodocumento);
            ViewBag.idtipomovimiento = new SelectList(db.TipoMovimiento, "Id", "Descripcion", transaccion.idtipomovimiento);
            return View(transaccion);
        }

        // GET: Transaccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccion.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcliente = new SelectList(db.Cliente, "Id", "Identificacion", transaccion.idcliente);
            ViewBag.idtipodocumento = new SelectList(db.TipoDocumento, "Id", "Descripcion", transaccion.idtipodocumento);
            ViewBag.idtipomovimiento = new SelectList(db.TipoMovimiento, "Id", "Descripcion", transaccion.idtipomovimiento);
            return View(transaccion);
        }

        // POST: Transaccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AsientoContable,NumeroDocumento,FechaRealizada,HoraRealizada,MontoTotal,idtipodocumento,idcliente,idtipomovimiento")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcliente = new SelectList(db.Cliente, "Id", "Identificacion", transaccion.idcliente);
            ViewBag.idtipodocumento = new SelectList(db.TipoDocumento, "Id", "Descripcion", transaccion.idtipodocumento);
            ViewBag.idtipomovimiento = new SelectList(db.TipoMovimiento, "Id", "Descripcion", transaccion.idtipomovimiento);
            return View(transaccion);
        }




        // GET: Transaccions/Sincronizar/5
        public Object Sincronizar(int? id)
        {
            Transaccion transaccion = db.Transaccion.Find(id);
            var resultado = sincronizacionContabilidadCompletada(transaccion);
            return this.Json(resultado, JsonRequestBehavior.AllowGet);
        }

        private string sincronizacionContabilidadCompletada(Transaccion transaccion)
        {
            client = new HttpClient();
            accountRequest = new AccountRequest();

            accountRequest.auxiliar = "5";
            accountRequest.moneda = "DOP";
            accountRequest.descripcion = "Integracion CuentasPorCobrar - Contabilidad";

            List<AccountDetalle> detalle = new List<AccountDetalle>();
            AccountDetalle accountDetalle = new AccountDetalle();
            accountDetalle.numero_cuenta = transaccion.TipoDocumento.CuentaContable.Codigo.ToString();
            accountDetalle.tipo_transaccion = "CR";
            accountDetalle.monto = transaccion.MontoTotal;
            detalle.Add(accountDetalle);

            accountDetalle = new AccountDetalle();
            accountDetalle.numero_cuenta = transaccion.TipoDocumento.CuentaContable.Codigo.ToString();
            accountDetalle.tipo_transaccion = "DB";
            accountDetalle.monto = transaccion.MontoTotal;
            detalle.Add(accountDetalle);

            accountRequest.detalle = detalle;

            string URL = "https://prjaccounting20190327125427.azurewebsites.net/api/accounting/post";
            string requestBody = new JavaScriptSerializer().Serialize(accountRequest);
            Console.Out.WriteLine(">>>>> RESUEST BODY: ");
            Console.Out.WriteLine(requestBody);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = requestBody.Length;
            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(requestBody);
            }

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    Console.Out.WriteLine(response);
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }

            return null;
        }







        // GET: Transaccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccion.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // POST: Transaccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaccion transaccion = db.Transaccion.Find(id);
            db.Transaccion.Remove(transaccion);
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
