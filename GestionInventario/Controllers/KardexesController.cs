using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionInventario.Models;

namespace GestionInventario.Controllers
{
    public class KardexesController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities db = new DB_A41D6A_GestionInventarioEntities();

        // GET: Kardexes
        public ActionResult Index()
        {
            var kardex = db.Kardex.Include(k => k.Compras).Include(k => k.Ventas);
            return View(kardex.ToList());
        }

        // GET: Kardexes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kardex kardex = db.Kardex.Find(id);
            if (kardex == null)
            {
                return HttpNotFound();
            }
            return View(kardex);
        }

        // GET: Kardexes/Create
        public ActionResult Create()
        {
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra");
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "idVenta");
            return View();
        }

        // POST: Kardexes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idKardex,saldo,fecha,idVenta,idCompra")] Kardex kardex)
        {
            if (ModelState.IsValid)
            {
                db.Kardex.Add(kardex);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", kardex.idCompra);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "idVenta", kardex.idVenta);
            return View(kardex);
        }

        // GET: Kardexes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kardex kardex = db.Kardex.Find(id);
            if (kardex == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", kardex.idCompra);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "idVenta", kardex.idVenta);
            return View(kardex);
        }

        // POST: Kardexes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idKardex,saldo,fecha,idVenta,idCompra")] Kardex kardex)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kardex).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", kardex.idCompra);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "idVenta", kardex.idVenta);
            return View(kardex);
        }

        // GET: Kardexes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kardex kardex = db.Kardex.Find(id);
            if (kardex == null)
            {
                return HttpNotFound();
            }
            return View(kardex);
        }

        // POST: Kardexes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kardex kardex = db.Kardex.Find(id);
            db.Kardex.Remove(kardex);
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
